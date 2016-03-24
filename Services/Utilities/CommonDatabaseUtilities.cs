using System.Configuration;

namespace Services.Utilities
{
	public static class CommonDatabaseUtilities
	{
		public static string CurrentActiveConnectionString()
		{
			var str = string.Empty;
			switch (ConfigurationManager.AppSettings["ConnectionSwitch"])
			{
				case "Production":
					str = ConfigurationManager.ConnectionStrings["PrimasoftPrdConnection"].ConnectionString;
					break;

				case "Test":
					str = ConfigurationManager.ConnectionStrings["PrimasoftUATConnection"].ConnectionString;
					break;

				case "Sales":
					str = ConfigurationManager.ConnectionStrings["PrimasoftSlsConnection"].ConnectionString;
					break;
			}
			return str;
		}
	}
}