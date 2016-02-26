namespace Services.Datawarehouse
{
	partial class Production
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Production));
			this.NiServicioProduccion = new System.Windows.Forms.NotifyIcon(this.components);
			this.CmsManejaServicio = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.EvlIssue = new System.Diagnostics.EventLog();
			this.CmsManejaServicio.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.EvlIssue)).BeginInit();
			// 
			// NiServicioProduccion
			// 
			this.NiServicioProduccion.ContextMenuStrip = this.CmsManejaServicio;
			this.NiServicioProduccion.Text = "Administrador de Servicio de Ventas";
			this.NiServicioProduccion.Visible = true;
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
			// Production
			// 
			this.ServiceName = "Production";
			this.CmsManejaServicio.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.EvlIssue)).EndInit();

		}

		#endregion

		private System.Windows.Forms.NotifyIcon NiServicioProduccion;
		private System.Windows.Forms.ContextMenuStrip CmsManejaServicio;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Diagnostics.EventLog EvlIssue;
	}
}
