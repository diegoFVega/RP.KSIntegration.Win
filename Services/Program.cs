using System;
using Services.Datawarehouse;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ServiceProcess;

namespace Services
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main()
		{
#if DEBUG
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new ServiceTestForm(new Production()));
#else
			ServiceBase[] ServicesToRun;
			ServicesToRun = new ServiceBase[]
			{
								new Production()
			};
			ServiceBase.Run(ServicesToRun);
#endif
		}
	}
}
