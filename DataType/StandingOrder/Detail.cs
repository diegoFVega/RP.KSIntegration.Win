using System.Collections.Generic;

namespace DataType.StandingOrder
{
	public class Detail
	{
		public string SoItemId { get; set; }

		public string ProductId { get; set; }

		public string ProductDescription { get; set; }

		public string Units { get; set; }

		public string UnitType { get; set; }

		public string Bunches { get; set; }

		public string TotalBoxes { get; set; }

		public string TotalUnits { get; set; }

		public string UnitPrice { get; set; }

		public string TotalPrice { get; set; }

		public string UnitCost { get; set; }

		public string TotalCost { get; set; }

		public string StemsBunch { get; set; }

		public string MarkCode { get; set; }

		public string BoxType { get; set; }

		public string VendorName { get; set; }

		public string IsAssorted { get; set; }

		public List<Breakdown> Breakdowns { get; set; }
	}
}