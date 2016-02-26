using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataType.Customer
{
	public class ForReceive : Message.Message
	{
		[JsonProperty("customers")]
		public List<Customer> Customers { get; set; }
	}
}