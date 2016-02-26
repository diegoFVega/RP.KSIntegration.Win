using Amazon;
using BusinessObjects;
using DataType.Login;
using Engine;
using Engine.Operations;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using UIX.Enums;
using UIX.Utilities;

namespace UIX
{
	public partial class FrmIntegrador : Form
	{
		private DataSet _dSetBoxType = new DataSet();
		private DataSet _dSetCustomer = new DataSet();
		private DataSet _dSetInventory = new DataSet();
		private DataSet _dSetInvoice = new DataSet();
		private DataSet _dSetPurchase = new DataSet();
		private DataSet _dSetStanding = new DataSet();
		private DataSet _dSetLocation = new DataSet();
		private DataSet _dSetProduct = new DataSet();
		private DataSet _dSetAllInvoices = new DataSet();
		private DataSet _dSetVendor = new DataSet();
		private DataSet _dSetVendorAvailability = new DataSet();
		private DataSet _dSetBoxCode = new DataSet();
		private ForReceive _loginInfo = new ForReceive();
		private StringBuilder _errorMessage = new StringBuilder();
		private string _fechaProceso;

		public FrmIntegrador()
		{
			InitializeComponent();
			Text = string.Format("Integrador KometSales-Primasoft v.{0}", Application.ProductVersion);
			TvwKsElements.ExpandAll();
		}

