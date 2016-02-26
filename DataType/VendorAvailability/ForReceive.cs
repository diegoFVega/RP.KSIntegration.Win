using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataType.VendorAvailability
{
	public abstract class ForReceive : Message.Message
	{
		[JsonProperty("VendorAvailability")]
		public List<PurchaseOrder> PurchaseOrders { get; set; }
	}
}