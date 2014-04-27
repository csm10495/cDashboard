//cDashboard - An overlay for Microsoft Windows
//(C) Charles Machalow 2014 under the MIT License
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;
using System.Diagnostics;



namespace cDashboard
{
    public partial class cDashboard : cForm
    {
        #region Global Variables
        /// <summary>
        /// this is the tick counter for timer1
        /// </summary>
        int timertime = 0;

        /// <summary>
        /// the amount of time in milliseconds needed for fade_in, fade_out
        /// </summary>
        int int_fade_milliseconds = 500;

        /// <summary>
        /// the state of the timer can be fading in, out, indash.
        /// </summary>
        enum timerstate { fadein = 0, fadeout = 1, indash = 2 };

        /// <summary>
        /// The current state of the main timer it can be fadein, fadeout, or indash
        /// </summary>
        timerstate cD_tstate = (int)timerstate.fadein;//the first state of the timer is fadein

        /// <summary>
        /// the keyboard hook used to catch key presses for certain keys
        /// </summary>
        globalKeyboardHook KeyHook = new globalKeyboardHook(); //KeyHook is the global key hook

        /// <summary>
        /// Signifies if the LCtrl key is down
        /// </summary>
        bool LCtrlIsDown = false;

        /// <summary>
        /// Signifies if the tilde key is down
        /// </summary>
        bool TildeIsDown = false;

        /// <summary>
        /// This will be changed to true after the form load is completed to allow stickies to be saved without conflicting locks
        /// </summary>
        bool CompletedForm_Load = false;

        /// <summary>
        /// opacity level of the dashboard
        /// </summary>
        int OpacityLevel = -1;

        /// <summary>
        /// favorite color for new stickies
        /// </summary>
        Color FavoriteStickyColor;

        /// <summary>
        /// favorite font for new stickies
        /// </summary>
        Font FavoriteStickyFont;

        #endregion

        #region Constructor
        /// <summary>
        /// Form Constructor
        /// </summary>
        public cDashboard()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Loading, Initial Setup
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

                if (int_cDash_processes_found > 1)
                {
                    MessageBox.Show("Another instance of cDashboard is already running.");
                    Application.Exit();
                }
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

            variable_setup(); //setup variables1
            // this.Focus(); //This makes it so the text is not edited by pressing keys after startup (while invisible)

