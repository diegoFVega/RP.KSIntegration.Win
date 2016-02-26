using Engine.Enum;
using Engine.Operations.ResourcesOps;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Engine.Operations.TransactionsOps
{
	public class Invoice : IDisposable
	{
		private string _currentConnectionString;

		public Invoice(string currentConnectionString)
		{
			_currentConnectionString = currentConnectionString;
		}

		~Invoice()
		{
			Dispose();
		}

		public void Dispose()
		{
			_currentConnectionString = null;
			GC.SuppressFinalize(this);
			GC.Collect();
		}

		public void CleanWorkspace(string shipDate, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};

			try
			{
				var parameters = new SortedList<string, string>();
				parameters.Add("p_shipDate", shipDate);

				engineDataHelper.GetQueryResult(Queries.CleanInvoiceWorkspace, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet, parameters);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Empty);
				stringBuilder.AppendLine(string.Format("Origen: {0}", ex.Source));
				stringBuilder.AppendLine(string.Format("Error: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Datos: {0}", ex.Data));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				stringBuilder.AppendLine(string.Empty);
				stringBuilder.AppendLine(string.Format("Trace: {0}", ex.StackTrace));

				infoMessage.AppendLine(stringBuilder.ToString());

				throw new Exception(stringBuilder.ToString());
			}
			finally
			{
				engineDataHelper.Dispose();
			}
		}

		public DataSet GetPreloadInvoices(ref StringBuilder infoMessage, int isIntegrate = 0)
		{
			var dSet = new DataSet();
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			var parameters = new SortedList<string, string>();

			infoMessage.AppendLine("Obteniendo las facturas precargadas");
			try
			{
				parameters.Add("p_IsIntegrate", isIntegrate.ToString());
				dSet = (DataSet)engineDataHelper.GetQueryResult(Queries.PreloadInvoices, CommandType.StoredProcedure, EngineDataHelperMode.ResultSet, parameters);
				parameters.Clear();
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Empty);
				stringBuilder.AppendLine(string.Format("Origen: {0}", ex.Source));
				stringBuilder.AppendLine(string.Format("Error: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Datos: {0}", ex.Data));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				stringBuilder.AppendLine(string.Empty);
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

		public DataSet GetTransferInvoices(string currentDate, ref StringBuilder infoMessage)
		{
			var dSet = new DataSet();
			var parameters = new SortedList<string, string>();
			var helper = new EngineDataHelper
			{
				CurrentStringConnection = this._currentConnectionString
			};

			try
			{
				parameters.Add("p_shipDate", currentDate);
				dSet = (DataSet)helper.GetQueryResult(Queries.GetTransferInvoice, CommandType.StoredProcedure, EngineDataHelperMode.ResultSet, parameters);
			}
			catch (Exception ex)
			{
				infoMessage.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				infoMessage.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				infoMessage.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(infoMessage.ToString());
			}
			finally
			{
				helper.Dispose();
			}

			return dSet;
		}

		public DataSet GetInvoiceInformation(string currentDate, ref StringBuilder infoMessage)
		{
			var dSet = new DataSet();
			var parameters = new SortedList<string, string>();
			var helper = new EngineDataHelper
			{
				CurrentStringConnection = this._currentConnectionString
			};

			try
			{
				parameters.Add("p_shipDate", currentDate);
				dSet = (DataSet)helper.GetQueryResult(Queries.GetInvoiceInformation, CommandType.StoredProcedure, EngineDataHelperMode.ResultSet, parameters);
			}
			catch (Exception ex)
			{
				infoMessage.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				infoMessage.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				infoMessage.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(infoMessage.ToString());
			}
			finally
			{
				helper.Dispose();
			}

			return dSet;
		}
	}
}