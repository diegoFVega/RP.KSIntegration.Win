using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataType.Location
{
	public abstract class ForReceive : Message.Message
	{
		[JsonProperty("locations")]
		public List<Location> Locations { get; set; }
	}
}