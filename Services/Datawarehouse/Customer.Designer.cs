namespace Services.Datawarehouse
{
	partial class Customer
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Customer));
			this.EvlIssue = new System.Diagnostics.EventLog();
			this.CmsManejaServicio = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.NiServicioCliente = new System.Windows.Forms.NotifyIcon(this.components);
			((System.ComponentModel.ISupportInitialize)(this.EvlIssue)).BeginInit();
			this.CmsManejaServicio.SuspendLayout();
			// 
			// CmsManejaServicio
			// 
			this.CmsManejaServicio.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
			this.CmsManejaServicio.Name = "CmsIniciarServicio";
			this.CmsManejaServicio.Size = new System.Drawing.Size(152, 26);
			this.CmsManejaServicio.Text = "Administrador de Servicio de Ventas";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
			this.toolStripMenuItem1.Text = "Iniciar Proceso";
			this.toolStripMenuItem1.ToolTipText = "Inicia el proceso";
			// 
			// NiServicioCliente
			// 
			this.NiServicioCliente.ContextMenuStrip = this.CmsManejaServicio;
			this.NiServicioCliente.Text = "Administrador de Servicio de Ventas";
			this.NiServicioCliente.Visible = true;
			// 
			// Customer
			// 
			this.ServiceName = "Customer";
			((System.ComponentModel.ISupportInitialize)(this.EvlIssue)).EndInit();
			this.CmsManejaServicio.ResumeLayout(false);

		}

		#endregion

		private System.Diagnostics.EventLog EvlIssue;
		private System.Windows.Forms.ContextMenuStrip CmsManejaServicio;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.NotifyIcon NiServicioCliente;
	}
}
