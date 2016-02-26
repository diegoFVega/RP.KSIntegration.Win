using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataType.BoxType
{
	public class ForReceive : Message.Message
	{
		[JsonProperty("BoxTypes")]
		public List<BoxTypeItem> BoxTypes { get; set; }
	}
}