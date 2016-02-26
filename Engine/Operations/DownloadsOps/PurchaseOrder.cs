using Engine.Enum;
using Engine.Operations.ResourcesOps;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Engine.Operations.DownloadsOps
{
	public class PurchaseOrder : IDisposable
	{
		public DataType.Login.ForReceive CurrentLogin { get; set; }
		public string ShipDate { get; set; }

		~PurchaseOrder()
		{
			Dispose();
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
			GC.Collect();
		}

		public DataSet DownloadPurchaseOrderInformation(ref StringBuilder infoMessage, string poNumber = null)
		{
			var stringBuilder = new StringBuilder();
			var parameters = new SortedList<string, string>();
			DataSet dSet;

			try
			{
				parameters.Add("authenticationToken", CurrentLogin.ApiKey);
				parameters.Add("date", ShipDate);

				if (string.IsNullOrEmpty(poNumber) != true)
					parameters.Add("number", poNumber);

				dSet = EngineOperationHelper.GetTableFromJson<DataType.Purchase.ForReceive>(ApiAddress.KometPurchaseDetailList, "text/plain", HttpVerb.GET, "purchaseOrders", parameters);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine(string.Format("Ha ocurrido una excepcion en la ejecucion de la consulta al metodo API {0}.", ApiAddress.KometPurchaseDetailList));
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));

				throw new Exception(stringBuilder.ToString());
			}
			return dSet;
		}

		public DataType.Message.Message SendPurchaseOrderInformation(int POItemId, string references, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var message = new DataType.Message.Message();

			try
			{
				JObject parameters = JObject.FromObject(new { authenticationToken = (string)CurrentLogin.ApiKey, purchaseOrderItemId = (int)POItemId, reference = (string)references });

				message = EngineOperationHelper.GetObjectFromJson<DataType.Message.Message>(ApiAddress.KometPOUpdate, "application/json", "POST", parameters);
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
				Dispose();
			}

			return message;
		}
	}
}