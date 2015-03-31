//This file is part of cDashboard
//cDashboard - An information-based overlay for Microsoft Windows
//cKBSetup - A keyboard shortcut setup form for cDashboard
//(C) Charles Machalow under the MIT License
using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Linq;

namespace cDashboard
{
    public partial class cKBSetup : cForm
    {
        //needed for determining which ctrl, alt key was hit
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys key);

        /// <summary>
        /// this variable is flipped back and forth to understand
        /// which key (1 or 2) the user is setting
        /// </summary>
        private bool isKey1 = true;

        /// <summary>
        /// temporary key1 value
        /// </summary>
        private Keys Key1;

        /// <summary>
        /// temporary key2 value
        /// </summary>
        private Keys Key2;

        public cKBSetup()
        {
            InitializeComponent();
        }

        /// <summary>
        /// special constructor to get keys from cDashboard
        /// </summary>
        /// <param name="k1"></param>
        /// <param name="K2"></param>
        public cKBSetup(Keys k1, Keys K2)
        {
            InitializeComponent();
            Key1 = k1;
            Key2 = K2;
        }

        /// <summary>
        /// form load
        /// displays current keyboard shortcut
        /// setup local temp Key1,2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cKBSetup_Load(object sender, EventArgs e)
        {
            label_combo.Text = Key1.ToString() + " + " + Key2.ToString();
        }

        /// <summary>
        /// called when a key is pressed, saves Key1, Key2 as needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cKBSetup_KeyDown(object sender, KeyEventArgs e)
        {
            // Console.WriteLine("Ctrl:{0}, LCtrl:{1}, RCtrl:{2}", GetAsyncKeyState(Keys.ControlKey), GetAsyncKeyState(Keys.LControlKey), GetAsyncKeyState(Keys.RControlKey));

            int checked_keyvalue = e.KeyValue;

            //special checking for modifiers (ctrl, shift, alt)
            if (e.KeyCode == Keys.ControlKey)
            {
                MessageBox.Show("");
            }
            else if (e.KeyCode == Keys.Menu) //alt
            {
                MessageBox.Show("");
            }
            else if (e.KeyCode == Keys.ShiftKey)
            {
                MessageBox.Show("");
            }

            //replace in settings
            if (isKey1)
            {
                Key1 = (Keys)checked_keyvalue;
                label_combo.Text = Key1.ToString() + " + ?";
            }
            else
            {
                Key2 = (Keys)checked_keyvalue;
                label_combo.Text = Key1.ToString() + " + " + Key2.ToString();
            }

            //boolean flip
            isKey1 = !isKey1;
        }

        /// <summary>
        /// save keys to settings, close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveShortcutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            replaceSetting(new string[] { "cDash", "Key1" }, new string[] { "cDash", "Key1", ((int)Key1).ToString() });
            replaceSetting(new string[] { "cDash", "Key2" }, new string[] { "cDash", "Key2", ((int)Key2).ToString() });

            MessageBox.Show("Setting will be applied after restarting the application");

            this.Close();
        }

        /// <summary>
        /// go back to default: Lctrl, tilde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void goBackToDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            replaceSetting(new string[] { "cDash", "Key1" }, new string[] { "cDash", "Key1", "162" });
            replaceSetting(new string[] { "cDash", "Key2" }, new string[] { "cDash", "Key2", "192" });

            MessageBox.Show("Setting will be applied after restarting the application");

            this.Close();
        }

        /// <summary>
        /// exits, without saving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitWithoutSavingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
