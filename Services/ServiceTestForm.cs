﻿using System;
using System.ServiceProcess;
using System.Windows.Forms;

namespace Services
{
#if DEBUG

	public partial class ServiceTestForm : Form
	{
		private ServiceBase service = null;

		public ServiceTestForm()
		{
			InitializeComponent();
		}

		#region State

		/// <summary>
		/// Since we can't have a ServiceController, determine our own state.
		/// </summary>
		private enum State
		{
			Stopped,
			Running,
			Paused
		}

		private State state = State.Stopped;

		/// <summary>
		/// Gets or sets service state. Allows start/stop/pause.
		/// </summary>
		private State ServState
		{
			get { return state; }
			set
			{
				switch (value)
				{
					case State.Paused:
						if (state == State.Running)
							InvokeServiceMember("OnPause");
						else
						{
							pause.Enabled = false;
							throw new ApplicationException("Can't pause unless running.");
						}
						break;

					case State.Running:
						if (state == State.Stopped)
							InvokeServiceMember("OnStart", new string[] { "" });
						else if (state == State.Paused)
							InvokeServiceMember("OnContinue");
						else
							throw new ApplicationException("Can't start unless stopped.");
						pause.Text = "Pause";
						break;

					case State.Stopped:
						InvokeServiceMember("OnStop");
						break;
				}
				state = value;
			}
		}

		#endregion State

		/// <summary>
		/// Create a test form for the given service.
		/// </summary>
		/// <param name="serv">
		/// Instance of a ServiceBase derivation.
		/// </param>
		public ServiceTestForm(ServiceBase serv)
		{
			service = serv;
			InitializeComponent();
		}

		private void InvokeServiceMember(string name)
		{
			InvokeServiceMember(name, null);
		}

		private void InvokeServiceMember(string name, object args)
		{
			InvokeServiceMember(name, new object[] { args });
		}

		private void InvokeServiceMember(string name, object[] args)
		{
			Type serviceType = service.GetType();
			serviceType.InvokeMember(name,
			System.Reflection.BindingFlags.Instance
			| System.Reflection.BindingFlags.InvokeMethod
			| System.Reflection.BindingFlags.NonPublic
			| System.Reflection.BindingFlags.Public,
			null,
			service,
			new object[] { args });
		}

		#region Event Handlers

		private void start_Click(object sender, EventArgs e)
		{
			try
			{
				this.Enabled = false;
				ServState = State.Running;
				output.Text = "Started";
				start.Enabled = false;
				stop.Enabled = true;
				pause.Enabled = true;
			}
			catch (Exception ex)
			{
				output.Text = ex.Message + "\r\n";
			}
			finally
			{
				this.Enabled = true;
			}
		}

		private void stop_Click(object sender, EventArgs e)
		{
			try
			{
				this.Enabled = false;
				ServState = State.Stopped;
				output.Text = "Stopped";
				start.Enabled = true;
				stop.Enabled = false;
				pause.Enabled = false;
			}
			catch (Exception ex)
			{
				output.Text = ex.Message + "\r\n";
			}
			finally
			{
				this.Enabled = true;
			}
		}

		private void pause_Click(object sender, EventArgs e)
		{
			try
			{
				this.Enabled = false;
				if (ServState == State.Paused)
				{
					ServState = State.Running;
					pause.Text = "Pause";
					output.Text = "Resumed";
				}
				else if (ServState == State.Running)
				{
					ServState = State.Paused;
					pause.Text = "Continue";
					output.Text = "Paused";
				}
				stop.Enabled = true;
				start.Enabled = false;
			}
			catch (Exception ex)
			{
				output.Text = ex.Message + "\r\n";
			}
			finally
			{
				this.Enabled = true;
			}
		}

		#endregion Event Handlers
	}

#endif
}