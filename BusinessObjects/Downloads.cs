using Amazon;
using BusinessObjects.Properties;
using Engine;
using Engine.Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace BusinessObjects
{
	public partial class Downloads : Form
	{
		public Downloads()
		{
			InitializeComponent();
		}

		private void Downloads_Load(object sender, EventArgs e)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
			BwkProcess.RunWorkerAsync(ActionType.BeginApp);
		}

		private void ShowProgressBar(bool showStatus)
		{
			SsMessages.BeginInvoke(new Action(() =>
			{
				foreach (Control control in Controls.OfType<Control>())
					control.Enabled = string.Format("{0}", control.Tag) != "unavaliable" ? !showStatus : showStatus;
				PnlProgress.Visible = showStatus;
				PnlProgress.Style = ProgressBarStyle.Marquee;
				PnlProgress.MarqueeAnimationSpeed = showStatus ? 10 : 0;
				TvwKsElements.SelectedNode = (TreeNode)null;
			}));
		}

		private void ChangeStatusMessage(string message)
		{
			SsMessages.BeginInvoke(new Action(() => LblMessages.Text = message));
		}

		private void StartSequence()
		{
			ChangeStatusMessage("Conectando a KometSales API");
			var loginMessage = Utilities.CommonUtilities.AutoLogin();

			if (loginMessage != null)
			{
				ChangeStatusMessage("Conexión realizada con éxito");
			}
			else
			{
				ChangeStatusMessage("Error al realizar la conexión");
			}
		}

		private void BwkProcess_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				var actionType = (ActionType)e.Argument;
				Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
				Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
				e.Result = true;
				ShowProgressBar(true);
				ChangeStatusMessage("Iniciando...");
				Thread.Sleep(1000);
				e.Result = Echo.Yes;
				switch (actionType)
				{
					case ActionType.BeginApp:
						StartSequence();
						e.Result = Echo.No;
						break;

					case ActionType.Download:
						DownloadProcess(TvwKsElements.Nodes);
						break;
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
				ChangeStatusMessage("Listo");
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
			else if (e.Result.Equals(Echo.Yes))
			{
				MessageBox.Show("Proceso Finalizado correctamente", "Proceso de integración", MessageBoxButtons.OK,
							MessageBoxIcon.Asterisk);

				this.Close();
			}
		}

		private void DownloadProcess(TreeNodeCollection nodes)
		{
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

			try
			{
				foreach (TreeNode node in nodes.OfType<TreeNode>())
				{
					if (node.Checked)
					{
						switch (node.Name)
						{
							case "KsInvoice":
								ChangeStatusMessage("Iniciando proceso de descarga de ordenes facturadas.");
								BusinessObjectTaskforce.DownloadInvoiceInformation(DtpProcessDate.Text, "1", "25");
								ChangeStatusMessage("proceso Finalizado");
								break;

							case "KsPurchase":
								var infoMessage = new StringBuilder();
								var prdEnvironment = Settings.Default.PrdEnvironment;
								//var prdEnvironment = Settings.Default.UATEnvironment;
								var downloadPO = new Engine.Operations.DownloadsOps.PurchaseOrder();
								var integratePO = new Engine.Operations.IntegrationsOps.PurchaseOrder(prdEnvironment);
								var transactionPO = new Engine.Operations.TransactionsOps.PurchaseOrder(prdEnvironment);
								var downloadOps = new DownloadOps(prdEnvironment);
								var integrationOps = new IntegrationOps(prdEnvironment);
								var _dSetPurchase = new DataSet();
								var _loginInfo = Utilities.CommonUtilities.AutoLogin();

								try
								{
									downloadPO.CurrentLogin = _loginInfo;
									downloadPO.ShipDate = DtpProcessDate.Text;
									integratePO.ShipDate = DtpProcessDate.Text;
									var poItems = new StringBuilder();
									var poString = string.Empty;
									var num = 1;

									switch (ChkMasiveProcess.Checked)
									{
										case false:
											// leer cola de proceso
											var dataOps = new EngineQueueHelper
											{
												AccessKey = @"AKIAJEKLKC5HVQS3NJ7A",
												SecretKey = @"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",
												RegionEndpointPlace = RegionEndpoint.USEast1,
												AmazonQueueAddress = @"https://sqs.us-east-1.amazonaws.com/905040198233/komet-sales-va-auto-packing-003"
											};

											transactionPO.CleanWorkspace(ref infoMessage, shipDate: DtpProcessDate.Text);

											ChangeStatusMessage("Obtener datos de la cola");
											var lista = dataOps.QueueProcess(ref infoMessage);

											transactionPO.SaveStackInformation(lista);

											var _dLista = transactionPO.RetrieveStackInformation();
											ChangeStatusMessage(string.Format("Datos recuperados: {0}", lista.Count));

											ChangeStatusMessage("Limpia area de trabajo");

											if (_dLista.Tables.Count > 0)
											{
												ChangeStatusMessage("Descargar información de purchase orders");
												foreach (DataRow item in _dLista.Tables[0].Rows.OfType<DataRow>())
												{
													poItems.AppendFormat((num < _dLista.Tables[0].Rows.Count?"{0},":"{0}"), item["PONumber"]);
													num++;
												}

												poString = poItems.ToString();

												List<string> poGroup = new List<string>();
												for (int i = 0; i < poString.Length; i += 80)
												{
													if ((i + 80) < poString.Length)
														poGroup.Add(poString.Substring(i, 80));
													else
														poGroup.Add(poString.Substring(i));
												}

												foreach (string poItem in poGroup)
												{
													var poStatus = "DS";
													var poMessage = "Successfull.";

													ChangeStatusMessage("Descargando información de purchase orders");
													transactionPO.CleanWorkspace(ref infoMessage, poNumber: poItem.Substring(0, poItem.Length - 1));

													_dSetPurchase = downloadPO.DownloadPurchaseOrderInformation(ref infoMessage, poItem.Substring(0, poItem.Length - 1));

													if (_dSetPurchase.Tables.Count > 0)
													{
														ChangeStatusMessage("Integrando información de purchase orders");
														integratePO.IntegratePurchaseOrderInformation(ref _dSetPurchase, ref infoMessage);

														foreach (DataRow rowLista in _dSetPurchase.Tables[0].Rows.OfType<DataRow>())
														{
															var poNumber = rowLista["number"].ToString();

															try
															{
																var _dSetVendorAvailability = downloadOps.DownloadVendorAvailabilityInformation(_loginInfo,
																		poNumber, ref infoMessage);

																if (_dSetVendorAvailability.Tables.Count > 0)
																{
																	integrationOps.IntegrateVendorAvailabilityInformation(ref _dSetVendorAvailability, ref infoMessage);
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
																transactionPO.SaveStackInformation(poNumber, poStatus, poMessage);
															}
														}
													}
												}
											}
											break;

										case true:
											ChangeStatusMessage("Limpiar espacio de trabajo.");
											transactionPO.CleanWorkspace(ref infoMessage, shipDate: DtpProcessDate.Text);
											ChangeStatusMessage(string.Format("Descargar datos con fecha {0}", DtpProcessDate.Text));
											_dSetPurchase = downloadPO.DownloadPurchaseOrderInformation(ref infoMessage);
											integratePO.IntegratePurchaseOrderInformation(ref _dSetPurchase, ref infoMessage);

											ChangeStatusMessage(string.Format("Descargaron {0} tablas", _dSetPurchase.Tables.Count));
											if ((_dSetPurchase.Tables.Count > 0) || (_dSetPurchase.Tables["purchaseOrders"] != null))
											{
												ChangeStatusMessage("Descargando Vendor Avalaibility");

												foreach (DataRow row in _dSetPurchase.Tables["purchaseOrders"].Rows.OfType<DataRow>())
												{
													var _dSetVendorAvailability = downloadOps.DownloadVendorAvailabilityInformation(_loginInfo,
														row["number"].ToString(), ref infoMessage);

													if (_dSetVendorAvailability.Tables.Count > 0)
													{
														integrationOps.IntegrateVendorAvailabilityInformation(ref _dSetVendorAvailability, ref infoMessage);
													}
												}
											}
											break;
									}
								}
								catch (Exception ex)
								{
									MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
								}
								finally
								{
									downloadPO.Dispose();
									integratePO.Dispose();
									transactionPO.Dispose();
								}
								break;

							case "KsStanding":
								ChangeStatusMessage("Iniciando proceso de descarga de ordenes fijas activas.");
								BusinessObjectTaskforce.DownloadStandingOrderInformation(DtpProcessDate.Text, DtpProcessDate.Text);
								ChangeStatusMessage("proceso Finalizado");
								break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		private void TvwKsElements_AfterCheck(object sender, TreeViewEventArgs e)
		{
			LblUntilDate.Visible = false;
			DtpUntilProcessDate.Visible = false;
			ChkMasiveProcess.Visible = false;

			switch (e.Node.Name)
			{
				case "KsInvoice":
				case "KsPurchase":
					LblUntilDate.Visible = false;
					DtpUntilProcessDate.Visible = false;
					break;

				case "KsStanding":
					LblUntilDate.Visible = true;
					DtpUntilProcessDate.Visible = true;
					break;
			}
		}

		private void TsbDownload_Click(object sender, EventArgs e)
		{
			BwkProcess.RunWorkerAsync(ActionType.Download);
		}
	}
}