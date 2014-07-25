namespace cDashboard
{
    partial class cStopwatch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be 
        /// 
        /// d; otherwise, false.</param>
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label_time = new System.Windows.Forms.Label();
            this.sw_timer = new System.Windows.Forms.Timer(this.components);
            this.richtextbox_lap = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sToolStripMenuItem,
            this.xToolStripMenuItem,
            this.lToolStripMenuItem,
            this.rToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.ShowItemToolTips = true;
            this.menuStrip1.Size = new System.Drawing.Size(255, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.menuStrip1_MouseDown);
            // 
            // sToolStripMenuItem
            // 
            this.sToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.sToolStripMenuItem.Name = "sToolStripMenuItem";
            this.sToolStripMenuItem.Size = new System.Drawing.Size(25, 20);
            this.sToolStripMenuItem.Text = "S";
            this.sToolStripMenuItem.ToolTipText = "Start/Stop";
            this.sToolStripMenuItem.Click += new System.EventHandler(this.sToolStripMenuItem_Click);
            // 
            // xToolStripMenuItem
            // 
            this.xToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.xToolStripMenuItem.Name = "xToolStripMenuItem";
            this.xToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.xToolStripMenuItem.Size = new System.Drawing.Size(27, 20);
            this.xToolStripMenuItem.Text = "X";
            this.xToolStripMenuItem.ToolTipText = "Close";
            this.xToolStripMenuItem.Click += new System.EventHandler(this.xToolStripMenuItem_Click);
            // 
            // lToolStripMenuItem
            // 
            this.lToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.lToolStripMenuItem.Name = "lToolStripMenuItem";
            this.lToolStripMenuItem.Size = new System.Drawing.Size(25, 20);
            this.lToolStripMenuItem.Text = "L";
            this.lToolStripMenuItem.ToolTipText = "Lap";
            this.lToolStripMenuItem.Click += new System.EventHandler(this.lToolStripMenuItem_Click);
            // 
            // rToolStripMenuItem
            // 
            this.rToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.rToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.rToolStripMenuItem.Name = "rToolStripMenuItem";
            this.rToolStripMenuItem.Size = new System.Drawing.Size(27, 20);
            this.rToolStripMenuItem.Text = "R";
            this.rToolStripMenuItem.ToolTipText = "Restart";
            this.rToolStripMenuItem.Click += new System.EventHandler(this.rToolStripMenuItem_Click);
            // 
            // label_time
            // 
            this.label_time.AutoSize = true;
            this.label_time.Font = new System.Drawing.Font("Arial", 33.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_time.ForeColor = System.Drawing.Color.White;
            this.label_time.Location = new System.Drawing.Point(-3, 21);
            this.label_time.Name = "label_time";
            this.label_time.Size = new System.Drawing.Size(264, 53);
            this.label_time.TabIndex = 2;
            this.label_time.Text = "00:00:00.00";
            this.label_time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_time.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_time_MouseDown);
            // 
            // sw_timer
            // 
            this.sw_timer.Interval = 10;
            this.sw_timer.Tick += new System.EventHandler(this.sw_timer_Tick);
            // 
            // richtextbox_lap
            // 
            this.richtextbox_lap.BackColor = System.Drawing.Color.Black;
            this.richtextbox_lap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richtextbox_lap.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.richtextbox_lap.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold);
            this.richtextbox_lap.ForeColor = System.Drawing.Color.White;
            this.richtextbox_lap.Location = new System.Drawing.Point(0, 71);
            this.richtextbox_lap.MaximumSize = new System.Drawing.Size(0, 112);
            this.richtextbox_lap.MinimumSize = new System.Drawing.Size(255, 0);
            this.richtextbox_lap.Name = "richtextbox_lap";
            this.richtextbox_lap.ReadOnly = true;
            this.richtextbox_lap.Size = new System.Drawing.Size(255, 112);
            this.richtextbox_lap.TabIndex = 4;
            this.richtextbox_lap.Text = "\"S\" to Start/Stop. \"L\" to Lap. \"R\" to Reset";
            this.richtextbox_lap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.richtextbox_lap_MouseDown);
            // 
            // cStopwatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(255, 93);
            this.Controls.Add(this.richtextbox_lap);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label_time);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "cStopwatch";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "cStopwatch";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.Load += new System.EventHandler(this.cStopwatch_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cStopwatch_MouseDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label label_time;
        internal System.Windows.Forms.Timer sw_timer;
        private System.Windows.Forms.ToolStripMenuItem sToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lToolStripMenuItem;
        public System.Windows.Forms.RichTextBox richtextbox_lap;
        private System.Windows.Forms.ToolStripMenuItem rToolStripMenuItem;
    }
}