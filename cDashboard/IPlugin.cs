using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
namespace cDashboard
{
    public interface IPlugin
    {
        System.Windows.Forms.Form GetForm();
    }
    public interface IPluginData
    {
        String name { get; }
    }
}
