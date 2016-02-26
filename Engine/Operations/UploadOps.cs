using DataType.Login;
using DataType.Message;
using Engine.Enum;
using Engine.Operations.ResourcesOps;
using Engine.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;

namespace Engine.Operations
{
	public class UploadOps : IDisposable
	{
		private string _currentConnectionString;

		public UploadOps(string currentConnectionString)
		{
			_currentConnectionString = currentConnectionString;
		}

		~UploadOps()
		{
			Dispose();
		}

		public void Dispose()
		{
			_currentConnectionString = null;
			GC.SuppressFinalize(this);
			GC.Collect();
		}

		public void GetInventoryData(ref DataSet dSet, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			try
			{
				dSet = (DataSet)engineDataHelper.GetQueryResult(Queries.InventorySelect, CommandType.StoredProcedure, EngineDataHelperMode.ResultSet);
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

		public void GetBoxCodesToReplace(ref DataSet dSet, string datePurchaseOrder, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};

			var sortedList = new SortedList<string, string>();
			try
			{
				sortedList.Add("p_fechaPedido", datePurchaseOrder);
				dSet = (DataSet)engineDataHelper.GetQueryResult(Queries.BoxCodesToChange, CommandType.StoredProcedure, EngineDataHelperMode.ResultSet);
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

		public void SendInventoryToKometSales(DataSet dSet, ForReceive currentLogin, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			try
			{
				foreach (DataRow row in dSet.Tables[0].Rows.OfType<DataRow>())
				{
					var sortedList = row.ToQueryParameters();
					var parameters = row.ToQueryParameters("p_");
					var dataSet = (DataSet)engineDataHelper.GetQueryResult(Queries.InventoryChildSelect, CommandType.StoredProcedure, EngineDataHelperMode.ResultSet, parameters);
					if (dataSet.Tables.Count != 0)
					{
						var num = 0;
						foreach (DataRow rowD in dataSet.Tables[0].Rows.OfType<DataRow>())
						{
							var mergedSortedList = rowD.ToQueryParameters(null, string.Format("[{0}]", num));
							sortedList = sortedList.MergeSortedLists(mergedSortedList);
							++num;
						}
					}
					sortedList.Add("authenticationToken", currentLogin.ApiKey);
					if (EngineOperationHelper.GetObjectFromJson<Message>(ApiAddress.KometInventoryAdd, "text/plain", HttpVerb.GET, sortedList).Status == "1")
						engineDataHelper.GetQueryResult(Queries.InventoryUpdate, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet, row.ToQueryParameters("p_"));
				}
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

		public void ShowInventoryWithErrors(ref DataSet dSet, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			try
			{
				dSet = engineDataHelper.GetQueryResult(Queries.InventoryWithErrors, CommandType.StoredProcedure, EngineDataHelperMode.ResultSet) as DataSet;
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

		public string ReplaceKsBoxCode(ForReceive currentLogin, string datePurchaseOrder, ReplaceBoxCodeMode replaceMode, ref StringBuilder infoMessage)
		{
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
			var stringBuilder = new StringBuilder();
			var parameters1 = new SortedList<string, string>();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			parameters1.Add("p_fechaPedido", datePurchaseOrder);

			try
			{
				DataSet dataSet;
				do
				{
					dataSet = (DataSet)engineDataHelper.GetQueryResult(Queries.BoxCodesToChange, CommandType.StoredProcedure, EngineDataHelperMode.ResultSet, parameters1);
					if (dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
					{
						var parameters2 = new JObject();
						var jarray = new JArray();
						foreach (DataRow row in dataSet.Tables[0].Rows.OfType<DataRow>())
						{
							var jobject = new JObject
							{
								{
									"currentBoxCode",
									(string) row["currentBoxCode"]
								},
								{
									"newBoxCode",
									(string) row["newBoxCode"]
								}
							};
							jarray.Add(jobject);
						}
						parameters2.Add("authenticationToken", currentLogin.ApiKey);
						parameters2.Add("boxes", jarray);

						var cajas = EngineOperationHelper.GetObjectFromJson<DataType.BoxChange.ForReceive>(ApiAddress.KometBoxCodeChange,
							"application/json", "POST", parameters2);

						foreach (var boxCodeType in cajas.BoxCodeTypes)
						{
							var parameters3 = new SortedList<string, string>
							{
								{
									"p_messageText",
									boxCodeType.MessageText
								},
								{
									"p_newBoxCode",
									boxCodeType.NewBoxCode
								}
							};
							engineDataHelper.GetQueryResult(Queries.BoxCodesToChangeUpdate, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet, parameters3);
						}
					}
				}
				while (dataSet.Tables[0].Rows.Count != 0);
				infoMessage.AppendLine("proceso finalizado");
			}
			catch (Exception ex)
			{
				infoMessage.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				infoMessage.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				infoMessage.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			return infoMessage.ToString();
		}
	}
}