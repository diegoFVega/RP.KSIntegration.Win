using System.ComponentModel;
using System.Windows.Forms;

namespace UIX
{
	partial class FrmIntegrador
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private IContainer components = null;

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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIntegrador));
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Tipos de cajas");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Clientes");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Vendedor");
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Productos");
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Bodegas");
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Todas las ordenes facturadas");
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Ordenes locales facturadas");
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Ordenes de compra");
			System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Ordenes fijas activas");
			System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Ordenes de clientes", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9});
			System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Desde KometSales", -2, -2, new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode10});
			System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Producción");
			System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Desde Primasoft", new System.Windows.Forms.TreeNode[] {
            treeNode12});
			System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Inventario");
			System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Homologar códigos de caja");
			System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Hacia KometSales", -2, -2, new System.Windows.Forms.TreeNode[] {
            treeNode14,
            treeNode15});
			System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Arbol de acciones", new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode13,
            treeNode16});
			this.CmsKsActions = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.TmiDownload = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.TmiUpload = new System.Windows.Forms.ToolStripMenuItem();
			this.lblFechaProceso = new System.Windows.Forms.Label();
			this.GpbDatos = new System.Windows.Forms.GroupBox();
			this.DgDataExplorer = new System.Windows.Forms.DataGrid();
			this.TspDataAction = new System.Windows.Forms.ToolStrip();
			this.TsbJoinInformation = new System.Windows.Forms.ToolStripButton();
			this.TsbSave = new System.Windows.Forms.ToolStripButton();
			this.TsbKSNotify = new System.Windows.Forms.ToolStripDropDownButton();
			this.invoicesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.notificarTransferenciaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.actualizarReferenciaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripSplitButton();
			this.normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.futureSalesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			this.descargaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.invoicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cargaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.invoicesToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.PnlProcess = new System.Windows.Forms.Panel();
			this.LblProcess = new System.Windows.Forms.Label();
			this.PbrProcess = new System.Windows.Forms.ProgressBar();
			this.IlsSteps = new System.Windows.Forms.ImageList(this.components);
			this.DtpProcessDate = new System.Windows.Forms.DateTimePicker();
			this.BwkProcess = new System.ComponentModel.BackgroundWorker();
			this.GpbAcciones = new System.Windows.Forms.GroupBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.TvwKsElements = new System.Windows.Forms.TreeView();
			this.TspKsAction = new System.Windows.Forms.ToolStrip();
			this.TsbDownload = new System.Windows.Forms.ToolStripButton();
			this.TsbUpload = new System.Windows.Forms.ToolStripButton();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.CmsKsActions.SuspendLayout();
			this.GpbDatos.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DgDataExplorer)).BeginInit();
			this.TspDataAction.SuspendLayout();
			this.PnlProcess.SuspendLayout();
			this.GpbAcciones.SuspendLayout();
			this.panel1.SuspendLayout();
			this.TspKsAction.SuspendLayout();
			this.SuspendLayout();
			// 
			// CmsKsActions
			// 
			this.CmsKsActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TmiDownload,
            this.toolStripSeparator1,
            this.TmiUpload});
			this.CmsKsActions.Name = "CmsKsActions";
			this.CmsKsActions.Size = new System.Drawing.Size(293, 54);
			// 
			// TmiDownload
			// 
			this.TmiDownload.Name = "TmiDownload";
			this.TmiDownload.Size = new System.Drawing.Size(292, 22);
			this.TmiDownload.Text = "Descargar información desde KometSales";
			this.TmiDownload.Click += new System.EventHandler(this.TmiDownload_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(289, 6);
			// 
			// TmiUpload
			// 
			this.TmiUpload.Name = "TmiUpload";
			this.TmiUpload.Size = new System.Drawing.Size(292, 22);
			this.TmiUpload.Text = "Cargar información hacia KometSales";
			// 
			// lblFechaProceso
			// 
			this.lblFechaProceso.Location = new System.Drawing.Point(0, 0);
			this.lblFechaProceso.Name = "lblFechaProceso";
			this.lblFechaProceso.Size = new System.Drawing.Size(102, 13);
			this.lblFechaProceso.TabIndex = 0;
			this.lblFechaProceso.Text = "Fecha de descarga ";
			// 
			// GpbDatos
			// 
			this.GpbDatos.Controls.Add(this.DgDataExplorer);
			this.GpbDatos.Controls.Add(this.TspDataAction);
			this.GpbDatos.Location = new System.Drawing.Point(215, 37);
			this.GpbDatos.Name = "GpbDatos";
			this.GpbDatos.Size = new System.Drawing.Size(677, 418);
			this.GpbDatos.TabIndex = 3;
			this.GpbDatos.TabStop = false;
			this.GpbDatos.Text = "Datos del sistema";
			// 
			// DgDataExplorer
			// 
			this.DgDataExplorer.BackgroundColor = System.Drawing.SystemColors.Control;
			this.DgDataExplorer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.DgDataExplorer.DataMember = "";
			this.DgDataExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DgDataExplorer.FlatMode = true;
			this.DgDataExplorer.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.DgDataExplorer.Location = new System.Drawing.Point(3, 41);
			this.DgDataExplorer.Margin = new System.Windows.Forms.Padding(1);
			this.DgDataExplorer.Name = "DgDataExplorer";
			this.DgDataExplorer.RowHeaderWidth = 40;
			this.DgDataExplorer.Size = new System.Drawing.Size(671, 374);
			this.DgDataExplorer.TabIndex = 7;
			// 
			// TspDataAction
			// 
			this.TspDataAction.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.TspDataAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbJoinInformation,
            this.TsbSave,
            this.TsbKSNotify,
            this.toolStripButton1,
            this.toolStripDropDownButton1});
			this.TspDataAction.Location = new System.Drawing.Point(3, 16);
			this.TspDataAction.Name = "TspDataAction";
			this.TspDataAction.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.TspDataAction.Size = new System.Drawing.Size(671, 25);
			this.TspDataAction.TabIndex = 0;
			this.TspDataAction.Text = "toolStrip1";
			// 
			// TsbJoinInformation
			// 
			this.TsbJoinInformation.Image = ((System.Drawing.Image)(resources.GetObject("TsbJoinInformation.Image")));
			this.TsbJoinInformation.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.TsbJoinInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TsbJoinInformation.Name = "TsbJoinInformation";
			this.TsbJoinInformation.Size = new System.Drawing.Size(68, 22);
			this.TsbJoinInformation.Text = "Integrar";
			this.TsbJoinInformation.Click += new System.EventHandler(this.TsbJoinInformation_Click);
			// 
			// TsbSave
			// 
			this.TsbSave.Image = ((System.Drawing.Image)(resources.GetObject("TsbSave.Image")));
			this.TsbSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.TsbSave.ImageTransparentColor = System.Drawing.Color.MediumAquamarine;
			this.TsbSave.Name = "TsbSave";
			this.TsbSave.Size = new System.Drawing.Size(72, 22);
			this.TsbSave.Text = "Guardar ";
			this.TsbSave.ToolTipText = "Guardar en base de datos";
			this.TsbSave.Visible = false;
			this.TsbSave.Click += new System.EventHandler(this.TsbSave_Click);
			// 
			// TsbKSNotify
			// 
			this.TsbKSNotify.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.invoicesToolStripMenuItem1});
			this.TsbKSNotify.Image = ((System.Drawing.Image)(resources.GetObject("TsbKSNotify.Image")));
			this.TsbKSNotify.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TsbKSNotify.Name = "TsbKSNotify";
			this.TsbKSNotify.Size = new System.Drawing.Size(155, 22);
			this.TsbKSNotify.Text = "Notificar a KometSales";
			// 
			// invoicesToolStripMenuItem1
			// 
			this.invoicesToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.notificarTransferenciaToolStripMenuItem,
            this.actualizarReferenciaToolStripMenuItem});
			this.invoicesToolStripMenuItem1.Name = "invoicesToolStripMenuItem1";
			this.invoicesToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
			this.invoicesToolStripMenuItem1.Text = "Invoices";
			// 
			// notificarTransferenciaToolStripMenuItem
			// 
			this.notificarTransferenciaToolStripMenuItem.Name = "notificarTransferenciaToolStripMenuItem";
			this.notificarTransferenciaToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			this.notificarTransferenciaToolStripMenuItem.Text = "Notificar transferencia";
			this.notificarTransferenciaToolStripMenuItem.Click += new System.EventHandler(this.notificarTransferenciaToolStripMenuItem_Click);
			// 
			// actualizarReferenciaToolStripMenuItem
			// 
			this.actualizarReferenciaToolStripMenuItem.Name = "actualizarReferenciaToolStripMenuItem";
			this.actualizarReferenciaToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			this.actualizarReferenciaToolStripMenuItem.Text = "Actualizar referencia";
			this.actualizarReferenciaToolStripMenuItem.Click += new System.EventHandler(this.actualizarReferenciaToolStripMenuItem_Click);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalToolStripMenuItem,
            this.futureSalesToolStripMenuItem});
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(174, 22);
			this.toolStripButton1.Text = "Reemplazar codigos de cajas";
			this.toolStripButton1.Visible = false;
			// 
			// normalToolStripMenuItem
			// 
			this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
			this.normalToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			this.normalToolStripMenuItem.Text = "Normal";
			this.normalToolStripMenuItem.Click += new System.EventHandler(this.normalToolStripMenuItem_Click);
			// 
			// futureSalesToolStripMenuItem
			// 
			this.futureSalesToolStripMenuItem.Name = "futureSalesToolStripMenuItem";
			this.futureSalesToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			this.futureSalesToolStripMenuItem.Text = "Future Sales";
			this.futureSalesToolStripMenuItem.Click += new System.EventHandler(this.futureSalesToolStripMenuItem_Click);
			// 
			// toolStripDropDownButton1
			// 
			this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.descargaToolStripMenuItem,
            this.cargaToolStripMenuItem});
			this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
			this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new System.Drawing.Size(88, 22);
			this.toolStripDropDownButton1.Text = "COM Test";
			// 
			// descargaToolStripMenuItem
			// 
			this.descargaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.invoicesToolStripMenuItem});
			this.descargaToolStripMenuItem.Name = "descargaToolStripMenuItem";
			this.descargaToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.descargaToolStripMenuItem.Text = "Descarga";
			// 
			// invoicesToolStripMenuItem
			// 
			this.invoicesToolStripMenuItem.Name = "invoicesToolStripMenuItem";
			this.invoicesToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.invoicesToolStripMenuItem.Text = "Invoices";
			this.invoicesToolStripMenuItem.Click += new System.EventHandler(this.invoicesToolStripMenuItem_Click);
			// 
			// cargaToolStripMenuItem
			// 
			this.cargaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.invoicesToolStripMenuItem2});
			this.cargaToolStripMenuItem.Name = "cargaToolStripMenuItem";
			this.cargaToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.cargaToolStripMenuItem.Text = "Carga";
			// 
			// invoicesToolStripMenuItem2
			// 
			this.invoicesToolStripMenuItem2.Name = "invoicesToolStripMenuItem2";
			this.invoicesToolStripMenuItem2.Size = new System.Drawing.Size(117, 22);
			this.invoicesToolStripMenuItem2.Text = "invoices";
			this.invoicesToolStripMenuItem2.Click += new System.EventHandler(this.invoicesToolStripMenuItem2_Click);
			// 
			// PnlProcess
			// 
			this.PnlProcess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PnlProcess.Controls.Add(this.LblProcess);
			this.PnlProcess.Controls.Add(this.PbrProcess);
			this.PnlProcess.Location = new System.Drawing.Point(400, 200);
			this.PnlProcess.Name = "PnlProcess";
			this.PnlProcess.Padding = new System.Windows.Forms.Padding(3);
			this.PnlProcess.Size = new System.Drawing.Size(200, 69);
			this.PnlProcess.TabIndex = 9;
			this.PnlProcess.Tag = "unavaliable";
			this.PnlProcess.Visible = false;
			// 
			// LblProcess
			// 
			this.LblProcess.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblProcess.Location = new System.Drawing.Point(3, 3);
			this.LblProcess.Name = "LblProcess";
			this.LblProcess.Size = new System.Drawing.Size(192, 45);
			this.LblProcess.TabIndex = 10;
			this.LblProcess.Tag = "unavaliable";
			this.LblProcess.Text = "Procesando";
			// 
			// PbrProcess
			// 
			this.PbrProcess.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.PbrProcess.Location = new System.Drawing.Point(3, 48);
			this.PbrProcess.Name = "PbrProcess";
			this.PbrProcess.Size = new System.Drawing.Size(192, 16);
			this.PbrProcess.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.PbrProcess.TabIndex = 9;
			this.PbrProcess.Tag = "unavaliable";
			// 
			// IlsSteps
			// 
			this.IlsSteps.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.IlsSteps.ImageSize = new System.Drawing.Size(16, 16);
			this.IlsSteps.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// DtpProcessDate
			// 
			this.DtpProcessDate.CustomFormat = "yyyy-MM-dd";
			this.DtpProcessDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.DtpProcessDate.Location = new System.Drawing.Point(120, 8);
			this.DtpProcessDate.Name = "DtpProcessDate";
			this.DtpProcessDate.Size = new System.Drawing.Size(105, 20);
			this.DtpProcessDate.TabIndex = 6;
			// 
			// BwkProcess
			// 
			this.BwkProcess.WorkerReportsProgress = true;
			this.BwkProcess.WorkerSupportsCancellation = true;
			this.BwkProcess.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BwkProcess_DoWork);
			this.BwkProcess.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BwkProcess_RunWorkerCompleted);
			// 
			// GpbAcciones
			// 
			this.GpbAcciones.Controls.Add(this.panel1);
			this.GpbAcciones.Controls.Add(this.TspKsAction);
			this.GpbAcciones.Location = new System.Drawing.Point(13, 37);
			this.GpbAcciones.Name = "GpbAcciones";
			this.GpbAcciones.Size = new System.Drawing.Size(196, 418);
			this.GpbAcciones.TabIndex = 7;
			this.GpbAcciones.TabStop = false;
			this.GpbAcciones.Text = "Acciones iniciales";
			this.GpbAcciones.Enter += new System.EventHandler(this.GpbAcciones_Enter);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.TvwKsElements);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 41);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(190, 374);
			this.panel1.TabIndex = 6;
			// 
			// TvwKsElements
			// 
			this.TvwKsElements.CheckBoxes = true;
			this.TvwKsElements.ContextMenuStrip = this.CmsKsActions;
			this.TvwKsElements.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TvwKsElements.Location = new System.Drawing.Point(0, 0);
			this.TvwKsElements.Name = "TvwKsElements";
			treeNode1.ContextMenuStrip = this.CmsKsActions;
			treeNode1.Name = "KsBoxType";
			treeNode1.SelectedImageIndex = 0;
			treeNode1.Tag = "download";
			treeNode1.Text = "Tipos de cajas";
			treeNode2.Name = "KsCustomer";
			treeNode2.Tag = "download";
			treeNode2.Text = "Clientes";
			treeNode3.Name = "KsVendor";
			treeNode3.Tag = "download";
			treeNode3.Text = "Vendedor";
			treeNode4.Name = "KsProduct";
			treeNode4.Tag = "download";
			treeNode4.Text = "Productos";
			treeNode5.Name = "KsLocation";
			treeNode5.Tag = "download";
			treeNode5.Text = "Bodegas";
			treeNode6.Name = "KsAllOrders";
			treeNode6.Text = "Todas las ordenes facturadas";
			treeNode7.Name = "KsInvoice";
			treeNode7.Tag = "download";
			treeNode7.Text = "Ordenes locales facturadas";
			treeNode8.Name = "KsPurchase";
			treeNode8.Text = "Ordenes de compra";
			treeNode9.ContextMenuStrip = this.CmsKsActions;
			treeNode9.Name = "KsStanding";
			treeNode9.Text = "Ordenes fijas activas";
			treeNode10.Name = "KsOrder";
			treeNode10.Text = "Ordenes de clientes";
			treeNode11.ContextMenuStrip = this.CmsKsActions;
			treeNode11.ImageIndex = -2;
			treeNode11.Name = "KsFromKometSales";
			treeNode11.SelectedImageIndex = -2;
			treeNode11.Tag = "download";
			treeNode11.Text = "Desde KometSales";
			treeNode12.Name = "PSProduction";
			treeNode12.Text = "Producción";
			treeNode13.ContextMenuStrip = this.CmsKsActions;
			treeNode13.Name = "PsFromPrimasoft";
			treeNode13.Text = "Desde Primasoft";
			treeNode14.Name = "KsInventory";
			treeNode14.Tag = "upload";
			treeNode14.Text = "Inventario";
			treeNode15.Name = "KsHomologue";
			treeNode15.Tag = "upload";
			treeNode15.Text = "Homologar códigos de caja";
			treeNode16.ContextMenuStrip = this.CmsKsActions;
			treeNode16.ImageIndex = -2;
			treeNode16.Name = "KsToKometSales";
			treeNode16.SelectedImageIndex = -2;
			treeNode16.Tag = "upload";
			treeNode16.Text = "Hacia KometSales";
			treeNode17.Name = "KsActions";
			treeNode17.Text = "Arbol de acciones";
			this.TvwKsElements.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode17});
			this.TvwKsElements.Scrollable = false;
			this.TvwKsElements.SelectedImageKey = "empty.gif";
			this.TvwKsElements.Size = new System.Drawing.Size(190, 374);
			this.TvwKsElements.StateImageList = this.IlsSteps;
			this.TvwKsElements.TabIndex = 5;
			this.TvwKsElements.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TvwKsElements_AfterCheck);
			this.TvwKsElements.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvwKsElements_AfterSelect);
			// 
			// TspKsAction
			// 
			this.TspKsAction.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.TspKsAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbDownload,
            this.TsbUpload});
			this.TspKsAction.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.TspKsAction.Location = new System.Drawing.Point(3, 16);
			this.TspKsAction.Name = "TspKsAction";
			this.TspKsAction.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.TspKsAction.Size = new System.Drawing.Size(190, 25);
			this.TspKsAction.Stretch = true;
			this.TspKsAction.TabIndex = 5;
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
			// TsbUpload
			// 
			this.TsbUpload.Image = ((System.Drawing.Image)(resources.GetObject("TsbUpload.Image")));
			this.TsbUpload.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.TsbUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TsbUpload.Name = "TsbUpload";
			this.TsbUpload.Size = new System.Drawing.Size(62, 22);
			this.TsbUpload.Text = "Cargar";
			this.TsbUpload.ToolTipText = "Cargar Información a KometSales";
			this.TsbUpload.Visible = false;
			this.TsbUpload.Click += new System.EventHandler(this.TsbUpload_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(530, 9);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 10;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(637, 11);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(100, 20);
			this.textBox1.TabIndex = 11;
			this.textBox1.Visible = false;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(753, 12);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(100, 20);
			this.textBox2.TabIndex = 12;
			this.textBox2.Visible = false;
			// 
			// FrmIntegrador
			// 
			this.ClientSize = new System.Drawing.Size(905, 465);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.PnlProcess);
			this.Controls.Add(this.GpbAcciones);
			this.Controls.Add(this.DtpProcessDate);
			this.Controls.Add(this.GpbDatos);
			this.Controls.Add(this.lblFechaProceso);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmIntegrador";
			this.Text = "Integrador KometSales-Primasoft";
			this.Load += new System.EventHandler(this.FrmIntegrador_Load);
			this.CmsKsActions.ResumeLayout(false);
			this.GpbDatos.ResumeLayout(false);
			this.GpbDatos.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.DgDataExplorer)).EndInit();
			this.TspDataAction.ResumeLayout(false);
			this.TspDataAction.PerformLayout();
			this.PnlProcess.ResumeLayout(false);
			this.GpbAcciones.ResumeLayout(false);
			this.GpbAcciones.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.TspKsAction.ResumeLayout(false);
			this.TspKsAction.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Label lblFechaProceso;
		private GroupBox GpbDatos;
		private ImageList IlsSteps;
		private ToolStrip TspDataAction;
		private ContextMenuStrip CmsKsActions;
		private ToolStripMenuItem TmiDownload;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripMenuItem TmiUpload;
		private DateTimePicker DtpProcessDate;
		private ToolStripButton TsbSave;
		private ToolStripButton TsbJoinInformation;
		private DataGrid DgDataExplorer;
		private BackgroundWorker BwkProcess;
		private Panel PnlProcess;
		private Label LblProcess;
		private ProgressBar PbrProcess;
		private GroupBox GpbAcciones;
		private Panel panel1;
		private TreeView TvwKsElements;
		private ToolStrip TspKsAction;
		private ToolStripButton TsbDownload;
		private ToolStripButton TsbUpload;
		private Button button1;
		private TextBox textBox1;
		private TextBox textBox2;
		private ToolStripSplitButton toolStripButton1;
		private ToolStripMenuItem normalToolStripMenuItem;
		private ToolStripMenuItem futureSalesToolStripMenuItem;
		private ToolStripDropDownButton toolStripDropDownButton1;
		private ToolStripMenuItem descargaToolStripMenuItem;
		private ToolStripMenuItem invoicesToolStripMenuItem;
		private ToolStripDropDownButton TsbKSNotify;
		private ToolStripMenuItem invoicesToolStripMenuItem1;
		private ToolStripMenuItem notificarTransferenciaToolStripMenuItem;
		private ToolStripMenuItem actualizarReferenciaToolStripMenuItem;
		private ToolStripMenuItem cargaToolStripMenuItem;
		private ToolStripMenuItem invoicesToolStripMenuItem2;
	}
}