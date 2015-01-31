using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using cDashboard;
namespace thingy
{
    [Export(typeof(IPlugin))]
    [ExportMetadata("name","Funny")]
    public class PluginInterface : cDashboard.IPlugin
    {
        public System.Windows.Forms.Form GetForm()
        {
            return new Funny();
        }
    }
}
