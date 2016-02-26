using System;
using System.Collections.Generic;

namespace DataType.Inventory
{
	public class ForSend
	{
		public string AuthenticationToken { get; set; }

		public string GrowerId { get; set; }

		public string Awb { get; set; }

		public string Reference { get; set; }

		public DateTime ShipDate { get; set; }

		public DateTime ArrivalDate { get; set; }

		public string CustomerId { get; set; }

		public string ProductId { get; set; }

		public UnitTypeStyle UnitType { get; set; }

		public int StemsBunch { get; set; }

		public string CompanyLocationId { get; set; }

		public List<Breakdown> Breakdowns { get; set; }
	}
}