		private void DownloadUploadExecute(TreeNode anotherNode)
		{
			try
			{
				if (anotherNode.Checked)
				{
					//----- Manejo de invoices -----
					var downloadInvoice = new Engine.Operations.DownloadsOps.Invoice();
					var integrateInvoice =
						new Engine.Operations.IntegrationsOps.Invoice(CommonDatabaseUtilities.CurrentActiveConnectionString());
					var transactionInvoice =
						new Engine.Operations.TransactionsOps.Invoice(CommonDatabaseUtilities.CurrentActiveConnectionString());

					//----- Manejo de PurchaseOrders -----
					var downloadPO = new Engine.Operations.DownloadsOps.PurchaseOrder();
					var integratePO =
						new Engine.Operations.IntegrationsOps.PurchaseOrder(CommonDatabaseUtilities.CurrentActiveConnectionString());
					var transactionPO =
						new Engine.Operations.TransactionsOps.PurchaseOrder(CommonDatabaseUtilities.CurrentActiveConnectionString());

					//----- Manejo de customers
					var downloadCstm = new Engine.Operations.DownloadsOps.Customer();

					var downloadOps = new DownloadOps(CommonDatabaseUtilities.CurrentActiveConnectionString());
					var integrationOps = new IntegrationOps(CommonDatabaseUtilities.CurrentActiveConnectionString());
					var uploadOps = new UploadOps(CommonDatabaseUtilities.CurrentActiveConnectionString());
					var dataWarehouseOps = new DataWarehouseOps(CommonDatabaseUtilities.CurrentActiveConnectionString());
					var saveInformationOps = new SaveInformationOps(CommonDatabaseUtilities.CurrentActiveConnectionString());
					var infoMessage = new StringBuilder();

					switch (anotherNode.Name)
					{
						case "KsBoxType":
							ChangeStatusMessage("Descargando informacion de Tipos de cajas");
							_dSetBoxType = downloadOps.DownloadBoxTypeInformation(_loginInfo, ref infoMessage);
							break;

						case "KsCustomer":
							ChangeStatusMessage("Descargando informacion de Clientes");
							downloadCstm.CurrentLogin = _loginInfo;
							_dSetCustomer = downloadCstm.DownloadCustomerInformation(ref infoMessage);
							break;

						case "KsVendor":
							ChangeStatusMessage("Descargando informacion de Vendedores");
							_dSetVendor = downloadOps.DownloadVendorInformation(_loginInfo, ref infoMessage);
							break;

						case "KsProduct":
							ChangeStatusMessage("Descargando informacion de Productos");
							_dSetProduct = downloadOps.DownloadProductInformation(_loginInfo, ref infoMessage);
							break;

						case "KsLocation":
							ChangeStatusMessage("Descargando informacion de Bodegas");
							_dSetLocation = downloadOps.DownloadLocationInformation(_loginInfo, ref infoMessage);
							break;

						case "KsAllOrders":
							downloadInvoice.OrderStatus = null;
							downloadInvoice.OrderLocationId = null;
							downloadInvoice.CurrentLogin = _loginInfo;
							downloadInvoice.ShipDate = DtpProcessDate.Text;
							integrateInvoice.ShipDate = DtpProcessDate.Text;

							if (
								MessageBox.Show(
									string.Format("Desea eliminar los datos del día {0} para volverlos a generar?",
										DtpProcessDate.Text), "Eliminar datos del día", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
									.Equals(DialogResult.Yes))
							{
								ChangeStatusMessage("Limpiando el espacio de trabajo");
								transactionInvoice.CleanWorkspace(DtpProcessDate.Text, ref infoMessage);
							}

							ChangeStatusMessage("Descargando información de ventas desde KometSales");
							_dSetInvoice = downloadInvoice.DownloadInvoiceInformation(ref infoMessage);
							ChangeStatusMessage("Integrando información en Primasoft");
							integrateInvoice.IntegrateInvoiceInformation(ref _dSetInvoice, ref infoMessage);
							ChangeStatusMessage("Obteniendo datos a mostrar");
							_dSetInvoice = transactionInvoice.GetPreloadInvoices(ref infoMessage);

							break;

						case "PSProduction":
							ChangeStatusMessage("Descargando Producción de Primasoft");
							downloadOps.DownloadProductionInformation(DtpProcessDate.Text, ref infoMessage);
							ChangeStatusMessage("Integrando Producción desde Primasoft");
							dataWarehouseOps.SendProductionToDW(DtpProcessDate.Text, DtpProcessDate.Text, ref infoMessage);
							break;

						case "KsInvoice":
							if (
								MessageBox.Show(
									string.Format("Desea eliminar los datos del día {0} para volverlos a generar?",
										DtpProcessDate.Text), "Eliminar datos del día", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
									.Equals(DialogResult.Yes))
							{
								ChangeStatusMessage("Limpiando el espacio de trabajo");
								transactionInvoice.CleanWorkspace(DtpProcessDate.Text, ref infoMessage);
							}

							downloadInvoice.OrderStatus = ConfigurationManager.AppSettings["KsInvoiceStatus"];
							downloadInvoice.OrderLocationId = ConfigurationManager.AppSettings["KSLocationId"];
							downloadInvoice.CurrentLogin = _loginInfo;
							downloadInvoice.ShipDate = DtpProcessDate.Text;
							integrateInvoice.ShipDate = DtpProcessDate.Text;

							ChangeStatusMessage("Descargando información de ventas desde KometSales");
							_dSetInvoice = downloadInvoice.DownloadInvoiceInformation(ref infoMessage);
							ChangeStatusMessage("Integrando información en Primasoft");
							integrateInvoice.IntegrateInvoiceInformation(ref _dSetInvoice, ref infoMessage);
							ChangeStatusMessage("Obteniendo datos a mostrar");
							_dSetInvoice = transactionInvoice.GetPreloadInvoices(ref infoMessage);
							break;

						case "KsPurchase":
							downloadPO.CurrentLogin = _loginInfo;
							downloadPO.ShipDate = DtpProcessDate.Text;
							integratePO.ShipDate = DtpProcessDate.Text;

							// leer cola de proceso
							var dataOps = new EngineQueueHelper
							{
								AccessKey = @"AKIAJEKLKC5HVQS3NJ7A",
								SecretKey = @"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",
								RegionEndpointPlace = RegionEndpoint.USEast1,
								AmazonQueueAddress = @"https://sqs.us-east-1.amazonaws.com/905040198233/komet-sales-va-auto-packing-003"
							};

							var lista = dataOps.QueueProcess(ref _errorMessage);

							transactionPO.CleanWorkspace(ref infoMessage, shipDate: DtpProcessDate.Text);

							foreach (var item in lista)
							{
								transactionPO.CleanWorkspace(ref infoMessage, poNumber: item.PONumber);
								_dSetPurchase = downloadPO.DownloadPurchaseOrderInformation(ref infoMessage, item.PONumber);
								integratePO.IntegratePurchaseOrderInformation(ref _dSetPurchase, ref infoMessage);

								if ((_dSetPurchase.Tables.Count > 0) || (_dSetPurchase.Tables["purchaseOrders"] != null))
								{
									infoMessage.AppendLine("h. Procesando los breakdowns de los Purchase Orders");

									foreach (DataRow row in _dSetPurchase.Tables["purchaseOrders"].Rows.OfType<DataRow>())
									{
										_dSetVendorAvailability = downloadOps.DownloadVendorAvailabilityInformation(_loginInfo,
											row["number"].ToString(), ref infoMessage);

										if (_dSetVendorAvailability.Tables.Count > 0)
										{
											integrationOps.IntegrateVendorAvailabilityInformation(ref _dSetVendorAvailability, ref infoMessage);
										}
									}
								}

								//notificar po descargada
							}
							break;

						case "KsStanding":
							_dSetStanding = downloadOps.DownloadStandingOrderInformation(_loginInfo, "2000-01-01",
								DtpProcessDate.Value.ToString("yyyy-MM-dd"), ref infoMessage);
							integrationOps.IntegrateStandingOrderInformation(_dSetStanding, ref infoMessage);
							saveInformationOps.CreateSodInformation(ref infoMessage);
							break;

						case "KsInventory":
							ChangeStatusMessage("Cargando información de Inventario a KometSales");
							uploadOps.GetInventoryData(ref _dSetInventory, ref infoMessage);
							break;

						case "KsHomologue":
							ChangeStatusMessage("Cargando información para homologar los códigos de cajas");
							uploadOps.GetBoxCodesToReplace(ref _dSetBoxCode, DtpProcessDate.Text, ref infoMessage);
							break;
					}
				}
				foreach (TreeNode anotherNode1 in anotherNode.Nodes.OfType<TreeNode>())
					DownloadUploadExecute(anotherNode1);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		private void IntegrateKsToPs(TreeNode node)
		{
			if (node.Checked)
			{
				var integrateCstm = new Engine.Operations.IntegrationsOps.Customer(CommonDatabaseUtilities.CurrentActiveConnectionString());
				var integrationOps = new IntegrationOps(CommonDatabaseUtilities.CurrentActiveConnectionString());
				var uploadOps = new UploadOps(CommonDatabaseUtilities.CurrentActiveConnectionString());
				var infoMessage = new StringBuilder();
				switch (node.Name)
				{
					case "KsBoxType":
						ChangeStatusMessage("Integrando informacion de Tipos de cajas");
						integrationOps.IntegrateBoxTypeInformation(ref _dSetBoxType, ref infoMessage);
						break;

					case "KsCustomer":
						ChangeStatusMessage("Integrando informacion de Clientes");
						integrateCstm.IntegrateCustomerInformation(ref _dSetCustomer, ref infoMessage);

						foreach (DataRow row in _dSetCustomer.Tables[0].Rows.OfType<DataRow>())
						{
							var downloadCstm = new Engine.Operations.DownloadsOps.Customer();
							downloadCstm.CurrentLogin = _loginInfo;
							var _dSetShipto = downloadCstm.DownloadCustomerShipToInformation(row["id"].ToString(), ref infoMessage);

							integrateCstm.IntegrateCustomerShipToInformation(_dSetShipto, row["id"].ToString(), ref infoMessage);
						}

						break;

					case "KsVendor":
						ChangeStatusMessage("Integrando informacion de Vendedores");
						integrationOps.IntegrateVendorInformation(ref _dSetVendor, ref infoMessage);
						break;

					case "KsProduct":
						ChangeStatusMessage("Integrando informacion de Productos");
						integrationOps.IntegrateProductInformation(ref _dSetProduct, ref infoMessage);
						break;

					case "KsLocation":
						ChangeStatusMessage("Integrando informacion de Bodegas");
						integrationOps.IntegrateLocationInformation(ref _dSetLocation, ref infoMessage);
						break;

					case "KsInvoice":
						ChangeStatusMessage("Integrando informacion de Facturas seleccionadas");
						integrationOps.UpdateStatusOnInvoices(ref _dSetInvoice, ref infoMessage);
						break;

					case "KsPurchase":
						ChangeStatusMessage("Integrando informacion de Ordenes de Compra seleccionadas");
						integrationOps.IntegratePurchaseOrderInformation(ref _dSetPurchase, ref infoMessage);
						break;

					case "KsInventory":
						uploadOps.SendInventoryToKometSales(_dSetInventory, _loginInfo, ref infoMessage);
						break;
				}
			}
			foreach (TreeNode node1 in node.Nodes.OfType<TreeNode>())
				IntegrateKsToPs(node1);
		}

		private void SaveChangesOnPs(TreeNode node)
		{
			if (node.Checked)
			{
				var infoMessage = new StringBuilder();
				var saveInformationOps =
					new SaveInformationOps(CommonDatabaseUtilities.CurrentActiveConnectionString());
				switch (node.Name)
				{
					case "KsBoxType":
						ChangeStatusMessage("Guardando informacion de Tipos de cajas");
						saveInformationOps.SaveBoxTypeInformation(ref _dSetBoxType, ref infoMessage);
						break;

					case "KsCustomer":
						ChangeStatusMessage("Guardando informacion de Clientes");
						saveInformationOps.SaveCustomerInformation(ref _dSetCustomer, ref infoMessage);
						break;

					case "KsVendor":
						ChangeStatusMessage("Guardando informacion de Vendedores");
						saveInformationOps.SaveVendorInformation(ref _dSetVendor, ref infoMessage);
						break;

					case "KsProduct":
						ChangeStatusMessage("Guardando informacion de Productos");
						saveInformationOps.SaveProductInformation(ref _dSetProduct, ref infoMessage);
						break;

					case "KsLocation":
						ChangeStatusMessage("Guardando informacion de Bodegas");
						saveInformationOps.SaveLocationInformation(ref _dSetLocation, ref infoMessage);
						break;
				}
			}
			foreach (TreeNode node1 in node.Nodes.OfType<TreeNode>())
				SaveChangesOnPs(node1);
		}

		private void TmiDownload_Click(object sender, EventArgs e)
		{
			BwkProcess.RunWorkerAsync(KsActionType.Download);
		}

		private void TsbDownload_Click(object sender, EventArgs e)
		{
			BwkProcess.RunWorkerAsync(KsActionType.Download);
		}

		private void TsbJoinInformation_Click(object sender, EventArgs e)
		{
			BwkProcess.RunWorkerAsync(KsActionType.Integrate);
			DgDataExplorer.Refresh();
			DgDataExplorer.DataSource = null;
			TvwKsElements.SelectedNode = null;
		}

		private void TsbSave_Click(object sender, EventArgs e)
		{
			BwkProcess.RunWorkerAsync(KsActionType.SaveInformation);
			DgDataExplorer.Refresh();
			DgDataExplorer.DataSource = null;
			TvwKsElements.SelectedNode = null;
		}

		private void TvwKsElements_AfterCheck(object sender, TreeViewEventArgs e)
		{
			foreach (TreeNode treeNode in e.Node.Nodes.OfType<TreeNode>())
				treeNode.Checked = e.Node.Checked;
		}

		private void TvwKsElements_AfterSelect(object sender, TreeViewEventArgs e)
		{
			DgDataExplorer.CaptionText = e.Node.Text;
			DgDataExplorer.Refresh();
			DgDataExplorer.DataSource = null;
			switch (e.Node.Name)
			{
				case "KsBoxType":
					if (_dSetBoxType != null && _dSetBoxType.Tables.Count != 0)
					{
						DgDataExplorer.DataSource = _dSetBoxType.Tables[0];
					}
					break;

				case "KsCustomer":
					if (_dSetCustomer != null && _dSetCustomer.Tables.Count != 0)
					{
						DgDataExplorer.DataSource = _dSetCustomer.Tables[0];
					}
					break;

				case "KsVendor":
					if (_dSetVendor != null && _dSetVendor.Tables.Count != 0)
					{
						DgDataExplorer.DataSource = _dSetVendor.Tables[0];
					}
					break;

				case "KsProduct":
					if (_dSetProduct != null && _dSetProduct.Tables.Count != 0)
					{
						DgDataExplorer.DataSource = _dSetProduct.Tables[0];
					}
					break;

				case "KsLocation":
					if (_dSetLocation != null && _dSetLocation.Tables.Count != 0)
					{
						DgDataExplorer.DataSource = _dSetLocation.Tables[0];
					}
					break;

				case "KsAllOrders":
					if (_dSetAllInvoices != null && _dSetAllInvoices.Tables.Count != 0)
					{
						DgDataExplorer.AutoSize = _dSetAllInvoices.Tables[0].Rows.Count != 0;
						DgDataExplorer.DataSource = _dSetAllInvoices.Tables[0];
					}
					break;

				case "KsInvoice":
					if (_dSetInvoice != null && _dSetInvoice.Tables.Count != 0)
					{
						DgDataExplorer.AutoSize = _dSetInvoice.Tables[0].Rows.Count != 0;
						DgDataExplorer.DataSource = _dSetInvoice.Tables[0];
					}
					break;

				case "KsPurchase":
					if (_dSetPurchase != null && _dSetPurchase.Tables.Count != 0)
					{
						DgDataExplorer.AutoSize = _dSetPurchase.Tables[0].Rows.Count != 0;
						DgDataExplorer.DataSource = _dSetPurchase.Tables[0];
					}
					break;

				case "KsInventory":
					if (_dSetInventory != null && _dSetInventory.Tables.Count != 0)
					{
						DgDataExplorer.DataSource = _dSetInventory.Tables[0];
					}
					break;
			}
			DgDataExplorer.Refresh();
		}

		private void TsbUpload_Click(object sender, EventArgs e)
		{
			BwkProcess.RunWorkerAsync(KsActionType.Upload);
		}

		private void FrmIntegrador_Load(object sender, EventArgs e)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
			BwkProcess.RunWorkerAsync(KsActionType.BeginApp);
		}

		private void StartSequence()
		{
			ChangeStatusMessage("Conectando a KometSales API");
			_loginInfo =
				new DownloadOps(CommonDatabaseUtilities.CurrentActiveConnectionString()).DownloadLoginInformation(
					new LoginInformation()
					{
						User = ConfigurationManager.AppSettings["KometUsername"],
						Password = ConfigurationManager.AppSettings["KometPassword"],
						ApiToken = ConfigurationManager.AppSettings["KometToken"]
					}, LoginMode.UseUserAndPassword, ref _errorMessage);
		}

		private void NotifyIntegrateInfo(TreeNode node)
		{
			try
			{
				if (node.Checked)
				{
					var infoMessage = new StringBuilder();
					switch (node.Name)
					{
						case "KsInvoice":
							var downloadInvoice = new Engine.Operations.DownloadsOps.Invoice();
							var integrateInvoice =
								new Engine.Operations.IntegrationsOps.Invoice(CommonDatabaseUtilities.CurrentActiveConnectionString());
							var transactionInvoice =
								new Engine.Operations.TransactionsOps.Invoice(CommonDatabaseUtilities.CurrentActiveConnectionString());

							downloadInvoice.CurrentLogin = _loginInfo;
							downloadInvoice.ShipDate = DtpProcessDate.Text;
							integrateInvoice.ShipDate = DtpProcessDate.Text;

							_dSetInvoice = transactionInvoice.GetTransferInvoices(DtpProcessDate.Text, ref infoMessage);
							downloadInvoice.SendTransferInvoices(DtpProcessDate.Text, ConfigurationManager.AppSettings["KSExternalAppId"],
								_dSetInvoice.Tables[0].Rows[0][0].ToString(), ref infoMessage);
							break;
					}
				}
			}
			catch (Exception ex)
			{
				BwkProcess.CancelAsync();
				throw new Exception(ex.Message, ex.InnerException);
			}
			foreach (TreeNode node1 in node.Nodes.OfType<TreeNode>())
				NotifyIntegrateInfo(node1);
		}

		private void UpdateKsInformation(TreeNode node)
		{
			try
			{
				if (node.Checked)
				{
					var infoMessage = new StringBuilder();
					switch (node.Name)
					{
						case "KsInvoice":
							var downloadInvoice = new Engine.Operations.DownloadsOps.Invoice();
							var integrateInvoice =
								new Engine.Operations.IntegrationsOps.Invoice(CommonDatabaseUtilities.CurrentActiveConnectionString());
							var transactionInvoice =
								new Engine.Operations.TransactionsOps.Invoice(CommonDatabaseUtilities.CurrentActiveConnectionString());

							downloadInvoice.CurrentLogin = _loginInfo;
							downloadInvoice.ShipDate = DtpProcessDate.Text;
							integrateInvoice.ShipDate = DtpProcessDate.Text;

							_dSetInvoice = transactionInvoice.GetInvoiceInformation(DtpProcessDate.Text, ref infoMessage);
							if ((_dSetInvoice != null) || (_dSetInvoice.Tables.Count != 0))
							{
								foreach (DataRow row in _dSetInvoice.Tables[0].Rows.OfType<DataRow>())
								{
									string invoiceId = row[0].ToString();
									string reference = (string.IsNullOrEmpty(row[1].ToString())) ? string.Empty : row[1].ToString();
									downloadInvoice.SendInvoiceInformation(invoiceId, reference, ref infoMessage);
								}
							}
							break;
					}
				}
			}
			catch (Exception ex)
			{
				BwkProcess.CancelAsync();
				throw new Exception(ex.Message, ex.InnerException);
			}
			foreach (TreeNode node1 in node.Nodes.OfType<TreeNode>())
				UpdateKsInformation(node1);
		}

		private void ShowProgressBar(bool showStatus)
		{
			PnlProcess.BeginInvoke(new Action(() =>
			{
				foreach (Control control in Controls.OfType<Control>())
					control.Enabled = string.Format("{0}", control.Tag) != "unavaliable" ? !showStatus : showStatus;
				PnlProcess.Visible = showStatus;
				PbrProcess.Style = ProgressBarStyle.Marquee;
				PbrProcess.MarqueeAnimationSpeed = showStatus ? 10 : 0;
				TvwKsElements.SelectedNode = (TreeNode)null;
			}));
		}

		private void ChangeStatusMessage(string message)
		{
			LblProcess.BeginInvoke(new Action(() => LblProcess.Text = message));
		}

		private void BwkProcess_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				var ksActionType = (Enums.KsActionType)e.Argument;
				Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
				Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
				e.Result = true;
				ShowProgressBar(true);
				ChangeStatusMessage("Iniciando...");
				Thread.Sleep(1000);
				e.Result = Enums.KsEcho.Yes;
				switch (ksActionType)
				{
					case KsActionType.BeginApp:
						StartSequence();
						e.Result = Enums.KsEcho.No;
						break;

					case KsActionType.Download:
					case KsActionType.Upload:
						DownloadUploadExecute(TvwKsElements.Nodes[0]);
						break;

					case KsActionType.Integrate:
						IntegrateKsToPs(TvwKsElements.Nodes[0]);
						break;

					case KsActionType.SaveInformation:
						SaveChangesOnPs(TvwKsElements.Nodes[0]);
						break;

					case KsActionType.NotifyToKs:
						NotifyIntegrateInfo(TvwKsElements.Nodes[0]);
						break;

					case KsActionType.UpdateToKs:
						UpdateKsInformation(TvwKsElements.Nodes[0]);
						break;
						//case KsActionType.ReplaceBoxCodesNormal:
						//	ReplaceBoxCodes();
						//	break;
						//case KsActionType.ReplaceBoxCodesFutureSales:
						//	ReplaceBoxCodes(ReplaceBoxCodeMode.FutureSales);
						//	break;
				}
			}
			catch (Exception ex)
			{
				BwkProcess.CancelAsync();
				throw new Exception(ex.Message, ex.InnerException);
			}
			finally
			{
				ChangeStatusMessage("Finalizando...");
				Thread.Sleep(1000);
				ShowProgressBar(false);
			}
		}

		private void BwkProcess_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				MessageBox.Show("El proceso a sido cancelado", "Proceso de integración", MessageBoxButtons.OK,
							MessageBoxIcon.Exclamation);
			}
			else if (e.Error != null)
			{
				MessageBox.Show(e.Error.Message, "Proceso de integración", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else if (e.Result.Equals(KsEcho.Yes))
			{
				MessageBox.Show("Proceso Finalizado correctamente", "Proceso de integración", MessageBoxButtons.OK,
							MessageBoxIcon.Asterisk);
			}
		}

		//private void ReplaceBoxCodes(ReplaceBoxCodeMode replaceMode = Engine.Enum.ReplaceBoxCodeMode.Normal)
		//{
		//	var uploadOps = new UploadOps(CommonDatabaseUtilities.CurrentActiveConnectionString());
		//	var infoMessage = new StringBuilder();
		//	ChangeStatusMessage("Enviando información de Boxcodes a reemplazar");
		//	ChangeStatusMessage(uploadOps.ReplaceKsBoxCode(_loginInfo, _fechaProceso, replaceMode, ref infoMessage));
		//}

		//private void toolStripButton2_Click(object sender, EventArgs e)
		//{
		//	//new FrmConfiguration().Show();
		//}

		private void futureSalesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_fechaProceso = DtpProcessDate.Text;
			BwkProcess.RunWorkerAsync(KsActionType.ReplaceBoxCodesFutureSales);
		}

		private void normalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_fechaProceso = DtpProcessDate.Text;
			BwkProcess.RunWorkerAsync(KsActionType.ReplaceBoxCodesNormal);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var bo = new BusinessObjects.DownloadTaskforce();
			bo.ExecuteProcess("Download");

			MessageBox.Show("proceso finalizado exitosamente");
		}

		private void invoicesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var datos = new BusinessObjects.DownloadTaskforce();
			var test = datos.DownloadInvoiceInformation(DtpProcessDate.Text, "1", "25");

			MessageBox.Show(test);
		}

		private void notificarTransferenciaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			BwkProcess.RunWorkerAsync(KsActionType.NotifyToKs);
		}

		private void actualizarReferenciaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			BwkProcess.RunWorkerAsync(KsActionType.UpdateToKs);
		}

		private void invoicesToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			var test = new UploadTaskforce();
			var testa = test.NotifyTransferInvoices("2015-11-26");
			MessageBox.Show(testa);
		}

		private void GpbAcciones_Enter(object sender, EventArgs e)
		{
		}
	}
}