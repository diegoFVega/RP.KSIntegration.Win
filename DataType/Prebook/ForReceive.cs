using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataType.Prebook
{
	public abstract class ForReceive : Message.Message
	{
		[JsonProperty("Prebook")]
		public List<Prebook> Prebooks { get; set; }
	}
}