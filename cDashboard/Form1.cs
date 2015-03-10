//This file is part of cDashboard
//cDashboard - An information-based overlay for Microsoft Windows
//This is the main controller form (the Dash)
//(C) Charles Machalow under the MIT License
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Utilities;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Web.Script.Serialization;
using System.Diagnostics;
using System.IO;
using Utilities;



namespace cDashboard
{
    public partial class cDashboard : cForm
    {

        #region Global Variables

        /// <summary>
        /// represents all cReminders as a dict
        /// long -> ticks
        /// string -> message
        /// </summary>
        private SortedDictionary<long, string> dict_cReminders = new SortedDictionary<long, string>();

        /// <summary>
        /// this is the tick counter for the fade timer
        /// </summary>
        private int fadetimertime = 0;

        /// <summary>
        /// the amount of time in milliseconds needed for fade_in, fade_out
        /// </summary>
        private int int_fade_milliseconds = 500;

        /// <summary>
        /// the state of the timer can be fading in, out, indash.
        /// </summary>
        private enum timerstate
        { fadein = 0, fadeout = 1, indash = 2 };

        /// <summary>
        /// The current state of the main timer it can be fadein, fadeout, or indash
        /// </summary>
        private timerstate cD_tstate = (int)timerstate.fadein;//the first state of the timer is fadein

        /// <summary>
        /// the keyboard hook used to catch key presses for certain keys
        /// </summary>
        private globalKeyboardHook KeyHook = new globalKeyboardHook(); //KeyHook is the global key hook

        /// <summary>
        /// Signifies if the LCtrl key is down
        /// </summary>
        private bool LCtrlIsDown = false;

        /// <summary>
        /// signifies if a wallpaper image is being used
        /// </summary>
        private bool UseWallpaperImage = false;

        /// <summary>
        /// Signifies if the tilde key is down
        /// </summary>
        private bool TildeIsDown = false;

        /// <summary>
        /// opacity level of the dashboard
        /// </summary>
        private int OpacityLevel = -1;

        /// <summary>
        /// favorite color for new stickies
        /// </summary>
        private Color FavoriteStickyColor;

        /// <summary>
        /// favorite font for new stickies
        /// </summary>
        private Font FavoriteStickyFont;

        /// <summary>
        /// represents the number of seconds in this cycle
        /// used for knowing when to update cWeather(s)
        /// </summary>
        private int TimerCounter = 0;

        #endregion Global Variables

        #region Constructor

        /// <summary>
        /// Form Constructor
        /// </summary>
        public cDashboard()
        {
            pluginsAssoc = new Dictionary<ToolStripItem, IPlugin>();
            pluginNames = new Dictionary<string, IPlugin>();
            pluginTypes = new Dictionary<Type, IPlugin>();
            plugins = new System.Threading.SemaphoreSlim(1);
            InitializeComponent();
            SetupPlugins();
        }

        #endregion Constructor

        #region plugins

        //This is used to identify which plugin should be called when a menu button is pressed.
        private Dictionary<ToolStripItem, IPlugin> pluginsAssoc;

        //This is used to identify for persistance what sorts of things should be loaded by name.
        private Dictionary<string, IPlugin> pluginNames;

        private Dictionary<Type, IPlugin> pluginTypes;
        System.Threading.SemaphoreSlim plugins;
        //Setup the plugins for use in the form.
        private void SetupPlugins()
        {
            foreach (var i in PluginContainer.plugins)
            {
                var new_toolstrip = pluginsToolStripMenuItem.DropDownItems.Add(i.Metadata.name);
                pluginsAssoc[new_toolstrip] = i.Value;
                new_toolstrip.Click += PluginToolStripHandler;
                pluginNames[i.Metadata.name] = i.Value;
                pluginNames[i.Metadata.name].LoadPlugin(SETTINGS_LOCATION, this);
                pluginTypes[i.Value.getFormType()] = i.Value;
            }
        }


        private void PluginToolStripHandler(object sender, EventArgs e)
        {
            var nf = pluginsAssoc[(ToolStripItem)sender].GetForm();
            var plugin = pluginsAssoc[(ToolStripItem)sender];
            if (plugin.DisposeOnClose)
            {
                nf.FormClosed += PluginFormClosedDispose;
            }
            //this is apparently essential for adding it as a parent.
            AddForm(nf);
            //Write configuration to the file.
            
            Console.WriteLine("Created plugin window");
        }

        private void PluginFormClosedDispose(object sender, FormClosedEventArgs e)
        {
            Controls.Remove((Control)sender);
            ((Form)sender).Dispose();
        }

        public void AddForm(Form nf)
        {
            nf.TopLevel = false;
            nf.Parent = this;
            nf.Show();
            Controls.Add(nf);
        }

        #endregion plugins

        #region Form Loading, Initial Setup

        /// <summary>
        /// exits cDashboard and removes the notifyicon
        /// </summary>
        private void exitApplication()
        {
            notifyIcon1.Visible = false;
            Environment.Exit(0);
        }

        /// <summary>
        /// This will be called on window creation to make Windows
        /// think of the Dash window as a toolwindow
        /// window to avoid being in the alt-tab menu
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                // turn on WS_EX_TOOLWINDOW style bit
                // By calling VK_F17
                // see WinUser.h for more information
                cp.ExStyle |= 0x80;

