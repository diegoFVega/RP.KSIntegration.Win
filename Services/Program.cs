using Services.Datawarehouse;
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
			Application.Run(new ServiceTestForm(new Customer()));
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