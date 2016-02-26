using DataType.Login;
using DataType.Message;
using Engine.Enum;
using Engine.Operations.ResourcesOps;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Engine.Operations.DownloadsOps
{
	public class Invoice : IDisposable
	{
		public ForReceive CurrentLogin { get; set; }
		public string ShipDate { get; set; }
		public string OrderStatus { get; set; }
		public string OrderLocationId { get; set; }

		~Invoice()
		{
			Dispose();
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
			GC.Collect();
		}

		public DataSet DownloadInvoiceInformation(ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var parameters = new SortedList<string, string>();
			var dSet = new DataSet();

			try
			{
				parameters.Add("authenticationToken", CurrentLogin.ApiKey);
				parameters.Add("orderDate", ShipDate);

				if (!string.IsNullOrEmpty(OrderStatus))
					parameters.Add("status", OrderStatus);
				if (!string.IsNullOrEmpty(OrderLocationId))
					parameters.Add("locationId", OrderLocationId);

				dSet = EngineOperationHelper.GetTableFromJson<DataType.Invoice.Invoice>(ApiAddress.KometInvoiceDetailList, "text/plain", HttpVerb.GET, "invoices", parameters);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine(string.Format("Ha ocurrido una excepcion en la ejecucion de la consulta al metodo API {0}.", ApiAddress.KometInvoiceDetailList));
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));

				throw new Exception(stringBuilder.ToString());
			}
			finally
			{
				Dispose();
			}

			return dSet;
		}

		public Message SendTransferInvoices(string currentDate, string externalAppId, string invoiceIds, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var message = new Message();
			var parameters = new SortedList<string, string>();

			try
			{
				if (string.IsNullOrEmpty(invoiceIds) != true)
				{
					parameters.Add("authenticationToken", CurrentLogin.ApiKey);
					parameters.Add("invoiceIds", invoiceIds);
					parameters.Add("externalSystemId", externalAppId);
					message = EngineOperationHelper.GetObjectFromJson<Message>(ApiAddress.KometInvoiceIntegrate, "text/plain",
						HttpVerb.GET, parameters);
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
				Dispose();
			}

			return message;
		}

		public Message SendInvoiceInformation(string invoiceId, string reference, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var message = new Message();

			try
			{
				var parameters2 = new JObject();
				parameters2.Add("authenticationToken", (string)CurrentLogin.ApiKey);
				parameters2.Add("invoiceId", (string)invoiceId);
				parameters2.Add("reference", (string)reference);

				message = EngineOperationHelper.GetObjectFromJson<Message>(ApiAddress.KometInvoiceUpdate, "application/json", "POST", parameters2);
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