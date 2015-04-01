//This file is part of cDashboard
//cDashboard - An information-based overlay for Microsoft Windows
//cKBSetup - A keyboard shortcut setup form for cDashboard
//(C) Charles Machalow under the MIT License
using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

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

        /// <summary>
        /// Normal constructor
        /// </summary>
        public cKBSetup()
        {
            InitializeComponent();
        }

        /// <summary>
        /// special constructor to get keys from cDashboard
        /// </summary>
        /// <param name="k1">key 1</param>
        /// <param name="K2">key 2</param>
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
            int checked_keyvalue = e.KeyValue;

            //special checking for modifiers (ctrl, shift, alt)
            if (e.KeyCode == Keys.ControlKey)
            {
                //Left
                if (GetAsyncKeyState(Keys.LControlKey) < 0)
                {
                    checked_keyvalue = (int)Keys.LControlKey;
                }
                //Right
                else if (GetAsyncKeyState(Keys.RControlKey) < 0)
                {
                    checked_keyvalue = (int)Keys.RControlKey;
                }
            }
            else if (e.KeyCode == Keys.Menu) //alt
            {
                //Left
                if (GetAsyncKeyState(Keys.LMenu) < 0)
                {
                    checked_keyvalue = (int)Keys.LMenu;
                }
                //Right
                else if (GetAsyncKeyState(Keys.RMenu) < 0)
                {
                    checked_keyvalue = (int)Keys.RMenu;
                }
            }
            else if (e.KeyCode == Keys.ShiftKey)
            {
                //Left
                if (GetAsyncKeyState(Keys.LShiftKey) < 0)
                {
                    checked_keyvalue = (int)Keys.LShiftKey;
                }
                //Right
                else if (GetAsyncKeyState(Keys.RShiftKey) < 0)
                {
                    checked_keyvalue = (int)Keys.RShiftKey;
                }
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
            if (MessageBox.Show("Are you sure you want to save the fade shortcut: " + Key1.ToString() + " + " + Key2.ToString(), "Save Fade Shortcut?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                replaceSetting(new string[] { "cDash", "Key1" }, new string[] { "cDash", "Key1", ((int)Key1).ToString() });
                replaceSetting(new string[] { "cDash", "Key2" }, new string[] { "cDash", "Key2", ((int)Key2).ToString() });
            }
            this.Close();
        }

        /// <summary>
        /// go back to default: Lctrl, tilde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void goBackToDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save the fade shortcut: LControlKey + Oemtilde", "Go Back To Default Fade Shortcut?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                replaceSetting(new string[] { "cDash", "Key1" }, new string[] { "cDash", "Key1", "162" });
                replaceSetting(new string[] { "cDash", "Key2" }, new string[] { "cDash", "Key2", "192" });
            }
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
