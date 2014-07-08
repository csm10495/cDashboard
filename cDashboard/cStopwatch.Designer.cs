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
            this.label_time = new System.Windows.Forms.Label();
            this.sw_timer = new System.Windows.Forms.Timer(this.components);
            this.label_started_time = new System.Windows.Forms.Label();
            this.sToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(255, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // label_time
            // 
            this.label_time.Font = new System.Drawing.Font("Arial", 33.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_time.ForeColor = System.Drawing.Color.White;
            this.label_time.Location = new System.Drawing.Point(-3, 21);
            this.label_time.Name = "label_time";
            this.label_time.Size = new System.Drawing.Size(264, 53);
            this.label_time.TabIndex = 2;
            this.label_time.Text = "00:00:00.00";
            this.label_time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sw_timer
            // 
            this.sw_timer.Interval = 10;
            this.sw_timer.Tick += new System.EventHandler(this.sw_timer_Tick);
            // 
            // label_started_time
            // 
            this.label_started_time.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_started_time.ForeColor = System.Drawing.Color.White;
            this.label_started_time.Location = new System.Drawing.Point(-1, 75);
            this.label_started_time.Name = "label_started_time";
            this.label_started_time.Size = new System.Drawing.Size(256, 20);
            this.label_started_time.TabIndex = 3;
            this.label_started_time.Text = "Click the Triangle then \"(Re)Start\" to Start";
            this.label_started_time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sToolStripMenuItem
            // 
            this.sToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.sToolStripMenuItem.Name = "sToolStripMenuItem";
            this.sToolStripMenuItem.Size = new System.Drawing.Size(26, 20);
            this.sToolStripMenuItem.Text = "S";
            this.sToolStripMenuItem.Click += new System.EventHandler(this.sToolStripMenuItem_Click);
            // 
            // cStopwatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(255, 97);
            this.Controls.Add(this.label_started_time);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label_time);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "cStopwatch";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "cStopwatch";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.Load += new System.EventHandler(this.cStopwatch_Load);
            this.Move += new System.EventHandler(this.cStopwatch_Move);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label label_time;
        private System.Windows.Forms.Label label_started_time;
        internal System.Windows.Forms.Timer sw_timer;
        private System.Windows.Forms.ToolStripMenuItem sToolStripMenuItem;
    }
}