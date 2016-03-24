using Engine.Enum;
using iAnywhere.Data.SQLAnywhere;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace Engine
{
	public class EngineDataHelper : IDisposable
	{
		public string CurrentStringConnection { get; set; }

		//private SAConnection _activeConnection = new SAConnection();

		public EngineDataHelper()
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US")
			{
				NumberFormat =
				{
					NumberDecimalSeparator = ".",
					NumberGroupSeparator = ","
				}
			};
		}

		~EngineDataHelper()
		{
			Dispose();
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
			GC.Collect();
		}

		private void InitializeConnection(ref SAConnection activeConnection, bool isOpenConnection)
		{
			var connectionStringBuilder = new SAConnectionStringBuilder();

			if (string.IsNullOrEmpty(activeConnection.ConnectionString))
				activeConnection.ConnectionString = (string.IsNullOrEmpty(CurrentStringConnection) == true) ? connectionStringBuilder.ConnectionString : CurrentStringConnection;

			if (activeConnection.State == ConnectionState.Closed && isOpenConnection.Equals(true))
			{
				activeConnection.Open();
			}
			else
			{
				if (activeConnection.State != ConnectionState.Open || !isOpenConnection.Equals(false))
					return;
				SAConnection.ClearPool(activeConnection);
				activeConnection.Close();
			}
		}

		public object GetQueryResult(string commandText, CommandType commandType, EngineDataHelperMode mode, SortedList<string, string> parameters = null)
		{
			var stringBuilder = new StringBuilder();
			var activeConnection = new SAConnection();
			var obj = new object();

			using (var selectCommand = new SACommand())
			{
				var str = string.Empty;
				commandText = string.Format("{0}", commandText);
				if (parameters != null)
				{
					var num = 0;
					foreach (var keyValuePair in parameters)
					{
						str = string.Format(num == Enumerable.Count<KeyValuePair<string, string>>(parameters) - 1 ? "{0}@{1} = '{2}'" : "{0}@{1} = '{2}', ", str, keyValuePair.Key, keyValuePair.Value.Replace("'", "''"));
						++num;
					}
				}
				try
				{
					commandText = string.Format(!string.IsNullOrEmpty(str) ? "{0} ({1})" : "{0}", commandText, str);
					InitializeConnection(ref activeConnection, true);
					selectCommand.Connection = activeConnection;
					selectCommand.CommandText = commandText;
					selectCommand.CommandType = commandType;

					switch (mode)
					{
						case EngineDataHelperMode.ResultSet:
							var saDataAdapter = new SADataAdapter(selectCommand);
							var dataSet = new DataSet();
							saDataAdapter.Fill(dataSet);
							obj = dataSet;
							break;

						case EngineDataHelperMode.NonResultSet:
							obj = selectCommand.ExecuteNonQuery();
							break;
					}
				}
				catch (SAException ex)
				{
					stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
					stringBuilder.AppendLine(string.Empty);
					stringBuilder.AppendLine(string.Format("Servidor: {0}", selectCommand.Connection.DataSource));
					stringBuilder.AppendLine(string.Format("Base de datos: {0}", selectCommand.Connection.Database));
					stringBuilder.AppendLine(string.Empty);
					stringBuilder.AppendLine(string.Format("Consulta: {0}", commandText));
					stringBuilder.AppendLine(string.Empty);
					stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
					stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
					throw new Exception(stringBuilder.ToString());
				}
				finally
				{
					InitializeConnection(ref activeConnection, false);
					Dispose();
				}
				return obj;
			}
		}

		public object GetQueryResult(ref SAConnection connection, string commandText, CommandType commandType, EngineDataHelperMode mode, SortedList<string, string> parameters = null)
		{
			var stringBuilder = new StringBuilder();
			var dataObject = new object();

			using (var selectCommand = new SACommand())
			{
				var str = string.Empty;

				commandText = string.Format("{0}", commandText);
				if (parameters != null)
				{
					var num = 0;
					foreach (var keyValuePair in parameters)
					{
						str = string.Format(num == Enumerable.Count<KeyValuePair<string, string>>(parameters) - 1 ? "{0}@{1} = '{2}'" : "{0}@{1} = '{2}', ", str, keyValuePair.Key, keyValuePair.Value.Replace("'", "''"));
						++num;
					}
				}
				try
				{
					commandText = string.Format(!string.IsNullOrEmpty(str) ? "{0} {1}" : "{0}", commandText, str);
					dataObject = commandText;
					selectCommand.Connection = connection;
					selectCommand.CommandText = commandText;
					selectCommand.CommandType = commandType;

					switch (mode)
					{
						case EngineDataHelperMode.ResultSet:
							var saDataAdapter = new SADataAdapter(selectCommand);
							var dataSet = new DataSet();
							saDataAdapter.Fill(dataSet);
							dataObject = dataSet;
							break;

						case EngineDataHelperMode.NonResultSet:
							dataObject = selectCommand.ExecuteNonQuery();
							break;
					}
				}
				catch (Exception ex)
				{
					stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
					stringBuilder.AppendLine(string.Empty);
					stringBuilder.AppendLine(string.Format("Consulta: {0}", commandText));
					stringBuilder.AppendLine(string.Empty);
					stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
					stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
					throw new Exception(stringBuilder.ToString());
				}
				return dataObject;
			}
		}

		public void GetQueryResult(string tableName, DataTable originTable)
		{
			var stringBuilder = new StringBuilder();
			var destinationTable = new DataTable();
			var activeConnection = new SAConnection();

			try
			{
				InitializeConnection(ref activeConnection, true);

				using (var saBulkCopy = new SABulkCopy(activeConnection))
				{
					destinationTable = ((DataSet)GetQueryResult(ref activeConnection, string.Format("Select Top(1) * from {0} with(nolock)", tableName), CommandType.Text, EngineDataHelperMode.ResultSet)).Tables[0];
					saBulkCopy.DestinationTableName = tableName;

					foreach (DataColumn dataColumn in originTable.Columns.OfType<DataColumn>())
					{
						if (destinationTable.Columns.IndexOf(dataColumn.ColumnName) != -1)
						{
							var bulkCopyColumnMapping = new SABulkCopyColumnMapping(dataColumn.ColumnName, dataColumn.ColumnName);
							saBulkCopy.ColumnMappings.Add(bulkCopyColumnMapping);
						}
					}

					saBulkCopy.WriteToServer(originTable);
					saBulkCopy.Close();
				}
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Empty);
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			finally
			{
				InitializeConnection(ref activeConnection, false);
				Dispose();
			}
		}

		public object TestConnection(string commandText, CommandType commandType, EngineDataHelperMode mode,
			SortedList<string, string> parameters = null)
		{
			var test = new StringBuilder();
			var activeConnection = new SAConnection();

			InitializeConnection(ref activeConnection, true);
			test.AppendLine("test " + CurrentStringConnection);
			test.AppendLine("conexion " + activeConnection.State);
			test.AppendLine("comando " + commandText);
			if (parameters != null)
				test.Append(string.Format("valor {0}: {1}", parameters.Keys[0].ToString(), parameters.Values[0].ToString()));

			return test;
		}
	}
}