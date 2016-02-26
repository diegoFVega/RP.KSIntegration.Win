using Newtonsoft.Json;

namespace DataType.Login
{
	public class ForReceive : Message.Message
	{
		[JsonProperty("apiKey")]
		public string ApiKey { get; set; }
	}
}