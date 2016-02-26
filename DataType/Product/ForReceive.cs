using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataType.Product
{
	public abstract class ForReceive : Message.Message
	{
		[JsonProperty("products")]
		public List<Product> Products { get; set; }
	}
}