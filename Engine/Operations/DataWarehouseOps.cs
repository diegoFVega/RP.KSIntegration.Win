using Engine.Enum;
using Engine.Operations.ResourcesOps;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Engine.Operations
{
	public class DataWarehouseOps : IDisposable
	{
		private string _currentConnectionString;

		public DataWarehouseOps(string currentConnectionString)
		{
			_currentConnectionString = currentConnectionString;
		}

		~DataWarehouseOps()
		{
			Dispose();
		}

		public void Dispose()
		{
			_currentConnectionString = null;
			GC.SuppressFinalize(this);
			GC.Collect();
		}

		public void SendInvoicesToDW(string processDate, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			var parameters = new SortedList<string, string>();
			infoMessage.AppendLine("Integrando la facturacion en staging area");
			try
			{
				parameters.Add("fec_ini", processDate);
				parameters.Add("fec_fin", processDate);
				engineDataHelper.GetQueryResult(Queries.SendInvoicesToDw, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet, parameters);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			finally
			{
				engineDataHelper.Dispose();
			}
		}

		public void SendProductionToDW(string productionDateStart, string productionDateEnd, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var parameters = new SortedList<string, string>();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			try
			{
				parameters.Add("fec_ini", productionDateStart);
				parameters.Add("fec_fin", productionDateEnd);
				engineDataHelper.GetQueryResult(Queries.SendProductionToDw, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet, parameters);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
		}

		public void GetProductionFromPS(string productionDateStart, string productionDateEnd, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var parameters = new SortedList<string, string>();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			try
			{
				parameters.Add("fec_ini", productionDateStart);
				parameters.Add("fec_fin", productionDateEnd);
				engineDataHelper.GetQueryResult(Queries.ProductionEtl, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet, parameters);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
		}

		public void SendCustomerToDW(ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			try
			{
				engineDataHelper.GetQueryResult(Queries.SendCustomerToDw, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
		}
	}
}