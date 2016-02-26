using Newtonsoft.Json;
using System;

namespace DataType.Purchase
{
	public class ProcessablePO
	{
		[JsonProperty("PONumber")]
		public string PONumber { get; set; }

		[JsonProperty("confirmDate")]
		public DateTime ConfirmDate { get; set; }

		[JsonProperty("companyId")]
		public string CompanyId { get; set; }
	}
}