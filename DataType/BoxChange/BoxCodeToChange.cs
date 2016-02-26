using Newtonsoft.Json;

namespace DataType.BoxChange
{
	public class BoxCodeToChange
	{
		[JsonProperty("currentBoxCode")]
		public string CurrentBoxCode { get; set; }

		[JsonProperty("newBoxCode")]
		public string NewBoxCode { get; set; }
	}
}