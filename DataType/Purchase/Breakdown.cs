﻿namespace DataType.Purchase
{
	public abstract class Breakdown
	{
		public int DetailsId { get; set; }

		public int ProductId { get; set; }

		public string ProductDescription { get; set; }

		public int StemsBunch { get; set; }

		public int Bunches { get; set; }

		public double Cost { get; set; }
	}
}