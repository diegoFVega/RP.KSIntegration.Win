using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataType.Invoice
{
	public class ForReceive : Message.Message
	{
		[JsonProperty("invoices")]
		public List<Invoice> Invoices { get; set; }
	}
}