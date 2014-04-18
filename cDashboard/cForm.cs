//This file is part of cDashboard
//cForm - An interface for widgets for cDashboard
//(C) Charles Machalow 2014 under the MIT License
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cDashboard
{
    public partial class cForm : Form
    {
        public cForm()
        {
            InitializeComponent();
        }

        //this exists as an interface because all cForms seem to have
        //various things in common
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
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\cDash Settings.cDash");
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

    }
}
