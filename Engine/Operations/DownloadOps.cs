using DataType.Login;
using Engine.Enum;
using Engine.Operations.ResourcesOps;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ForReceive = DataType.Login.ForReceive;

namespace Engine.Operations
{
	public class DownloadOps : IDisposable
	{
		private string _currentConnectionString;

		public DownloadOps(string currentConnectionString)
		{
			_currentConnectionString = currentConnectionString;
		}

		~DownloadOps()
		{
			Dispose();
		}

		public void Dispose()
		{
			_currentConnectionString = null;
			GC.SuppressFinalize(this);
			GC.Collect();
		}

		public DataSet DownloadBoxTypeInformation(ForReceive login, ref StringBuilder infoMessage)
		{
			var parameters = new SortedList<string, string>();
			var stringBuilder = new StringBuilder();
			DataSet tableFromJson;
			try
			{
				parameters.Add("authenticationToken", login.ApiKey);
				tableFromJson = EngineOperationHelper.GetTableFromJson<DataType.BoxType.ForReceive>(ApiAddress.KometBoxTypeList, "text/plain", HttpVerb.GET, "boxtypes", parameters);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			return tableFromJson;
		}

		public DataSet DownloadVendorInformation(ForReceive login, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var parameters = new SortedList<string, string>();
			DataSet tableFromJson;
			try
			{
				parameters.Add("authenticationToken", login.ApiKey);
				tableFromJson = EngineOperationHelper.GetTableFromJson<DataType.Vendor.ForReceive>(ApiAddress.KometVendorList, "text/plain", HttpVerb.GET, "vendors", parameters);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			return tableFromJson;
		}

		public DataSet DownloadVendorAvailabilityInformation(ForReceive login, string poNumber, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var parameters = new SortedList<string, string>();
			DataSet tableFromJson;

			try
			{
				parameters.Add("authenticationToken", login.ApiKey);
				parameters.Add("purchaseOrderNumbers", poNumber);
				tableFromJson = EngineOperationHelper.GetTableFromJson<DataType.VendorAvailability.ForReceive>(ApiAddress.KometVendorAvailabilityList, "text/plain", HttpVerb.GET, "purchaseOrders", parameters);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			return tableFromJson;
		}

		public DataSet DownloadStandingOrderInformation(ForReceive login, string dateStartOrder, string dateEndOrder, ref StringBuilder infoMessage, string status = null, string locationId = null)
		{
			var stringBuilder = new StringBuilder();
			var parameters = new SortedList<string, string>();
			DataSet tableFromJson;
			try
			{
				parameters.Add("authenticationToken", login.ApiKey);
				parameters.Add("dateFrom", dateStartOrder);
				parameters.Add("dateTo", dateEndOrder);
				parameters.Add("status", status);
				tableFromJson = EngineOperationHelper.GetTableFromJson<DataType.StandingOrder.ForReceive>(ApiAddress.KometStandingOrderList, "text/plain", HttpVerb.GET, "standingOrders", parameters);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			return tableFromJson;
		}

		public DataSet DownloadLocationInformation(ForReceive login, ref StringBuilder infoMessage, string status = null)
		{
			var stringBuilder = new StringBuilder();
			var parameters = new SortedList<string, string>();
			DataSet tableFromJson;
			try
			{
				parameters.Add("authenticationToken", login.ApiKey);
				parameters.Add("status", status);
				tableFromJson = EngineOperationHelper.GetTableFromJson<DataType.Location.ForReceive>(ApiAddress.KometLocationList, "text/plain", HttpVerb.GET, "locations", parameters);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			return tableFromJson;
		}

		public ForReceive DownloadLoginInformation(LoginInformation currentLoginInformation, LoginMode loginMode, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			ForReceive currentLogin = null;
			try
			{
				switch (loginMode)
				{
					case LoginMode.UseUserAndPassword:
						infoMessage.AppendLine("Conexión mediante usuario/contraseña");
						currentLogin = EngineOperationHelper.GetObjectFromJson<ForReceive>(ApiAddress.KometLogin, "text/plain", HttpVerb.GET, new SortedList<string, string>
			{
				{
				"login",
				currentLoginInformation.User
				},
				{
				"password",
				currentLoginInformation.Password
				}
			});
						break;

					case LoginMode.UseApiToken:
						infoMessage.AppendLine("Conexión mediante API Token");
						var forReceive2 = new ForReceive();
						forReceive2.ApiKey = currentLoginInformation.ApiToken;
						forReceive2.MessageText = "Ok";
						forReceive2.Status = "0";
						currentLogin = forReceive2;
						break;
				}
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			return currentLogin;
		}

		public DataSet DownloadProductInformation(ForReceive login, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var parameters = new SortedList<string, string>();
			DataSet tableFromJson;
			try
			{
				parameters.Add("authenticationToken", login.ApiKey);
				tableFromJson = EngineOperationHelper.GetTableFromJson<DataType.Product.ForReceive>(ApiAddress.KometProductList, "text/plain", HttpVerb.GET, "products", parameters);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			return tableFromJson;
		}

		public void DownloadProductionInformation(string datePurchaseOrder, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var parameters = new SortedList<string, string>();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			try
			{
				parameters.Add("fec_ini", datePurchaseOrder);
				parameters.Add("fec_fin", datePurchaseOrder);
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
	}
}