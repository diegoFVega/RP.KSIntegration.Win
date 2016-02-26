namespace BusinessObjects
{
	partial class Downloads
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Ordenes locales facturadas");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Ordenes de compra ");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Ordenes fijas activas");
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Downloads));
			this.GpbAcciones = new System.Windows.Forms.GroupBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.TvwKsElements = new System.Windows.Forms.TreeView();
			this.SsMessages = new System.Windows.Forms.StatusStrip();
			this.LblMessages = new System.Windows.Forms.ToolStripStatusLabel();
			this.PnlProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.BwkProcess = new System.ComponentModel.BackgroundWorker();
			this.LblFecha = new System.Windows.Forms.Label();
			this.DtpProcessDate = new System.Windows.Forms.DateTimePicker();
			this.LblUntilDate = new System.Windows.Forms.Label();
			this.DtpUntilProcessDate = new System.Windows.Forms.DateTimePicker();
			this.ChkMasiveProcess = new System.Windows.Forms.CheckBox();
			this.GpbDatos = new System.Windows.Forms.GroupBox();
			this.TspKsAction = new System.Windows.Forms.ToolStrip();
			this.TsbDownload = new System.Windows.Forms.ToolStripButton();
			this.GpbAcciones.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SsMessages.SuspendLayout();
			this.GpbDatos.SuspendLayout();
			this.TspKsAction.SuspendLayout();
			this.SuspendLayout();
			// 
			// GpbAcciones
			// 
			this.GpbAcciones.Controls.Add(this.panel1);
			this.GpbAcciones.Location = new System.Drawing.Point(12, 12);
			this.GpbAcciones.Name = "GpbAcciones";
			this.GpbAcciones.Size = new System.Drawing.Size(321, 167);
			this.GpbAcciones.TabIndex = 8;
			this.GpbAcciones.TabStop = false;
			this.GpbAcciones.Text = "Acciones";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.TvwKsElements);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 16);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(315, 148);
			this.panel1.TabIndex = 6;
			// 
			// TvwKsElements
			// 
			this.TvwKsElements.CheckBoxes = true;
			this.TvwKsElements.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TvwKsElements.Location = new System.Drawing.Point(0, 0);
			this.TvwKsElements.Name = "TvwKsElements";
			treeNode1.Name = "KsInvoice";
			treeNode1.Tag = "download";
			treeNode1.Text = "Ordenes locales facturadas";
			treeNode2.Name = "KsPurchase";
			treeNode2.Text = "Ordenes de compra ";
			treeNode3.Name = "KsStanding";
			treeNode3.Text = "Ordenes fijas activas";
			this.TvwKsElements.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
			this.TvwKsElements.Scrollable = false;
			this.TvwKsElements.SelectedImageKey = "empty.gif";
			this.TvwKsElements.Size = new System.Drawing.Size(315, 148);
			this.TvwKsElements.TabIndex = 5;
			this.TvwKsElements.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TvwKsElements_AfterCheck);
			// 
			// SsMessages
			// 
			this.SsMessages.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LblMessages,
            this.PnlProgress});
			this.SsMessages.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.SsMessages.Location = new System.Drawing.Point(0, 190);
			this.SsMessages.Name = "SsMessages";
			this.SsMessages.Size = new System.Drawing.Size(608, 22);
			this.SsMessages.TabIndex = 11;
			this.SsMessages.Text = "statusStrip1";
			// 
			// LblMessages
			// 
			this.LblMessages.Name = "LblMessages";
			this.LblMessages.Size = new System.Drawing.Size(32, 17);
			this.LblMessages.Text = "Listo";
			// 
			// PnlProgress
			// 
			this.PnlProgress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.PnlProgress.Name = "PnlProgress";
			this.PnlProgress.Size = new System.Drawing.Size(150, 16);
			this.PnlProgress.Step = 1;
			this.PnlProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			// 
			// BwkProcess
			// 
			this.BwkProcess.WorkerReportsProgress = true;
			this.BwkProcess.WorkerSupportsCancellation = true;
			this.BwkProcess.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BwkProcess_DoWork);
			this.BwkProcess.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BwkProcess_RunWorkerCompleted);
			// 
			// LblFecha
			// 
			this.LblFecha.AutoSize = true;
			this.LblFecha.Location = new System.Drawing.Point(29, 39);
			this.LblFecha.Name = "LblFecha";
			this.LblFecha.Size = new System.Drawing.Size(94, 13);
			this.LblFecha.TabIndex = 11;
			this.LblFecha.Text = "Fecha de Proceso";
			// 
			// DtpProcessDate
			// 
			this.DtpProcessDate.CustomFormat = "yyyy-MM-dd";
			this.DtpProcessDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.DtpProcessDate.Location = new System.Drawing.Point(129, 35);
			this.DtpProcessDate.Name = "DtpProcessDate";
			this.DtpProcessDate.Size = new System.Drawing.Size(94, 20);
			this.DtpProcessDate.TabIndex = 12;
			// 
			// LblUntilDate
			// 
			this.LblUntilDate.AutoSize = true;
			this.LblUntilDate.Location = new System.Drawing.Point(88, 72);
			this.LblUntilDate.Name = "LblUntilDate";
			this.LblUntilDate.Size = new System.Drawing.Size(35, 13);
			this.LblUntilDate.TabIndex = 13;
			this.LblUntilDate.Text = "Hasta";
			this.LblUntilDate.Visible = false;
			// 
			// DtpUntilProcessDate
			// 
			this.DtpUntilProcessDate.CustomFormat = "yyyy-MM-dd";
			this.DtpUntilProcessDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.DtpUntilProcessDate.Location = new System.Drawing.Point(129, 68);
			this.DtpUntilProcessDate.Name = "DtpUntilProcessDate";
			this.DtpUntilProcessDate.Size = new System.Drawing.Size(94, 20);
			this.DtpUntilProcessDate.TabIndex = 14;
			this.DtpUntilProcessDate.Visible = false;
			// 
			// ChkMasiveProcess
			// 
			this.ChkMasiveProcess.AutoSize = true;
			this.ChkMasiveProcess.Location = new System.Drawing.Point(32, 104);
			this.ChkMasiveProcess.Name = "ChkMasiveProcess";
			this.ChkMasiveProcess.Size = new System.Drawing.Size(101, 17);
			this.ChkMasiveProcess.TabIndex = 15;
			this.ChkMasiveProcess.Text = "Proceso masivo";
			this.ChkMasiveProcess.UseVisualStyleBackColor = true;
			this.ChkMasiveProcess.Visible = false;
			// 
			// GpbDatos
			// 
			this.GpbDatos.Controls.Add(this.TspKsAction);
			this.GpbDatos.Controls.Add(this.ChkMasiveProcess);
			this.GpbDatos.Controls.Add(this.DtpUntilProcessDate);
			this.GpbDatos.Controls.Add(this.LblUntilDate);
			this.GpbDatos.Controls.Add(this.DtpProcessDate);
			this.GpbDatos.Controls.Add(this.LblFecha);
			this.GpbDatos.Location = new System.Drawing.Point(339, 12);
			this.GpbDatos.Name = "GpbDatos";
			this.GpbDatos.Size = new System.Drawing.Size(254, 167);
			this.GpbDatos.TabIndex = 12;
			this.GpbDatos.TabStop = false;
			this.GpbDatos.Text = "Parametros del proceso";
			// 
			// TspKsAction
			// 
			this.TspKsAction.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.TspKsAction.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.TspKsAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbDownload});
			this.TspKsAction.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.TspKsAction.Location = new System.Drawing.Point(3, 139);
			this.TspKsAction.Name = "TspKsAction";
			this.TspKsAction.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.TspKsAction.Size = new System.Drawing.Size(248, 25);
			this.TspKsAction.Stretch = true;
			this.TspKsAction.TabIndex = 16;
			this.TspKsAction.Text = "Acciones en KometSales";
			// 
			// TsbDownload
			// 
			this.TsbDownload.Image = ((System.Drawing.Image)(resources.GetObject("TsbDownload.Image")));
			this.TsbDownload.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.TsbDownload.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TsbDownload.Name = "TsbDownload";
			this.TsbDownload.Size = new System.Drawing.Size(79, 22);
			this.TsbDownload.Text = "Descargar";
			this.TsbDownload.ToolTipText = "Descargar la información desde KometSales";
			this.TsbDownload.Click += new System.EventHandler(this.TsbDownload_Click);
			// 
			// Downloads
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(608, 212);
			this.Controls.Add(this.GpbDatos);
			this.Controls.Add(this.SsMessages);
			this.Controls.Add(this.GpbAcciones);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Downloads";
			this.Text = "Descarga de información desde Kometsales";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.Downloads_Load);
			this.GpbAcciones.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.SsMessages.ResumeLayout(false);
			this.SsMessages.PerformLayout();
			this.GpbDatos.ResumeLayout(false);
			this.GpbDatos.PerformLayout();
			this.TspKsAction.ResumeLayout(false);
			this.TspKsAction.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox GpbAcciones;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.StatusStrip SsMessages;
		private System.Windows.Forms.ToolStripStatusLabel LblMessages;
		private System.Windows.Forms.ToolStripProgressBar PnlProgress;
		private System.ComponentModel.BackgroundWorker BwkProcess;
		private System.Windows.Forms.Label LblUntilDate;
		private System.Windows.Forms.DateTimePicker DtpUntilProcessDate;
		private System.Windows.Forms.CheckBox ChkMasiveProcess;
		private System.Windows.Forms.GroupBox GpbDatos;
		private System.Windows.Forms.DateTimePicker DtpProcessDate;
		private System.Windows.Forms.Label LblFecha;
		private System.Windows.Forms.TreeView TvwKsElements;
		private System.Windows.Forms.ToolStripButton TsbDownload;
		private System.Windows.Forms.ToolStrip TspKsAction;
	}
}