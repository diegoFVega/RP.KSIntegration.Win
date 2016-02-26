using Newtonsoft.Json;
using System.Text;

namespace DataType.Message
{
	public class Message
	{
		private string _status;

		[JsonProperty("status")]
		public string Status
		{
			get
			{
				if (_status != "1")
				{
					var stringBuilder = new StringBuilder();
					stringBuilder.AppendLine("Se ha recibido un mensaje de error en la respuesta generada por el método JSON");
					stringBuilder.AppendLine(string.Format("Estado {0}", _status));
					stringBuilder.AppendLine(string.Format("Mensaje {0}", MessageText));
				}
				return _status;
			}
			set
			{
				_status = value;
			}
		}

		[JsonProperty("message")]
		public string MessageText { get; set; }
	}
}