using Engine.Enum;
using Engine.Operations.ResourcesOps;
using iAnywhere.Data.SQLAnywhere;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Engine.Operations.TransactionsOps
{
	public class PurchaseOrder : IDisposable
	{
		private string _currentConnectionString;
		public SAConnection DBActiveConnection { get; set; }

		public PurchaseOrder(string currentConnectionString)
		{
			_currentConnectionString = currentConnectionString;
		}

		~PurchaseOrder()
		{
			Dispose();
		}

		public void Dispose()
		{
			_currentConnectionString = null;
			GC.SuppressFinalize(this);
			GC.Collect();
		}

		public void CleanWorkspace(ref StringBuilder infoMessage, string shipDate = null, string poNumber = null)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};

			try
			{
				var parameters = new SortedList<string, string>();

				parameters.Clear();

				if (string.IsNullOrEmpty(shipDate) != true)
				{
					infoMessage.AppendLine(string.Format("Eliminando registros del día {0}", shipDate));
					parameters.Add("p_shipDate", shipDate);
					engineDataHelper.GetQueryResult(Queries.CleanPOWorkspace, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet, parameters);
				}
				if (string.IsNullOrEmpty(poNumber) != true)
				{
					infoMessage.AppendLine(string.Format("Eliminando registros de la PO: {0}", poNumber));
					parameters.Add("p_number", poNumber);
					engineDataHelper.GetQueryResult(Queries.CleanPOWorkspaceA, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet, parameters);
				}
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Origen: {0}", ex.Source));
				stringBuilder.AppendLine(string.Format("Error: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Datos: {0}", ex.Data));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				stringBuilder.AppendLine("--------------------------------------------------------------");
				stringBuilder.AppendLine(string.Format("Trace: {0}", ex.StackTrace));

				infoMessage.AppendLine(stringBuilder.ToString());

				throw new Exception(stringBuilder.ToString());
			}
			finally
			{
				engineDataHelper.Dispose();
			}
		}

		public void SaveStackInformation(List<DataType.Purchase.ProcessablePO> stack)
		{
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			var parameters = new SortedList<string, string>();

			engineDataHelper.GetQueryResult("delete from ks_pods where poestado <> 'TR'", CommandType.Text, EngineDataHelperMode.NonResultSet);

			foreach (DataType.Purchase.ProcessablePO item in stack)
			{
				parameters.Clear();
				parameters.Add("p_PoNumber", item.PONumber);
				parameters.Add("p_PoStatus", "GE");
				parameters.Add("p_PoMessage", "Downolad from Queue, ready for download PO and VA");

				engineDataHelper.GetQueryResult("sp_KS_POStack", CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet, parameters);
			}
		}

		public void SaveStackInformation(string poNumber, string poStatus, string poMessage)
		{
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			var parameters = new SortedList<string, string>();

			parameters.Clear();
			parameters.Add("p_PoNumber", poNumber);
			parameters.Add("p_PoStatus", poStatus);
			parameters.Add("p_PoMessage", poMessage);

			engineDataHelper.GetQueryResult("sp_KS_POStack", CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet, parameters);
		}

		public DataSet RetrieveStackInformation()
		{
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};

			return (DataSet)engineDataHelper.GetQueryResult("select * from v_KS_POStack;", CommandType.Text, EngineDataHelperMode.ResultSet);
		}
	}
}