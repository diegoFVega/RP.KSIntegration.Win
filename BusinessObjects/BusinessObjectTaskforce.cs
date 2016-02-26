using Amazon;
using BusinessObjects.Properties;
using BusinessObjects.Utilities;
using DataType.Login;
using Engine;
using Engine.Operations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace BusinessObjects
{
	public static class BusinessObjectTaskforce
	{
		private static ForReceive _loginInfo;

		public static string DownloadInvoiceInformation(string invoiceDate, string invoiceStatus, string location)
		{
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

			var infoMessage = new StringBuilder();
			var prdEnvironment = Settings.Default.PrdEnvironment;
			//var prdEnvironment = Settings.Default.UATEnvironment;
			var downloadInvoice = new Engine.Operations.DownloadsOps.Invoice();
			var integrateInvoice =
				new Engine.Operations.IntegrationsOps.Invoice(prdEnvironment);
			var transactionInvoice =
				new Engine.Operations.TransactionsOps.Invoice(prdEnvironment);

			try
			{
				_loginInfo = Utilities.CommonUtilities.AutoLogin();

				downloadInvoice.OrderStatus = invoiceStatus;
				downloadInvoice.OrderLocationId = location;
				downloadInvoice.CurrentLogin = _loginInfo;
				downloadInvoice.ShipDate = invoiceDate;
				integrateInvoice.ShipDate = invoiceDate;

				if (
						MessageBox.Show(
							string.Format("Desea eliminar los datos del día {0} para volverlos a generar?",
								invoiceDate), "Eliminar datos del día", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
							.Equals(DialogResult.Yes))
				{
					infoMessage.AppendLine("Limpiando el espacio de trabajo");
					transactionInvoice.CleanWorkspace(invoiceDate, ref infoMessage);
				}

				infoMessage.AppendLine("Descargando información de ventas desde KometSales");
				var dSetInvoice = downloadInvoice.DownloadInvoiceInformation(ref infoMessage);
				infoMessage.AppendLine("Integrando información en Primasoft");
				integrateInvoice.IntegrateInvoiceInformation(ref dSetInvoice, ref infoMessage);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				downloadInvoice.Dispose();
				integrateInvoice.Dispose();
			}

			return infoMessage.ToString();
		}

		public static string DownloadStandingOrderInformation(string startSoDate, string endSoDate)
		{
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

			var prdEnvironment = Settings.Default.PrdEnvironment;
			var infoMessage = new StringBuilder();
			var downloadOps = new DownloadOps(prdEnvironment);
			var integrationOps = new IntegrationOps(prdEnvironment);
			var saveInformationOps = new SaveInformationOps(prdEnvironment);

			try
			{
				_loginInfo = CommonUtilities.AutoLogin();
				var dSetStanding = downloadOps.DownloadStandingOrderInformation(_loginInfo, startSoDate,
					endSoDate, ref infoMessage, "Active");
				integrationOps.IntegrateStandingOrderInformation(dSetStanding, ref infoMessage);
				saveInformationOps.CreateSodInformation(ref infoMessage);
				infoMessage.AppendLine("Proceso realizado con éxito");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
				downloadOps.Dispose();
				integrationOps.Dispose();
				saveInformationOps.Dispose();
			}

			return infoMessage.ToString();
		}

		public static string DownloadPurchaseOrderInformarion(string shipDate)
		{
			var infoMessage = new StringBuilder();
			var prdEnvironment = Settings.Default.PrdEnvironment;
			//var prdEnvironment = Settings.Default.UATEnvironment;
			var downloadPO = new Engine.Operations.DownloadsOps.PurchaseOrder();
			var integratePO = new Engine.Operations.IntegrationsOps.PurchaseOrder(prdEnvironment);
			var transactionPO = new Engine.Operations.TransactionsOps.PurchaseOrder(prdEnvironment);
			var downloadOps = new DownloadOps(prdEnvironment);
			var integrationOps = new IntegrationOps(prdEnvironment);
			var _dSetPurchase = new DataSet();

			try
			{
				var loginInfo = Utilities.CommonUtilities.AutoLogin();

				downloadPO.CurrentLogin = loginInfo;
				downloadPO.ShipDate = shipDate;
				integratePO.ShipDate = shipDate;
				var poString = new StringBuilder();
				var poParameters = string.Empty;
				var num = 1;

				// leer cola de proceso
				var dataOps = new EngineQueueHelper
				{
					AccessKey = @"AKIAJEKLKC5HVQS3NJ7A",
					SecretKey = @"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",
					RegionEndpointPlace = RegionEndpoint.USEast1,
					AmazonQueueAddress = @"https://sqs.us-east-1.amazonaws.com/905040198233/komet-sales-va-auto-packing-003"
				};

				infoMessage.AppendLine("Obtener datos de la cola");
				var lista = dataOps.QueueProcess(ref infoMessage);

				infoMessage.AppendLine(string.Format("Datos recuperados: {0}", lista.Count));

				infoMessage.AppendLine("Limpia area de trabajo");
				transactionPO.CleanWorkspace(ref infoMessage, shipDate: shipDate);

				infoMessage.AppendLine("Descargar información de purchase orders");
				transactionPO.SaveStackInformation(lista);

				foreach (var item in lista)
				{
					poString.AppendFormat((num < lista.Count?"{0},":"{0}"), item.PONumber);
					num++;
				}

				poParameters = poString.ToString();

				List<string> poGroup = new List<string>();
				for (int i = 0; i < poParameters.Length; i += 80)
				{
					if ((i + 80) < poString.Length)
						poGroup.Add(poParameters.Substring(i, 80));
					else
						poGroup.Add(poParameters.Substring(i));
				}

				foreach (var item in poGroup)
				{
					_dSetPurchase = downloadPO.DownloadPurchaseOrderInformation(ref infoMessage, item.Substring(0, item.Length - 1));

					if (_dSetPurchase.Tables.Count > 0)
					{
						infoMessage.AppendLine("Integrando información de purchase orders");
						integratePO.IntegratePurchaseOrderInformation(ref _dSetPurchase, ref infoMessage);
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
				downloadOps.Dispose();
				integrationOps.Dispose();
			}

			return infoMessage.ToString();
		}
	}
}