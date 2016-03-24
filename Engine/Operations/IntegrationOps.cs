using Engine.Enum;
using Engine.Operations.ResourcesOps;
using Engine.Utilities;
using iAnywhere.Data.SQLAnywhere;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Engine.Operations
{
	public class IntegrationOps : IDisposable
	{
		private string _currentConnectionString;

		public SAConnection DBActiveConnection { get; set; }

		public IntegrationOps(string currentConnectionString)
		{
			_currentConnectionString = currentConnectionString;
		}

		~IntegrationOps()
		{
			Dispose();
		}

		public void Dispose()
		{
			_currentConnectionString = null;
			GC.SuppressFinalize(this);
			GC.Collect();
		}

		public void IntegrateBoxTypeInformation(ref DataSet dSet, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			try
			{
				if (dSet.Tables.Count > 0)
				{
					foreach (DataRow row in dSet.Tables[0].Rows.OfType<DataRow>())
						engineDataHelper.GetQueryResult(Queries.BoxTypeInsert, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet, row.ToQueryParameters("p_"));
				}
				dSet = (DataSet)engineDataHelper.GetQueryResult("select * from ks_boxtype", CommandType.Text, EngineDataHelperMode.ResultSet);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Origen: {0}", ex.Source));
				stringBuilder.AppendLine(string.Format("Error: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Datos: {0}", ex.Data));
				stringBuilder.AppendLine(string.Format("Trace: {0}", ex.StackTrace));

				infoMessage.AppendLine(stringBuilder.ToString());
				throw new Exception(stringBuilder.ToString());
			}
			finally
			{
				engineDataHelper.Dispose();
			}
		}

		public void IntegrateVendorInformation(ref DataSet dSet, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			try
			{
				if (dSet.Tables.Count > 0)
				{
					foreach (DataRow row in dSet.Tables[0].Rows.OfType<DataRow>())
						engineDataHelper.GetQueryResult(Queries.VendorInsert, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet, row.ToQueryParameters("p_"));
				}
				dSet = (DataSet)engineDataHelper.GetQueryResult("select * from ks_Vendor", CommandType.Text, EngineDataHelperMode.ResultSet);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Origen: {0}", ex.Source));
				stringBuilder.AppendLine(string.Format("Error: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Datos: {0}", ex.Data));
				stringBuilder.AppendLine(string.Format("Trace: {0}", ex.StackTrace));

				infoMessage.AppendLine(stringBuilder.ToString());
				throw new Exception(stringBuilder.ToString());
			}
			finally
			{
				engineDataHelper.Dispose();
			}
		}

		public void IntegrateStandingOrderInformation(DataSet dSet, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			try
			{
				infoMessage.AppendLine("a. Limpiar el area de trabajo");
				engineDataHelper.GetQueryResult(Queries.CleanSOWorkspace, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet);
				infoMessage.AppendLine("b. Integrando los Standing Orders en staging area");
				if (dSet.Tables.Count > 0)
				{
					infoMessage.AppendLine("c. Procesando los encabezados de los Standing Orders");
					engineDataHelper.GetQueryResult("KS_S_SO", dSet.Tables["standingOrders"]);
					infoMessage.AppendLine("d. Procesando los detalles de los Standing Orders");
					engineDataHelper.GetQueryResult("KS_S_SODetail", dSet.Tables["details"]);
					if (dSet.Tables["breakdowns"] != null)
					{
						infoMessage.AppendLine("e. Procesando los breakdowns de los Standing Orders");
						engineDataHelper.GetQueryResult("KS_S_SODBreakdown", dSet.Tables["breakdowns"]);
					}
				}
				infoMessage.AppendLine("g. Procesando por ETL las facturas");
				engineDataHelper.GetQueryResult(Queries.StandingOrderEtl, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Origen: {0}", ex.Source));
				stringBuilder.AppendLine(string.Format("Error: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Datos: {0}", ex.Data));
				stringBuilder.AppendLine(string.Format("Trace: {0}", ex.StackTrace));

				infoMessage.AppendLine(stringBuilder.ToString());
				throw new Exception(stringBuilder.ToString());
			}
			finally
			{
				engineDataHelper.Dispose();
			}
		}

		public DataSet GetNoPairCustomers(ref StringBuilder infoMessage, string processDate)
		{
			DataSet dSet;
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			var parameters = new SortedList<string, string>();
			infoMessage.AppendLine("Obteniendo clientes no emparejados");
			try
			{
				parameters.Add("p_fechaProceso", processDate);
				dSet = (DataSet)engineDataHelper.GetQueryResult("SP_PS_ClientesNoEmparejaddos", CommandType.StoredProcedure, EngineDataHelperMode.ResultSet, parameters);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Origen: {0}", ex.Source));
				stringBuilder.AppendLine(string.Format("Error: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Datos: {0}", ex.Data));
				stringBuilder.AppendLine(string.Format("Trace: {0}", ex.StackTrace));

				infoMessage.AppendLine(stringBuilder.ToString());
				throw new Exception(stringBuilder.ToString());
			}
			finally
			{
				engineDataHelper.Dispose();
			}

			return dSet;
		}

		public DataSet GetPackingsWithBills(string datePurchaseOrder, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			var parameters = new SortedList<string, string>();
			var dSet = new DataSet();
			try
			{
				parameters.Add("p_fechaPedido", datePurchaseOrder);
				dSet = (DataSet)engineDataHelper.GetQueryResult(Queries.PackingsWithBills, CommandType.StoredProcedure, EngineDataHelperMode.ResultSet, parameters);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Origen: {0}", ex.Source));
				stringBuilder.AppendLine(string.Format("Error: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Datos: {0}", ex.Data));
				stringBuilder.AppendLine(string.Format("Trace: {0}", ex.StackTrace));

				infoMessage.AppendLine(stringBuilder.ToString());
				throw new Exception(stringBuilder.ToString());
			}
			finally
			{
				engineDataHelper.Dispose();
			}
			return dSet;
		}

		public void UpdateStatusOnInvoices(ref DataSet dSet, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			try
			{
				if (dSet.Tables.Count > 0)
				{
					foreach (DataRow row in dSet.Tables[0].Rows.OfType<DataRow>())
						engineDataHelper.GetQueryResult(Queries.ChangeSpecificInvoiceStatus, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet, row.ToQueryParameters("p_"));
				}
				dSet = (DataSet)engineDataHelper.GetQueryResult(Queries.GetInvoiceInformation, CommandType.Text, EngineDataHelperMode.ResultSet);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Origen: {0}", ex.Source));
				stringBuilder.AppendLine(string.Format("Error: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Datos: {0}", ex.Data));
				stringBuilder.AppendLine(string.Format("Trace: {0}", ex.StackTrace));

				infoMessage.AppendLine(stringBuilder.ToString());
				throw new Exception(stringBuilder.ToString());
			}
			finally
			{
				engineDataHelper.Dispose();
			}
		}

		public void IntegrateLocationInformation(ref DataSet dSet, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			try
			{
				if (dSet.Tables.Count <= 0)
					return;
				foreach (DataRow row in dSet.Tables[0].Rows.OfType<DataRow>())
					engineDataHelper.GetQueryResult(Queries.LocationInsert, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet, row.ToQueryParameters("p_"));
				dSet = (DataSet)engineDataHelper.GetQueryResult("select * from ks_Location", CommandType.Text, EngineDataHelperMode.ResultSet);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Origen: {0}", ex.Source));
				stringBuilder.AppendLine(string.Format("Error: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Datos: {0}", ex.Data));
				stringBuilder.AppendLine(string.Format("Trace: {0}", ex.StackTrace));

				infoMessage.AppendLine(stringBuilder.ToString());
				throw new Exception(stringBuilder.ToString());
			}
			finally
			{
				engineDataHelper.Dispose();
			}
		}

		public void IntegrateProductInformation(ref DataSet dSet, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			try
			{
				if (dSet.Tables.Count > 0)
				{
					foreach (DataRow row in dSet.Tables[0].Rows.OfType<DataRow>())
						engineDataHelper.GetQueryResult(Queries.ProductInsert, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet, row.ToQueryParameters("p_"));
				}
				dSet = (DataSet)engineDataHelper.GetQueryResult("select * from ks_Product", CommandType.Text, EngineDataHelperMode.ResultSet);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Origen: {0}", ex.Source));
				stringBuilder.AppendLine(string.Format("Error: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Datos: {0}", ex.Data));
				stringBuilder.AppendLine(string.Format("Trace: {0}", ex.StackTrace));

				infoMessage.AppendLine(stringBuilder.ToString());
				throw new Exception(stringBuilder.ToString());
			}
			finally
			{
				engineDataHelper.Dispose();
			}
		}

		public void IntegratePurchaseOrderInformation(ref DataSet dSet, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			try
			{
				infoMessage.AppendLine("a. Limpiar el area de trabajo");
				engineDataHelper.GetQueryResult(Queries.CleanPOWorkspace, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet);

				infoMessage.AppendLine("b. Integrando los Purchase Orders en staging area");
				if (dSet.Tables.Count > 0)
				{
					infoMessage.AppendLine("c. Procesando los encabezados de los Purchase Orders");
					engineDataHelper.GetQueryResult("KS_S_PO", dSet.Tables["purchaseOrders"]);

					infoMessage.AppendLine("d. Procesando los detalles de los Purchase Orders");
					engineDataHelper.GetQueryResult("KS_S_PODetail", dSet.Tables["details"]);

					if (dSet.Tables["breakdowns"] != null)
					{
						infoMessage.AppendLine("e. Procesando los breakdowns de los Purchase Orders");
						engineDataHelper.GetQueryResult("KS_S_PODetailBreakdown", dSet.Tables["breakdowns"]);
					}

					if (dSet.Tables["boxes"] != null)
					{
						infoMessage.AppendLine("f. Procesando los boxes de los Purchase Orders");
						engineDataHelper.GetQueryResult("KS_S_PODetailBox", dSet.Tables["boxes"]);
					}
				}
				infoMessage.AppendLine("g. Procesando por ETL las facturas");
				engineDataHelper.GetQueryResult(Queries.POStandarizeInfo, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet);
				engineDataHelper.GetQueryResult(Queries.POEtl, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Origen: {0}", ex.Source));
				stringBuilder.AppendLine(string.Format("Error: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Datos: {0}", ex.Data));
				stringBuilder.AppendLine(string.Format("Trace: {0}", ex.StackTrace));

				infoMessage.AppendLine(stringBuilder.ToString());
				throw new Exception(stringBuilder.ToString());
			}
			finally
			{
				engineDataHelper.Dispose();
			}
		}

		public void IntegrateVendorAvailabilityInformation(string poNumber, ref DataSet dSet, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};

			try
			{
				infoMessage.AppendLine("a. Limpiar el area de trabajo");
				engineDataHelper.GetQueryResult(Queries.VAInitializeProcess, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet);

				infoMessage.AppendLine("b. Integrando los Vendor Availability en staging area");
				if ((dSet.Tables.Count > 0) || (dSet.Tables["purchaseOrders"].Rows.Count > 0))
				{
					infoMessage.AppendLine("c. Procesando los encabezados de los Vendor Availability");
					engineDataHelper.GetQueryResult("KS_S_POVA", dSet.Tables["purchaseOrders"]);

					infoMessage.AppendLine("d. Procesando los detalles de los Vendor Availability");
					engineDataHelper.GetQueryResult("KS_S_POVADetails", dSet.Tables["details"]);

					infoMessage.AppendLine("e. Procesando los contenidos de los Vendor Availability");
					engineDataHelper.GetQueryResult("KS_S_POVADDetails", dSet.Tables["vendorAvailabilityDetails"]);
				}

				infoMessage.AppendLine("g. Procesando por ETL las facturas");

				var parameters = new SortedList<string, string>();
				parameters.Clear();
				parameters.Add("p_poNumber", poNumber);

				engineDataHelper.GetQueryResult(Queries.VAEtl, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet, parameters);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Origen: {0}", ex.Source));
				stringBuilder.AppendLine(string.Format("Error: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Datos: {0}", ex.Data));
				stringBuilder.AppendLine(string.Format("Trace: {0}", ex.StackTrace));

				infoMessage.AppendLine(stringBuilder.ToString());
				throw new Exception(stringBuilder.ToString());
			}
			finally
			{
				engineDataHelper.Dispose();
			}
		}
	}
}