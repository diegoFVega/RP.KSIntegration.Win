using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataType.BoxChange
{
	public class ForReceive : Message.Message
	{
		[JsonProperty("boxes")]
		public List<BoxCodeType> BoxCodeTypes { get; set; }
	}
}