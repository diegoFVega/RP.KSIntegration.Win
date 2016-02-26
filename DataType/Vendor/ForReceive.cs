using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataType.Vendor
{
	public abstract class ForReceive : Message.Message
	{
		[JsonProperty("vendors")]
		public List<Vendor> Vendor { get; set; }
	}
}