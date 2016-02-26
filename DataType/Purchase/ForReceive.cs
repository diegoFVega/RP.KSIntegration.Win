using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataType.Purchase
{
	public abstract class ForReceive : Message.Message
	{
		[JsonProperty("Purchases")]
		public List<Purchase> Purchases { get; set; }
	}
}