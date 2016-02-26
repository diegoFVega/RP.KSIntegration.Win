using Newtonsoft.Json;

namespace DataType.BoxChange
{
	public class BoxCodeType : Message.Message
	{
		[JsonProperty("newBoxCode")]
		public string NewBoxCode { get; set; }
	}
}