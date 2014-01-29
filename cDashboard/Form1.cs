
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
    public partial class cDashboard : Form
    {

        #region Global Variables
        /// <summary>
        /// this is the tick counter for timer1
        /// </summary>
        int timertime = 0;

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
        /// list of all stickies in program
        /// </summary>
        // List<RichTextBox> list_stickies = new List<RichTextBox>();
        public List<cSticky> list_cStickies = new List<cSticky>();

        /// <summary>
        /// Signifies if the 1 key is down
        /// </summary>
        bool OneKeyIsDown = false;


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
            label_date.Focus(); //This makes it so the text is not edited by pressing keys after startup (while invisible)

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
                if (current_item[0] == "RichTextBox")
                {
                    //we have a "RichTextBox"
                    while (Convert.ToInt32(current_item[1]) + 1 > list_cStickies.Count)
                    {
                        //there aren't enough stickies, so we should add more, until this one makes sense
                        cSticky cSticky_new = new cSticky();
                        cSticky_new.Name = "RichTextBox" + list_cStickies.Count.ToString();
                        list_cStickies.Add(cSticky_new);
                    }
                    //at this point, we have enough richtextboxes
                    //we move onto the property [2]

                    //the property is back color
                    if (current_item[2] == "BackColor")
                    {
                        list_cStickies[Convert.ToInt32(current_item[1])].Controls.Find("menustrip", false)[0].BackColor = Color.FromArgb(Convert.ToInt32(current_item[3]), Convert.ToInt32(current_item[4]), Convert.ToInt32(current_item[5]));
                        list_cStickies[Convert.ToInt32(current_item[1])].BackColor = Color.FromArgb(Convert.ToInt32(current_item[3]), Convert.ToInt32(current_item[4]), Convert.ToInt32(current_item[5]));
                        list_cStickies[Convert.ToInt32(current_item[1])].Controls.Find("rtb", false)[0].BackColor = Color.FromArgb(Convert.ToInt32(current_item[3]), Convert.ToInt32(current_item[4]), Convert.ToInt32(current_item[5]));
                    }

                    //the property is location
                    if (current_item[2] == "Location")
                    {
                        list_cStickies[Convert.ToInt32(current_item[1])].Location = new Point(Convert.ToInt32(current_item[3]), Convert.ToInt32(current_item[4]));
                    }

                    //the property is size
                    if (current_item[2] == "Size")
                    {
                        list_cStickies[Convert.ToInt32(current_item[1])].Size = new Size(Convert.ToInt32(current_item[3]), Convert.ToInt32(current_item[4]));
                    }

                    //the property is font style
                    //   if (current_item[2] == "FontStyle")
                    //  {
                    //      list_cStickies[Convert.ToInt32(current_item[1])].Font = new Font(current_item[3], Convert.ToSingle(current_item[4]));
                    //  }
                }
            }

            //add stickies to form
            foreach (cSticky cSticky_new in list_cStickies)
            {
                //attempts to load richtextbox files, if the file doesn't exist, it fails gracefully
                if (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\" + "RichTextBox" + cSticky_new.Name.Substring(cSticky_new.Name.LastIndexOf("x") + 1).ToString() + ".rtf"))
                {
                    ((RichTextBox)cSticky_new.Controls.Find("rtb", false)[0]).LoadFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\" + "RichTextBox" + cSticky_new.Name.Substring(cSticky_new.Name.LastIndexOf("x") + 1).ToString() + ".rtf");
                }

                //add control
                cSticky_new.TopLevel = false;
                cSticky_new.Parent = this;
                Controls.Add(cSticky_new);
                cSticky_new.Show();
                cSticky_new.BringToFront();
            }
        }

        /// <summary>
        /// Sets variables for proper key hooking and begins the hook
        /// </summary>
        private void variable_setup()
        {
            //add Shift and Ctrl to the list of hooked keys
            KeyHook.HookedKeys.Add(Keys.Oemtilde);
            KeyHook.HookedKeys.Add(Keys.D1);
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
                    }
                }
            }
            this.WindowState = FormWindowState.Maximized;
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

        #region Settings Finding / Setting

        /// <summary>
        /// return settings from file
        /// </summary>
        /// <returns></returns>
        private List<List<string>> getSettingsList()
        {
            //StreamReader to read settings
            System.IO.StreamReader sr = new System.IO.StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\cDash Settings.cDash");

            //this will be the list of lines from the settings file 
            //THE settings_list WILL NOT INCLUDE BLANK LINES OR LINES STARTING WITH #
            List<List<string>> settings_list = new List<List<string>>();

            //each line
            string tmp_line = sr.ReadLine();

            //add proper lines to settings_list
            while (tmp_line != null)
            {
                //check for blank lines or ones that start with #
                if (tmp_line != "" && tmp_line.Substring(0, 1) != "#")
                {
                    //this will be the list that will be added to settings_list
                    List<string> tmp_list = new List<string>();

                    //delimit tmp_string
                    foreach (string s in tmp_line.Split(';'))
                    {
                        tmp_list.Add(s);
                    }
                    //final add
                    settings_list.Add(tmp_list);
                }

                //increment line
                tmp_line = sr.ReadLine();
            }
            //close file
            sr.Close();
            return settings_list;
        }


        /// <summary>
        /// Generic settings replacement method
        /// </summary>
        /// <param name="list_find">Strings to find in settings</param>
        /// <param name="list_replace">Replace ENTIRE line</param>
        private void replaceSetting(List<string> list_find, List<string> list_replace)
        {
            List<List<string>> list_settings = getSettingsList();

            int x = 0;
            bool was_replaced = false;

            //look for the replacement setting
            foreach (List<string> string_currentline in list_settings)
            {
                int y = 0;
                foreach (string string_finditem in list_find)
                {
                    if ((string_finditem != string_currentline[y]))
                    {
                        goto notcorrectline;
                    }
                    y++;
                }
                //if we make it to this point, this is the correct line, so perform the replacement

                //make replacement
                list_settings[x] = list_replace;
                was_replaced = true;
                break;

            notcorrectline: ;
                x++;

            }

            //in case the setting wasn't found, add it uniquely 
            if (was_replaced == false)
            {
                list_settings.Add(list_replace);
            }
            saveSettingsList(list_settings);
        }


        /// <summary>
        /// Will grab the NON identifying setting value from what would be a string_currentline
        /// </summary>
        /// <param name="list_setting_identifiers">The identifier of the setting EX: {cDash, DefaultMonitor} </param>
        /// <returns></returns>
        private List<string> getSpecificSetting(List<string> list_identifiers)
        {
            //Full settings list
            List<List<string>> list_settings = getSettingsList();

            //will be be the actual setting without the identifier EX: {0,255,50}
            List<string> list_specific_settings = new List<string>();

            //itterate through every line of the settings file
            foreach (List<string> list_currentline in list_settings)
            {
                //assume that the currentline is the setting we want
                bool bool_currentline_is_identified = true;

                //if the currently observed line has less parts then the identifier we are searching for, move on
                if (list_currentline.Count() < list_identifiers.Count())
                {
                    continue;
                }

                int x = 0;
                //itterate through the currentline's parts
                for (; x < list_identifiers.Count(); x++)
                {
                    //if they identifiers don't match the currentline, then break, and continue
                    if (list_currentline[x] != list_identifiers[x])
                    {
                        bool_currentline_is_identified = false;
                        break;
                    }
                }

                //if this is not the correct line, continue with another line
                if (bool_currentline_is_identified == false)
                {
                    continue;
                }
                else
                {
                    //add remaining parts to the output of this method
                    for (; x < list_currentline.Count(); x++)
                    {
                        list_specific_settings.Add(list_currentline[x]);
                    }
                    break;
                }

            }

            return list_specific_settings;
            //note a setting does not exist if this method returns a List<string> where .Count() == 0
        }


        /// <summary>
        /// Saves settings list to AppData from a list_settings
        /// </summary>
        /// <param name="list_settings">a list of lists of parts of settings lines</param>
        private void saveSettingsList(List<List<string>> list_settings)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\cDash Settings.cDash");
            sw.WriteLine("# cDashBoard Settings File");
            sw.WriteLine("# A # tells the program to ignore this line");
            sw.WriteLine("# Don't edit this file unless you know what you are doing");
            foreach (List<string> currentline in list_settings)
            {
                sw.WriteLine(string.Join(";", currentline));
            }
            sw.Close();

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
            //Check if the hooked key is the 1 key
            if (e.KeyCode == Keys.D1)
            {
                //Set the bool properly
                OneKeyIsDown = true;
            }

            //Check if the hooked key is the tilde key
            if (e.KeyCode == Keys.Oemtilde)
            {
                //Set the bool properly
                TildeIsDown = true;
            }

            //if both hooked keys are down, and we are ready to fadein, fade in the dash
            if (TildeIsDown && OneKeyIsDown && cD_tstate == timerstate.fadein && CompletedForm_Load == true)
            {
                //theoretically should fix incorrectly shapped loads
                moveToPrimaryMonitor();

                fade_in();

                //reset which keys are down
                TildeIsDown = false;
                OneKeyIsDown = false;
                e.Handled = true;
                goto done;
            }

            //if both hooked keys are down, and we are ready to fadeout, fadeout in the dash
            if (TildeIsDown && OneKeyIsDown && cD_tstate == timerstate.indash)
            {
                fade_out();

                //reset which keys are down
                TildeIsDown = false;
                OneKeyIsDown = false;
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
            //Check if the hooked key is the 1 key
            if (e.KeyCode == Keys.D1)
            {
                //Set the bool properly
                OneKeyIsDown = false;
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
            this.Focus();
            this.Visible = true;


            //READ STICKYS and PUT THEM IN RICHTEXTBOXES
            /*
            //open text file and put text in text box
            if (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\cDashBoardText.cDash"))
            {
                System.IO.StreamReader file = new System.IO.StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\cDashBoardText.cDash");
                string tmpreadoffile = file.ReadToEnd().ToString();
                file.Close();
                richTextBox1.Text = tmpreadoffile;   
            }
            
             
            richTextBox1.Select(richTextBox1.Text.Length, 0); //move cursor to end of text (if not already there)

            richTextBox1.Focus();
             */

            maintimer.Interval = 10; //set interval
            maintimer.Start(); //begin timer

        }

        /// <summary>
        /// cues the form's fade out process
        /// </summary>
        private void fade_out()
        {
            cD_tstate = timerstate.fadeout;
            maintimer.Interval = 10;
            label_date.Focus(); //This makes it so the text is not edited by pressing keys after startup (while invisible)
        }




        /// <summary>
        /// timer tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timertime++; //increment fade in

            //fade in related code
            #region fadeintimer
            if (cD_tstate == (int)timerstate.fadein)
            {
                this.Opacity = ((double)timertime / OpacityLevel) * Convert.ToDouble("." + OpacityLevel.ToString());
                if (timertime == OpacityLevel)
                {
                    this.Opacity = Convert.ToDouble("." + OpacityLevel);
                    timertime = 0;
                    cD_tstate = timerstate.indash; //We set the timerstate to indash to allow for timekeeping
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
                this.Opacity = Convert.ToDouble("." + OpacityLevel.ToString()) - ((double)timertime / OpacityLevel) * Convert.ToDouble("." + OpacityLevel.ToString());
                if (timertime == OpacityLevel)
                {
                    this.Opacity = 0;
                    this.Visible = false;
                    timertime = 0;
                    maintimer.Stop();
                    cD_tstate = timerstate.fadein;
                }
            }
            #endregion

            //always happening when timer is going
            #region alwaysontimer
            //set time label
            label_time.Text = (DateTime.Now.ToString()).Substring(DateTime.Now.ToString().IndexOf(" ")).Trim();
            label_time.Left = this.Width - label_time.Width + 14;
            //set date label
            string datelabeltext = (DateTime.Now.DayOfWeek.ToString()) + ", " + DateTime.Now.ToString("MMMMMMMMMMMMMM") + " " + DateTime.Now.Day.ToString() + ", " + DateTime.Now.Year.ToString();

            //try to make it look a tad bit nicer
            if (datelabeltext.Length < 24)
            {
                datelabeltext = "    " + datelabeltext;
            }
            label_date.Text = datelabeltext;
            label_date.Left = this.Width - label_date.Width;
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
        #endregion

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
        /// exits application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            ((MenuStrip)cSticky_new.Controls.Find("menustrip", false)[0]).BackColor = pickedColor;
            ((RichTextBox)cSticky_new.Controls.Find("rtb", false)[0]).BackColor = pickedColor;
            ((RichTextBox)cSticky_new.Controls.Find("rtb", false)[0]).Font = FavoriteStickyFont;
            cSticky_new.TopLevel = false;
            cSticky_new.Parent = this;
            Controls.Add(cSticky_new);
            cSticky_new.Show();
            cSticky_new.BringToFront();
            cSticky_new.Name = "RichTextBox" + list_cStickies.Count.ToString();
            list_cStickies.Add(cSticky_new);

            //this code will add this new sticky to the settings file
            if (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\cDash Settings.cDash"))
            {
                int currentsticky = list_cStickies.Count - 1;
                System.IO.StreamWriter sw = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\cDash Settings.cDash", true);
                sw.WriteLine("RichTextBox;" + currentsticky + ";BackColor;" + list_cStickies[currentsticky].BackColor.R.ToString() + ";" + list_cStickies[currentsticky].BackColor.G.ToString() + ";" + list_cStickies[currentsticky].BackColor.B.ToString());
                sw.WriteLine("RichTextBox;" + currentsticky + ";Location;" + list_cStickies[currentsticky].Location.X.ToString() + ";" + list_cStickies[currentsticky].Location.Y.ToString());
                sw.WriteLine("RichTextBox;" + currentsticky + ";Size;" + list_cStickies[currentsticky].Size.Width.ToString() + ";" + list_cStickies[currentsticky].Size.Height.ToString());
                sw.WriteLine("RichTextBox;" + currentsticky + ";FontStyle;" + ((RichTextBox)list_cStickies[currentsticky].Controls.Find("rtb", false)[0]).Font.Name.ToString() + ";" + ((RichTextBox)list_cStickies[currentsticky].Controls.Find("rtb", false)[0]).Font.Size.ToString());
                sw.Close();
            }
            else
            {
                //this error should never EVER be thrown
                MessageBox.Show("System.IO.Exception: File does not exist");
            }


            ((RichTextBox)cSticky_new.Controls.Find("rtb", false)[0]).SaveFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\" + "RichTextBox" + cSticky_new.Name.Substring(cSticky_new.Name.LastIndexOf("x") + 1).ToString() + ".rtf");
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
        /// called if a control is removed
        /// ex: if the x button is clicked on a child from, handle deleting the sticky
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_ControlRemoved(object sender, ControlEventArgs e)
        {
            cSticky cSticky_this = (cSticky)e.Control;

            List<List<string>> list_settings = getSettingsList();

            //we must find the number of this sticky
            int int_removed_sticky = Convert.ToInt32(cSticky_this.Name.Substring(cSticky_this.Name.LastIndexOf("x") + 1));
            int int_current_sticky_to_be_moved = (int_removed_sticky + 1);

            //remove from list_cStickies
            foreach (cSticky cSticky_tmp in list_cStickies)
            {
                if (cSticky_tmp.Name == cSticky_this.Name)
                {
                    list_cStickies.Remove(cSticky_tmp);
                    break;
                }

            }

            //safe delete rtf for this textbox
            if (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\" + "RichTextBox" + cSticky_this.Name.Substring(cSticky_this.Name.LastIndexOf("x") + 1).ToString() + ".rtf"))
            {
                System.IO.File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\" + "RichTextBox" + cSticky_this.Name.Substring(cSticky_this.Name.LastIndexOf("x") + 1).ToString() + ".rtf");
            }

            //we need to restart the foreach each time because the size of list_settings changes
        //get rid of every setting relating to the RichTextBox being removed
        get_rid_of_more_settings:
            foreach (List<string> currentline in list_settings)
            {
                if (currentline[0] == "RichTextBox" && Convert.ToInt32(currentline[1]) == int_removed_sticky)
                {
                    list_settings.Remove(currentline);
                    goto get_rid_of_more_settings;
                }
            }


            //go through the entire list, and move RTBs down in increment
            //do this until all after the removal have been removed
            while (int_current_sticky_to_be_moved < list_cStickies.Count + 1)
            {
                //Since the stickies may be in the wrong order, we need to find them by name
                foreach (cSticky cSticky_tmp in list_cStickies)
                {
                    //if the current RichTextBox is the right one, we can change decrement numbers
                    if (Convert.ToInt32(cSticky_tmp.Name.Substring(cSticky_tmp.Name.LastIndexOf("x") + 1)) == int_current_sticky_to_be_moved)
                    {


                        //safe move rtf for this textbox
                        if (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\" + "RichTextBox" + cSticky_tmp.Name.Substring(cSticky_tmp.Name.LastIndexOf("x") + 1).ToString() + ".rtf"))
                        {
                            System.IO.File.Move(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\" + "RichTextBox" + cSticky_tmp.Name.Substring(cSticky_tmp.Name.LastIndexOf("x") + 1).ToString() + ".rtf", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\" + "RichTextBox" + (int_current_sticky_to_be_moved - 1).ToString() + ".rtf");
                        }

                        //rename the actual .Name of the RichTextBoxes
                        cSticky_tmp.Name = "RichTextBox" + (int_current_sticky_to_be_moved - 1).ToString();

                        //we need to restart the foreach each time because the size of list_settings changes
                    decrement_settings:
                        //decrement settings (numerically) above the removed
                        foreach (List<string> currentline in list_settings)
                        {
                            if (currentline[0] == "RichTextBox" && Convert.ToInt32(currentline[1]) == int_current_sticky_to_be_moved)
                            {
                                currentline[1] = (int_current_sticky_to_be_moved - 1).ToString();
                                goto decrement_settings;
                            }
                        }

                        int_current_sticky_to_be_moved++;
                        break;
                    }
                }
            }







            //remove this richtextbox from current form
            Controls.Remove(cSticky_this);
            cSticky_this.Dispose();
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
                this.Focus();
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


        /// <summary>
        /// double click the time label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_time_DoubleClick(object sender, EventArgs e)
        {
            //send down the dash
            fade_out();
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

        private void tESTToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
        /// double click the date label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_date_DoubleClick(object sender, EventArgs e)
        {
            //send down the dash
            fade_out();
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











    }

}
