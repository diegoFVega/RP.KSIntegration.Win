﻿namespace Services
{
	partial class ProjectInstaller
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
			this.serviceInstaller1 = new System.ServiceProcess.ServiceInstaller();
			this.serviceInstaller2 = new System.ServiceProcess.ServiceInstaller();
			this.serviceInstaller3 = new System.ServiceProcess.ServiceInstaller();
			// 
			// serviceProcessInstaller1
			// 
			this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
			this.serviceProcessInstaller1.Password = null;
			this.serviceProcessInstaller1.Username = null;
			// 
			// serviceInstaller1
			// 
			this.serviceInstaller1.DisplayName = "Rosaprima Production Datawarehouse Service";
			this.serviceInstaller1.ServiceName = "Rosaprima Production Datawarehouse Service";
			// 
			// serviceInstaller2
			// 
			this.serviceInstaller2.DisplayName = "Rosaprima Sales Datawarehouse Service";
			this.serviceInstaller2.ServiceName = "Rosaprima Sales Datawarehouse Service";
			// 
			// serviceInstaller3
			// 
			this.serviceInstaller3.DisplayName = "Rosaprima Customer Datawarehouse Service";
			this.serviceInstaller3.ServiceName = "Rosaprima Customer Datawarehouse Service";
			// 
			// ProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller1,
            this.serviceInstaller1,
            this.serviceInstaller2,
            this.serviceInstaller3});

		}

		#endregion

		private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;
		private System.ServiceProcess.ServiceInstaller serviceInstaller1;
		private System.ServiceProcess.ServiceInstaller serviceInstaller2;
		private System.ServiceProcess.ServiceInstaller serviceInstaller3;
	}
}