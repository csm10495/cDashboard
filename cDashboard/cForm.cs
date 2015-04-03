//This file is part of cDashboard
//cDashboard - An information-based overlay for Microsoft Windows
//cForm - An interface for widgets for cDashboard
//(C) Charles Machalow under the MIT License
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.ComponentModel.Composition;

namespace cDashboard
{
    [Export]
    [ExportMetadata("Plugin",null)]
    public partial class cForm : Form
    {
        public cForm()
        {
            InitializeComponent();
        }

        #region Global Variables

        /// <summary>
        /// location for settings related files
        /// </summary>
        protected readonly string SETTINGS_LOCATION = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\";

        /// <summary>
        /// represents if Form_Load Completion
        /// </summary>
        protected bool CompletedForm_Load = false;

        /// <summary>
        /// used for makinmg io locks files
        /// </summary>
        private static readonly Object lock_object = new Object();

        #endregion

        //this exists as an interface because all cForms seem to have various things in common
        #region Settings List Related Methods

        /// <summary>
        /// Will grab the NON identifying setting value from what would be a string_currentline
        /// calls the overloaded list version
        /// </summary>
        /// <param name="array_identifiers"></param>
        /// <returns></returns>
        protected List<string> getSpecificSetting(string[] array_identifiers)
        {
            return getSpecificSetting(array_identifiers.ToList());
        }

        /// <summary>
        /// array version of replace setting
        /// </summary>
        /// <param name="array_find"></param>
        /// <param name="array_replace"></param>
        protected void replaceSetting(string[] array_find, string[] array_replace)
        {
            replaceSetting(array_find.ToList(), array_replace.ToList());
        }

        /// <summary>
        /// return settings from file
        /// </summary>
        /// <returns></returns>
        protected List<List<string>> getSettingsList()
        {
            //this will be the list of lines from the settings file 
            //THE settings_list WILL NOT INCLUDE BLANK LINES OR LINES STARTING WITH #
            List<List<string>> settings_list = new List<List<string>>();

            lock (lock_object)
            {
                //StreamReader to read settings
                System.IO.StreamReader sr = new System.IO.StreamReader(SETTINGS_LOCATION + "cDash Settings.cDash");

                //each line
                string tmp_line = sr.ReadLine();

                //add proper lines to settings_list
                while (tmp_line != null)
                {
                    //check for blank lines or ones that start with #
                    if (tmp_line != "") //&& tmp_line.Substring(0, 1) != "#") 
                    //changed line to maintain comment structure of settings file
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
            }
            return settings_list;
        }

        /// <summary>
        /// Generic settings replacement method
        /// </summary>
        /// <param name="list_find">Strings to find in settings</param>
        /// <param name="list_replace">Replace ENTIRE line</param>
        protected void replaceSetting(List<string> list_find, List<string> list_replace)
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
        /// Saves settings list to AppData from a list_settings
        /// </summary>
        /// <param name="list_settings">a list of lists of parts of settings lines</param>
        protected void saveSettingsList(List<List<string>> list_settings)
        {
            lock (lock_object)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(SETTINGS_LOCATION + "cDash Settings.cDash");
                // sw.WriteLine("# cDashBoard Settings File");
                // sw.WriteLine("# A # tells the program to ignore this line");
                // sw.WriteLine("# Don't edit this file unless you know what you are doing");
                // removed so that this isn't printed over and over...
                foreach (List<string> currentline in list_settings)
                {
                    sw.WriteLine(string.Join(";", currentline));
                }
                sw.Close();
            }
        }

        /// <summary>
        /// Will grab the NON identifying setting value from what would be a string_currentline
        /// </summary>
        /// <param name="list_setting_identifiers">The identifier of the setting EX: {cDash, DefaultMonitor} </param>
        /// <returns></returns>
        protected List<string> getSpecificSetting(List<string> list_identifiers)
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

        #endregion

        /// <summary>
        /// randomizes files in a folder and numbers them starting with 1
        /// </summary>
        /// <param name="name"></param>
        protected void randomizeFiles(string name)
        {
            //create random ordering of image files (for psuedo randomness)
            Random r = new Random(DateTime.Now.Millisecond);

            //list of files in directory
            string[] files = System.IO.Directory.GetFiles(SETTINGS_LOCATION + name);

            //this will give us an array of 1 ... 2 ... files.Length
            int[] rand_ints = Enumerable.Range(1, files.Length).OrderBy(t => r.Next()).ToArray();

            //temporary names so that there are not naming conflicts during the next step
            foreach (string f in files)
            {
                //ensure that things happen on different ticks
                System.Threading.Thread.Sleep(5);
                File.Move(f, f.Substring(0, f.LastIndexOf("\\") + 1) + DateTime.Now.Ticks);
            }

            //refresh directory, with new tick-based names
            files = System.IO.Directory.GetFiles(SETTINGS_LOCATION + name);

            //actually number each file via the rand_ints numbering
            int x = 0;
            foreach (string f in files)
            {
                File.Move(f, f.Substring(0, f.LastIndexOf("\\") + 1) + rand_ints[x]);
                x++;
            }
        }

