using cDashboard;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Forms;
//(C) Alexander White under the MIT License
namespace CdashboardPluginTest
{
    //This tag tells the ComponentModel.Composition components to export this publically for use in cDashboard, specifying the type that this is to be shown as.
    [Export(typeof(IPlugin))]
    //This tells the Composition framework about some useful metadata, as defined in IPluginData.
    [ExportMetadata("name", "Funny")]
    public class PluginInterface : cDashboard.IPlugin
    {
        //Provides access to the cDashboard main code base.
        private WeakReference cDash=null;
        //save the initialized forms, but allow the cDashboard to dispose of them if one is closed.
        private List<WeakReference> initialized;

        public PluginInterface()
        {
            initialized = new List<WeakReference>();

        }

        //This returns an instance of the form provided by the plugin.
        public System.Windows.Forms.Form GetForm()
        {
            var q = new Funny();
            initialized.Add(new System.WeakReference(q));
            return q;
        }

        //Wrapper to initialize the form to the desired location.
        private System.Windows.Forms.Form GetForm(System.Drawing.Point Location)
        {
            var c = GetForm();
            c.Location = Location;
            return c;
        }

        //This saves the locations of the forms to a file, so they might be initialized afterwards.
        public void SavePlugin(string settingsLocation)
        {
            //It is done this way in order to avoid write conflicts, which this eliminates.
            List<string> lines = new List<string>();
                foreach (var rf in initialized)
                {
                    if (rf.IsAlive)
                    {
                        Form a = (Form)rf.Target;
                        lines.Add(a.Location.X.ToString() + ' ' + a.Location.Y.ToString());
                    }
                }
            System.IO.File.WriteAllLines(settingsLocation + "funny.cDash", lines.ToArray());
          
        }

        //This loads the positions of the forms in order to display them as they were.
        public void LoadPlugin(string settingsLocation, cDashboard.cDashboard dash)
        {
            //Never know when this might come in handy
            if (cDash == null)
                cDash = new WeakReference(dash);


            if (!System.IO.File.Exists(settingsLocation + "funny.cDash"))
                return;
            var locations = new List<System.Drawing.Point>();
            var lines=System.IO.File.ReadAllLines(settingsLocation + "funny.cDash");
           foreach(var line in lines)
            {
                var c = line.Split(' ');
                if (c.Count() == 2) {
                    locations.Add(
                        new System.Drawing.Point(int.Parse(c[0]), int.Parse(c[1])));
                }
            }
            foreach (var l in locations)
                dash.AddForm(GetForm(l));
        }

        //There's no reason to not dispose of the window.
        public bool DisposeOnClose { get { return true; } }
        //Returns the type of the form provided(for easy matching).
        public Type getFormType()
        {
            return typeof(Funny);
        }
    }
}