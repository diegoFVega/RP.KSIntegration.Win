using System;
using System.Collections.Generic;

namespace DataType.Purchase
{
	public class Purchase
	{
		public int Id { get; set; }

		public string Number { get; set; }

		public string Origin { get; set; }

		public int VendorId { get; set; }

		public string VendorName { get; set; }

		public string ShipDate { get; set; }

		public string Status { get; set; }

		public int LocationId { get; set; }

		public string LocationName { get; set; }

		public double TotalCost { get; set; }

		public string Comments { get; set; }

		public int TotalBoxes { get; set; }

		public int PurchaseId { get; set; }

		public string EstadoRegistro { get; set; }

		public DateTime FechaDeRegistro { get; set; }

		public List<Detail> Details { get; set; }
	}
}