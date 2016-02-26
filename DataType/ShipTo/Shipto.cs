namespace DataType.ShipTo
{
	public abstract class Shipto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Address { get; set; }

		public string City { get; set; }

		public string State { get; set; }

		public string Zipcode { get; set; }

		public string Country { get; set; }

		public string Phone { get; set; }

		public string Fax { get; set; }

		public string CarrierName { get; set; }

		public string CarrierAccount { get; set; }
	}
}