            //create appdata directory
            if (!System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard"))
                System.IO.Directory.CreateDirectory((Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard"));

            //File Building
            buildAndSettingsFileCreation();

            //this will be the list of lines from the settings file 
            //THE settings_list WILL NOT INCLUDE BLANK LINES OR LINES STARTING WITH #
            List<List<string>> settings_list = getSettingsList();


            //Read settings and create stickies
            createStickiesFromFiles(settings_list);

            //Read settings not pertaining to stickies
            otherSettings(settings_list);

            CompletedForm_Load = true;
        }

        /// <summary>
        /// Will read settings not directly related to stickies
        /// </summary>
        /// <param name="settings_list"></param>
        private void otherSettings(List<List<string>> settings_list)
        {
            string FSFN = ""; //Favorite Sticky Font Name
            float FSFS = -1; //Favorite Sticky Font Size

            foreach (List<string> currentline in settings_list)
            {
                //Most global cDash settings

                //set backcolor of the Dash from settings
                if (currentline[0] == "cDash")
                {
                    if (currentline[1] == "BackColor")
                    {
                        this.BackColor = Color.FromArgb(Convert.ToInt32(currentline[2]), Convert.ToInt32(currentline[3]), Convert.ToInt32(currentline[4]));
                        menuStrip1.BackColor = this.BackColor;
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
                }
                else if (currentline[0] == "cPic")
                {
                    //this would mean that this form already exists
                    if (!(Controls.Find(currentline[1], true).Length > 0))
                    {
                        cPic cPic_new = new cPic();
                        cPic_new.Name = currentline[1];
                        cPic_new.BackgroundImage = Image.FromFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\" + getSpecificSetting(new[] { "cPic", currentline[1], "FileName" })[0]);
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
            //move dash to set monitor
            moveToPrimaryMonitor();
        }

        /// <summary>
        /// Create the stickies from the settings file
        /// </summary>
        private void createStickiesFromFiles(List<List<string>> settings_list)
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
                if (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\" + cSticky_new.Name + ".rtf"))
                {
                    ((RichTextBox)cSticky_new.Controls.Find("rtb", false)[0]).LoadFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\" + cSticky_new.Name + ".rtf");
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
                if (!System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\BuildData.cDash"))
                {
                    //creates build data
                    //at this point this number doesn't mean much :P
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\BuildData.cDash");
                    sw.Write("144");
                    sw.Close();
                    label_build.Text = "cDashBoard Alpha Build 144";
                }
                else
                {
                    //read build number
                    System.IO.StreamReader sr = new System.IO.StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\BuildData.cDash");
                    int buildnum = Convert.ToInt32(sr.ReadToEnd().ToString());
                    sr.Close();

                    //increment build number
                    buildnum++;

                    //write incremented build number
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\BuildData.cDash");
                    sw.Write(buildnum.ToString());
                    sw.Close();

                    //display build number
                    label_build.Text = "cDashBoard Alpha Build " + buildnum;
                }
                #endregion
            }

            //create the settings file
            if (!System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\cDash Settings.cDash"))
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\cDash Settings.cDash");
                sw.WriteLine("# cDashBoard Settings File");
                sw.WriteLine("# A # tells the program to ignore this line");
                sw.WriteLine("# Don't edit this file unless you know what you are doing");
                //set default opacity
                sw.WriteLine("cDash;Opacity;85");
                sw.WriteLine("cDash;FavoriteStickyColor;NULL");
                sw.WriteLine("cDash;FavoriteStickyFontName;Arial Black");
                sw.WriteLine("cDash;FavoriteStickyFontSize;12");
                sw.WriteLine("cDash;FadeLengthInMilliseconds;500");
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
            //goes through every cSticky
            foreach (cSticky current_csticky in this.Controls.OfType<cSticky>())
            {
                //make sure that cStickies are not lost to resizing the dash
                if (current_csticky.Location.X > this.Location.X + this.Size.Width)
                {
                    int form_widths = this.Width + current_csticky.Width;
                    int total_width = current_csticky.Location.X + current_csticky.Width;
                    current_csticky.Location = new Point(total_width - form_widths - 10, current_csticky.Location.Y);
                }
                if (current_csticky.Location.Y > this.Location.Y + this.Size.Height)
                {
                    int form_height = this.Height + current_csticky.Height;
                    int total_height = current_csticky.Location.Y + current_csticky.Height;
                    current_csticky.Location = new Point(current_csticky.Location.X, total_height - form_height - 10);
                }
                if (current_csticky.Location.X < 0)
                {
                    current_csticky.Location = new Point(0, current_csticky.Location.Y);
                }
                if (current_csticky.Location.Y < 0)
                {
                    current_csticky.Location = new Point(current_csticky.Location.X, 0);
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
        private void setCDashDefaultMonitorToolStripMenuItem_Click(object sender, EventArgs e)
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
        top: ;
            FormCollection fc = Application.OpenForms;
            foreach (Form form_tmp in fc)
            {
                if (form_tmp.Name == "Multi_Monitor_Selection_Overlay")
                {
                    //close monitor selection overlay
                    form_tmp.Close();
                    goto top;
                }
            }
        }

        #endregion

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
                //theoretically should fix incorrectly shapped loads
                moveToPrimaryMonitor();

                fade_in();

                //reset which keys are down
                TildeIsDown = false;
                LCtrlIsDown = false;
                e.Handled = true;
                goto done;
            }

            //if both hooked keys are down, and we are ready to fadeout, fadeout in the dash
            if (TildeIsDown && LCtrlIsDown && cD_tstate == timerstate.indash)
            {
                fade_out();

                //reset which keys are down
                TildeIsDown = false;
                LCtrlIsDown = false;
                e.Handled = true;
                goto done;
            }

            e.Handled = false;
        done: ;
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
            cD_tstate = (int)timerstate.fadein; //cD_tstate is the timer state
            //this.Focus();

            moveToPrimaryMonitor(); //ensures proper form sizing 

            this.Visible = true;

            maintimer.Interval = 1; //set interval
            maintimer.Start(); //begin timer
        }

        /// <summary>
        /// cues the form's fade out process
        /// </summary>
        private void fade_out()
        {
            cD_tstate = timerstate.fadeout;
            maintimer.Interval = 1;
            // this.Focus(); //This makes it so the text is not edited by pressing keys after startup (while invisible)
        }

        /// <summary>
        /// timer tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timertime++; //increment fade timer

            //fade in related code
            #region fadeintimer
            if (cD_tstate == (int)timerstate.fadein)
            {

                //computes amount of change in opacity per clock tick then applies it
                double double_change_in_opacity_per_tick = (Convert.ToDouble(OpacityLevel)) / Convert.ToDouble(int_fade_milliseconds) * 1 / 10;
                this.Opacity = timertime * double_change_in_opacity_per_tick;

                if (this.Opacity >= (Convert.ToDouble(OpacityLevel) * 1 / 100))
                {
                    cD_tstate = timerstate.indash; //We set the timerstate to indash to allow for timekeeping
                    this.Opacity = Convert.ToDouble("." + OpacityLevel);
                    timertime = 0;
                    maintimer.Interval = 1000;
                }
            }
            #endregion

            //indash related code 
            #region indashtimer
            if (cD_tstate == timerstate.indash)
            {

            }
            #endregion

            //fadeout related code
            #region fadeouttimer
            if (cD_tstate == timerstate.fadeout)
            {
                //computes amount of change in opacity per clock tick then applies it
                double double_change_in_opacity_per_tick = (Convert.ToDouble(OpacityLevel)) / Convert.ToDouble(int_fade_milliseconds) * 1 / 10;
                this.Opacity = (Convert.ToDouble(OpacityLevel) * 1 / 100) - (Convert.ToDouble(timertime) * double_change_in_opacity_per_tick);

                if (this.Opacity <= 0)
                {
                    cD_tstate = timerstate.fadein;
                    this.Opacity = 0;
                    this.Visible = false;
                    timertime = 0;
                    maintimer.Stop();
                }
            }
            #endregion

            //always happening when timer is going
            #region alwaysontimer
            //set time label
            button_time.Text = (DateTime.Now.ToString()).Substring(DateTime.Now.ToString().IndexOf(" ")).Trim();
            //set date label
            string datelabeltext = (DateTime.Now.DayOfWeek.ToString()) + ", " + DateTime.Now.ToString("MMMMMMMMMMMMMM") + " " + DateTime.Now.Day.ToString() + ", " + DateTime.Now.Year.ToString();

            button_date.Text = datelabeltext;
            #endregion
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
            if (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\cDash Settings.cDash"))
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\cDash Settings.cDash", true);
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


            ((RichTextBox)cSticky_new.Controls.Find("rtb", false)[0]).SaveFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\" + long_unique_timestamp.ToString() + ".rtf");
        }
        #endregion

        #region Calls to fade_out()
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
        /// add a new cStopwatch cForm to the cDash
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newCStopwatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //represents the a unique time stamp for use as name of form
            long long_unique_timestamp = DateTime.Now.Ticks;

            //add the cStopwatch to settings
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\cDash Settings.cDash", true);
            sw.WriteLine("cStopwatch;" + long_unique_timestamp.ToString() + ";Location;10;25");
            sw.WriteLine("cStopwatch;" + long_unique_timestamp.ToString() + ";StartDateTime;NULL");
            sw.WriteLine("cStopwatch;" + long_unique_timestamp.ToString() + ";TimerRunning;False");
            sw.WriteLine("cStopwatch;" + long_unique_timestamp.ToString() + ";EndDateTime;NULL");
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
        private void exportBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = folderBrowserDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //if the user clicks ok
                if (System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard"))
                {
                    //create directory
                    System.IO.Directory.CreateDirectory(folderBrowserDialog1.SelectedPath + "\\cDashBoard");

                    //copy files
                    foreach (string file in System.IO.Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard"))
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
        private void importCDashDataToolStripMenuItem_Click(object sender, EventArgs e)
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
                if (System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard"))
                {
                    //copy files
                    foreach (string file in System.IO.Directory.GetFiles(folderBrowserDialog1.SelectedPath))
                    {
                        //get file without path
                        string str_nopath_file = file.Substring(file.LastIndexOf("\\") + 1);
                        System.IO.File.Copy(file, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\" + str_nopath_file, true);
                    }
                }
                //reload the form to represent changes in files
                Form1_Load(this, null);
            }
        }

        /// <summary>
        /// show about box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            Application.Exit();
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
        private void setCDashBackColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //show the color selection dialog
            DialogResult result = colorDialog1.ShowDialog();

            //if the user clicks Ok, make the sticky, otherwise cancel
            if (result == DialogResult.OK)
            {
                this.BackColor = colorDialog1.Color;
                menuStrip1.BackColor = this.BackColor;
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
            }
        }

        /// <summary>
        /// exit application from notify icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitCDashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// called when this drop down menu is opened
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setOpacityToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            //make sure it displays the current opacity
            textbox_opacity.Text = OpacityLevel.ToString();
        }

        /// <summary>
        /// called when this drop down menu is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setOpacityToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            //if the casting fails, then break gracefully
            try
            {
                OpacityLevel = Convert.ToInt32(textbox_opacity.Text);
            }
            catch
            {
                MessageBox.Show("The opacity must be an integer!");
                return;
            }

            //if the user gives 100 (or greater) we must save it as 99 to make sure that 
            //the it goes 99 -> .99 instead of 100 -> .10
            if (OpacityLevel > 99)
            {
                OpacityLevel = 99;
            }

            this.Opacity = Convert.ToDouble("." + OpacityLevel.ToString());

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
        private void setFavoriteStickyColorToolStripMenuItem_Click(object sender, EventArgs e)
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
        private void setFavoriteFontToolStripMenuItem_Click(object sender, EventArgs e)
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
        private void setInMillisecondsToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            //set textbox text equal to saved fade length
            textbox_fadetime.Text = int_fade_milliseconds.ToString();
        }

        /// <summary>
        /// save fade length setting when the menu item is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setInMillisecondsToolStripMenuItem_DropDownClosed_1(object sender, EventArgs e)
        {
            //if the casting fails, then break gracefully
            try
            {
                int_fade_milliseconds = Convert.ToInt32(textbox_fadetime.Text);
            }
            catch
            {
                MessageBox.Show("The fade_time must be an integer!");
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

            //setup the OpenFileDialog to only accept images
            openFileDialog1.Filter = "Image Files (*.bmp, *.jpg, *.png, *.tiff, *.gif)|*.bmp;*.jpg;*.png;*.tiff;*.gif";

            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                System.IO.File.Copy(openFileDialog1.FileName, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\" + openFileDialog1.SafeFileName, false);
                System.IO.File.Move(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\" + openFileDialog1.SafeFileName, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\" + long_unique_timestamp.ToString() + System.IO.Path.GetExtension(openFileDialog1.FileName));

                cPic cPic_new = new cPic();
                cPic_new.Name = long_unique_timestamp.ToString();
                cPic_new.Location = new Point(10, 25);
                cPic_new.Size = new Size(350, 400);
                cPic_new.TopLevel = false;
                cPic_new.Parent = this;

                //unique cPic setup
                cPic_new.BackgroundImage = Image.FromFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\" + long_unique_timestamp.ToString() + System.IO.Path.GetExtension(openFileDialog1.FileName));
                cPic_new.BackgroundImageLayout = ImageLayout.None;

                Controls.Add(cPic_new);
                cPic_new.Show();
                cPic_new.BringToFront();

                //add the cPic to settings
                System.IO.StreamWriter sw = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\cDash Settings.cDash", true);
                sw.WriteLine("cPic;" + long_unique_timestamp.ToString() + ";FileName;" + long_unique_timestamp.ToString() + System.IO.Path.GetExtension(openFileDialog1.FileName));
                sw.WriteLine("cPic;" + long_unique_timestamp.ToString() + ";Size;350;400");
                sw.WriteLine("cPic;" + long_unique_timestamp.ToString() + ";Location;10;25");
                sw.WriteLine("cPic;" + long_unique_timestamp.ToString() + ";ImageLayout;None");
                sw.Close();
            }
        }

        #endregion

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

        top:
            foreach (List<string> currentsetting in list_settings)
            {
                if (currentsetting[0].Substring(0, 1) == "#")   //fixed problem with comment persistance
                    continue;

                //this has to do with the control to be removed
                if (currentsetting[1] == this_control.Name)
                {
                    list_settings.Remove(currentsetting);
                    goto top;
                }
            }

            //special deletion of associated files (cPic, cSticky)
            if (this_control.GetType() == typeof(cPic) || this_control.GetType() == typeof(cSticky))
            {
                int int_len = this_control.Name.Length;

                foreach (string file in System.IO.Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\"))
                {
                    //f is the name of the file with extension
                    string f = file.Substring(file.LastIndexOf("\\") + 1);
                    string f2 = f.Substring(0, int_len);
                    string n = this_control.Name;
                    //if timestamp == timestamp
                    if (f2 == n)
                    {
                        //special handling for cPic
                        if (this_control.GetType() == typeof(cPic))
                            this_control.BackgroundImage.Dispose();
                        System.IO.File.Delete(file);
                        break;
                    }
                }
            }

            //remove control
            Controls.Remove(this_control);
            this_control.Dispose();
            //save fixed settings
            saveSettingsList(list_settings);

        }

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
        /// if the program is closing, this is called
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //cleanly removes the notify icon from the system tray
            notifyIcon1.Visible = false;
        }


    }

}
