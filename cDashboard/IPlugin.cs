using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
//(C) Alexander White under the MIT License
namespace cDashboard
{
    public interface IPlugin
    {/// <summary>
     /// Reports on whether something has happened which would require the plugin's settings to be saved.
     /// </summary>
        bool NeedsSaving { get; }
        /// <summary>
        /// Obtain an instance of the Form.
        /// </summary>
        /// <returns>A new instance of the Form provided by a plugin.</returns>
        System.Windows.Forms.Form GetForm();
        /// <summary>
        /// Save the relevant information for saving the plugin to disk.
        /// </summary>
        void SavePlugin(string settingsLocation);
        /// <summary>
        /// Loads the plugin's persistence data from disk.
        /// </summary>
        /// <param name="settingsLocation">The location of the settings files</param>
        /// <param name="d">The instance of cDashboard</param>
        void LoadPlugin(string settingsLocation, cDashboard d);
        /// <summary>
        ///Informs the program that the form ought to be disposed of when it is closed. 
        /// </summary>

        bool DisposeOnClose { get; }
        Type getFormType();
    }
    public interface IPluginData
    {
        String name { get; }
    }
}
