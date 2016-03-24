using DataType.Login;
using Engine.Enum;
using Engine.Operations.ResourcesOps;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Engine.Operations.DownloadsOps
{
	public class Customer : IDisposable
	{
		public ForReceive CurrentLogin { get; set; }

		~Customer()
		{
			Dispose();
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
			GC.Collect();
		}

		public DataSet DownloadCustomerInformation(ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var parameters = new SortedList<string, string>();
			DataSet tableFromJson;

			try
			{
				parameters.Add("authenticationToken", CurrentLogin.ApiKey);
				tableFromJson = EngineOperationHelper.GetTableFromJson<DataType.Customer.ForReceive>(ApiAddress.KometCustomerList, "text/plain", HttpVerb.GET, "customers", parameters);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				infoMessage = stringBuilder;
				throw new Exception(stringBuilder.ToString());
			}

			return tableFromJson;
		}

		public DataSet DownloadCustomerShipToInformation(string customerId, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var parameters = new SortedList<string, string>();
			DataSet tableFromJson;

			try
			{
				parameters.Add("authenticationToken", CurrentLogin.ApiKey);
				parameters.Add("customerId", customerId);
				tableFromJson = EngineOperationHelper.GetTableFromJson<DataType.ShipTo.ForReceive>(ApiAddress.KometCustomerShipToList, "text/plain", HttpVerb.GET, "shiptos", parameters);
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
	}
}