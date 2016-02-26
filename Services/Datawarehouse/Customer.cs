using System;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace Services.Datawarehouse
{
	partial class Customer : ServiceBase
	{
		private readonly System.Timers.Timer _controlServiceTimer;

		public Customer()
		{
			InitializeComponent();
			_controlServiceTimer = new System.Timers.Timer();
		}

		protected override void OnStart(string[] args)
		{
			var infoMessage = new StringBuilder();

			//infoMessage.AppendLine("Proceso de descarga a standing area iniciará en: {0}", IdleTimeToStart().ToString("T"));

			_controlServiceTimer.Interval = IdleTimeToStart().TotalMilliseconds;
			_controlServiceTimer.AutoReset = true;
			_controlServiceTimer.Enabled = true;
			_controlServiceTimer.Start();
			_controlServiceTimer.Elapsed += new ElapsedEventHandler(ControlServiceTimer_Elapsed);
		}

		protected override void OnStop()
		{
			NiServicioCliente.BalloonTipIcon = ToolTipIcon.Info;
			NiServicioCliente.BalloonTipTitle = "Stoping service...";
			NiServicioCliente.BalloonTipText = "Se ha detenido el proceso";
			NiServicioCliente.ShowBalloonTip(60000);
			_controlServiceTimer.AutoReset = false;
			_controlServiceTimer.Enabled = false;
			GC.Collect();
		}

		protected override void OnPause()
		{
			_controlServiceTimer.AutoReset = false;
			_controlServiceTimer.Enabled = false;
		}

		private void ControlServiceTimer_Elapsed(object sender, ElapsedEventArgs eearg)
		{
			_controlServiceTimer.Enabled = false;

			new Thread(new ThreadStart(ProcesoAEjecutar))
			{
				Name = "Rosaprima Production Datawarehouse Service",
				Priority = ThreadPriority.Normal
			}.Start();
		}

		/// <summary>
		/// Calcula el tiempo de la proxima ejecución basado en la fecha y hora actual, utiliza un mecanismo de compensación para que su funcionamiento sea con la recurrencia indicada
		/// </summary>
		/// <returns>La hora de la proxima ejecución</returns>
		private static TimeSpan IdleTimeToStart()
		{
			var currentTime = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));
			var currentHour = new TimeSpan(DateTime.Now.Hour, 0, 0);
			var recurrencyPeriod = TimeSpan.Parse(ConfigurationManager.AppSettings["KSLatencyProcess"]);
			var nextTime = currentHour.Add(recurrencyPeriod);

			while (nextTime <= currentTime)
				nextTime = nextTime.Add(recurrencyPeriod);

			return nextTime.Subtract(TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss")));
		}

		private void ProcesoAEjecutar()
		{
			var infoMessage = new StringBuilder();
			var now = DateTime.Now;
			var startProcessTime = Convert.ToDateTime(ConfigurationManager.AppSettings["DWStartTime"]);

			try
			{
				//if (DateTime.Now.ToString("HH:mm") == startProcessTime.ToString("HH:mm"))
				//{
				var daysBefore = Convert.ToInt32(ConfigurationManager.AppSettings["DWDaysBeforeDownload"]);
				var startProcess = now.Date.AddDays(-daysBefore);

				MessageBox.Show(string.Format("dumb process {0} - {1}", now.ToString("yyyy-MM-dd"), startProcess.ToString("yyyy-MM-dd")));
				//}
			}
			catch (Exception ex)
			{
				infoMessage.AppendLine(string.Empty);
				infoMessage.AppendLine("------------------ Error ------------------");
				infoMessage.AppendLine(string.Format("Origen: {0}", ex.Source));
				infoMessage.AppendLine(string.Format("Error: {0}", ex.Message));
				infoMessage.AppendLine(string.Format("Datos: {0}", ex.Data));
				infoMessage.AppendLine(string.Format("Trace: {0}", ex.StackTrace));
				infoMessage.AppendLine("-------------------------------------------");

				//_mailer.SendANotification(infoMessage.ToString(), string.Format("error en servicio: {0}", ServiceName));
				EvlIssue.WriteEntry(infoMessage.ToString(), EventLogEntryType.Error);
				NiServicioCliente.BalloonTipIcon = ToolTipIcon.Error;
			}
			finally
			{
				var elapsedTime = DateTime.Now.Subtract(now);

				NiServicioCliente.BalloonTipIcon = ToolTipIcon.Info;
				elapsedTime = IdleTimeToStart();
				elapsedTime = TimeSpan.Parse(ConfigurationManager.AppSettings["KSLatencyProcess"]);
				Thread.Sleep(elapsedTime.Milliseconds);

				elapsedTime = IdleTimeToStart();

				_controlServiceTimer.Interval = elapsedTime.TotalMilliseconds;
				NiServicioCliente.BalloonTipTitle = "Process results...";
				NiServicioCliente.BalloonTipText = string.Format("Tiempo de ejecucion: {0}", elapsedTime.ToString("T"));
				NiServicioCliente.ShowBalloonTip(60000);
				_controlServiceTimer.Start();
			}
		}
	}
}