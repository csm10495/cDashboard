using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
namespace cDashboard
{
    public enum ProgramActionSaveable {
        OnFormMove,
        OnFormClose,
        OnFormFocused
    }
    public interface IPlugin
    {
        System.Windows.Forms.Form GetForm();
        //Save the relevant information for saving the plugin to disk.
        void SavePlugin(string settingsLocation);
        //Load the plugin's persistance data from disk.
        void LoadPlugin(string settingsLocation,cDashboard d);
        //Informs the program that the form ought to be disposed of when it is closed.
        bool DisposeOnClose { get; }
        Type getFormType();
    }
    public interface IPluginData
    {
        String name { get; }
    }
}
