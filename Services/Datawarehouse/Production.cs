using Engine.Operations;
using Services.Utilities;
using System;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Timers;

namespace Services.Datawarehouse
{
	partial class Production : ServiceBase
	{
		private readonly System.Timers.Timer _controlServiceTimer;
		private StringBuilder infoMessage = new StringBuilder();

		public Production()
		{
			InitializeComponent();
			_controlServiceTimer = new System.Timers.Timer();
		}

		protected override void OnStart(string[] args)
		{
			infoMessage.AppendLine(string.Format("-> El proceso de descarga a Staging area iniciará en: {0}", IdleTimeToStart().ToString("T")));
			EvlIssue.WriteEntry(infoMessage.ToString(), EventLogEntryType.Warning);

			_controlServiceTimer.Interval = IdleTimeToStart().TotalMilliseconds;
			_controlServiceTimer.AutoReset = true;
			_controlServiceTimer.Enabled = true;
			_controlServiceTimer.Start();
			_controlServiceTimer.Elapsed += new ElapsedEventHandler(ControlServiceTimer_Elapsed);
		}

#if DEBUG

		protected virtual void OnStop(string[] args)
		{
			OnStop();
		}

		protected virtual void OnPause(string[] args)
		{
			OnPause();
		}

#endif

		protected override void OnStop()
		{
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

			infoMessage.Clear();
		}

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
			var currentTime = DateTime.Now;
			var startProcessTime = Convert.ToDateTime(ConfigurationManager.AppSettings["DWCnfStartTime"]);
			var eventType = EventLogEntryType.Information;

			try
			{
				if (currentTime.ToString("HH:mm") == startProcessTime.ToString("HH:mm"))
				{
					var daysBefore = Convert.ToInt32(ConfigurationManager.AppSettings["DWDaysBeforeDownload"]);
					var startDate = currentTime.Date.AddDays(-daysBefore).ToString("yyyy-MM-dd");
					var finishDate = currentTime.Date.ToString("yyyy-MM-dd");
					var dataWarehouseOps = new DataWarehouseOps(CommonDatabaseUtilities.CurrentActiveConnectionString());

					infoMessage.AppendLine(string.Format("-> Fechas de proceso desde: {0} hasta:{1}", startDate, finishDate));
					infoMessage.AppendLine("-> 1. Obtener informacion desde PS");
					dataWarehouseOps.GetProductionFromPS(startDate, finishDate, ref infoMessage);

					infoMessage.AppendLine("-> 2. Envio de informacion a DW");
					dataWarehouseOps.SendProductionToDW(startDate, finishDate, ref infoMessage);
				}
				else
				{
					infoMessage.AppendLine(string.Format("-> Proceso inactivo, trabajo a las {0}", ConfigurationManager.AppSettings["DWCnfStartTime"]));
					infoMessage.AppendLine("Gracias por su comprensión.");

					eventType = EventLogEntryType.Warning;
				}
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

				eventType = EventLogEntryType.Error;
			}
			finally
			{
				infoMessage.AppendLine("Fin de proceso");

				var elapsedTime = DateTime.Now.Subtract(currentTime);

				infoMessage.AppendLine(string.Format("Tiempo de ejecucion: {0}", elapsedTime.TotalSeconds));
				EvlIssue.WriteEntry(infoMessage.ToString(), eventType);

				elapsedTime = TimeSpan.Parse(ConfigurationManager.AppSettings["KSLatencyProcess"]);
				Thread.Sleep(elapsedTime.Milliseconds);
				_controlServiceTimer.Interval = IdleTimeToStart().TotalMilliseconds;
				_controlServiceTimer.Start();
			}
		}
	}
}