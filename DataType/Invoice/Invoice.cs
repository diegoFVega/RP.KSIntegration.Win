namespace DataType.Invoice
{
	public class Invoice
	{
		public int Id { get; set; }

		public int Number { get; set; }

		public int CustomerId { get; set; }

		public string CustomerName { get; set; }

		public string BillCity { get; set; }

		public string BillState { get; set; }

		public string BillAddress { get; set; }

		public string BillZipCode { get; set; }

		public string BillCountry { get; set; }

		public string OrderRef { get; set; }

		public string ShipName { get; set; }

		public string ShipCity { get; set; }

		public string ShipState { get; set; }

		public string ShipAddress { get; set; }

		public string ShipZipCode { get; set; }

		public string ShipCountry { get; set; }

		public string ShipDate { get; set; }

		public int CarrierId { get; set; }

		public string CarrierName { get; set; }

		public int SalesPersonId { get; set; }

		public string SalesPersonName { get; set; }

		public int LocationId { get; set; }

		public string LocationName { get; set; }

		public string CustomerPoNumber { get; set; }

		public int TotalBoxes { get; set; }

		public string Subtotal { get; set; }

		public string AdditionalCharges { get; set; }

		public string Taxes { get; set; }

		public string Total { get; set; }

		public string Status { get; set; }

		public int Invoices_Id { get; set; }

		public string CreatedOn { get; set; }
	}
}