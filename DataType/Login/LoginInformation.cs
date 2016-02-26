using System.Text;

namespace DataType.Login
{
	public class LoginInformation
	{
		public string User { get; set; }

		public string Password { get; set; }

		public string ApiToken { get; set; }

		public override string ToString()
		{
			var stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("Usuario: {0}", User));
			stringBuilder.AppendLine(string.Format("Clave: {0}", Password));
			stringBuilder.AppendLine(string.Format("Token: {0}", ApiToken));
			return stringBuilder.ToString();
		}
	}
}