using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataType.ShipTo
{
	public abstract class ForReceive : Message.Message
	{
		[JsonProperty("Shiptos")]
		public List<Shipto> Shiptos { get; set; }
	}
}