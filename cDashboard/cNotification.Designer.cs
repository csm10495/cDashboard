namespace cDashboard
{
    partial class cNotification
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cNotification));
            this.picturebox_logo = new System.Windows.Forms.PictureBox();
            this.label_title = new System.Windows.Forms.Label();
            this.label_text = new System.Windows.Forms.Label();
            this.panel_underline = new System.Windows.Forms.Panel();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.button_close = new System.Windows.Forms.Button();
            this.timer_show = new System.Windows.Forms.Timer(this.components);
            this.timer_fade = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picturebox_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // picturebox_logo
            // 
            this.picturebox_logo.Image = ((System.Drawing.Image)(resources.GetObject("picturebox_logo.Image")));
            this.picturebox_logo.Location = new System.Drawing.Point(2, 1);
            this.picturebox_logo.Name = "picturebox_logo";
            this.picturebox_logo.Size = new System.Drawing.Size(148, 147);
            this.picturebox_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picturebox_logo.TabIndex = 0;
            this.picturebox_logo.TabStop = false;
            this.tooltip.SetToolTip(this.picturebox_logo, "cDash Logo");
            // 
            // label_title
            // 
            this.label_title.AutoEllipsis = true;
            this.label_title.BackColor = System.Drawing.Color.Transparent;
            this.label_title.Font = new System.Drawing.Font("Arial Black", 20F, System.Drawing.FontStyle.Bold);
            this.label_title.Location = new System.Drawing.Point(156, -3);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(260, 38);
            this.label_title.TabIndex = 1;
            this.label_title.Text = "cNotification";
            this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tooltip.SetToolTip(this.label_title, "cNotification Title");
            // 
            // label_text
            // 
            this.label_text.AutoEllipsis = true;
            this.label_text.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_text.Location = new System.Drawing.Point(152, 45);
            this.label_text.Name = "label_text";
            this.label_text.Size = new System.Drawing.Size(287, 103);
            this.label_text.TabIndex = 3;
            this.label_text.Text = "cNotification text";
            this.label_text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tooltip.SetToolTip(this.label_text, "cNotification Text");
            // 
            // panel_underline
            // 
            this.panel_underline.BackColor = System.Drawing.Color.Black;
            this.panel_underline.Location = new System.Drawing.Point(156, 32);
            this.panel_underline.Name = "panel_underline";
            this.panel_underline.Size = new System.Drawing.Size(276, 10);
            this.panel_underline.TabIndex = 4;
            // 
            // button_close
            // 
            this.button_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_close.Location = new System.Drawing.Point(416, 1);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(24, 23);
            this.button_close.TabIndex = 5;
            this.button_close.Text = "X";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // timer_show
            // 
            this.timer_show.Interval = 1000;
            this.timer_show.Tick += new System.EventHandler(this.timer_show_Tick);
            // 
            // timer_fade
            // 
            this.timer_fade.Interval = 1;
            this.timer_fade.Tick += new System.EventHandler(this.timer_fade_tick);
            // 
            // cNotification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(442, 151);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.panel_underline);
            this.Controls.Add(this.label_text);
            this.Controls.Add(this.picturebox_logo);
            this.Controls.Add(this.label_title);
            this.Name = "cNotification";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "cNotification";
            this.Load += new System.EventHandler(this.cNotification_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturebox_logo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picturebox_logo;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Label label_text;
        private System.Windows.Forms.Panel panel_underline;
        private System.Windows.Forms.ToolTip tooltip;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.Timer timer_show;
        private System.Windows.Forms.Timer timer_fade;
    }
}