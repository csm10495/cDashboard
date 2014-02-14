//This file is part of cDashboard
//cSticky - A sticky note for widget for cDashboard
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

namespace cDashboard
{
    public partial class cSticky : Form
    {
        public cSticky()
        {
            InitializeComponent();
        }

        #region Global Variables

        /// <summary>
        /// used with locking
        /// </summary>
        bool CompletedForm_Load = false;

        #endregion


        #region Settings List Related Methods

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

        #endregion


        /// <summary>
        /// form loading void
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cSticky_Load(object sender, EventArgs e)
        {
            //release lock on functions
            CompletedForm_Load = true;
            //focus on the rtb so that user can type right after making new sticky
            rtb.Focus();
        }

        /// <summary>
        /// save the text to a file when editted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rtb_TextChanged(object sender, EventArgs e)
        {
            //to avoid file locks
            if (CompletedForm_Load == true)
            {
                rtb.SaveFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\" + "RichTextBox" + this.Name.Substring(this.Name.LastIndexOf("x") + 1).ToString() + ".rtf");
            }
        }

        /// <summary>
        /// must save new form location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cSticky_Move(object sender, EventArgs e)
        {
            if (CompletedForm_Load == true)
            {
                List<string> list_find = new List<string>();
                List<string> list_replace = new List<string>();

                list_find.Add("RichTextBox");
                list_find.Add(this.Name.Substring(this.Name.LastIndexOf("x") + 1).ToString());
                list_find.Add("Location");

                foreach (string s in list_find)
                {
                    list_replace.Add(s);
                }

                list_replace.Add(this.Location.X.ToString());
                list_replace.Add(this.Location.Y.ToString());

                replaceSetting(list_find, list_replace);
            }
        }

        /// <summary>
        /// when the resizing is complete, save the new size
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cSticky_ResizeEnd(object sender, EventArgs e)
        {
            if (CompletedForm_Load == true)
            {
                List<string> find = new List<string>();
                List<string> replace = new List<string>();

                find.Add("RichTextBox");
                find.Add(this.Name.Substring(this.Name.LastIndexOf("x") + 1).ToString());
                find.Add("Size");

                replace.Add("RichTextBox");
                replace.Add(this.Name.Substring(this.Name.LastIndexOf("x") + 1).ToString());
                replace.Add("Size");
                replace.Add(this.Size.Width.ToString());
                replace.Add(this.Size.Height.ToString());

                replaceSetting(find, replace);
            }
        }

        #region MenuStrip

        /// <summary>
        /// change backcolor of the sticky
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeBackcolorToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult dr = colorDialog1.ShowDialog();

            //only do something, if user clicks ok
            if (dr == DialogResult.OK)
            {
                rtb.BackColor = colorDialog1.Color;
                menustrip.BackColor = colorDialog1.Color;

                List<string> find = new List<string>();
                List<string> replace = new List<string>();

                find.Add("RichTextBox");
                replace.Add("RichTextBox");
                string num = (this.Name.Substring(this.Name.LastIndexOf("x") + 1));
                find.Add(num);
                replace.Add(num);
                find.Add("BackColor");
                replace.Add("BackColor");
                replace.Add(rtb.BackColor.R.ToString());
                replace.Add(rtb.BackColor.G.ToString());
                replace.Add(rtb.BackColor.B.ToString());

                replaceSetting(find, replace);

                //List<List<String>> list_settings = getSettingsList();

                //foreach (List<String> currentline in list_settings)
                //{

                //    if (currentline[0] == "RichTextBox" && currentline[1] == rtb.Name.Substring(rtb.Name.LastIndexOf("x") + 1).ToString() && currentline[2] == "BackColor")
                //    {
                //        currentline[3] = rtb.BackColor.R.ToString();
                //        currentline[4] = rtb.BackColor.G.ToString();
                //        currentline[5] = rtb.BackColor.B.ToString();
                //        break;
                //    }
                //}

                //saveSettingsList(list_settings);
            }
        }


        /// <summary>
        /// change the font of the current cSticky
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = fontDialog1.ShowDialog();

            //only do something, if user clicks ok
            if (dr == DialogResult.OK)
            {
                rtb.Font = fontDialog1.Font;
                rtb.ForeColor = fontDialog1.Color;
            }
        }

        /// <summary>
        /// brings up the save file dialog to save the current cSticky's richtext
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveThisTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        /// <summary>
        /// saves the rtb text to the savefiledialog1's filename
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            rtb.SaveFile(saveFileDialog1.FileName);
        }

        #endregion

        #region ContextMenu


        /// <summary>
        /// undo recent text changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            rtb.Undo();
        }

        /// <summary>
        /// standard cut content to clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtb.Cut();
        }

        /// <summary>
        /// standard copy content to clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtb.Copy();
        }

        /// <summary>
        /// standard paste content from clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtb.Paste();
        }

        /// <summary>
        /// standard text-based select all function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtb.SelectAll();
        }

        #endregion


    }
}
