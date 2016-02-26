using System.Collections.Generic;

namespace DataType.Invoice
{
	public class Detail
	{
		public int DetailsId { get; set; }

		public int InvoiceId { get; set; }

		public string Awb { get; set; }

		public string Ref { get; set; }

		public int ProductId { get; set; }

		public string ProductDescription { get; set; }

		public int GrowerId { get; set; }

		public string GrowerName { get; set; }

		public int TotalBoxes { get; set; }

		public int BoxTypeId { get; set; }

		public string BoxType { get; set; }

		public string Units { get; set; }

		public string UnitType { get; set; }

		public int TotalUnits { get; set; }

		public int BunchesBox { get; set; }

		public int StemsBunch { get; set; }

		public string MarkCode { get; set; }

		public string UnitPrice { get; set; }

		public string Amount { get; set; }

		public string SalesType { get; set; }

		public List<Breakdown> Breakdowns { get; set; }

		public List<Box> Boxes { get; set; }
	}
}