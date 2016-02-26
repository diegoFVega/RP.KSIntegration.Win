using BusinessObjects.Properties;
using DataType.Login;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace BusinessObjects
{
	[ProgId("RBO.UploadTaskforce"),
		ComVisible(true),
		ClassInterface(ClassInterfaceType.AutoDual),
		Guid("51C298F1-EA63-42ED-9A6B-61AD6A9F5C01")]
	public class UploadTaskforce : IUploadTaskforce
	{
		private ForReceive _loginInfo = new ForReceive();

		public UploadTaskforce()
		{
		}

		public string TestCreateObject(string testString = null)
		{
			StringBuilder message = new StringBuilder();
			Type currentObject = GetType();
			MethodInfo[] methodObjects = currentObject.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);

			message.AppendLine(string.Format("Parametro aceptado: {0}", string.IsNullOrEmpty(testString) ? "Ninguno" : testString));
			foreach (var specificMethod in methodObjects)
			{
				message.AppendLine(string.Format("método: {0} tipo retornado: {1}", specificMethod.Name, specificMethod.ReturnType.FullName));
			}

			return message.ToString();
		}

		public string NotifyTransferInvoices(string invoiceDate)
		{
			var _dSetInvoice = new DataSet();
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

				downloadInvoice.CurrentLogin = _loginInfo;
				downloadInvoice.ShipDate = invoiceDate;
				integrateInvoice.ShipDate = invoiceDate;

				infoMessage.AppendLine("a. Obtiene las Invoices transferidas");
				_dSetInvoice = transactionInvoice.GetTransferInvoices(invoiceDate, ref infoMessage);
				infoMessage.AppendLine("b. Notifica a Kometsales sobre las facturas transferidas");
				downloadInvoice.SendTransferInvoices(invoiceDate, Settings.Default.KometExternalAppId,
					_dSetInvoice.Tables[0].Rows[0][0].ToString(), ref infoMessage);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				downloadInvoice.Dispose();
				integrateInvoice.Dispose();
				transactionInvoice.Dispose();
			}

			return infoMessage.ToString();
		}

		public string UpdateInvoiceInformation(string invoiceDate)
		{
			var _dSetInvoice = new DataSet();
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

				downloadInvoice.CurrentLogin = _loginInfo;
				downloadInvoice.ShipDate = invoiceDate;
				integrateInvoice.ShipDate = invoiceDate;

				infoMessage.AppendLine("a. Obtiene las Invoices transferidas");
				_dSetInvoice = transactionInvoice.GetInvoiceInformation(invoiceDate, ref infoMessage);
				infoMessage.AppendLine("b. Actualizando información en Kometsales");
				if ((_dSetInvoice != null) || (_dSetInvoice.Tables.Count != 0))
				{
					foreach (DataRow row in _dSetInvoice.Tables[0].Rows.OfType<DataRow>())
					{
						string invoiceId = row[0].ToString();
						string reference = (string.IsNullOrEmpty(row[1].ToString())) ? string.Empty : row[1].ToString();
						downloadInvoice.SendInvoiceInformation(invoiceId, reference, ref infoMessage);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				downloadInvoice.Dispose();
				integrateInvoice.Dispose();
				transactionInvoice.Dispose();
			}

			return infoMessage.ToString();
		}

		public void SendPurchaseOrderReference(int purchaseOrderItemId, string reference)
		{
			var infoMessage = new StringBuilder();
			var downloadPO = new Engine.Operations.DownloadsOps.PurchaseOrder();

			try
			{
				_loginInfo = Utilities.CommonUtilities.AutoLogin();

				downloadPO.CurrentLogin = _loginInfo;

				downloadPO.SendPurchaseOrderInformation(purchaseOrderItemId, reference, ref infoMessage);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				downloadPO.Dispose();
			}
		}
	}
}