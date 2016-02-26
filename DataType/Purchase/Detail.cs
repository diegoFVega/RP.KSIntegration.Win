using System.Collections.Generic;

namespace DataType.Purchase
{
	public class Detail
	{
		public string CustomerName { get; set; }

		public string TotalCost { get; set; }

		public int Prebook { get; set; }

		public string BoxType { get; set; }

		public string UnitType { get; set; }

		public string OrderType { get; set; }

		public string ProductDescription { get; set; }

		public int StemsBunch { get; set; }

		public int Units { get; set; }

		public int ProductId { get; set; }

		public int TotalUnits { get; set; }

		public string StandingOrder { get; set; }

		public int CustomerId { get; set; }

		public string LineItemStatus { get; set; }

		public double UnitCost { get; set; }

		public int Bunches { get; set; }

		public int TotalBoxes { get; set; }

		public string MarkCode { get; set; }

		public List<Breakdown> Breakdowns { get; set; }

		public List<Box> Boxes { get; set; }
	}
}