﻿namespace cDashboard
{
    partial class cAbout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cAbout));
            this.picture_icon = new System.Windows.Forms.PictureBox();
            this.label_project_name = new System.Windows.Forms.Label();
            this.label_details = new System.Windows.Forms.Label();
            this.button_close = new System.Windows.Forms.Button();
            this.Line = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_weather_data = new System.Windows.Forms.LinkLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.timer_animation = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picture_icon)).BeginInit();
            this.SuspendLayout();
            // 
            // picture_icon
            // 
            this.picture_icon.Image = ((System.Drawing.Image)(resources.GetObject("picture_icon.Image")));
            this.picture_icon.Location = new System.Drawing.Point(12, 12);
            this.picture_icon.Name = "picture_icon";
            this.picture_icon.Size = new System.Drawing.Size(200, 200);
            this.picture_icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture_icon.TabIndex = 0;
            this.picture_icon.TabStop = false;
            // 
            // label_project_name
            // 
            this.label_project_name.AutoSize = true;
            this.label_project_name.Font = new System.Drawing.Font("Arial Black", 25.75F, System.Drawing.FontStyle.Bold);
            this.label_project_name.Location = new System.Drawing.Point(214, -1);
            this.label_project_name.Name = "label_project_name";
            this.label_project_name.Size = new System.Drawing.Size(248, 50);
            this.label_project_name.TabIndex = 1;
            this.label_project_name.Text = "cDashboard";
            // 
            // label_details
            // 
            this.label_details.AutoSize = true;
            this.label_details.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Bold);
            this.label_details.Location = new System.Drawing.Point(231, 49);
            this.label_details.MaximumSize = new System.Drawing.Size(225, 0);
            this.label_details.Name = "label_details";
            this.label_details.Size = new System.Drawing.Size(218, 90);
            this.label_details.TabIndex = 2;
            this.label_details.Text = "An overlay layer for \r\nMicrosoft Windows\r\nMIT License\r\nWritten by Charles Machalo" +
    "w\r\n(csm10495)";
            this.label_details.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_close
            // 
            this.button_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_close.Font = new System.Drawing.Font("Arial Black", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_close.Location = new System.Drawing.Point(223, 163);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(226, 49);
            this.button_close.TabIndex = 3;
            this.button_close.Text = "Okay";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // Line
            // 
            this.Line.BackColor = System.Drawing.Color.Black;
            this.Line.Location = new System.Drawing.Point(223, 84);
            this.Line.Name = "Line";
            this.Line.Size = new System.Drawing.Size(226, 2);
            this.Line.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(223, 102);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(226, 2);
            this.panel1.TabIndex = 5;
            // 
            // label_weather_data
            // 
            this.label_weather_data.AutoSize = true;
            this.label_weather_data.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_weather_data.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.label_weather_data.Location = new System.Drawing.Point(254, 144);
            this.label_weather_data.Name = "label_weather_data";
            this.label_weather_data.Size = new System.Drawing.Size(169, 16);
            this.label_weather_data.TabIndex = 6;
            this.label_weather_data.TabStop = true;
            this.label_weather_data.Text = "Weather Data from Yahoo! Weather";
            this.label_weather_data.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.label_weather_data_LinkClicked);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(223, 142);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(226, 2);
            this.panel2.TabIndex = 5;
            // 
            // timer_animation
            // 
            this.timer_animation.Interval = 1;
            this.timer_animation.Tick += new System.EventHandler(this.timer_animation_Tick);
            // 
            // cAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 221);
            this.Controls.Add(this.picture_icon);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label_weather_data);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Line);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.label_details);
            this.Controls.Add(this.label_project_name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "cAbout";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "cAbout cDashboard";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.cAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picture_icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picture_icon;
        private System.Windows.Forms.Label label_project_name;
        private System.Windows.Forms.Label label_details;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.Panel Line;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel label_weather_data;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Timer timer_animation;

    }
}
