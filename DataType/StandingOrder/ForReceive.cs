using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataType.StandingOrder
{
	public abstract class ForReceive : Message.Message
	{
		[JsonProperty("StandingOrder")]
		public List<StandingOrder> StandingOrders { get; set; }
	}
}