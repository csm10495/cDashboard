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
        /// <param name="disposing">true if managed resources should be 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cDashboard));
            this.uitimer = new System.Windows.Forms.Timer(this.components);
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyicon_menustrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitCDashboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button_date = new System.Windows.Forms.Button();
            this.button_time = new System.Windows.Forms.Button();
            this.label_build = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new cDashboard.cToolStripMenuItem();
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
            this.newCPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newCStopwatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newCWeatherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newCBatteryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newCMoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newCReminderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newCRViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new cDashboard.cToolStripMenuItem();

            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cDashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cDashBackgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setCDashBackcolorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cDashWallpaperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setCDashWallpaperImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.combobox_wallpaper = new System.Windows.Forms.ToolStripComboBox();
            this.setOpacityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textbox_opacity = new System.Windows.Forms.ToolStripTextBox();
            this.setFadeTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setInMillisecondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textbox_fadetime = new System.Windows.Forms.ToolStripTextBox();
            this.setDateTimeColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boardlessModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cStickyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.favoriteColorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.favoriteFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cWeatherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setCWeatherUnitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.celciusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cDashDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cNotificationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolstrip_displaytime = new System.Windows.Forms.ToolStripTextBox();
            this.fadetimer = new System.Windows.Forms.Timer(this.components);
            this.PluginSaveTimer = new System.Windows.Forms.Timer(this.components);
            this.automaticallyCheckForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyicon_menustrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uitimer
            // 
            this.uitimer.Enabled = true;
            this.uitimer.Interval = 1000;
            this.uitimer.Tick += new System.EventHandler(this.uitimer_Tick);
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
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "Image.jpg";
            this.openFileDialog1.Title = "Select Image File For cPic";
            // 
            // button_date
            // 
            this.button_date.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_date.BackColor = System.Drawing.Color.Transparent;
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
            this.button_date.UseVisualStyleBackColor = false;
            this.button_date.Click += new System.EventHandler(this.button_date_Click);
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
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
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
            this.newCPicToolStripMenuItem,
            this.newCStopwatchToolStripMenuItem,
            this.newCWeatherToolStripMenuItem,
            this.newCBatteryToolStripMenuItem,
            this.newCMoteToolStripMenuItem,
            this.newCReminderToolStripMenuItem,
            this.newCRViewerToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.hideToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.pluginsToolStripMenuItem});
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
            this.newStickyToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.newStickyToolStripMenuItem.Text = "New cSticky";
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
            // newCPicToolStripMenuItem
            // 
            this.newCPicToolStripMenuItem.Name = "newCPicToolStripMenuItem";
            this.newCPicToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.newCPicToolStripMenuItem.Text = "New cPic...";
            this.newCPicToolStripMenuItem.Click += new System.EventHandler(this.newCPicToolStripMenuItem_Click);
            // 
            // newCStopwatchToolStripMenuItem
            // 
            this.newCStopwatchToolStripMenuItem.Name = "newCStopwatchToolStripMenuItem";
            this.newCStopwatchToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.newCStopwatchToolStripMenuItem.Text = "New cStopwatch";
            this.newCStopwatchToolStripMenuItem.Click += new System.EventHandler(this.newCStopwatchToolStripMenuItem_Click);
            // 
            // newCWeatherToolStripMenuItem
            // 
            this.newCWeatherToolStripMenuItem.Name = "newCWeatherToolStripMenuItem";
            this.newCWeatherToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.newCWeatherToolStripMenuItem.Text = "New cWeather...";
            this.newCWeatherToolStripMenuItem.Click += new System.EventHandler(this.newCWeatherToolStripMenuItem_Click);
            // 
            // newCBatteryToolStripMenuItem
            // 
            this.newCBatteryToolStripMenuItem.Name = "newCBatteryToolStripMenuItem";
            this.newCBatteryToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.newCBatteryToolStripMenuItem.Text = "New cBattery";
            this.newCBatteryToolStripMenuItem.Click += new System.EventHandler(this.newCBatteryToolStripMenuItem_Click);
            // 
            // newCMoteToolStripMenuItem
            // 
            this.newCMoteToolStripMenuItem.Name = "newCMoteToolStripMenuItem";
            this.newCMoteToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.newCMoteToolStripMenuItem.Text = "New cMote";
            this.newCMoteToolStripMenuItem.Click += new System.EventHandler(this.newCMoteToolStripMenuItem_Click);
            // 
            // newCReminderToolStripMenuItem
            // 
            this.newCReminderToolStripMenuItem.Name = "newCReminderToolStripMenuItem";
            this.newCReminderToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.newCReminderToolStripMenuItem.Text = "New cReminder...";
            this.newCReminderToolStripMenuItem.Click += new System.EventHandler(this.newCReminderToolStripMenuItem_Click);
            // 
            // newCRViewerToolStripMenuItem
            // 
            this.newCRViewerToolStripMenuItem.Name = "newCRViewerToolStripMenuItem";
            this.newCRViewerToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.newCRViewerToolStripMenuItem.Text = "New cRViewer...";
            this.newCRViewerToolStripMenuItem.Click += new System.EventHandler(this.newCRViewerToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdatesToolStripMenuItem,
            this.automaticallyCheckForUpdatesToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(276, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for Updates...";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            this.hideToolStripMenuItem.Click += new System.EventHandler(this.hideToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.exitToolStripMenuItem.Text = "Exit cDashboard";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // pluginsToolStripMenuItem
            // 
            this.pluginsToolStripMenuItem.Name = "pluginsToolStripMenuItem";
            this.pluginsToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.pluginsToolStripMenuItem.Text = "Plugins";
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
            this.cDashToolStripMenuItem,
            this.cStickyToolStripMenuItem,
            this.cWeatherToolStripMenuItem,
            this.cDashDataToolStripMenuItem,
            this.cNotificationToolStripMenuItem});
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            // 
            // cDashToolStripMenuItem
            // 
            this.cDashToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultMonitorToolStripMenuItem,
            this.cDashBackgroundToolStripMenuItem,
            this.setOpacityToolStripMenuItem,
            this.setFadeTimeToolStripMenuItem,
            this.setDateTimeColorToolStripMenuItem,
            this.boardlessModeToolStripMenuItem});
            this.cDashToolStripMenuItem.Name = "cDashToolStripMenuItem";
            this.cDashToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.cDashToolStripMenuItem.Text = "cDash...";
            // 
            // defaultMonitorToolStripMenuItem
            // 
            this.defaultMonitorToolStripMenuItem.Name = "defaultMonitorToolStripMenuItem";
            this.defaultMonitorToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.defaultMonitorToolStripMenuItem.Text = "Default Monitor...";
            this.defaultMonitorToolStripMenuItem.Click += new System.EventHandler(this.defaultMonitorToolStripMenuItem_Click);
            // 
            // cDashBackgroundToolStripMenuItem
            // 
            this.cDashBackgroundToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setCDashBackcolorToolStripMenuItem,
            this.cDashWallpaperToolStripMenuItem});
            this.cDashBackgroundToolStripMenuItem.Name = "cDashBackgroundToolStripMenuItem";
            this.cDashBackgroundToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.cDashBackgroundToolStripMenuItem.Text = "Background";
            // 
            // setCDashBackcolorToolStripMenuItem
            // 
            this.setCDashBackcolorToolStripMenuItem.Name = "setCDashBackcolorToolStripMenuItem";
            this.setCDashBackcolorToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.setCDashBackcolorToolStripMenuItem.Text = "Set cDash Backcolor....";
            this.setCDashBackcolorToolStripMenuItem.Click += new System.EventHandler(this.setCDashBackcolorToolStripMenuItem_Click);
            // 
            // cDashWallpaperToolStripMenuItem
            // 
            this.cDashWallpaperToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setCDashWallpaperImageToolStripMenuItem,
            this.combobox_wallpaper});
            this.cDashWallpaperToolStripMenuItem.Name = "cDashWallpaperToolStripMenuItem";
            this.cDashWallpaperToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.cDashWallpaperToolStripMenuItem.Text = "Wallpaper";
            this.cDashWallpaperToolStripMenuItem.DropDownOpened += new System.EventHandler(this.cDashWallpaperToolStripMenuItem_DropDownOpened_1);
            // 
            // setCDashWallpaperImageToolStripMenuItem
            // 
            this.setCDashWallpaperImageToolStripMenuItem.Name = "setCDashWallpaperImageToolStripMenuItem";
            this.setCDashWallpaperImageToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.setCDashWallpaperImageToolStripMenuItem.Text = "Image...";
            this.setCDashWallpaperImageToolStripMenuItem.Click += new System.EventHandler(this.setCDashWallpaperImageToolStripMenuItem_Click_1);
            // 
            // combobox_wallpaper
            // 
            this.combobox_wallpaper.Items.AddRange(new object[] {
            "Center",
            "None",
            "Stretch",
            "Tile",
            "Zoom"});
            this.combobox_wallpaper.Name = "combobox_wallpaper";
            this.combobox_wallpaper.Size = new System.Drawing.Size(121, 23);
            this.combobox_wallpaper.SelectedIndexChanged += new System.EventHandler(this.combobox_wallpaper_SelectedIndexChanged_1);
            // 
            // setOpacityToolStripMenuItem
            // 
            this.setOpacityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textbox_opacity});
            this.setOpacityToolStripMenuItem.Name = "setOpacityToolStripMenuItem";
            this.setOpacityToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.setOpacityToolStripMenuItem.Text = "Opacity...";
            this.setOpacityToolStripMenuItem.DropDownClosed += new System.EventHandler(this.setOpacityToolStripMenuItem_DropDownClosed_1);
            this.setOpacityToolStripMenuItem.DropDownOpened += new System.EventHandler(this.setOpacityToolStripMenuItem_DropDownOpened_1);
            // 
            // textbox_opacity
            // 
            this.textbox_opacity.Name = "textbox_opacity";
            this.textbox_opacity.Size = new System.Drawing.Size(100, 23);
            // 
            // setFadeTimeToolStripMenuItem
            // 
            this.setFadeTimeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setInMillisecondsToolStripMenuItem});
            this.setFadeTimeToolStripMenuItem.Name = "setFadeTimeToolStripMenuItem";
            this.setFadeTimeToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.setFadeTimeToolStripMenuItem.Text = "Fade Time...";
            // 
            // setInMillisecondsToolStripMenuItem
            // 
            this.setInMillisecondsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textbox_fadetime});
            this.setInMillisecondsToolStripMenuItem.Name = "setInMillisecondsToolStripMenuItem";
            this.setInMillisecondsToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.setInMillisecondsToolStripMenuItem.Text = "Set in Milliseconds...";
            this.setInMillisecondsToolStripMenuItem.DropDownClosed += new System.EventHandler(this.setInMillisecondsToolStripMenuItem_DropDownClosed);
            this.setInMillisecondsToolStripMenuItem.DropDownOpened += new System.EventHandler(this.setInMillisecondsToolStripMenuItem_DropDownOpened_1);
            // 
            // textbox_fadetime
            // 
            this.textbox_fadetime.Name = "textbox_fadetime";
            this.textbox_fadetime.Size = new System.Drawing.Size(100, 23);
            // 
            // setDateTimeColorToolStripMenuItem
            // 
            this.setDateTimeColorToolStripMenuItem.Name = "setDateTimeColorToolStripMenuItem";
            this.setDateTimeColorToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.setDateTimeColorToolStripMenuItem.Text = "UI Text Color...";
            this.setDateTimeColorToolStripMenuItem.Click += new System.EventHandler(this.setDateTimeColorToolStripMenuItem_Click_1);
            // 
            // boardlessModeToolStripMenuItem
            // 
            this.boardlessModeToolStripMenuItem.Name = "boardlessModeToolStripMenuItem";
            this.boardlessModeToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.boardlessModeToolStripMenuItem.Text = "Boardless Mode";
            this.boardlessModeToolStripMenuItem.CheckedChanged += new System.EventHandler(this.boardlessModeToolStripMenuItem_CheckedChanged);
            this.boardlessModeToolStripMenuItem.Click += new System.EventHandler(this.boardlessModeToolStripMenuItem_Click);
            // 
            // cStickyToolStripMenuItem
            // 
            this.cStickyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.favoriteColorToolStripMenuItem1,
            this.favoriteFontToolStripMenuItem});
            this.cStickyToolStripMenuItem.Name = "cStickyToolStripMenuItem";
            this.cStickyToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.cStickyToolStripMenuItem.Text = "cSticky...";
            // 
            // favoriteColorToolStripMenuItem1
            // 
            this.favoriteColorToolStripMenuItem1.Name = "favoriteColorToolStripMenuItem1";
            this.favoriteColorToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.favoriteColorToolStripMenuItem1.Text = "Favorite Color";
            this.favoriteColorToolStripMenuItem1.Click += new System.EventHandler(this.favoriteColorToolStripMenuItem1_Click);
            // 
            // favoriteFontToolStripMenuItem
            // 
            this.favoriteFontToolStripMenuItem.Name = "favoriteFontToolStripMenuItem";
            this.favoriteFontToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.favoriteFontToolStripMenuItem.Text = "Favorite Font";
            this.favoriteFontToolStripMenuItem.Click += new System.EventHandler(this.favoriteFontToolStripMenuItem_Click);
            // 
            // cWeatherToolStripMenuItem
            // 
            this.cWeatherToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setCWeatherUnitToolStripMenuItem});
            this.cWeatherToolStripMenuItem.Name = "cWeatherToolStripMenuItem";
            this.cWeatherToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.cWeatherToolStripMenuItem.Text = "cWeather...";
            // 
            // setCWeatherUnitToolStripMenuItem
            // 
            this.setCWeatherUnitToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fToolStripMenuItem,
            this.celciusToolStripMenuItem});
            this.setCWeatherUnitToolStripMenuItem.Name = "setCWeatherUnitToolStripMenuItem";
            this.setCWeatherUnitToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.setCWeatherUnitToolStripMenuItem.Text = "Temperature Unit...";
            // 
            // fToolStripMenuItem
            // 
            this.fToolStripMenuItem.Name = "fToolStripMenuItem";
            this.fToolStripMenuItem.Size = new System.Drawing.Size(87, 22);
            this.fToolStripMenuItem.Text = "°F";
            this.fToolStripMenuItem.Click += new System.EventHandler(this.fToolStripMenuItem_Click_1);
            // 
            // celciusToolStripMenuItem
            // 
            this.celciusToolStripMenuItem.Name = "celciusToolStripMenuItem";
            this.celciusToolStripMenuItem.Size = new System.Drawing.Size(87, 22);
            this.celciusToolStripMenuItem.Text = "°C";
            this.celciusToolStripMenuItem.Click += new System.EventHandler(this.cToolStripMenuItem_Click_1);
            // 
            // cDashDataToolStripMenuItem
            // 
            this.cDashDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem,
            this.importToolStripMenuItem});
            this.cDashDataToolStripMenuItem.Name = "cDashDataToolStripMenuItem";
            this.cDashDataToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.cDashDataToolStripMenuItem.Text = "cDash Data...";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // cNotificationToolStripMenuItem
            // 
            this.cNotificationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayTimeToolStripMenuItem});
            this.cNotificationToolStripMenuItem.Name = "cNotificationToolStripMenuItem";
            this.cNotificationToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.cNotificationToolStripMenuItem.Text = "cNotification";
            // 
            // displayTimeToolStripMenuItem
            // 
            this.displayTimeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolstrip_displaytime});
            this.displayTimeToolStripMenuItem.Name = "displayTimeToolStripMenuItem";
            this.displayTimeToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.displayTimeToolStripMenuItem.Text = "Display Time in Seconds";
            this.displayTimeToolStripMenuItem.DropDownClosed += new System.EventHandler(this.displayTimeToolStripMenuItem_DropDownClosed);
            this.displayTimeToolStripMenuItem.DropDownOpened += new System.EventHandler(this.displayTimeToolStripMenuItem_DropDownOpened);
            // 
            // toolstrip_displaytime
            // 
            this.toolstrip_displaytime.Name = "toolstrip_displaytime";
            this.toolstrip_displaytime.Size = new System.Drawing.Size(152, 23);
            this.toolstrip_displaytime.Text = "5";
            // 
            // fadetimer
            // 
            this.fadetimer.Interval = 1;
            this.fadetimer.Tick += new System.EventHandler(this.fadetimer_Tick);
            // 
            // PluginSaveTimer
            // 
            this.PluginSaveTimer.Enabled = true;
            this.PluginSaveTimer.Interval = 1000;
            this.PluginSaveTimer.Tick += new System.EventHandler(this.PluginSaveTimer_Tick);
           // automaticallyCheckForUpdatesToolStripMenuItem
            // 
            this.automaticallyCheckForUpdatesToolStripMenuItem.Name = "automaticallyCheckForUpdatesToolStripMenuItem";
            this.automaticallyCheckForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(276, 22);
            this.automaticallyCheckForUpdatesToolStripMenuItem.Text = "Automatically Check for Updates ";
            this.automaticallyCheckForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.automaticallyCheckForUpdatesToolStripMenuItem_Click);
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
            this.DoubleBuffered = true;
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
            this.MouseLeave += new System.EventHandler(this.cDashboard_MouseLeave);
            this.notifyicon_menustrip.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer uitimer;
        private System.Windows.Forms.Label label_build;
        private System.Windows.Forms.MenuStrip menuStrip1;
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
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip notifyicon_menustrip;
        private System.Windows.Forms.ToolStripMenuItem exitCDashboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem favoriteColorToolStripMenuItem;
        private System.Windows.Forms.Button button_time;
        private System.Windows.Forms.Button button_date;
        private System.Windows.Forms.ToolStripMenuItem newCPicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem newCStopwatchToolStripMenuItem;
        public System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer fadetimer;
        private System.Windows.Forms.ToolStripMenuItem newCWeatherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cDashDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cStickyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem favoriteColorToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem favoriteFontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cDashToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultMonitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cDashBackgroundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setCDashBackcolorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cDashWallpaperToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setCDashWallpaperImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox combobox_wallpaper;
        private System.Windows.Forms.ToolStripMenuItem setOpacityToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox textbox_opacity;
        private System.Windows.Forms.ToolStripMenuItem setFadeTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setInMillisecondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox textbox_fadetime;
        private System.Windows.Forms.ToolStripMenuItem cWeatherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setCWeatherUnitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem celciusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setDateTimeColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem boardlessModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newCBatteryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newCMoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newCReminderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newCRViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cNotificationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolstrip_displaytime;

        private System.Windows.Forms.ToolStripMenuItem pluginsToolStripMenuItem;
        private System.Windows.Forms.Timer PluginSaveTimer;
        private cDashboard.cToolStripMenuItem fileToolStripMenuItem;
        private cDashboard.cToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem automaticallyCheckForUpdatesToolStripMenuItem;
    }
}

