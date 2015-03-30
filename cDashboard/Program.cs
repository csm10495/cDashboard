//This file is part of cDashboard
//cDashboard - An information-based overlay for Microsoft Windows
//(C) Charles Machalow under the MIT License
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace cDashboard
{
    static class PluginContainer
    {
        /// <summary>
        /// Ultimately the accessible list of Plugins for the program.
        /// </summary>
        public static IEnumerable<Lazy<IPlugin, IPluginData>> plugins;
    }
      class Program
    {

        CompositionContainer _container;
        /// <summary>
        /// The Plugins loaded by MEF.
        /// </summary>
        [ImportMany]
        IEnumerable<Lazy<IPlugin,IPluginData>> plugins = null;
        private Program()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));
            catalog.Catalogs.Add(new DirectoryCatalog(Environment.CurrentDirectory));
            _container = new CompositionContainer(catalog);
            try
            {
                _container.ComposeParts(this);
            }
            catch (System.Reflection.ReflectionTypeLoadException v)
            {
                foreach (var c in v.LoaderExceptions)
                    Console.WriteLine(c.ToString());
            }
            catch (CompositionException e) {
                Console.WriteLine(e.ToString());
            }
            if (plugins != null)
            {
                foreach (var q in plugins)
                {
                    Console.WriteLine(q.Metadata.name);
                }
                PluginContainer.plugins= plugins;
            }
            else
                Console.WriteLine("No plugins loaded");
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Program m = new Program();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //prevents form from gaining focus while being invisible for the first time
            cDashboard cDash = new cDashboard();
            cDash.Show();
            cDash.Visible = false;
           
            Application.Run();
        }
    }
}
