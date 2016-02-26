using BusinessObjects.Properties;
using DataType.Login;
using Engine.Operations;
using System;
using System.Text;

namespace BusinessObjects.Utilities
{
	public static class CommonUtilities
	{
		public static ForReceive AutoLogin()
		{
			var loginMethods = new DownloadOps(Settings.Default.PrdEnvironment);
			var infoMessage = new StringBuilder();
			ForReceive loginInfo;
			var loginInformation = new LoginInformation
			{
				User = Settings.Default.KometUsername,
				Password = Settings.Default.KometPassword
			};

			try
			{
				loginInfo = loginMethods.DownloadLoginInformation(loginInformation, LoginMode.UseUserAndPassword, ref infoMessage);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

			return loginInfo;
		}
	}
}