using Amazon;
using BusinessObjects.Properties;
using BusinessObjects.Utilities;
using Engine;
using Engine.Operations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace BusinessObjects
{
	[ProgId("RBO.DownloadTaskforce"),
	ComVisible(true),
	ClassInterface(ClassInterfaceType.AutoDual),
	Guid("E26200EF-8BE0-4E00-9459-DFEFEFCB7495")]
	public class DownloadTaskforce : IDownloadTaskforce
	{
		public string DownloadInvoiceInformation(string invoiceDate, string invoiceStatus, string location)
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
			var loginInfo = CommonUtilities.AutoLogin();

			try
			{
				loginInfo = CommonUtilities.AutoLogin();
				downloadInvoice.OrderStatus = invoiceStatus;
				downloadInvoice.OrderLocationId = location;
				downloadInvoice.CurrentLogin = loginInfo;
				downloadInvoice.ShipDate = invoiceDate;
				integrateInvoice.ShipDate = invoiceDate;

				if (
						MessageBox.Show(
							$"Desea eliminar los datos del día {invoiceDate} para volverlos a generar?", @"Eliminar datos del día", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

		public string DownloadPurchaseOrderInformarion(string shipDate)
		{
			var infoMessage = new StringBuilder();
			var prdEnvironment = Settings.Default.PrdEnvironment;
			//var prdEnvironment = Settings.Default.UATEnvironment;
			var downloadPo = new Engine.Operations.DownloadsOps.PurchaseOrder();
			var integratePo = new Engine.Operations.IntegrationsOps.PurchaseOrder(prdEnvironment);
			var transactionPo = new Engine.Operations.TransactionsOps.PurchaseOrder(prdEnvironment);
			var downloadOps = new DownloadOps(prdEnvironment);
			var integrationOps = new IntegrationOps(prdEnvironment);
			var loginInfo = CommonUtilities.AutoLogin();

			try
			{
				string poString = string.Empty;

				downloadPo.CurrentLogin = loginInfo;
				downloadPo.ShipDate = shipDate;
				integratePo.ShipDate = shipDate;

				var dataOps = new EngineQueueHelper
				{
					AccessKey = @"AKIAJEKLKC5HVQS3NJ7A",
					SecretKey = @"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",
					RegionEndpointPlace = RegionEndpoint.USEast1,
					AmazonQueueAddress = @"https://sqs.us-east-1.amazonaws.com/905040198233/komet-sales-va-auto-packing-003"
				};

				var lista = dataOps.QueueProcess(ref infoMessage);
				transactionPo.SaveStackInformation(lista);

				var dLista = transactionPo.RetrieveStackInformation();

				if (dLista.Tables.Count > 0)
				{
					poString = dLista.Tables[0].Rows.Cast<DataRow>()
						.Aggregate(poString, (current, item) => current + (item["PONumber"].ToString() + ","));

					var poGroup = new List<string>();
					for (var i = 0; i < poString.Length; i += 80)
					{
						poGroup.Add((i + 80) < poString.Length ? poString.Substring(i, 80) : poString.Substring(i));
					}

					foreach (string poItem in poGroup)
					{
						var poStatus = "DS";
						var poMessage = "Successfull.";
						var poNumberItem = poItem.Substring(0, poItem.Length - 1);

						transactionPo.CleanWorkspace(ref infoMessage, poNumber: poNumberItem);

						DataSet dSetPurchase;
						using (dSetPurchase = downloadPo.DownloadPurchaseOrderInformation(ref infoMessage, poNumberItem))
						{
							if (dSetPurchase.Tables.Count > 0)
							{
								integratePo.IntegratePurchaseOrderInformation(ref dSetPurchase, ref infoMessage);

								foreach (DataRow rowLista in dSetPurchase.Tables[0].Rows.OfType<DataRow>())
								{
									var poNumberId = rowLista["number"].ToString();

									try
									{
										var dSetVendorAvailability = downloadOps.DownloadVendorAvailabilityInformation(loginInfo,
											poNumberId, ref infoMessage);

										if (dSetVendorAvailability.Tables.Count > 0)
										{
											integrationOps.IntegrateVendorAvailabilityInformation(poNumberId, ref dSetVendorAvailability, ref infoMessage);
										}
										else
										{
											poStatus = "NV";
											poMessage = "No vendor availability";
										}
									}
									catch (Exception ex)
									{
										poStatus = "ER";
										poMessage = ex.Message;
									}
									finally
									{
										transactionPo.SaveStackInformation(poNumberId, poStatus, poMessage);
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				downloadPo.Dispose();
				integratePo.Dispose();
				transactionPo.Dispose();
				downloadOps.Dispose();
				integrationOps.Dispose();
			}

			return infoMessage.ToString();
		}

		public string DownloadStandingOrderInformation(string startSoDate, string endSoDate)
		{
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

			var prdEnvironment = Settings.Default.PrdEnvironment;
			var infoMessage = new StringBuilder();
			var downloadOps = new DownloadOps(prdEnvironment);
			var integrationOps = new IntegrationOps(prdEnvironment);
			var saveInformationOps = new SaveInformationOps(prdEnvironment);
			var loginInfo = CommonUtilities.AutoLogin();

			try
			{
				var dSetStanding = downloadOps.DownloadStandingOrderInformation(loginInfo, startSoDate,
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

		public void DownloadVendorAvailability(string poNumber)
		{
			var infoMessage = new StringBuilder();
			var prdEnvironment = Settings.Default.PrdEnvironment;
			//var prdEnvironment = Settings.Default.UATEnvironment;
			var transactionPo = new Engine.Operations.TransactionsOps.PurchaseOrder(prdEnvironment);
			var downloadOps = new DownloadOps(prdEnvironment);
			var integrationOps = new IntegrationOps(prdEnvironment);
			var poStatus = "DS";
			var poMessage = "Success.";
			var loginInfo = CommonUtilities.AutoLogin();

			try
			{
				var dSetVendorAvailability = downloadOps.DownloadVendorAvailabilityInformation(loginInfo,
																poNumber, ref infoMessage);

				if (dSetVendorAvailability.Tables.Count > 0)
				{
					integrationOps.IntegrateVendorAvailabilityInformation(poNumber, ref dSetVendorAvailability, ref infoMessage);
				}
			}
			catch (Exception ex)
			{
				poStatus = "ER";
				poMessage = ex.Message;
			}
			finally
			{
				transactionPo.SaveStackInformation(poNumber, poStatus, poMessage);
			}
		}

		public void ExecuteProcess(string processToExecute)
		{
			var frm = new Downloads();
			frm.ShowDialog();
		}

		public string TestCreateObject(string testString = null)
		{
			StringBuilder message = new StringBuilder();
			Type currentObject = GetType();
			MethodInfo[] methodObjects = currentObject.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);

			message.AppendLine($"Parametro aceptado: {(string.IsNullOrEmpty(testString) ? "Ninguno" : testString)}");
			foreach (var specificMethod in methodObjects)
			{
				message.AppendLine($"método: {specificMethod.Name} tipo retornado: {specificMethod.ReturnType.FullName}");
			}

			return message.ToString();
		}
	}
}