        //import both the SendMessage method and the ReleaseCapture method from user32.dll
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        /// <summary>
        /// calls the user32.dll to move the form
        /// </summary>
        /// <param name="e"></param>
        protected void dragForm(ref MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //removes mouse capture from current object, and allows it to be sent elsewhere
                ReleaseCapture();

                //WM_NCLBUTTONDOWN = 0xA1
                //HT_CAPTION = 0x2
                //send a windows message that the left mouse button is down on the titlebar 
                SendMessage(this.Handle, 0xA1, 0x2, 0);
            }
        }

        /// <summary>
        /// Windows Message processor to resize form
        /// </summary>
        /// <param name="m"></param>
        protected void formResizer(ref Message m)
        {
            //WINDOWS API CODES 
            //WM_NCHITTEST = 0x84
            //HTLEFT = 10
            //HTRIGHT = 11
            //HTTOP = 12
            //HTTOPLEFT = 13
            //HTTOPRIGHT = 14
            //HTBOTTOM = 15
            //HTBOTTOMLEFT = 16
            //HTBOTTOMRIGHT = 17
            if (m.Msg == 0x84)
            {
                //get the lower 4 bits
                int x = (int)(m.LParam.ToInt64() & 0xFFFF);
                //get higher 4 bits
                int y = (int)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);
                Point pt = PointToClient(new Point(x, y));

                //top
                if (pt.Y <= 10 && ClientSize.Height >= 25)
                {
                    m.Result = (IntPtr)(12);
                }

                //bottom
                if (pt.Y >= ClientSize.Height - 10 && ClientSize.Height >= 25)
                {
                    m.Result = (IntPtr)(15);
                }

                //left
                if (pt.X <= 15 && ClientSize.Height >= 25)
                {
                    m.Result = (IntPtr)(10);
                }

                //right
                if (pt.X >= ClientSize.Width - 15 && ClientSize.Height >= 25)
                {
                    m.Result = (IntPtr)(11);
                }

                //bottom right
                if (pt.X >= ClientSize.Width - 25 && pt.Y >= ClientSize.Height - 25 && ClientSize.Height >= 25)
                {
                    m.Result = (IntPtr)(IsMirrored ? 16 : 17);
                }

                //bottom left
                if (pt.X <= 25 && pt.Y >= ClientSize.Height - 25 && ClientSize.Height >= 25)
                {
                    m.Result = (IntPtr)(IsMirrored ? 17 : 16);
                }

                //top left
                if (pt.X <= 15 && pt.Y <= 15 && ClientSize.Height >= 25)
                {
                    m.Result = (IntPtr)(IsMirrored ? 14 : 13);
                }

                //top right
                //this also handles small forms
                if (pt.X >= ClientSize.Width - 25 && pt.Y <= 25)
                {
                    m.Result = (IntPtr)(IsMirrored ? 13 : 14);
                }

                //we get results between 10 and 17, so return
                if (m.Result.ToInt32() <= 17 && m.Result.ToInt32() >= 10)
                {
                    //special handling for if this is a cPic...
                    //hide the menustrip again as a failsafe, it will prevent most chances for error
                    if (this is cPic)
                    {
                        ((cPic)this).menuStrip1.Visible = false;
                    }
                    return;
                }
            }

            //if this isn't the message we are modifying, let it go
            base.WndProc(ref m);
        }

        #region Events

        /// <summary>
        /// Will be fired upon changing of
        /// form size
        /// form location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cForm_ResizeEnd(object sender, EventArgs e)
        {
            if (CompletedForm_Load)
            {
                //handle move
                replaceSetting(new string[] { this.GetType().Name, this.Name, "Location" }, new string[] { this.GetType().Name, this.Name, "Location", this.Location.X.ToString(), this.Location.Y.ToString() });

                //handle resize
                replaceSetting(new string[] { this.GetType().Name, this.Name, "Size" }, new string[] { this.GetType().Name, this.Name, "Size", this.Size.Width.ToString(), this.Size.Height.ToString() });
            }
        }

        #endregion
    }
}
