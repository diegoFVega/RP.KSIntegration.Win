namespace Services
{
#if DEBUG
	partial class ServiceTestForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button start;
		private System.Windows.Forms.Button stop;
		private System.Windows.Forms.Button pause;
		private System.Windows.Forms.TextBox output;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceTestForm));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.start = new System.Windows.Forms.Button();
			this.pause = new System.Windows.Forms.Button();
			this.stop = new System.Windows.Forms.Button();
			this.output = new System.Windows.Forms.TextBox();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.start);
			this.groupBox1.Controls.Add(this.pause);
			this.groupBox1.Controls.Add(this.stop);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(251, 78);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "State";
			// 
			// start
			// 
			this.start.Image = ((System.Drawing.Image)(resources.GetObject("start.Image")));
			this.start.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.start.ImageKey = "(none)";
			this.start.Location = new System.Drawing.Point(6, 19);
			this.start.Name = "start";
			this.start.Size = new System.Drawing.Size(75, 23);
			this.start.TabIndex = 0;
			this.start.Text = "Start";
			this.start.UseVisualStyleBackColor = true;
			this.start.Click += new System.EventHandler(this.start_Click);
			// 
			// pause
			// 
			this.pause.Enabled = false;
			this.pause.Image = ((System.Drawing.Image)(resources.GetObject("pause.Image")));
			this.pause.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.pause.Location = new System.Drawing.Point(168, 19);
			this.pause.Name = "pause";
			this.pause.Size = new System.Drawing.Size(75, 23);
			this.pause.TabIndex = 4;
			this.pause.Text = "Pause";
			this.pause.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.pause.UseVisualStyleBackColor = true;
			this.pause.Click += new System.EventHandler(this.pause_Click);
			// 
			// stop
			// 
			this.stop.Enabled = false;
			this.stop.Image = ((System.Drawing.Image)(resources.GetObject("stop.Image")));
			this.stop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.stop.Location = new System.Drawing.Point(87, 19);
			this.stop.Name = "stop";
			this.stop.Size = new System.Drawing.Size(75, 23);
			this.stop.TabIndex = 1;
			this.stop.Text = "Stop";
			this.stop.UseVisualStyleBackColor = true;
			this.stop.Click += new System.EventHandler(this.stop_Click);
			// 
			// output
			// 
			this.output.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.output.Location = new System.Drawing.Point(12, 120);
			this.output.Multiline = true;
			this.output.Name = "output";
			this.output.Size = new System.Drawing.Size(251, 64);
			this.output.TabIndex = 6;
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "pause.png");
			this.imageList1.Images.SetKeyName(1, "play.png");
			this.imageList1.Images.SetKeyName(2, "stop.png");
			// 
			// ServiceTestForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(279, 196);
			this.Controls.Add(this.output);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "ServiceTestForm";
			this.Text = "Service Test Form";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private System.Windows.Forms.ImageList imageList1;
	}
#endif
}