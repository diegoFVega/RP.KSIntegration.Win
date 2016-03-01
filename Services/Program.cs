using Services.Datawarehouse;
using System.ServiceProcess;
using System.Windows.Forms;

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
			Application.Run(new ServiceTestForm(new Sale()));
#else
			ServiceBase[] ServicesToRun;
			ServicesToRun = new ServiceBase[]
			{
								new Production(),
								new Sale()
			};
			ServiceBase.Run(ServicesToRun);
#endif
		}
	}
}