                return cp;
            }
        }

        /// <summary>
        /// check for duplicate cDashboard processes
        /// </summary>
        private void check_for_duplicate_processes()
        {
            //check if another cDashboard Process is running
            //warn user, close this one if open
            int int_cDash_processes_found = 0;
            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName.Contains("cDashboard") && !p.ProcessName.Contains("vshost"))
                {
                    int_cDash_processes_found = int_cDash_processes_found + 1;
                }
            }

            //Another cDashboard is running, toggle it
            if (int_cDash_processes_found > 1)
            {
                List<string> list_port_setting = getSpecificSetting(new string[] { "cDash", "TCPListenPort" });
                TcpClient client = new TcpClient("127.0.0.1", Convert.ToUInt16(list_port_setting[0]));
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("cdash-toggle");

                NetworkStream stream = client.GetStream();

                // Send the message
                stream.Write(data, 0, data.Length);

                stream.Close();
                client.Close();

                exitApplication();
            }

        }

        /// <summary>
        /// The form loading method.
        /// calls variable_setup() -> proper key hooking
        /// calls buildAndSettingsFileCreation() -> Checks and displays the build number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            check_for_duplicate_processes(); //check for duplicate cDashboard processes

            this.notifyIcon1.Visible = true;

            variable_setup(); //setup variables1
            // this.Focus(); //This makes it so the text is not edited by pressing keys after startup (while invisible)

            //create appdata directory
            if (!System.IO.Directory.Exists(SETTINGS_LOCATION))
                System.IO.Directory.CreateDirectory((SETTINGS_LOCATION));

            //File Building
            buildAndSettingsFileCreation();

            //this will be the list of lines from the settings file
            //THE settings_list WILL NOT INCLUDE BLANK LINES OR LINES STARTING WITH #
            List<List<string>> settings_list = getSettingsList();

            //Read settings and create stickies
            createStickiesFromFiles(ref settings_list);

            //Read settings not pertaining to stickies
            otherSettings(ref settings_list);

            //start listening server
            new System.Threading.Thread(runServer).Start();

            CompletedForm_Load = true;
        }

        /// <summary>
        /// runs a local TCP server listening for a command to toggle
        /// the dash up and down
        /// </summary>
        private void runServer()
        {
            //TCPListener with random (unused port)
            TcpListener tlistener = new TcpListener(System.Net.IPAddress.Loopback, 0);

            tlistener.Start();
            replaceSetting(new string[] { "cDash", "TCPListenPort" }, new string[] { "cDash", "TCPListenPort", ((IPEndPoint)tlistener.LocalEndpoint).Port.ToString() });

            while (true)
            {
                Socket s = tlistener.AcceptSocket();

                byte[] data = new byte[100];
                int size = s.Receive(data);
                
                if (System.Text.Encoding.ASCII.GetString(data, 0, data.Length).Contains("cdash-toggle"))
                {
                    fade_toggle();
                }

                s.Close();
            }
        }

        /// <summary>
        /// Will read settings not directly related to stickies
        /// </summary>
        /// <param name="settings_list"></param>
        private void otherSettings(ref List<List<string>> settings_list)
        {
            string FSFN = ""; //Favorite Sticky Font Name
            float FSFS = -1; //Favorite Sticky Font Size
            bool BoardlessMode = false;

            foreach (List<string> currentline in settings_list)
            {
                //cReminders
                if (currentline[0] == "cReminders")
                {
                    //handle empty cReminders list
                    if (currentline.Count == 1)
                    {
                        continue;
                    }

                    List<string> cReminders = currentline;
                    cReminders.RemoveAt(0);

                    if (cReminders.Count % 2 != 0)
                    {
                        MessageBox.Show("No message for certain cReminder?, this shouldn't happen.");
                    }
                    else
                    {
                        for (int i = 0; i < cReminders.Count; i++)
                        {
                            long ticks = Convert.ToInt64(cReminders[i]);
                            string msg = cReminders[i + 1];
                            dict_cReminders.Add(ticks, msg);
                            i++;
                        }
                    }
                }

                //Most global cDash settings
                //set backcolor of the Dash from settings
                if (currentline[0] == "cDash")
                {
                    //updates ui to signify auto checking
                    if (currentline[1] == "AutoUpdateCheck")
                    {
                        if (currentline[2] == "True")
                        {
                            automaticallyCheckForUpdatesToolStripMenuItem.Checked = true;

                            new System.Threading.Thread(() => updateCheck(true)).Start();
                        }
                    }

                    //handle BoardlessMode setting
                    if (currentline[1] == "BoardlessMode")
                    {
                        if (currentline[2] == "True")
                        {
                            BoardlessMode = true;
                            boardlessModeToolStripMenuItem.Checked = true;
                        }
                    }

                    //special handling for global cWeather settings
                    if (currentline[1] == "cWeather")
                    {
                        if (currentline[2] == "Unit")
                        {
                            if (currentline[3] == "F")
                            {
                                fToolStripMenuItem.Checked = true;
                                celciusToolStripMenuItem.Checked = false;
                            }
                            else
                            {
                                celciusToolStripMenuItem.Checked = true;
                                fToolStripMenuItem.Checked = false;
                            }
                        }
                    }

                    if (currentline[1] == "BackColor")
                    {
                        this.BackColor = Color.FromArgb(Convert.ToInt32(currentline[2]), Convert.ToInt32(currentline[3]), Convert.ToInt32(currentline[4]));
                        //line removed so that transparent color works
                        //menuStrip1.BackColor = this.BackColor;
                    }

                    if (currentline[1] == "Opacity")
                    {
                        OpacityLevel = Convert.ToInt32(currentline[2]);
                        textbox_opacity.Text = currentline[2];
                    }

                    if (currentline[1] == "FadeLengthInMilliseconds")
                    {
                        int_fade_milliseconds = Convert.ToInt32(currentline[2]);
                    }

                    if (currentline[1] == "DateTimeStripColor")
                    {
                        button_time.ForeColor = Color.FromArgb(Convert.ToInt32(currentline[2]), Convert.ToInt32(currentline[3]), Convert.ToInt32(currentline[4]));
                        button_date.ForeColor = Color.FromArgb(Convert.ToInt32(currentline[2]), Convert.ToInt32(currentline[3]), Convert.ToInt32(currentline[4]));
                        menuStrip1.ForeColor = Color.FromArgb(Convert.ToInt32(currentline[2]), Convert.ToInt32(currentline[3]), Convert.ToInt32(currentline[4]));
                    }

                    if (currentline[1] == "FavoriteStickyColor")
                    {
                        if (currentline[2] == "NULL")
                        {
                            //don't initialize FavoriteStickyColor
                        }
                        else
                        {
                            FavoriteStickyColor = Color.FromArgb(Convert.ToInt32(currentline[2]), Convert.ToInt32(currentline[3]), Convert.ToInt32(currentline[4]));
                        }
                    }

                    if (currentline[1] == "FavoriteStickyFontName")
                    {
                        FSFN = currentline[2];
                    }

                    if (currentline[1] == "FavoriteStickyFontSize")
                    {
                        FSFS = Convert.ToSingle(currentline[2]);
                    }

                    //setup cDash wallpaper
                    if (currentline[1] == "Wallpaper")
                    {
                        UseWallpaperImage = Convert.ToBoolean(currentline[2]);

                        //if we are using a wallpaper image, grab it from file
                        if (UseWallpaperImage)
                        {
                            this.BackgroundImage = Image.FromFile(getSpecificSetting(new string[] { "cDash", "WallpaperImage" })[0]);
                        }
                    }

                    //set cDash wallpaper layout
                    if (currentline[1] == "WallpaperImageLayout")
                    {
                        if (currentline[2] == "Center")
                        {
                            this.BackgroundImageLayout = ImageLayout.Center;
                        }
                        else if (currentline[2] == "None")
                        {
                            this.BackgroundImageLayout = ImageLayout.None;
                        }
                        else if (currentline[2] == "Stretch")
                        {
                            this.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                        else if (currentline[2] == "Tile")
                        {
                            this.BackgroundImageLayout = ImageLayout.Tile;
                        }
                        else if (currentline[2] == "Zoom")
                        {
                            this.BackgroundImageLayout = ImageLayout.Zoom;
                        }
                    }
                }
                //Handling for cBattery
                else if (currentline[0] == "cBattery")
                {
                    //this would mean that this form already exists
                    if (!(Controls.Find(currentline[1], true).Length > 0))
                    {
                        cBattery cBattery_new = new cBattery();
                        cBattery_new.Name = currentline[1];
                        cBattery_new.TopLevel = false;
                        cBattery_new.Parent = this;
                        Controls.Add(cBattery_new);
                    }

                    //get form by name
                    cBattery this_cBattery = (cBattery)this.Controls.Find(currentline[1], true)[0];

                    if (currentline[2] == "Location")
                    {
                        this_cBattery.Location = new Point(Convert.ToInt16(currentline[3]), Convert.ToInt16(currentline[4]));
                    }

                    this_cBattery.Show();
                    this_cBattery.BringToFront();
                }
                //Handling for cMote
                else if (currentline[0] == "cMote")
                {
                    //this would mean that this form already exists
                    if (!(Controls.Find(currentline[1], true).Length > 0))
                    {
                        cMote cMote_new = new cMote();
                        cMote_new.Name = currentline[1];
                        cMote_new.TopLevel = false;
                        cMote_new.Parent = this;
                        Controls.Add(cMote_new);
                    }

                    //get form by name
                    cMote this_cMote = (cMote)this.Controls.Find(currentline[1], true)[0];

                    if (currentline[2] == "Location")
                    {
                        this_cMote.Location = new Point(Convert.ToInt16(currentline[3]), Convert.ToInt16(currentline[4]));
                    }

                    this_cMote.Show();
                    this_cMote.BringToFront();
                }
                //Handling for cRViewer
                else if (currentline[0] == "cRViewer")
                {
                    //this would mean that this form already exists
                    if (!(Controls.Find(currentline[1], true).Length > 0))
                    {
                        cRViewer cRViewer_new = new cRViewer();
                        cRViewer_new.Name = currentline[1];
                        cRViewer_new.TopLevel = false;
                        cRViewer_new.Parent = this;
                        Controls.Add(cRViewer_new);
                    }

                    //get form by name
                    cRViewer this_cRViewer = (cRViewer)this.Controls.Find(currentline[1], true)[0];

                    if (currentline[2] == "Location")
                    {
                        this_cRViewer.Location = new Point(Convert.ToInt16(currentline[3]), Convert.ToInt16(currentline[4]));
                    }
                    else if (currentline[2] == "Size")
                    {
                        this_cRViewer.Size = new Size(Convert.ToInt16(currentline[3]), Convert.ToInt16(currentline[4]));
                    }

                    this_cRViewer.Show();
                    this_cRViewer.BringToFront();
                }
                //Handling for cWeather
                else if (currentline[0] == "cWeather")
                {
                    //this would mean that this form already exists
                    if (!(Controls.Find(currentline[1], true).Length > 0))
                    {
                        cWeather cWeather_new = new cWeather();
                        cWeather_new.Name = currentline[1];
                        cWeather_new.TopLevel = false;
                        cWeather_new.Parent = this;
                        Controls.Add(cWeather_new);
                    }

                    //get form by name
                    cWeather this_cWeather = (cWeather)this.Controls.Find(currentline[1], true)[0];

                    if (currentline[2] == "Location")
                    {
                        this_cWeather.Location = new Point(Convert.ToInt16(currentline[3]), Convert.ToInt16(currentline[4]));
                    }

                    if (currentline[2] == "WOEID")
                    {
                        this_cWeather.WOEID = currentline[3];

                        //if WOEID is set, get weather info
                        if (this_cWeather.WOEID != "NULL")
                        {
                            this_cWeather.getWeatherInfo();
                        }
                    }

                    this_cWeather.Show();
                    this_cWeather.BringToFront();
                }
                else if (currentline[0] == "cPic")
                {
                    //this would mean that this form already exists
                    if (!(Controls.Find(currentline[1], true).Length > 0))
                    {
                        cPic cPic_new = new cPic();
                        cPic_new.Name = currentline[1];

                        //randomize files in folder
                        randomizeFiles(currentline[1]);

                        //use 1st image
                        cPic_new.BackgroundImage = Image.FromFile(SETTINGS_LOCATION + currentline[1] + "\\1");
                        cPic_new.Tag = 1;

                        cPic_new.TopLevel = false;
                        cPic_new.Parent = this;
                        Controls.Add(cPic_new);
                    }

                    //get form by name
                    cPic this_cPic = (cPic)this.Controls.Find(currentline[1], true)[0];

                    if (currentline[2] == "ImageLayout")
                    {
                        if (currentline[3] == "Center")
                        {
                            this_cPic.BackgroundImageLayout = ImageLayout.Center;
                        }
                        else if (currentline[3] == "None")
                        {
                            this_cPic.BackgroundImageLayout = ImageLayout.None;
                        }
                        else if (currentline[3] == "Stretch")
                        {
                            this_cPic.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                        else if (currentline[3] == "Tile")
                        {
                            this_cPic.BackgroundImageLayout = ImageLayout.Tile;
                        }
                        else if (currentline[3] == "Zoom")
                        {
                            this_cPic.BackgroundImageLayout = ImageLayout.Zoom;
                        }
                    }
                    else if (currentline[2] == "Size")
                    {
                        this_cPic.Size = new Size(Convert.ToInt16(currentline[3]), Convert.ToInt16(currentline[4]));
                    }
                    else if (currentline[2] == "Location")
                    {
                        this_cPic.Location = new Point(Convert.ToInt16(currentline[3]), Convert.ToInt16(currentline[4]));
                    }

                    this_cPic.Show();
                    this_cPic.BringToFront();
                }
                else if (currentline[0] == "cStopwatch")
                {
                    //this would mean that this form already exists
                    if (!(Controls.Find(currentline[1], true).Length > 0))
                    {
                        cStopwatch cStopwatch_new = new cStopwatch();
                        cStopwatch_new.Name = currentline[1];
                        cStopwatch_new.TopLevel = false;
                        cStopwatch_new.Parent = this;
                        Controls.Add(cStopwatch_new);
                    }

                    //get form by name
                    cStopwatch this_cStopwatch = (cStopwatch)this.Controls.Find(currentline[1], true)[0];

                    if (currentline[2] == "Location")
                    {
                        this_cStopwatch.Location = new Point(Convert.ToInt16(currentline[3]), Convert.ToInt16(currentline[4]));
                    }

                    this_cStopwatch.Show();
                    this_cStopwatch.BringToFront();
                }
            }
            FavoriteStickyFont = new Font(FSFN, FSFS);

            if (BoardlessMode)
            {
                goBoardless();
            }

            //move dash to set monitor
            moveToPrimaryMonitor();
        }

        /// <summary>
        /// Create the stickies from the settings file
        /// </summary>
        private void createStickiesFromFiles(ref List<List<string>> settings_list)
        {
            foreach (List<string> current_item in settings_list)
            {
                if (current_item[0] == "cSticky")
                {
                    //we have a "cSticky"

                    //check if this one has already been made
                    if (this.Controls.Find(current_item[1], true).Length == 0)
                    {
                        //if we make it in here, the control wasn't created yet
                        cSticky cSticky_new = new cSticky();
                        cSticky_new.Name = current_item[1];
                        cSticky_new.TopLevel = false;
                        cSticky_new.Parent = this;
                        Controls.Add(cSticky_new);
                    }

                    //the property is back color
                    if (current_item[2] == "BackColor")
                    {
                        this.Controls.Find(current_item[1], true)[0].Controls.Find("menustrip", false)[0].BackColor = Color.FromArgb(Convert.ToInt32(current_item[3]), Convert.ToInt32(current_item[4]), Convert.ToInt32(current_item[5]));
                        this.Controls.Find(current_item[1], true)[0].BackColor = Color.FromArgb(Convert.ToInt32(current_item[3]), Convert.ToInt32(current_item[4]), Convert.ToInt32(current_item[5]));
                        this.Controls.Find(current_item[1], true)[0].Controls.Find("rtb", false)[0].BackColor = Color.FromArgb(Convert.ToInt32(current_item[3]), Convert.ToInt32(current_item[4]), Convert.ToInt32(current_item[5]));
                    }

                    //the property is location
                    if (current_item[2] == "Location")
                    {
                        this.Controls.Find(current_item[1], true)[0].Location = new Point(Convert.ToInt32(current_item[3]), Convert.ToInt32(current_item[4]));
                    }

                    //the property is size
                    if (current_item[2] == "Size")
                    {
                        this.Controls.Find(current_item[1], true)[0].Size = new Size(Convert.ToInt32(current_item[3]), Convert.ToInt32(current_item[4]));
                    }
                }
            }

            //add stickies to form
            foreach (cSticky cSticky_new in this.Controls.OfType<cSticky>())
            {
                //attempts to load richtextbox files, if the file doesn't exist, it fails gracefully
                if (System.IO.File.Exists(SETTINGS_LOCATION + cSticky_new.Name + ".rtf"))
                {
                    ((RichTextBox)cSticky_new.Controls.Find("rtb", false)[0]).LoadFile(SETTINGS_LOCATION + cSticky_new.Name + ".rtf");
                }
                else
                {
                    ((RichTextBox)cSticky_new.Controls.Find("rtb", false)[0]).Text = "File missing";
                }

                //display control
                cSticky_new.Show();
                cSticky_new.BringToFront();
            }
        }

        /// <summary>
        /// Sets variables for proper key hooking and begins the hook
        /// </summary>
        private void variable_setup()
        {
            //add tilde and LeftCtrl to the list of hooked keys
            KeyHook.HookedKeys.Add(Keys.Oemtilde);
            KeyHook.HookedKeys.Add(Keys.LControlKey);
            //begin hook
            KeyHook.hook();
            //Setup Key Event Handlers
            KeyHook.KeyDown += new KeyEventHandler(KeyHook_KeyDown);
            KeyHook.KeyUp += new KeyEventHandler(KeyHook_KeyUp);
        }

        /// <summary>
        /// check for the build file, increment build number, display build number on form
        /// Create settings file, if it doesn't already exists
        /// </summary>
        private void buildAndSettingsFileCreation()
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                #region Build Data

                //check if build file exists
                if (!System.IO.File.Exists(SETTINGS_LOCATION + "BuildData.cDash"))
                {
                    //creates build data
                    //at this point this number doesn't mean much :P
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(SETTINGS_LOCATION + "BuildData.cDash");
                    sw.Write("144");
                    sw.Close();
                    label_build.Text = "cDashBoard Alpha Build 144";
                }
                else
                {
                    //read build number
                    System.IO.StreamReader sr = new System.IO.StreamReader(SETTINGS_LOCATION + "BuildData.cDash");
                    int buildnum = Convert.ToInt32(sr.ReadToEnd().ToString());
                    sr.Close();

                    //increment build number
                    buildnum++;

                    //write incremented build number
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(SETTINGS_LOCATION + "BuildData.cDash");
                    sw.Write(buildnum.ToString());
                    sw.Close();

                    //display build number
                    label_build.Text = "cDashBoard Alpha Build " + buildnum;
                }

                #endregion Build Data
            }

            //create the settings file
            if (!System.IO.File.Exists(SETTINGS_LOCATION + "cDash Settings.cDash"))
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(SETTINGS_LOCATION + "cDash Settings.cDash");
                sw.WriteLine("# cDashBoard Settings File");
                sw.WriteLine("# A # tells the program to ignore this line");
                sw.WriteLine("# Don't edit this file unless you know what you are doing");
                //set default opacity
                sw.WriteLine("cDash;Opacity;85");
                sw.WriteLine("cDash;FavoriteStickyColor;NULL");
                sw.WriteLine("cDash;FavoriteStickyFontName;Arial");
                sw.WriteLine("cDash;FavoriteStickyFontSize;12");
                sw.WriteLine("cDash;FadeLengthInMilliseconds;500");
                sw.WriteLine("cDash;Wallpaper;False");
                sw.WriteLine("cDash;WallpaperImage;NULL");
                sw.WriteLine("cDash;WallpaperImageLayout;None");
                sw.WriteLine("cDash;DateTimeStripColor;0;0;0");
                sw.WriteLine("cDash;cWeather;Unit;F");
                sw.WriteLine("cDash;BackColor;123;123;123");
                sw.WriteLine("cDash;BoardlessMode;False");
                sw.WriteLine("cDash;TCPListenPort;54523");
                sw.WriteLine("cDash;GitHubAPIReleaseURL;https://api.github.com/repos/csm10495/cDashboard/releases");
                sw.WriteLine("cDash;AutoUpdateCheck;True");

                sw.Close();

                //if there is more than one monitor, check which monitor the user wants to use
                if (Screen.AllScreens.Count() > 1)
                {
                    multiMonitorSetup(true);
                }
                else
                {
                    //add a "Default" value if there is only one monitor during install
                    List<List<string>> list_settings = getSettingsList();
                    List<string> list_currentline = new List<string>(new string[] { "cDash", "PrimaryMonitor", "Default" });
                    list_settings.Add(list_currentline);
                    saveSettingsList(list_settings);
                }

                notifyIcon1.ShowBalloonTip(8, "Welcome to cDashboard!", "To bring up (or bring down) the dash, either use the keyboard shortcut, Ctrl-~, or double-click this icon.", ToolTipIcon.Info);
            }
        }

        #endregion


        #region Monitor Settings

        /// <summary>
        /// moves the cDash to the set monitor
        /// </summary>
        private void moveToPrimaryMonitor()
        {
            this.WindowState = FormWindowState.Normal;

            //set Primary Monitor for the dash
            string[] tmp = { "cDash", "PrimaryMonitor" };
            List<string> list_primarymonitorline = getSpecificSetting(new List<string>(tmp));
            if (list_primarymonitorline.Count == 1)
            {
                //go through monitors to find the one that is = to the setting
                foreach (Screen s in Screen.AllScreens)
                {
                    //we have correct monitor
                    if (s.ToString() == list_primarymonitorline[0])
                    {
                        this.StartPosition = FormStartPosition.Manual;
                        this.Left = s.Bounds.Left;
                        this.Top = s.Bounds.Top;
                    }
                }
            }

            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// This will be called to cycle through all cForms to ensure that all
        /// cForms were viewable on screen
        /// </summary>
        private void makeSureCFormsAreOnScreen()
        {
            //goes through every cForm
            foreach (cForm current_cForm in this.Controls.OfType<cForm>())
            {
                //make sure that cForms are not lost to resizing the dash
                if (current_cForm.Location.X > this.Location.X + this.Size.Width)
                {
                    int form_widths = this.Width + current_cForm.Width;
                    int total_width = current_cForm.Location.X + current_cForm.Width;
                    current_cForm.Location = new Point(total_width - form_widths - 10, current_cForm.Location.Y);
                }
                if (current_cForm.Location.Y > this.Location.Y + this.Size.Height)
                {
                    int form_height = this.Height + current_cForm.Height;
                    int total_height = current_cForm.Location.Y + current_cForm.Height;
                    current_cForm.Location = new Point(current_cForm.Location.X, total_height - form_height - 10);
                }
                if (current_cForm.Location.X < 0)
                {
                    current_cForm.Location = new Point(0, current_cForm.Location.Y);
                }
                if (current_cForm.Location.Y < 0)
                {
                    current_cForm.Location = new Point(current_cForm.Location.X, 0);
                }
            }
        }

        /// <summary>
        /// If the size of the dashboard changed, the monitor or resolution must have changed.
        /// Therefore make sure that all forms are still in the dash, and not lost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cDashboard_SizeChanged(object sender, EventArgs e)
        {
            makeSureCFormsAreOnScreen();
        }

        /// <summary>
        /// set default monitor, which will also move the form and save the setting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void defaultMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            multiMonitorSetup(false);
        }

        /// <summary>1
        /// Setup multiple monitor wizard
        /// </summary>
        /// <param name="isFreshInstall">true if this is a new install, false if just a monitor switch</param>
        private void multiMonitorSetup(bool isFreshInstall)
        {
            //alert the user as to what is going on, only if this is a new install
            if (isFreshInstall == true)
            {
                MessageBox.Show("Hello! cDashBoard has detected that you have " + Screen.AllScreens.Count().ToString() + " Monitors. So this little wizard will allow cDashBoard to be setup and appear properly. After clicking Ok, you will see giant big white boxes over each monitor. Click the box on the monitor you want cDashBoard to appear on. Don't worry this can be changed later.", "cDashboard Multi-Monitor Setup");
            }
            //make the boxes appear
            foreach (Screen s in Screen.AllScreens)
            {
                //create a form that will overlay Screen s
                Form form_tmp = new Form();
                form_tmp.Click += new EventHandler(Monitor_Overlay_Click);
                form_tmp.Opacity = .9;
                form_tmp.Name = "Multi_Monitor_Selection_Overlay";
                form_tmp.WindowState = FormWindowState.Normal;
                form_tmp.StartPosition = FormStartPosition.Manual;
                form_tmp.Location = s.Bounds.Location;
                form_tmp.FormBorderStyle = FormBorderStyle.None;
                form_tmp.SizeGripStyle = SizeGripStyle.Hide;
                form_tmp.WindowState = FormWindowState.Maximized;
                form_tmp.TopMost = true;
                form_tmp.Show();
                //create label for the new form
                Label label_monitor_name = new Label();
                form_tmp.Controls.Add(label_monitor_name);
                label_monitor_name.Show();
                label_monitor_name.AutoSize = true;
                //make the label talk about the monitor
                label_monitor_name.Text = "Click the monitor you would like to use for cDashBoard\n" + s.ToString();
                label_monitor_name.Location = new Point((form_tmp.Width / 2) - label_monitor_name.Width / 2, (form_tmp.Height / 2) - label_monitor_name.Height / 2);

                if (isFreshInstall == false)
                {
                    //add a cancel button to each form
                    Button button_cancel = new Button();
                    form_tmp.Controls.Add(button_cancel);
                    button_cancel.Show();
                    button_cancel.AutoSize = true;
                    button_cancel.FlatStyle = FlatStyle.Flat;
                    button_cancel.Text = "Cancel";
                    button_cancel.Location = new Point(0, 0);
                    button_cancel.Click += new EventHandler(Mult_Monitor_button_cancel_Click);
                }
            }
        }

        /// <summary>
        /// Gets the monitor that the form was clicked on and make this the dashboard's display form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Monitor_Overlay_Click(object sender, EventArgs e)
        {
            List<string> list_find = new List<string>(new string[] { "cDash", "PrimaryMonitor" });
            List<string> list_replace = new List<string>(new string[] { "cDash", "PrimaryMonitor", Screen.FromControl(((Form)sender)).ToString() });

            //close overlays
            closeMultiMontiorOverlays();

            //replace setting
            replaceSetting(list_find, list_replace);

            //move the Dash to the new monitor
            moveToPrimaryMonitor();
        }

        /// <summary>
        /// Cancel the monitor change operation and close the overlay windows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mult_Monitor_button_cancel_Click(object sender, EventArgs e)
        {
            closeMultiMontiorOverlays();
        }

        /// <summary>
        /// closes all multi monitor setup overlay windows
        /// </summary>
        private void closeMultiMontiorOverlays()
        {

            //get all forms open forms and scan to make sure it is a monitor form

            FormCollection fc = Application.OpenForms;
            for (int x = fc.Count - 1; x >= 0; x--)
            {
                if (fc[x].Name == "Multi_Monitor_Selection_Overlay")
                {
                    //close monitor selection overlay
                    fc[x].Close();
                }
            }
        }

        #endregion Monitor Settings

        #region Key Hooks and Fades

        /// <summary>
        /// Every time a hooked key is pressed down, this will be called
        /// </summary>
        /// <param name="sender">Control that the action is for</param>
        /// <param name="e">Arguements for the event</param>
        private void KeyHook_KeyDown(object sender, KeyEventArgs e)
        {
            //Check if the hooked key is the LCtrl
            if (e.KeyCode == Keys.LControlKey)
            {
                //Set the bool properly
                LCtrlIsDown = true;
            }

            //Check if the hooked key is the tilde key
            if (e.KeyCode == Keys.Oemtilde)
            {
                //Set the bool properly
                TildeIsDown = true;
            }

            //if both hooked keys are down, and we are ready to fadein, fade in the dash
            if (TildeIsDown && LCtrlIsDown && cD_tstate == timerstate.fadein && CompletedForm_Load == true)
            {
                //kill attempts to call a fade_in during a fade_in
                if (this.Opacity != 0)
                    return;

                fade_in();

                //reset which keys are down
                //don't reset LCtrl (for convinence)
                TildeIsDown = false;
                e.Handled = true;
                return;
            }

            //if both hooked keys are down, and we are ready to fadeout, fadeout in the dash
            if (TildeIsDown && LCtrlIsDown && cD_tstate == timerstate.indash)
            {
                fade_out();

                //reset which keys are down
                //don't reset LCtrl (for convinence)
                TildeIsDown = false;
                e.Handled = true;
                return;
            }

            e.Handled = false;
        }

        /// <summary>
        /// Every time a hooked key is let up, this will be called
        /// </summary>
        /// <param name="sender">Control that the action is for</param>
        /// <param name="e">Arguements for the event</param>
        private void KeyHook_KeyUp(object sender, KeyEventArgs e)
        {
            //Check if the hooked key is the LCtrl
            if (e.KeyCode == Keys.LControlKey)
            {
                //Set the bool properly
                LCtrlIsDown = false;
            }

            //Check if the hooked key is the tilde key
            if (e.KeyCode == Keys.Oemtilde)
            {
                //Set the bool properly
                TildeIsDown = false;
            }
        }

        /// <summary>
        /// cues the form's fade in process
        /// </summary>
        private void fade_in()
        {
            moveToPrimaryMonitor(); //ensures proper form sizing

            updateTimeDate(); //updates time/date on form

            cD_tstate = (int)timerstate.fadein; //cD_tstate is the timer state

            this.Visible = true;

            updateCBattery(); //updates all cBattery

            updateCMote(); //updates all cMote

            fadetimer.Start(); //begin timer
        }

        /// <summary>
        /// cues the form's fade out process
        /// </summary>
        public void fade_out()
        {
            cD_tstate = timerstate.fadeout;

            fadetimer.Start();
        }

        /// <summary>
        /// fades in if not in
        /// fades out if in
        /// </summary>
        private void fade_toggle()
        {
            if (cD_tstate == timerstate.fadein)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        fade_in();
                    }));
                }
                else
                {
                    fade_in();
                }
            }
            else if (cD_tstate == timerstate.indash)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        fade_out();
                    }));
                }
                else
                {
                    fade_out();
                }
            }
        }

        /// <summary>
        /// adds to TimerCounter and updates cWeathers if 3600 seconds (1 hour) have elapsed
        /// </summary>
        private void updateCWeather()
        {
            TimerCounter++;

            if (TimerCounter > 3600)
            {
                TimerCounter = 0;
                foreach (cWeather this_cWeather in this.Controls.OfType<cWeather>())
                {
                    this_cWeather.getWeatherInfo();
                }
            }
        }

        /// <summary>
        /// update cBatterys
        /// </summary>
        private void updateCBattery()
        {
            foreach (cBattery this_cBattery in this.Controls.OfType<cBattery>())
            {
                this_cBattery.update();
            }
        }

        /// <summary>
        /// updates all cMotes
        /// </summary>
        private void updateCMote()
        {
            foreach (cMote this_cMote in this.Controls.OfType<cMote>())
            {
                this_cMote.getSpotifyInfoViaThread();
            }
        }

        /// <summary>
        /// accessor for dict_cReminders
        /// </summary>
        public SortedDictionary<long, string> getDictCReminders()
        {
            return dict_cReminders;
        }

        /// <summary>
        /// checks for any expired cReminders, and display, remove them
        /// </summary>
        public void updateDictCReminders()
        {
            List<string> cReminders = getSpecificSetting(new string[] { "cReminders" });

            if (cReminders.Count % 2 != 0)
            {
                MessageBox.Show("No message for certain cReminder?, this shouldn't happen.");
            }
            else
            {
                dict_cReminders.Clear();
                for (int i = 0; i < cReminders.Count; i++)
                {
                    long ticks = Convert.ToInt64(cReminders[i]);
                    string msg = cReminders[i + 1];
                    dict_cReminders.Add(ticks, msg);
                    i++;
                }
            }

            updateCRViewers();
        }

        /// <summary>
        /// triggers an update to all DGVs in cRViewers
        /// </summary>
        private void updateCRViewers()
        {
            foreach (cRViewer this_cRViewer in this.Controls.OfType<cRViewer>())
            {
                this_cRViewer.updateDGV();
            }
        }

        /// <summary>
        /// check if any cReminders should be displayed, display them
        /// </summary>
        private void checkForCReminders()
        {
            if (dict_cReminders.Count > 0)
            {
                if (DateTime.Now.Ticks > dict_cReminders.First().Key)
                {
                    long key = dict_cReminders.First().Key;
                    string value = dict_cReminders.First().Value.ToString();
                    dict_cReminders.Remove(dict_cReminders.First().Key);

                    //show cNotification
                    cNotification tmp = new cNotification(value, "cReminder");

                    //keep settings up-to-date
                    List<string> cReminders = new List<string>();
                    cReminders.Add("cReminders");
                    foreach (KeyValuePair<long, string> i in dict_cReminders)
                    {
                        cReminders.Add(i.Key.ToString());
                        cReminders.Add(i.Value.ToString());
                    }

                    replaceSetting(cReminders.GetRange(0, 1), cReminders);

                    updateCRViewers();
                }
            }
        }

        /// <summary>
        /// timer tick updates every second
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uitimer_Tick(object sender, EventArgs e)
        {
            updateCWeather();

            if (cD_tstate == timerstate.indash)
            {
                checkForCReminders();
                updateTimeDate();
            }
        }

        /// <summary>
        /// timer tick specifically for fades, 
        /// when running, attempts to go 1/ms = 1000/s
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fadetimer_Tick(object sender, EventArgs e)
        {
            //increment timer ticking
            fadetimertime++;

            //update time/date during fade
            updateTimeDate();

            //fadeout related code

            #region Fade Out

            if (cD_tstate == timerstate.fadeout)
            {
                //computes amount of change in opacity per clock tick then applies it
                double double_change_in_opacity_per_tick = (Convert.ToDouble(OpacityLevel)) / Convert.ToDouble(int_fade_milliseconds) * 1 / 10;
                this.Opacity = (Convert.ToDouble(OpacityLevel) * 1 / 100) - (Convert.ToDouble(fadetimertime) * double_change_in_opacity_per_tick);

                if (this.Opacity <= 0)
                {
                    cD_tstate = timerstate.fadein;
                    this.Opacity = 0;
                    this.Visible = false;
                    fadetimertime = 0;
                    fadetimer.Stop();
                    //go to next image in slideshows
                    rotateCPic();
                }
            }

            #endregion Fade Out

            //fade in related code

            #region Fade In

            if (cD_tstate == timerstate.fadein)
            {
                //computes amount of change in opacity per clock tick then applies it
                double double_change_in_opacity_per_tick = (Convert.ToDouble(OpacityLevel)) / Convert.ToDouble(int_fade_milliseconds) * 1 / 10;
                this.Opacity = fadetimertime * double_change_in_opacity_per_tick;

                if (this.Opacity >= (Convert.ToDouble(OpacityLevel) * 1 / 100))
                {
                    cD_tstate = timerstate.indash; //We set the timerstate to indash to allow for timekeeping
                    this.Opacity = Convert.ToDouble(OpacityLevel) / 100;
                    fadetimertime = 0;
                    fadetimer.Stop();
                }
            }

            #endregion Fade In
        }

        /// <summary>
        /// updates the time and date visible in the form
        /// </summary>
        private void updateTimeDate()
        {
            //set time label
            button_time.Text = (DateTime.Now.ToString()).Substring(DateTime.Now.ToString().IndexOf(" ")).Trim();
            //set date label
            button_date.Text = (DateTime.Now.DayOfWeek.ToString()) + ", " + DateTime.Now.ToString("MMMMMMMMMMMMMM") + " " + DateTime.Now.Day.ToString() + ", " + DateTime.Now.Year.ToString();
        }

        /// <summary>
        /// rotates through images in all cPic slideshows
        /// </summary>
        private void rotateCPic()
        {
            //go through every cPic and randomly pick image from folder
            foreach (cPic this_cPic in this.Controls.OfType<cPic>())
            {
                try
                {
                    this_cPic.BackgroundImage.Dispose();
                }
                catch (Exception) { }

                //set cPic_new's background image equal to next in its folder
                try
                {
                    if (!this_cPic.dgv_sm.Visible)
                    {
                        Random r = new Random();
                        string[] files = System.IO.Directory.GetFiles(SETTINGS_LOCATION + this_cPic.Name);
                        if (files.Length < Convert.ToInt16(this_cPic.Tag) + 1)
                        {
                            this_cPic.Tag = 0;
                        }

                        this_cPic.Tag = Convert.ToInt16(this_cPic.Tag) + 1;

                        using (Image img = Image.FromFile(SETTINGS_LOCATION + this_cPic.Name + "\\" + this_cPic.Tag))
                        {
                            this_cPic.BackgroundImage = new Bitmap(img);
                            img.Dispose();
                        }
                    }
                }
                catch (Exception) { }
            }
        }

        #endregion


        #region Colored Sticky Creation

        /// <summary>
        /// add red sticky
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            makeNewSticky(Color.Red);
        }

        /// <summary>
        /// add orange sticky
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void orangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            makeNewSticky(Color.Orange);
        }

        /// <summary>
        /// add yellow sticky
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void yellowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            makeNewSticky(Color.Yellow);
        }

        /// <summary>
        /// add green sticky
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            makeNewSticky(Color.Green);
        }

        /// <summary>
        /// add blue sticky
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            makeNewSticky(Color.Blue);
        }

        /// <summary>
        /// add indigo sticky
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void indigoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            makeNewSticky(Color.Indigo);
        }

        /// <summary>
        /// add violet sticky
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void violetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            makeNewSticky(Color.Violet);
        }

        /// <summary>
        /// Makes new sticky of desired color
        /// </summary>
        /// <param name="pickedColor">The desired color</param>
        private void makeNewSticky(Color pickedColor)
        {
            //this code will add a new sticky to the form
            cSticky cSticky_new = new cSticky();
            cSticky_new.Location = new Point(10, 25);
            cSticky_new.Size = new Size(350, 400);
            cSticky_new.BackColor = pickedColor;

            //this will actually change the colors of the new form to match pickedColor
            ((MenuStrip)cSticky_new.Controls.Find("menustrip", false)[0]).BackColor = pickedColor;
            ((RichTextBox)cSticky_new.Controls.Find("rtb", false)[0]).BackColor = pickedColor;
            ((RichTextBox)cSticky_new.Controls.Find("rtb", false)[0]).Font = FavoriteStickyFont;
            cSticky_new.TopLevel = false;
            cSticky_new.Parent = this;
            Controls.Add(cSticky_new);
            cSticky_new.Show();
            cSticky_new.BringToFront();

            //represents the a unique time stamp for use as name of form / image
            long long_unique_timestamp = DateTime.Now.Ticks;
            cSticky_new.Name = long_unique_timestamp.ToString();

            //this code will add this new sticky to the settings file
            if (System.IO.File.Exists(SETTINGS_LOCATION + "cDash Settings.cDash"))
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(SETTINGS_LOCATION + "cDash Settings.cDash", true);
                sw.WriteLine("cSticky;" + long_unique_timestamp.ToString() + ";BackColor;" + cSticky_new.BackColor.R.ToString() + ";" + cSticky_new.BackColor.G.ToString() + ";" + cSticky_new.BackColor.B.ToString());
                sw.WriteLine("cSticky;" + long_unique_timestamp.ToString() + ";Location;" + cSticky_new.Location.X.ToString() + ";" + cSticky_new.Location.Y.ToString());
                sw.WriteLine("cSticky;" + long_unique_timestamp.ToString() + ";Size;" + cSticky_new.Size.Width.ToString() + ";" + cSticky_new.Size.Height.ToString());
                sw.WriteLine("cSticky;" + long_unique_timestamp.ToString() + ";FontStyle;" + ((RichTextBox)cSticky_new.Controls.Find("rtb", false)[0]).Font.Name.ToString() + ";" + ((RichTextBox)cSticky_new.Controls.Find("rtb", false)[0]).Font.Size.ToString());
                sw.Close();
            }
            else
            {
                //this error should never EVER be thrown
                MessageBox.Show("System.IO.Exception: File does not exist");
            }

            ((RichTextBox)cSticky_new.Controls.Find("rtb", false)[0]).SaveFile(SETTINGS_LOCATION + long_unique_timestamp.ToString() + ".rtf");
        }

        #endregion


        #region Calls to fade_out()

        /// <summary>
        /// double click the notify icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            //bring up dash if it is down, bring it down if it is up
            if (cD_tstate != timerstate.indash)
            {
                //  this.Focus();
                fade_in();
            }
            else
            {
                fade_out();
            }
        }

        /// <summary>
        /// hides the dash, just by calling fade_in()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fade_out();
        }

        /// <summary>
        /// Replacement for label_time
        /// single (not double) click fades the dash out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_time_Click(object sender, EventArgs e)
        {
            fade_out();
        }

        /// <summary>
        /// Replacement for button_date
        /// single (not double) click fades the dash out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_date_Click(object sender, EventArgs e)
        {
            fade_out();
        }

        /// <summary>
        /// fade out by clicking the dash (not on a cForm)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cDashboard_Click(object sender, EventArgs e)
        {
            fade_out();
        }
        #endregion


        #region Menustrip Items

        /// <summary>
        /// toggles the automatically check for updates setting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void automaticallyCheckForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            automaticallyCheckForUpdatesToolStripMenuItem.Checked = !automaticallyCheckForUpdatesToolStripMenuItem.Checked;

            if (automaticallyCheckForUpdatesToolStripMenuItem.Checked)
            {
                replaceSetting(new string[] { "cDash", "AutoUpdateCheck" }, new string[] { "cDash", "AutoUpdateCheck", "True" });
            }
            else
            {
                replaceSetting(new string[] { "cDash", "AutoUpdateCheck" }, new string[] { "cDash", "AutoUpdateCheck", "False" });
            }
        }

        /// <summary>
        /// adds ".0"'s onto the string to match format
        /// 1.1.4->1.1.4.0
        /// </summary>
        /// <param name="release_version"></param>
        /// <returns></returns>
        private string formatReleaseString(ref string release_version)
        {
            //add extra .0 onto the end of the version number to lengthen
            while (release_version.Count(f => f == '.') < 3)
            {
                release_version += ".0";
            }

            return release_version;
        }

        /// <summary>
        /// attempts to update via thread
        /// </summary>
        /// <param name="autocheck">true if this is an automatic (not user done) check</param>
        private void updateCheck(bool autocheck)
        {
            List<string> list_api_url = getSpecificSetting(new string[] { "cDash", "GitHubAPIReleaseURL" });

            if (list_api_url.Count == 0)
            {
                replaceSetting(new string[] { "cDash", "GitHubAPIReleaseURL" }, new string[] { "cDash", "GitHubAPIReleaseURL", "https://api.github.com/repos/csm10495/cDashboard/releases" });
                list_api_url.Add("https://api.github.com/repos/csm10495/cDashboard/releases");
            }

            List<Dictionary<string, dynamic>> dict = getDictFromJsonUrl(list_api_url[0]);

            //only fade if in dash
            if (cD_tstate == timerstate.indash) { fade_toggle(); }

            if (dict == null)
            {
                if (!autocheck)
                {
                    MessageBox.Show("Unable to check updates, you may be offline.");
                }
            }
            else
            {
                string release_version = ((string)dict[0]["tag_name"]).Substring(1);

                //add extra .0 onto the end of the version number to lengthen
                release_version = formatReleaseString(ref release_version);

                if (release_version != ProductVersion)
                {
                    string changelog = Environment.NewLine;

                    //populate changelog
                    foreach (var i in dict)
                    {
                        //don't go further back in time than necessary
                        string frs = formatReleaseString(i["tag_name"]);
                        if (frs.Substring(1) == ProductVersion)       //.Substring to get rid of the 'v'
                            break;

                        changelog = changelog + formatReleaseString(i["tag_name"]) + Environment.NewLine + i["body"] + Environment.NewLine + Environment.NewLine;
                    }

                    string update_string = "This update will take cDashboard from " + ProductVersion + " -> " + release_version + Environment.NewLine + "Changelog:" + Environment.NewLine + changelog + Environment.NewLine + "Would you like to update?";

                    cMessageBox cm = new cMessageBox(update_string, "cDashboard Update Available!");
                    DialogResult result = cm.cShowDialog();
                    cm.Close();
                    cm.Dispose(); //must be done because it is shown as a dialog
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            using (WebClient wc = new WebClient())
                            {
                                wc.DownloadFile(dict[0]["assets"][0]["browser_download_url"], "cDashboard_new.exe");
                            }

                            MessageBox.Show("Completed download, ready to update.", "cDashboard Update Ready!");
                            StreamWriter sw = new StreamWriter("cUpdate.bat", false);
                            string script = "@echo off" + Environment.NewLine + "echo cDashboard update in progress..." + Environment.NewLine + "timeout /t 3" + Environment.NewLine;
                            script += "del " + System.Reflection.Assembly.GetEntryAssembly().Location + Environment.NewLine;
                            script += "move /Y cDashboard_new.exe " + System.Reflection.Assembly.GetEntryAssembly().Location + Environment.NewLine;
                            script += "del cDashboard_new.exe" + Environment.NewLine;
                            script += "start " + System.Reflection.Assembly.GetEntryAssembly().Location + Environment.NewLine;
                            script += "start /b \"\" cmd /c del \"%~f0\"&exit /b";
                            sw.Write(script);
                            sw.Close();
                            System.Diagnostics.Process.Start("cUpdate.bat");

                            exitApplication();
                        }
                        catch
                        {
                            if (!autocheck)
                            {
                                MessageBox.Show("Unable to download update. You may be offline, please try again later.", "cDashboard");
                            }
                        }
                    }
                }
                else
                {
                    if (!autocheck)
                    {
                        MessageBox.Show("You have the latest version of cDashboard!", "cDashboard " + ProductVersion);
                    }
                }
            }
        }

        /// <summary>
        /// Check for updates via GitHub API
        /// The user is allowed to change the release url manually in the settings file
        /// if they want to follow a different repo, etc...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new System.Threading.Thread(() => updateCheck(false)).Start();
        }

        /// <summary>
        /// takes in a url and returns an object graph from the downloaded json
        /// </summary>
        /// <param name="url">url of json file</param>
        /// <returns></returns>
        private List<Dictionary<string, dynamic>> getDictFromJsonUrl(string url)
        {
            //predeclare before try/catch
            List<Dictionary<string, dynamic>> dict;
            WebClient client = new WebClient();

            //try/catch is used to detect connectivity
            //any catch should be the result of a lack of internet access
            try
            {
                //Tell it that this is an IE browser
                client.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; AS; rv:11.0) like Gecko";
                string string_url_text = client.DownloadString(url);
                dict = new JavaScriptSerializer().Deserialize<List<Dictionary<string, dynamic>>>(string_url_text);
            }
            catch
            {
                return null;
            }

            return dict;
        }

        /// <summary>
        /// show display time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void displayTimeToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            int display_time = -1;

            try
            {
                display_time = Convert.ToInt16(getSpecificSetting(new string[] { "cNotification", "DisplayTime" })[0]);
            }
            catch
            {
                replaceSetting(new string[] { "cNotification", "DisplayTime" }, new string[] { "cNotification", "DisplayTime", "5" });
                toolstrip_displaytime.Text = "5";
                return;
            }

            toolstrip_displaytime.Text = display_time.ToString();
        }

        /// <summary>
        ///  update display time in settings after doing verification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void displayTimeToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            int display_time = Convert.ToInt16(getSpecificSetting(new string[] { "cNotification", "DisplayTime" })[0]);
            int new_display_time = -1;

            try
            {
                new_display_time = Convert.ToInt16(toolstrip_displaytime.Text);
            }
            catch
            {
                MessageBox.Show("Display Time needs to be an integer greater than 1 and less than 11");
                return;
            }

            if (!(new_display_time >= 2 && new_display_time <= 10))
            {
                MessageBox.Show("Display Time needs to be an integer greater than 1 and less than 11");
                return;
            }

            replaceSetting(new string[] { "cNotification", "DisplayTime" }, new string[] { "cNotification", "DisplayTime", new_display_time.ToString() });
        }

        /// <summary>
        /// adds a new cRViewer to the dash
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newCRViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //represents the a unique time stamp for use as name of form
            long long_unique_timestamp = DateTime.Now.Ticks;

            //add the cRViewer to settings
            System.IO.StreamWriter sw = new System.IO.StreamWriter(SETTINGS_LOCATION + "cDash Settings.cDash", true);
            sw.WriteLine("cRViewer;" + long_unique_timestamp.ToString() + ";Location;10;25");
            sw.Close();

            cRViewer cRViewer_new = new cRViewer();
            cRViewer_new.Name = long_unique_timestamp.ToString();
            cRViewer_new.Location = new Point(10, 25);
            cRViewer_new.TopLevel = false;
            cRViewer_new.Parent = this;

            Controls.Add(cRViewer_new);
            cRViewer_new.Show();
            cRViewer_new.BringToFront();
        }

        /// <summary>
        /// add cReminder to the dash
        /// NOTE: this is not saved in settings as it is a setup form, not persistent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newCReminderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //represents the a unique time stamp for use as name of form
            long long_unique_timestamp = DateTime.Now.Ticks;

            cReminder cReminder_new = new cReminder();
            cReminder_new.Name = long_unique_timestamp.ToString();
            cReminder_new.Location = new Point(10, 25);
            cReminder_new.TopLevel = false;
            cReminder_new.Parent = this;

            Controls.Add(cReminder_new);
            cReminder_new.Show();
            cReminder_new.BringToFront();
        }

        /// <summary>
        /// creates an instance of cMote as a child form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newCMoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //represents the a unique time stamp for use as name of form
            long long_unique_timestamp = DateTime.Now.Ticks;

            //add the cMote to settings
            System.IO.StreamWriter sw = new System.IO.StreamWriter(SETTINGS_LOCATION + "cDash Settings.cDash", true);
            sw.WriteLine("cMote;" + long_unique_timestamp.ToString() + ";Location;10;25");
            sw.WriteLine("cMote;" + long_unique_timestamp.ToString() + ";SpotifyIntegration;False");
            sw.Close();

            cMote cMote_new = new cMote();
            cMote_new.Name = long_unique_timestamp.ToString();
            cMote_new.Location = new Point(10, 25);
            cMote_new.TopLevel = false;
            cMote_new.Parent = this;

            Controls.Add(cMote_new);
            cMote_new.Show();
            cMote_new.BringToFront();
        }

        /// <summary>
        /// creates a new cBattery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newCBatteryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //represents the a unique time stamp for use as name of form
            long long_unique_timestamp = DateTime.Now.Ticks;

            cBattery cBattery_new = new cBattery();
            cBattery_new.Name = long_unique_timestamp.ToString();
            cBattery_new.Location = new Point(10, 25);
            cBattery_new.TopLevel = false;
            cBattery_new.Parent = this;

            Controls.Add(cBattery_new);
            cBattery_new.Show();
            cBattery_new.BringToFront();

            //add the cBattery to settings
            System.IO.StreamWriter sw = new System.IO.StreamWriter(SETTINGS_LOCATION + "cDash Settings.cDash", true);
            sw.WriteLine("cBattery;" + long_unique_timestamp.ToString() + ";Location;10;25");
            sw.Close();
        }

        /// <summary>
        /// Toggles BoardlessMode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void boardlessModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            boardlessModeToolStripMenuItem.Checked = !boardlessModeToolStripMenuItem.Checked;

            if (boardlessModeToolStripMenuItem.Checked)
                goBoardless();
            else
                goNotBoardless();
        }

        /// <summary>
        /// Toggles a transparent background
        /// </summary>
        private void goBoardless()
        {
            if (this.BackgroundImage != null)
            {
                this.BackgroundImage.Dispose();
                this.BackgroundImage = null;
            }
            this.BackColor = Color.Fuchsia;
            this.TransparencyKey = Color.Fuchsia;

            button_date.Visible = false;
            button_time.Visible = false;

            boardlessModeToolStripMenuItem.Checked = true;
        }

        /// <summary>
        /// Toggles a non-transparent background
        /// </summary>
        private void goNotBoardless()
        {
            if (UseWallpaperImage)
            {
                string layout = getSpecificSetting(new string[] { "cDash", "WallpaperImageLayout" })[0];

                if (layout == "Center")
                {
                    this.BackgroundImageLayout = ImageLayout.Center;
                }
                else if (layout == "None")
                {
                    this.BackgroundImageLayout = ImageLayout.None;
                }
                else if (layout == "Stretch")
                {
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }
                else if (layout == "Tile")
                {
                    this.BackgroundImageLayout = ImageLayout.Tile;
                }
                else if (layout == "Zoom")
                {
                    this.BackgroundImageLayout = ImageLayout.Zoom;
                }

                this.BackgroundImage = Image.FromFile(getSpecificSetting(new string[] { "cDash", "WallpaperImage" })[0]);
            }

            List<string> list_backcolor = getSpecificSetting(new string[] { "cDash", "BackColor" });
            this.BackColor = Color.FromArgb(Convert.ToInt16(list_backcolor[0]), Convert.ToInt16(list_backcolor[1]), Convert.ToInt16(list_backcolor[2]));

            button_time.Visible = true;
            button_date.Visible = true;
            this.TransparencyKey = Color.Empty;
            boardlessModeToolStripMenuItem.Checked = false;
        }

        /// <summary>
        /// Whenever boardless mode is activated/deactivated this will be called to save the
        /// boardless mode setting to the settings file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void boardlessModeToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (boardlessModeToolStripMenuItem.Checked)
                replaceSetting(new string[] { "cDash", "BoardlessMode" }, new string[] { "cDash", "BoardlessMode", "True" });
            else
                replaceSetting(new string[] { "cDash", "BoardlessMode" }, new string[] { "cDash", "BoardlessMode", "False" });
        }

        /// <summary>
        /// Change cWeather unit to °F
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            celciusToolStripMenuItem.Checked = false;
            fToolStripMenuItem.Checked = true;

            replaceSetting(new string[] { "cDash", "cWeather", "Unit" }, new string[] { "cDash", "cWeather", "Unit", "F" });

            //force refresh
            TimerCounter = 99999;
        }

        /// <summary>
        /// Change cWeather unit to °C
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            fToolStripMenuItem.Checked = false;
            celciusToolStripMenuItem.Checked = true;

            replaceSetting(new string[] { "cDash", "cWeather", "Unit" }, new string[] { "cDash", "cWeather", "Unit", "C" });

            //force refresh
            TimerCounter = 99999;
        }

        /// <summary>
        /// create new cWeather widget
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newCWeatherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //represents the a unique time stamp for use as name of form
            long long_unique_timestamp = DateTime.Now.Ticks;

            cWeather cWeather_new = new cWeather("NULL");
            cWeather_new.Name = long_unique_timestamp.ToString();
            cWeather_new.Location = new Point(10, 25);
            cWeather_new.TopLevel = false;
            cWeather_new.Parent = this;

            Controls.Add(cWeather_new);
            cWeather_new.Show();
            cWeather_new.BringToFront();

            //add the cWeather to settings
            System.IO.StreamWriter sw = new System.IO.StreamWriter(SETTINGS_LOCATION + "cDash Settings.cDash", true);
            sw.WriteLine("cWeather;" + long_unique_timestamp.ToString() + ";WOEID;" + "NULL");
            sw.WriteLine("cWeather;" + long_unique_timestamp.ToString() + ";Location;10;25");
            sw.Close();
        }

        /// <summary>
        /// Set date/time/strip color as displayed in Dash
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setDateTimeColorToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //show the color selection dialog
            DialogResult result = colorDialog1.ShowDialog();

            List<string> find = new List<string>();
            List<string> replace = new List<string>();
            find.Add("cDash");
            find.Add("DateTimeStripColor");
            replace.Add("cDash");
            replace.Add("DateTimeStripColor");

            //if the user clicks Ok, set the color, otherwise cancel
            if (result == DialogResult.OK)
            {
                FavoriteStickyColor = colorDialog1.Color;
                replace.Add(colorDialog1.Color.R.ToString());
                replace.Add(colorDialog1.Color.G.ToString());
                replace.Add(colorDialog1.Color.B.ToString());
                replaceSetting(find, replace);
            }

            button_time.ForeColor = Color.FromArgb(colorDialog1.Color.R, colorDialog1.Color.G, colorDialog1.Color.B);
            button_date.ForeColor = Color.FromArgb(colorDialog1.Color.R, colorDialog1.Color.G, colorDialog1.Color.B);
            menuStrip1.ForeColor = Color.FromArgb(colorDialog1.Color.R, colorDialog1.Color.G, colorDialog1.Color.B);
        }

        /// <summary>
        /// called to set a wallpaper as the background of the dash as opposed to simply a color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setCDashWallpaperImageToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //represents the a unique time stamp for use as name of form / image
            long long_unique_timestamp = DateTime.Now.Ticks;

            //force multiselect to be false
            openFileDialog1.Multiselect = false;

            //setup the OpenFileDialog to only accept images
            openFileDialog1.Filter = "Image Files (*.bmp, *.jpg, *.png, *.tiff, *.gif)|*.bmp;*.jpg;*.png;*.tiff;*.gif";

            //change title of openFileDialog1
            openFileDialog1.Title = "Select wallpaper for cDashboard...";

            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                //remove wallpaper image if it exists
                string string_old_file_location = getSpecificSetting(new string[] { "cDash", "WallpaperImage" })[0];
                if (System.IO.File.Exists(string_old_file_location))
                {
                    if (this.BackgroundImage != null)
                    {
                        this.BackgroundImage.Dispose();
                        this.BackgroundImage = null;
                    }
                    System.IO.File.Delete(string_old_file_location);
                }

                System.IO.File.Copy(openFileDialog1.FileName, SETTINGS_LOCATION + openFileDialog1.SafeFileName, false);
                System.IO.File.Move(SETTINGS_LOCATION + openFileDialog1.SafeFileName, SETTINGS_LOCATION + long_unique_timestamp.ToString() + System.IO.Path.GetExtension(openFileDialog1.FileName));

                //set image
                this.BackgroundImage = Image.FromFile(SETTINGS_LOCATION + long_unique_timestamp.ToString() + System.IO.Path.GetExtension(openFileDialog1.FileName));

                //we are using a wallpaper image
                //set as such
                UseWallpaperImage = true;
                replaceSetting(new string[] { "cDash", "Wallpaper" }, new string[] { "cDash", "Wallpaper", "True" });
                replaceSetting(new string[] { "cDash", "WallpaperImage" }, new string[] { "cDash", "WallpaperImage", SETTINGS_LOCATION + long_unique_timestamp.ToString() + System.IO.Path.GetExtension(openFileDialog1.FileName) });

                goNotBoardless();
                boardlessModeToolStripMenuItem.Checked = false;
            }
        }

        /// <summary>
        /// add a new cStopwatch cForm to the cDash
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newCStopwatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //represents the a unique time stamp for use as name of form
            long long_unique_timestamp = DateTime.Now.Ticks;

            //add the cStopwatch to settings
            System.IO.StreamWriter sw = new System.IO.StreamWriter(SETTINGS_LOCATION + "cDash Settings.cDash", true);
            sw.WriteLine("cStopwatch;" + long_unique_timestamp.ToString() + ";Location;10;25");
            sw.WriteLine("cStopwatch;" + long_unique_timestamp.ToString() + ";StartDateTime;NULL");
            sw.WriteLine("cStopwatch;" + long_unique_timestamp.ToString() + ";TimerRunning;False");
            sw.WriteLine("cStopwatch;" + long_unique_timestamp.ToString() + ";EndDateTime;NULL");
            sw.WriteLine("cStopwatch;" + long_unique_timestamp.ToString() + ";Laps");
            sw.WriteLine("cStopwatch;" + long_unique_timestamp.ToString() + ";LapTime;NULL");
            sw.Close();

            cStopwatch cStopwatch_new = new cStopwatch();
            cStopwatch_new.Name = long_unique_timestamp.ToString();
            cStopwatch_new.Location = new Point(10, 25);
            cStopwatch_new.TopLevel = false;
            cStopwatch_new.Parent = this;

            Controls.Add(cStopwatch_new);
            cStopwatch_new.Show();
            cStopwatch_new.BringToFront();
        }

        /// <summary>
        /// exports cDashBoard Settings folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = folderBrowserDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //if the user clicks ok
                if (System.IO.Directory.Exists(SETTINGS_LOCATION))
                {
                    //create directory
                    System.IO.Directory.CreateDirectory(folderBrowserDialog1.SelectedPath + "\\cDashBoard");

                    //copy files
                    foreach (string file in System.IO.Directory.GetFiles(SETTINGS_LOCATION))
                    {
                        //represents file name without a path
                        string str_nopath_file = file.Substring(file.LastIndexOf("\\") + 1);
                        System.IO.File.Copy(file, folderBrowserDialog1.SelectedPath + "\\cDashBoard\\" + str_nopath_file);
                    }
                }
                MessageBox.Show("Data exported successfully");
            }
        }

        /// <summary>
        /// imports cDashBoard Settings folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = folderBrowserDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //basic check to see if folder has "cDash Settings.cDash"
                bool bool_found_cdash_file = false;
                foreach (string file in System.IO.Directory.GetFiles(folderBrowserDialog1.SelectedPath))
                {
                    //represents file name without a path
                    string str_nopath_file = file.Substring(file.LastIndexOf("\\") + 1);
                    if (str_nopath_file == "cDash Settings.cDash")
                    {
                        bool_found_cdash_file = true;
                    }
                }
                if (!bool_found_cdash_file)
                {
                    MessageBox.Show("Folder is not an appropriate cDashBoard folder");
                    return;
                }

                //if the user clicks ok
                if (System.IO.Directory.Exists(SETTINGS_LOCATION))
                {
                    //copy files
                    foreach (string file in System.IO.Directory.GetFiles(folderBrowserDialog1.SelectedPath))
                    {
                        //get file without path
                        string str_nopath_file = file.Substring(file.LastIndexOf("\\") + 1);
                        System.IO.File.Copy(file, SETTINGS_LOCATION + str_nopath_file, true);
                    }
                }
                //reload the form to represent changes in files
                Form1_Load(this, null);
            }
        }

        /// <summary>
        /// fades out the dash and then shows the about box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fade_out();
            Form about = new cAbout();
            about.Show();
        }

        /// <summary>
        /// exits application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exitApplication();
        }

        /// <summary>
        /// Allows the creation of any color sticky
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //show the color selection dialog
            DialogResult result = colorDialog1.ShowDialog();

            //if the user clicks Ok, make the sticky, otherwise cancel
            if (result == DialogResult.OK)
            {
                makeNewSticky(colorDialog1.Color);
            }
        }

        /// <summary>
        /// sets the back color of the dash, and saves it to settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setCDashBackcolorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //show the color selection dialog
            DialogResult result = colorDialog1.ShowDialog();

            //if the user clicks Ok, make the sticky, otherwise cancel
            if (result == DialogResult.OK)
            {
                this.BackColor = colorDialog1.Color;
                //removed for transparent color
                //menuStrip1.BackColor = this.BackColor;
                List<List<string>> list_settings = getSettingsList();

                foreach (List<string> currentline in list_settings)
                {
                    if (currentline[0] == "cDash" && currentline[1] == "BackColor")
                    {
                        list_settings.Remove(currentline);
                        break;
                    }
                }
                List<string> line_to_be_added = new List<string>();
                line_to_be_added.Add("cDash");
                line_to_be_added.Add("BackColor");
                line_to_be_added.Add(this.BackColor.R.ToString());
                line_to_be_added.Add(this.BackColor.G.ToString());
                line_to_be_added.Add(this.BackColor.B.ToString());

                list_settings.Add(line_to_be_added);
                saveSettingsList(list_settings);

                //special handling if a wallpaper had been used before
                if (UseWallpaperImage)
                {
                    //remove image if currently in use
                    if (this.BackgroundImage != null)
                    {
                        this.BackgroundImage.Dispose();
                        this.BackgroundImage = null;
                    }

                    //remove wallpaper image
                    System.IO.File.Delete(getSpecificSetting(new string[] { "cDash", "WallpaperImage" })[0]);

                    //we are not using a wallpaper image
                    //set as such
                    UseWallpaperImage = false;
                    replaceSetting(new string[] { "cDash", "Wallpaper" }, new string[] { "cDash", "Wallpaper", "False" });
                    replaceSetting(new string[] { "cDash", "WallpaperImage" }, new string[] { "cDash", "WallpaperImage", "NULL" });
                }
                goNotBoardless();
                boardlessModeToolStripMenuItem.Checked = false;
            }
        }

        /// <summary>
        /// exit application from notify icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitCDashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exitApplication();
        }

        /// <summary>
        /// called when this drop down menu is opened
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setOpacityToolStripMenuItem_DropDownOpened_1(object sender, EventArgs e)
        {
            //make sure it displays the current opacity
            textbox_opacity.Text = OpacityLevel.ToString();
        }

        /// <summary>
        /// called when this drop down menu is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setOpacityToolStripMenuItem_DropDownClosed_1(object sender, EventArgs e)
        {
            int tmp_OpacityLevel = -1;

            //if the casting fails, then break gracefully
            try
            {
                tmp_OpacityLevel = Convert.ToInt32(textbox_opacity.Text);
            }
            catch
            {
                MessageBox.Show("The opacity must be an integer!");
                return;
            }

            //if the user sets this to low, it could be hard to use/see
            if (tmp_OpacityLevel <= 15)
            {
                MessageBox.Show("The Opacity level must be greater than 15 and less than 101");
                textbox_opacity.Text = OpacityLevel.ToString();
                return;
            }

            //any number over 99 will save as 100
            if (tmp_OpacityLevel > 99)
            {
                tmp_OpacityLevel = 100;
            }

            OpacityLevel = tmp_OpacityLevel;

            this.Opacity = Convert.ToDouble(OpacityLevel) / 100;

            //replace the setting
            List<string> find = new List<string>();
            List<string> replace = new List<string>();

            find.Add("cDash");
            replace.Add("cDash");
            find.Add("Opacity");
            replace.Add("Opacity");
            replace.Add(OpacityLevel.ToString());
            replaceSetting(find, replace);
        }

        /// <summary>
        /// make a new sticky of the user's favorite color
        /// ask user to pick color if it is still null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void favoriteColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> find = new List<string>();
            List<string> replace = new List<string>();
            find.Add("cDash");
            find.Add("FavoriteStickyColor");
            replace.Add("cDash");
            replace.Add("FavoriteStickyColor");

            //still need to set favorite color
            if (getSpecificSetting(find)[0] == "NULL")
            {
                //show the color selection dialog
                DialogResult result = colorDialog1.ShowDialog();

                //if the user clicks Ok, set the color, otherwise cancel
                if (result == DialogResult.OK)
                {
                    FavoriteStickyColor = colorDialog1.Color;
                    makeNewSticky(FavoriteStickyColor);
                    replace.Add(FavoriteStickyColor.R.ToString());
                    replace.Add(FavoriteStickyColor.G.ToString());
                    replace.Add(FavoriteStickyColor.B.ToString());
                    replaceSetting(find, replace);
                }
            }
            else
            {
                makeNewSticky(FavoriteStickyColor);
            }
        }

        /// <summary>
        /// set favorite sticky color w/o making a new sticky
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void favoriteColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //show the color selection dialog
            DialogResult result = colorDialog1.ShowDialog();

            List<string> find = new List<string>();
            List<string> replace = new List<string>();
            find.Add("cDash");
            find.Add("FavoriteStickyColor");
            replace.Add("cDash");
            replace.Add("FavoriteStickyColor");

            //if the user clicks Ok, set the color, otherwise cancel
            if (result == DialogResult.OK)
            {
                FavoriteStickyColor = colorDialog1.Color;
                replace.Add(FavoriteStickyColor.R.ToString());
                replace.Add(FavoriteStickyColor.G.ToString());
                replace.Add(FavoriteStickyColor.B.ToString());
                replaceSetting(find, replace);
            }
        }

        /// <summary>
        /// set favorite sticky font
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void favoriteFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = false;

            DialogResult result = fontDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                FavoriteStickyFont = fontDialog1.Font;

                List<string> find = new List<string>();
                List<string> replace = new List<string>();
                find.Add("cDash");
                find.Add("FavoriteStickyFontName");
                replace.Add("cDash");
                replace.Add("FavoriteStickyFontName");
                replace.Add(FavoriteStickyFont.Name.ToString());
                replaceSetting(find, replace);
                find[1] = "FavoriteStickyFontSize";
                replace[1] = "FavoriteStickyFontSize";
                replace[2] = FavoriteStickyFont.Size.ToString();
                replaceSetting(find, replace);
            }

            fontDialog1.ShowColor = true;
        }

        /// <summary>
        /// called when the textbox for editting fade time is made visible
        /// aka when the drop down is opened
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setInMillisecondsToolStripMenuItem_DropDownOpened_1(object sender, EventArgs e)
        {
            //set textbox text equal to saved fade length
            textbox_fadetime.Text = int_fade_milliseconds.ToString();
        }

        /// <summary>
        /// save fade length setting when the menu item is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setInMillisecondsToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            int old_int_fade_milliseconds = int_fade_milliseconds; //hold value

            //if the casting fails, then break gracefully
            try
            {
                int_fade_milliseconds = Convert.ToInt32(textbox_fadetime.Text);
            }
            catch
            {
                MessageBox.Show("The fade time must be an integer greater than zero and less than 2000!");
                return;
            }

            if (int_fade_milliseconds <= 0 || int_fade_milliseconds > 2000)
            {
                MessageBox.Show("The fade time must be an integer greater than zero and less than 2000!");
                int_fade_milliseconds = old_int_fade_milliseconds;
                return;
            }

            //replace the setting
            List<string> find = new List<string>();
            List<string> replace = new List<string>();

            find.Add("cDash");
            replace.Add("cDash");
            find.Add("FadeLengthInMilliseconds");
            replace.Add("FadeLengthInMilliseconds");
            replace.Add(int_fade_milliseconds.ToString());
            replaceSetting(find, replace);
        }

        /// <summary>
        /// Creates a new cPic child form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newCPicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //represents the a unique time stamp for use as name of form / image
            long long_unique_timestamp = DateTime.Now.Ticks;

            //set multiselect to true
            openFileDialog1.Multiselect = true;

            //setup the OpenFileDialog to only accept images
            openFileDialog1.Filter = "Image Files (*.bmp, *.jpg, *.png, *.tiff, *.gif)|*.bmp;*.jpg;*.png;*.tiff;*.gif";

            //change title of openFileDialog1
            openFileDialog1.Title = "Select image(s) for cPic...";

            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                //make directory
                System.IO.Directory.CreateDirectory(SETTINGS_LOCATION + long_unique_timestamp.ToString());

                foreach (string file in openFileDialog1.FileNames)
                {
                    System.IO.File.Copy(file, SETTINGS_LOCATION + file.Substring(file.LastIndexOf("\\") + 1), false);

                    //sleeping for 1 millisecond eliminates chance of multiple completion on same tick
                    System.Threading.Thread.Sleep(1);

                    System.IO.File.Move(SETTINGS_LOCATION + file.Substring(file.LastIndexOf("\\") + 1), SETTINGS_LOCATION + long_unique_timestamp.ToString() + "\\" + DateTime.Now.Ticks.ToString());
                }
                cPic cPic_new = new cPic();
                cPic_new.Name = long_unique_timestamp.ToString();
                cPic_new.Location = new Point(10, 25);
                cPic_new.Size = new Size(350, 400);
                cPic_new.TopLevel = false;
                cPic_new.Parent = this;
                cPic_new.Tag = 1;

                //properly name, randomize files
                randomizeFiles(cPic_new.Name);

                //set cPic_new's background image equal to image number 1 from the folder
                cPic_new.BackgroundImage = Image.FromFile(SETTINGS_LOCATION + cPic_new.Name + "\\1");

                //changed default behavior to zoom
                cPic_new.BackgroundImageLayout = ImageLayout.Zoom;

                Controls.Add(cPic_new);
                cPic_new.Show();
                cPic_new.BringToFront();

                //add the cPic to settings
                System.IO.StreamWriter sw = new System.IO.StreamWriter(SETTINGS_LOCATION + "cDash Settings.cDash", true);
                sw.WriteLine("cPic;" + long_unique_timestamp.ToString() + ";FolderName;" + long_unique_timestamp.ToString());
                sw.WriteLine("cPic;" + long_unique_timestamp.ToString() + ";Size;350;400");
                sw.WriteLine("cPic;" + long_unique_timestamp.ToString() + ";Location;10;25");
                sw.WriteLine("cPic;" + long_unique_timestamp.ToString() + ";ImageLayout;Zoom");
                sw.Close();
            }
        }

        /// <summary>
        /// changing the selected index changes the image layout of the wallpaper
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void combobox_wallpaper_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string string_layout = "";

            if (combobox_wallpaper.SelectedIndex == 0)
            {
                this.BackgroundImageLayout = ImageLayout.Center;
                string_layout = "Center";
            }
            else if (combobox_wallpaper.SelectedIndex == 1)
            {
                this.BackgroundImageLayout = ImageLayout.None;
                string_layout = "None";
            }
            else if (combobox_wallpaper.SelectedIndex == 2)
            {
                this.BackgroundImageLayout = ImageLayout.Stretch;
                string_layout = "Stretch";
            }
            else if (combobox_wallpaper.SelectedIndex == 3)
            {
                this.BackgroundImageLayout = ImageLayout.Tile;
                string_layout = "Tile";
            }
            else if (combobox_wallpaper.SelectedIndex == 4)
            {
                this.BackgroundImageLayout = ImageLayout.Zoom;
                string_layout = "Zoom";
            }

            replaceSetting(new[] { "cDash", "WallpaperImageLayout" }, new[] { "cDash", "WallpaperImageLayout", string_layout });
        }

        /// <summary>
        /// reflects proper setting in combobox_wallpaper
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cDashWallpaperToolStripMenuItem_DropDownOpened_1(object sender, EventArgs e)
        {
            if (this.BackgroundImageLayout == ImageLayout.Center)
            {
                combobox_wallpaper.SelectedIndex = 0;
            }
            else if (this.BackgroundImageLayout == ImageLayout.None)
            {
                combobox_wallpaper.SelectedIndex = 1;
            }
            else if (this.BackgroundImageLayout == ImageLayout.Stretch)
            {
                combobox_wallpaper.SelectedIndex = 2;
            }
            else if (this.BackgroundImageLayout == ImageLayout.Tile)
            {
                combobox_wallpaper.SelectedIndex = 3;
            }
            else if (this.BackgroundImageLayout == ImageLayout.Zoom)
            {
                combobox_wallpaper.SelectedIndex = 4;
            }
        }

        /// <summary>
        /// Class with antialiasing to get rid of fuschia artifacts on File, Edit
        /// </summary>
        public class cToolStripMenuItem : ToolStripMenuItem
        {
            protected override void OnPaint(PaintEventArgs e)
            {
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                base.OnPaint(e);
            }
        }

        #endregion


        #region Extra Events

        /// <summary>
        /// called if a control is removed
        /// ex: if the x button is clicked on a child from, handle deleting the cForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_ControlRemoved(object sender, ControlEventArgs e)
        {
            Control this_control = e.Control;

            List<List<string>> list_settings = getSettingsList();

            //special deletion of associated files (cPic, cSticky)
            if (this_control.GetType() == typeof(cSticky))
            {
                int int_len = this_control.Name.Length;

                foreach (string file in System.IO.Directory.GetFiles(SETTINGS_LOCATION))
                {
                    //f is the name of the file with extension
                    string f = file.Substring(file.LastIndexOf("\\") + 1);

                    //ignore files with short names
                    if (f.Length < int_len)
                        continue;

                    string f2 = f.Substring(0, int_len);

                    if (f2 == this_control.Name)
                    {
                        System.IO.File.Delete(file);
                        break;
                    }
                }
            }

            //special handling for cPic
            if (this_control.GetType() == typeof(cPic))
            {
                //manual memory management
                //will throw exception if already disposed
                try { this_control.BackgroundImage.Dispose(); }
                catch (Exception) { }
                ((cPic)this_control).dgv_sm.Dispose();
                //delete folder
                System.IO.Directory.Delete(SETTINGS_LOCATION + this_control.Name, true);
            }

            //This should be here instead of up there, just in case we need to know a setting to delete the control
            for (int x = list_settings.Count - 1; x >= 0; x--)
            {
                List<string> currentsetting = list_settings[x];
                if (currentsetting[0].Substring(0, 1) == "#")   //fixed problem with comment persistance
                    continue;

                //this has to do with the control to be removed
                //make sure that there are enough options in currentsetting
                if (currentsetting.Count > 1 && currentsetting[1] == this_control.Name)
                {
                    list_settings.RemoveAt(x);
                }
            }

            //remove control
            Controls.Remove(this_control);
            this_control.Dispose();
            //save fixed settings
            saveSettingsList(list_settings);
        }

        /// <summary>
        /// if the program is closing, this is called
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //cleanly removes the notify icon from the system tray
            notifyIcon1.Visible = false;
        }

        /// <summary>
        /// should make it so the time and date buttons won't get unnecessary focus
        /// this would have happened if dash is on monitor 1 and user moves mouse to 2 while dash is up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cDashboard_MouseLeave(object sender, EventArgs e)
        {
            menuStrip1.Focus();
        }

        #endregion Extra Events

        private void PluginSaveTimer_Tick(object sender, EventArgs e)
        {
            foreach(var i in pluginTypes.Values)
            {
                i.SavePlugin(SETTINGS_LOCATION);
            }
        }

    }
}
