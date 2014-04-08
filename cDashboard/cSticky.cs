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
    public partial class cSticky : cForm
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
                rtb.SaveFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cDashBoard\\" + this.Name + ".rtf");
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

                list_find.Add("cSticky");
                list_find.Add(this.Name);
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

                find.Add("cSticky");
                find.Add(this.Name);
                find.Add("Size");

                replace.Add("cSticky");
                replace.Add(this.Name);
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

                find.Add("cSticky");
                replace.Add("cSticky");
                string num = (this.Name);
                find.Add(num);
                replace.Add(num);
                find.Add("BackColor");
                replace.Add("BackColor");
                replace.Add(rtb.BackColor.R.ToString());
                replace.Add(rtb.BackColor.G.ToString());
                replace.Add(rtb.BackColor.B.ToString());

                replaceSetting(find, replace);
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
