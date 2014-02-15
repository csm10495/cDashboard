namespace cDashboard
{


    partial class cDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cDashboard));
            this.maintimer = new System.Windows.Forms.Timer(this.components);
            this.label_build = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newStickyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yellowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indigoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.violetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.favoriteColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setCDashBackColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setCDashDefaultMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setOpacityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textbox_opacity = new System.Windows.Forms.ToolStripTextBox();
            this.setFavoriteStickyColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setFavoriteFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyicon_menustrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitCDashboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_time = new System.Windows.Forms.Button();
            this.button_date = new System.Windows.Forms.Button();
            this.setFadeTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setInMillisecondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textbox_fadetime = new System.Windows.Forms.ToolStripTextBox();
            this.menuStrip1.SuspendLayout();
            this.notifyicon_menustrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // maintimer
            // 
            this.maintimer.Interval = 10;
            this.maintimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label_build
            // 
            this.label_build.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_build.AutoSize = true;
            this.label_build.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_build.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label_build.Location = new System.Drawing.Point(955, 9);
            this.label_build.Name = "label_build";
            this.label_build.Size = new System.Drawing.Size(0, 27);
            this.label_build.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.menuStrip1.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1264, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newStickyToolStripMenuItem,
            this.hideToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newStickyToolStripMenuItem
            // 
            this.newStickyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.redToolStripMenuItem,
            this.orangeToolStripMenuItem,
            this.yellowToolStripMenuItem,
            this.greenToolStripMenuItem,
            this.blueToolStripMenuItem,
            this.indigoToolStripMenuItem,
            this.violetToolStripMenuItem,
            this.customColorToolStripMenuItem,
            this.favoriteColorToolStripMenuItem});
            this.newStickyToolStripMenuItem.Name = "newStickyToolStripMenuItem";
            this.newStickyToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.newStickyToolStripMenuItem.Text = "New Sticky";
            // 
            // redToolStripMenuItem
            // 
            this.redToolStripMenuItem.Name = "redToolStripMenuItem";
            this.redToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.redToolStripMenuItem.Text = "Red";
            this.redToolStripMenuItem.Click += new System.EventHandler(this.redToolStripMenuItem_Click);
            // 
            // orangeToolStripMenuItem
            // 
            this.orangeToolStripMenuItem.Name = "orangeToolStripMenuItem";
            this.orangeToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.orangeToolStripMenuItem.Text = "Orange";
            this.orangeToolStripMenuItem.Click += new System.EventHandler(this.orangeToolStripMenuItem_Click);
            // 
            // yellowToolStripMenuItem
            // 
            this.yellowToolStripMenuItem.Name = "yellowToolStripMenuItem";
            this.yellowToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.yellowToolStripMenuItem.Text = "Yellow";
            this.yellowToolStripMenuItem.Click += new System.EventHandler(this.yellowToolStripMenuItem_Click);
            // 
            // greenToolStripMenuItem
            // 
            this.greenToolStripMenuItem.Name = "greenToolStripMenuItem";
            this.greenToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.greenToolStripMenuItem.Text = "Green";
            this.greenToolStripMenuItem.Click += new System.EventHandler(this.greenToolStripMenuItem_Click);
            // 
            // blueToolStripMenuItem
            // 
            this.blueToolStripMenuItem.Name = "blueToolStripMenuItem";
            this.blueToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.blueToolStripMenuItem.Text = "Blue";
            this.blueToolStripMenuItem.Click += new System.EventHandler(this.blueToolStripMenuItem_Click);
            // 
            // indigoToolStripMenuItem
            // 
            this.indigoToolStripMenuItem.Name = "indigoToolStripMenuItem";
            this.indigoToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.indigoToolStripMenuItem.Text = "Indigo";
            this.indigoToolStripMenuItem.Click += new System.EventHandler(this.indigoToolStripMenuItem_Click);
            // 
            // violetToolStripMenuItem
            // 
            this.violetToolStripMenuItem.Name = "violetToolStripMenuItem";
            this.violetToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.violetToolStripMenuItem.Text = "Violet";
            this.violetToolStripMenuItem.Click += new System.EventHandler(this.violetToolStripMenuItem_Click);
            // 
            // customColorToolStripMenuItem
            // 
            this.customColorToolStripMenuItem.Name = "customColorToolStripMenuItem";
            this.customColorToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.customColorToolStripMenuItem.Text = "Custom Color...";
            this.customColorToolStripMenuItem.Click += new System.EventHandler(this.customColorToolStripMenuItem_Click);
            // 
            // favoriteColorToolStripMenuItem
            // 
            this.favoriteColorToolStripMenuItem.Name = "favoriteColorToolStripMenuItem";
            this.favoriteColorToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.favoriteColorToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.favoriteColorToolStripMenuItem.Text = "Favorite Color";
            this.favoriteColorToolStripMenuItem.Click += new System.EventHandler(this.favoriteColorToolStripMenuItem_Click);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            this.hideToolStripMenuItem.Click += new System.EventHandler(this.hideToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.exitToolStripMenuItem.Text = "Exit cDashboard";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setCDashBackColorToolStripMenuItem,
            this.setCDashDefaultMonitorToolStripMenuItem,
            this.setOpacityToolStripMenuItem,
            this.setFadeTimeToolStripMenuItem,
            this.setFavoriteStickyColorToolStripMenuItem,
            this.setFavoriteFontToolStripMenuItem});
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            // 
            // setCDashBackColorToolStripMenuItem
            // 
            this.setCDashBackColorToolStripMenuItem.Name = "setCDashBackColorToolStripMenuItem";
            this.setCDashBackColorToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.setCDashBackColorToolStripMenuItem.Text = "Set cDash BackColor...";
            this.setCDashBackColorToolStripMenuItem.Click += new System.EventHandler(this.setCDashBackColorToolStripMenuItem_Click);
            // 
            // setCDashDefaultMonitorToolStripMenuItem
            // 
            this.setCDashDefaultMonitorToolStripMenuItem.Name = "setCDashDefaultMonitorToolStripMenuItem";
            this.setCDashDefaultMonitorToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.setCDashDefaultMonitorToolStripMenuItem.Text = "Set Default Monitor...";
            this.setCDashDefaultMonitorToolStripMenuItem.Click += new System.EventHandler(this.setCDashDefaultMonitorToolStripMenuItem_Click);
            // 
            // setOpacityToolStripMenuItem
            // 
            this.setOpacityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textbox_opacity});
            this.setOpacityToolStripMenuItem.Name = "setOpacityToolStripMenuItem";
            this.setOpacityToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.setOpacityToolStripMenuItem.Text = "Set Opacity...";
            this.setOpacityToolStripMenuItem.DropDownClosed += new System.EventHandler(this.setOpacityToolStripMenuItem_DropDownClosed);
            this.setOpacityToolStripMenuItem.DropDownOpened += new System.EventHandler(this.setOpacityToolStripMenuItem_DropDownOpened);
            // 
            // textbox_opacity
            // 
            this.textbox_opacity.Name = "textbox_opacity";
            this.textbox_opacity.Size = new System.Drawing.Size(100, 23);
            // 
            // setFavoriteStickyColorToolStripMenuItem
            // 
            this.setFavoriteStickyColorToolStripMenuItem.Name = "setFavoriteStickyColorToolStripMenuItem";
            this.setFavoriteStickyColorToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.setFavoriteStickyColorToolStripMenuItem.Text = "Set Favorite Sticky Color...";
            this.setFavoriteStickyColorToolStripMenuItem.Click += new System.EventHandler(this.setFavoriteStickyColorToolStripMenuItem_Click);
            // 
            // setFavoriteFontToolStripMenuItem
            // 
            this.setFavoriteFontToolStripMenuItem.Name = "setFavoriteFontToolStripMenuItem";
            this.setFavoriteFontToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.setFavoriteFontToolStripMenuItem.Text = "Set Favorite Sticky Font...";
            this.setFavoriteFontToolStripMenuItem.Click += new System.EventHandler(this.setFavoriteFontToolStripMenuItem_Click);
            // 
            // colorDialog1
            // 
            this.colorDialog1.FullOpen = true;
            // 
            // fontDialog1
            // 
            this.fontDialog1.AllowScriptChange = false;
            this.fontDialog1.AllowSimulations = false;
            this.fontDialog1.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fontDialog1.FontMustExist = true;
            this.fontDialog1.ShowColor = true;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "This is a test";
            this.notifyIcon1.BalloonTipTitle = "cDashboard Test!";
            this.notifyIcon1.ContextMenuStrip = this.notifyicon_menustrip;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "cDashboard Process is running...";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // notifyicon_menustrip
            // 
            this.notifyicon_menustrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitCDashboardToolStripMenuItem});
            this.notifyicon_menustrip.Name = "notifyicon_menustrip";
            this.notifyicon_menustrip.Size = new System.Drawing.Size(159, 26);
            // 
            // exitCDashboardToolStripMenuItem
            // 
            this.exitCDashboardToolStripMenuItem.Name = "exitCDashboardToolStripMenuItem";
            this.exitCDashboardToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.exitCDashboardToolStripMenuItem.Text = "Exit cDashboard";
            this.exitCDashboardToolStripMenuItem.Click += new System.EventHandler(this.exitCDashboardToolStripMenuItem_Click);
            // 
            // button_time
            // 
            this.button_time.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_time.BackColor = System.Drawing.Color.Transparent;
            this.button_time.FlatAppearance.BorderSize = 0;
            this.button_time.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button_time.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button_time.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_time.Font = new System.Drawing.Font("Arial Black", 36F, System.Drawing.FontStyle.Bold);
            this.button_time.Location = new System.Drawing.Point(917, 590);
            this.button_time.Name = "button_time";
            this.button_time.Size = new System.Drawing.Size(364, 74);
            this.button_time.TabIndex = 5;
            this.button_time.Text = "12:09:33 AM";
            this.button_time.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_time.UseVisualStyleBackColor = false;
            this.button_time.Click += new System.EventHandler(this.button_time_Click);
            // 
            // button_date
            // 
            this.button_date.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_date.FlatAppearance.BorderSize = 0;
            this.button_date.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button_date.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button_date.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_date.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_date.Location = new System.Drawing.Point(887, 650);
            this.button_date.Name = "button_date";
            this.button_date.Size = new System.Drawing.Size(382, 34);
            this.button_date.TabIndex = 6;
            this.button_date.Text = "Date";
            this.button_date.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_date.UseVisualStyleBackColor = true;
            this.button_date.Click += new System.EventHandler(this.button_date_Click);
            // 
            // setFadeTimeToolStripMenuItem
            // 
            this.setFadeTimeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setInMillisecondsToolStripMenuItem});
            this.setFadeTimeToolStripMenuItem.Name = "setFadeTimeToolStripMenuItem";
            this.setFadeTimeToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.setFadeTimeToolStripMenuItem.Text = "Set Fade Time...";
            // 
            // setInMillisecondsToolStripMenuItem
            // 
            this.setInMillisecondsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textbox_fadetime});
            this.setInMillisecondsToolStripMenuItem.Name = "setInMillisecondsToolStripMenuItem";
            this.setInMillisecondsToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.setInMillisecondsToolStripMenuItem.Text = "Set in Milliseconds...";
            this.setInMillisecondsToolStripMenuItem.DropDownOpened += new System.EventHandler(this.setInMillisecondsToolStripMenuItem_DropDownOpened);
            // 
            // textbox_fadetime
            // 
            this.textbox_fadetime.Name = "textbox_fadetime";
            this.textbox_fadetime.Size = new System.Drawing.Size(100, 23);
            // 
            // cDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.button_date);
            this.Controls.Add(this.button_time);
            this.Controls.Add(this.label_build);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "cDashboard";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.Text = "cDashboard";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.cDashboard_SizeChanged);
            this.Click += new System.EventHandler(this.cDashboard_Click);
            this.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.Form1_ControlRemoved);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.notifyicon_menustrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer maintimer;
        private System.Windows.Forms.Label label_build;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newStickyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yellowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indigoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem violetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customColorToolStripMenuItem;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setCDashBackColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setCDashDefaultMonitorToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip notifyicon_menustrip;
        private System.Windows.Forms.ToolStripMenuItem exitCDashboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setOpacityToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox textbox_opacity;
        private System.Windows.Forms.ToolStripMenuItem favoriteColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setFavoriteStickyColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setFavoriteFontToolStripMenuItem;
        private System.Windows.Forms.Button button_time;
        private System.Windows.Forms.Button button_date;
        private System.Windows.Forms.ToolStripMenuItem setFadeTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setInMillisecondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox textbox_fadetime;
    }
}

