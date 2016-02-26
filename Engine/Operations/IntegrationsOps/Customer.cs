using Engine.Enum;
using Engine.Operations.ResourcesOps;
using Engine.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Engine.Operations.IntegrationsOps
{
	public class Customer : IDisposable
	{
		private string _currentConnectionString;

		public Customer()
		{
			_currentConnectionString = string.Empty;
		}

		public Customer(string currentConnectionString)
		{
			_currentConnectionString = currentConnectionString;
		}

		~Customer()
		{
			Dispose();
		}

		public void Dispose()
		{
			_currentConnectionString = null;
			GC.SuppressFinalize(this);
			GC.Collect();
		}

		public void IntegrateCustomerInformation(ref DataSet dSet, ref StringBuilder infoMessage)
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
					{
						engineDataHelper.GetQueryResult(Queries.CustomerInsert, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet, row.ToQueryParameters("p_"));
					}
				}
				dSet = (DataSet)engineDataHelper.GetQueryResult("select * from ks_Customer", CommandType.Text, EngineDataHelperMode.ResultSet);
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

		public void IntegrateCustomerShipToInformation(DataSet dSet, string customerId, ref StringBuilder infoMessage)
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
					engineDataHelper.GetQueryResult(Queries.ShipToInitialize, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet);
					engineDataHelper.GetQueryResult("ks_s_shipto", dSet.Tables[0]);
				}

				var parameters = new SortedList<string, string>();

				parameters.Clear();
				parameters.Add("p_customerId", customerId);

				engineDataHelper.GetQueryResult(Queries.ShipToEtl, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet, parameters);
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