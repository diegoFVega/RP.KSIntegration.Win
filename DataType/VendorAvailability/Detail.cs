using System.Collections.Generic;

namespace DataType.VendorAvailability
{
	public class Detail
	{
		public string poItemId { get; set; }
		public int details_id { get; set; }
		public int purchaseOrders_id { get; set; }
		public List<VendorAvailabilityDetails> VendorAvailabilityDetails { get; set; }
	}
}