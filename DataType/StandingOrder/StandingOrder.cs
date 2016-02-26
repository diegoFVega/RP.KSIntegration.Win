using System.Collections.Generic;

namespace DataType.StandingOrder
{
	public class StandingOrder
	{
		public string Id { get; set; }

		public string Number { get; set; }

		public string CustomerName { get; set; }

		public string PriceList { get; set; }

		public string CarrierName { get; set; }

		public string Recurrence { get; set; }

		public string Days { get; set; }

		public string StartDate { get; set; }

		public string EndDate { get; set; }

		public string Salesperson { get; set; }

		public string LocationCode { get; set; }

		public string LocationName { get; set; }

		public string ShipToName { get; set; }

		public string ShipToCarrier { get; set; }

		public string ShipToStreet { get; set; }

		public string ShipToCity { get; set; }

		public string ShipToState { get; set; }

		public string ShipToZipcode { get; set; }

		public string ShipToCountry { get; set; }

		public string ShipToPhone { get; set; }

		public string ShipToFax { get; set; }

		public List<Detail> Details { get; set; }
	}
}