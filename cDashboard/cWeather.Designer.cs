namespace cDashboard
{
    partial class cWeather
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cWeather));
            this.button_set = new System.Windows.Forms.Button();
            this.panel_zip_input = new System.Windows.Forms.Panel();
            this.button_auto_locate = new System.Windows.Forms.Button();
            this.label_instructions = new System.Windows.Forms.Label();
            this.rtb_input = new System.Windows.Forms.RichTextBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_weather_ui = new System.Windows.Forms.Panel();
            this.pictureBox_attrib = new System.Windows.Forms.PictureBox();
            this.label_zip = new System.Windows.Forms.Label();
            this.label_weather = new System.Windows.Forms.Label();
            this.label_pop = new System.Windows.Forms.Label();
            this.label_wind = new System.Windows.Forms.Label();
            this.label_humidity = new System.Windows.Forms.Label();
            this.label_low = new System.Windows.Forms.Label();
            this.label_high = new System.Windows.Forms.Label();
            this.label_current_temp = new System.Windows.Forms.Label();
            this.pictureBox_weather_img = new System.Windows.Forms.PictureBox();
            this.panel_refreshing = new System.Windows.Forms.Panel();
            this.label_refreshing = new System.Windows.Forms.Label();
            this.pictureBox_logo = new System.Windows.Forms.PictureBox();
            this.backgroundWorker_refresh = new System.ComponentModel.BackgroundWorker();
            this.panel_zip_input.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.panel_weather_ui.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_attrib)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_weather_img)).BeginInit();
            this.panel_refreshing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // button_set
            // 
            this.button_set.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_set.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_set.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_set.ForeColor = System.Drawing.Color.White;
            this.button_set.Location = new System.Drawing.Point(8, 86);
            this.button_set.Name = "button_set";
            this.button_set.Size = new System.Drawing.Size(199, 30);
            this.button_set.TabIndex = 1;
            this.button_set.Text = "Set Location";
            this.button_set.UseVisualStyleBackColor = true;
            this.button_set.Click += new System.EventHandler(this.button_set_Click);
            // 
            // panel_zip_input
            // 
            this.panel_zip_input.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.panel_zip_input.Controls.Add(this.button_auto_locate);
            this.panel_zip_input.Controls.Add(this.label_instructions);
            this.panel_zip_input.Controls.Add(this.button_set);
            this.panel_zip_input.Controls.Add(this.rtb_input);
            this.panel_zip_input.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_zip_input.Location = new System.Drawing.Point(0, 24);
            this.panel_zip_input.Name = "panel_zip_input";
            this.panel_zip_input.Size = new System.Drawing.Size(280, 125);
            this.panel_zip_input.TabIndex = 1;
            // 
            // button_auto_locate
            // 
            this.button_auto_locate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_auto_locate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_auto_locate.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Bold);
            this.button_auto_locate.ForeColor = System.Drawing.Color.White;
            this.button_auto_locate.Location = new System.Drawing.Point(213, 86);
            this.button_auto_locate.Name = "button_auto_locate";
            this.button_auto_locate.Size = new System.Drawing.Size(59, 30);
            this.button_auto_locate.TabIndex = 3;
            this.button_auto_locate.Text = "By IP";
            this.button_auto_locate.UseVisualStyleBackColor = true;
            this.button_auto_locate.Click += new System.EventHandler(this.button_auto_locate_Click);
            // 
            // label_instructions
            // 
            this.label_instructions.AutoSize = true;
            this.label_instructions.Font = new System.Drawing.Font("Arial Black", 8F, System.Drawing.FontStyle.Bold);
            this.label_instructions.ForeColor = System.Drawing.Color.White;
            this.label_instructions.Location = new System.Drawing.Point(3, 4);
            this.label_instructions.MaximumSize = new System.Drawing.Size(400, 0);
            this.label_instructions.Name = "label_instructions";
            this.label_instructions.Size = new System.Drawing.Size(275, 15);
            this.label_instructions.TabIndex = 2;
            this.label_instructions.Text = "Input a zip code, city, state, or country, city.";
            // 
            // rtb_input
            // 
            this.rtb_input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_input.BackColor = System.Drawing.Color.Black;
            this.rtb_input.Font = new System.Drawing.Font("Arial Narrow", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_input.ForeColor = System.Drawing.Color.White;
            this.rtb_input.Location = new System.Drawing.Point(8, 29);
            this.rtb_input.Multiline = false;
            this.rtb_input.Name = "rtb_input";
            this.rtb_input.Size = new System.Drawing.Size(264, 51);
            this.rtb_input.TabIndex = 0;
            this.rtb_input.Text = "";
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.menuStrip.Font = new System.Drawing.Font("Arial", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xToolStripMenuItem,
            this.aToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(280, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.MouseDown += new System.Windows.Forms.MouseEventHandler(this.menuStrip_MouseDown);
            // 
            // xToolStripMenuItem
            // 
            this.xToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.xToolStripMenuItem.Name = "xToolStripMenuItem";
            this.xToolStripMenuItem.Size = new System.Drawing.Size(27, 20);
            this.xToolStripMenuItem.Text = "X";
            this.xToolStripMenuItem.Click += new System.EventHandler(this.xToolStripMenuItem_Click);
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeLocationToolStripMenuItem,
            this.refreshDataToolStripMenuItem});
            this.aToolStripMenuItem.Font = new System.Drawing.Font("Wingdings 3", 8.25F);
            this.aToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(28, 20);
            this.aToolStripMenuItem.Text = "p";
            // 
            // changeLocationToolStripMenuItem
            // 
            this.changeLocationToolStripMenuItem.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeLocationToolStripMenuItem.Name = "changeLocationToolStripMenuItem";
            this.changeLocationToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.changeLocationToolStripMenuItem.Text = "Change Location...";
            this.changeLocationToolStripMenuItem.Click += new System.EventHandler(this.changeLocationToolStripMenuItem_Click);
            // 
            // refreshDataToolStripMenuItem
            // 
            this.refreshDataToolStripMenuItem.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshDataToolStripMenuItem.Name = "refreshDataToolStripMenuItem";
            this.refreshDataToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.refreshDataToolStripMenuItem.Text = "Refresh Data";
            this.refreshDataToolStripMenuItem.Click += new System.EventHandler(this.refreshDataToolStripMenuItem_Click);
            // 
            // panel_weather_ui
            // 
            this.panel_weather_ui.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.panel_weather_ui.Controls.Add(this.pictureBox_attrib);
            this.panel_weather_ui.Controls.Add(this.label_zip);
            this.panel_weather_ui.Controls.Add(this.label_weather);
            this.panel_weather_ui.Controls.Add(this.label_pop);
            this.panel_weather_ui.Controls.Add(this.label_wind);
            this.panel_weather_ui.Controls.Add(this.label_humidity);
            this.panel_weather_ui.Controls.Add(this.label_low);
            this.panel_weather_ui.Controls.Add(this.label_high);
            this.panel_weather_ui.Controls.Add(this.label_current_temp);
            this.panel_weather_ui.Controls.Add(this.pictureBox_weather_img);
            this.panel_weather_ui.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_weather_ui.Location = new System.Drawing.Point(0, 24);
            this.panel_weather_ui.Name = "panel_weather_ui";
            this.panel_weather_ui.Size = new System.Drawing.Size(280, 125);
            this.panel_weather_ui.TabIndex = 3;
            this.panel_weather_ui.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_weather_ui_MouseDown);
            // 
            // pictureBox_attrib
            // 
            this.pictureBox_attrib.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_attrib.Image")));
            this.pictureBox_attrib.Location = new System.Drawing.Point(8, 102);
            this.pictureBox_attrib.Name = "pictureBox_attrib";
            this.pictureBox_attrib.Size = new System.Drawing.Size(92, 19);
            this.pictureBox_attrib.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_attrib.TabIndex = 10;
            this.pictureBox_attrib.TabStop = false;
            this.pictureBox_attrib.Click += new System.EventHandler(this.pictureBox_attrib_Click);
            // 
            // label_zip
            // 
            this.label_zip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_zip.AutoEllipsis = true;
            this.label_zip.BackColor = System.Drawing.Color.Transparent;
            this.label_zip.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_zip.ForeColor = System.Drawing.Color.White;
            this.label_zip.Location = new System.Drawing.Point(148, 4);
            this.label_zip.Name = "label_zip";
            this.label_zip.Size = new System.Drawing.Size(131, 23);
            this.label_zip.TabIndex = 0;
            this.label_zip.Text = "11581";
            this.label_zip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_weather
            // 
            this.label_weather.AutoSize = true;
            this.label_weather.Font = new System.Drawing.Font("Arial Narrow", 11F, System.Drawing.FontStyle.Bold);
            this.label_weather.ForeColor = System.Drawing.Color.White;
            this.label_weather.Location = new System.Drawing.Point(4, 60);
            this.label_weather.Name = "label_weather";
            this.label_weather.Size = new System.Drawing.Size(91, 20);
            this.label_weather.TabIndex = 9;
            this.label_weather.Text = "Partly Cloudy";
            this.label_weather.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_pop
            // 
            this.label_pop.AutoSize = true;
            this.label_pop.Font = new System.Drawing.Font("Arial Narrow", 11F, System.Drawing.FontStyle.Bold);
            this.label_pop.ForeColor = System.Drawing.Color.White;
            this.label_pop.Location = new System.Drawing.Point(4, 82);
            this.label_pop.Name = "label_pop";
            this.label_pop.Size = new System.Drawing.Size(136, 20);
            this.label_pop.TabIndex = 8;
            this.label_pop.Text = "000% Chance of Rain";
            this.label_pop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_wind
            // 
            this.label_wind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_wind.BackColor = System.Drawing.Color.Transparent;
            this.label_wind.Font = new System.Drawing.Font("Arial Narrow", 13F, System.Drawing.FontStyle.Bold);
            this.label_wind.ForeColor = System.Drawing.Color.White;
            this.label_wind.Location = new System.Drawing.Point(120, 98);
            this.label_wind.Name = "label_wind";
            this.label_wind.Size = new System.Drawing.Size(160, 23);
            this.label_wind.TabIndex = 7;
            this.label_wind.Text = "Wind: 000 MPH SSE";
            this.label_wind.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_humidity
            // 
            this.label_humidity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_humidity.BackColor = System.Drawing.Color.Transparent;
            this.label_humidity.Font = new System.Drawing.Font("Arial Narrow", 13F, System.Drawing.FontStyle.Bold);
            this.label_humidity.ForeColor = System.Drawing.Color.White;
            this.label_humidity.Location = new System.Drawing.Point(146, 72);
            this.label_humidity.Name = "label_humidity";
            this.label_humidity.Size = new System.Drawing.Size(134, 30);
            this.label_humidity.TabIndex = 6;
            this.label_humidity.Text = "Humidity: ";
            this.label_humidity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_low
            // 
            this.label_low.AutoSize = true;
            this.label_low.Font = new System.Drawing.Font("Arial Narrow", 13F, System.Drawing.FontStyle.Bold);
            this.label_low.ForeColor = System.Drawing.Color.White;
            this.label_low.Location = new System.Drawing.Point(62, 29);
            this.label_low.Name = "label_low";
            this.label_low.Size = new System.Drawing.Size(48, 22);
            this.label_low.TabIndex = 5;
            this.label_low.Text = "Low: ";
            this.label_low.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_high
            // 
            this.label_high.AutoSize = true;
            this.label_high.Font = new System.Drawing.Font("Arial Narrow", 13F, System.Drawing.FontStyle.Bold);
            this.label_high.ForeColor = System.Drawing.Color.White;
            this.label_high.Location = new System.Drawing.Point(62, 7);
            this.label_high.Name = "label_high";
            this.label_high.Size = new System.Drawing.Size(48, 22);
            this.label_high.TabIndex = 4;
            this.label_high.Text = "High:";
            this.label_high.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_current_temp
            // 
            this.label_current_temp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_current_temp.BackColor = System.Drawing.Color.Transparent;
            this.label_current_temp.Font = new System.Drawing.Font("Arial Narrow", 34F, System.Drawing.FontStyle.Bold);
            this.label_current_temp.ForeColor = System.Drawing.Color.White;
            this.label_current_temp.Location = new System.Drawing.Point(154, 25);
            this.label_current_temp.Name = "label_current_temp";
            this.label_current_temp.Size = new System.Drawing.Size(130, 55);
            this.label_current_temp.TabIndex = 3;
            this.label_current_temp.Text = "110°F";
            this.label_current_temp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox_weather_img
            // 
            this.pictureBox_weather_img.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_weather_img.Location = new System.Drawing.Point(6, 5);
            this.pictureBox_weather_img.Name = "pictureBox_weather_img";
            this.pictureBox_weather_img.Size = new System.Drawing.Size(50, 50);
            this.pictureBox_weather_img.TabIndex = 1;
            this.pictureBox_weather_img.TabStop = false;
            // 
            // panel_refreshing
            // 
            this.panel_refreshing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.panel_refreshing.Controls.Add(this.label_refreshing);
            this.panel_refreshing.Controls.Add(this.pictureBox_logo);
            this.panel_refreshing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_refreshing.Location = new System.Drawing.Point(0, 24);
            this.panel_refreshing.Name = "panel_refreshing";
            this.panel_refreshing.Size = new System.Drawing.Size(280, 125);
            this.panel_refreshing.TabIndex = 3;
            // 
            // label_refreshing
            // 
            this.label_refreshing.AutoSize = true;
            this.label_refreshing.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_refreshing.ForeColor = System.Drawing.Color.White;
            this.label_refreshing.Location = new System.Drawing.Point(39, 8);
            this.label_refreshing.Name = "label_refreshing";
            this.label_refreshing.Size = new System.Drawing.Size(201, 31);
            this.label_refreshing.TabIndex = 0;
            this.label_refreshing.Text = "Refreshing Data...";
            // 
            // pictureBox_logo
            // 
            this.pictureBox_logo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_logo.Image")));
            this.pictureBox_logo.Location = new System.Drawing.Point(26, 41);
            this.pictureBox_logo.Name = "pictureBox_logo";
            this.pictureBox_logo.Size = new System.Drawing.Size(227, 71);
            this.pictureBox_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_logo.TabIndex = 1;
            this.pictureBox_logo.TabStop = false;
            // 
            // backgroundWorker_refresh
            // 
            this.backgroundWorker_refresh.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_refresh_DoWork);
            this.backgroundWorker_refresh.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_refresh_RunWorkerCompleted);
            // 
            // cWeather
            // 
            this.AcceptButton = this.button_set;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(280, 149);
            this.Controls.Add(this.panel_zip_input);
            this.Controls.Add(this.panel_refreshing);
            this.Controls.Add(this.panel_weather_ui);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "cWeather";
            this.Text = "cWeather";
            this.Load += new System.EventHandler(this.cWeather_Load);
            this.panel_zip_input.ResumeLayout(false);
            this.panel_zip_input.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panel_weather_ui.ResumeLayout(false);
            this.panel_weather_ui.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_attrib)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_weather_img)).EndInit();
            this.panel_refreshing.ResumeLayout(false);
            this.panel_refreshing.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem;
        private System.Windows.Forms.Panel panel_zip_input;
        private System.Windows.Forms.Label label_instructions;
        private System.Windows.Forms.Button button_set;
        private System.Windows.Forms.RichTextBox rtb_input;
        private System.Windows.Forms.Panel panel_weather_ui;
        private System.Windows.Forms.Label label_zip;
        private System.Windows.Forms.PictureBox pictureBox_weather_img;
        private System.Windows.Forms.Label label_current_temp;
        private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshDataToolStripMenuItem;
        private System.Windows.Forms.Label label_low;
        private System.Windows.Forms.Label label_high;
        private System.Windows.Forms.Label label_humidity;
        private System.Windows.Forms.Label label_wind;
        private System.Windows.Forms.Label label_pop;
        private System.Windows.Forms.Label label_weather;
        private System.Windows.Forms.PictureBox pictureBox_attrib;
        private System.Windows.Forms.Panel panel_refreshing;
        private System.Windows.Forms.Label label_refreshing;
        private System.Windows.Forms.PictureBox pictureBox_logo;
        private System.ComponentModel.BackgroundWorker backgroundWorker_refresh;
        private System.Windows.Forms.Button button_auto_locate;
    }
}