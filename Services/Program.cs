using System.ServiceProcess;
using System.Windows.Forms;

namespace Services
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		private static void Main()
		{
#if DEBUG
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new ServiceTestForm(new Services.Datawarehouse.Customer()));
#else
			ServiceBase[] ServicesToRun;
			ServicesToRun = new ServiceBase[]
			{
								new Services.Datawarehouse.Production(),
								new Services.Datawarehouse.Customer(),
								new Services.Datawarehouse.Sale()
			};
			ServiceBase.Run(ServicesToRun);
#endif
		}
	}
}