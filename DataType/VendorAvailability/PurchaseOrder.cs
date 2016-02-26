using System.Collections.Generic;

namespace DataType.VendorAvailability
{
	public class PurchaseOrder
	{
		public string id { get; set; }
		public string number { get; set; }
		public string shipDate { get; set; }
		public int purchaseOrders_id { get; set; }
		public List<Detail> details { get; set; }
	}
}