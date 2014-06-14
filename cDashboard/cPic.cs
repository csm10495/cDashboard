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
    public partial class cPic : cForm
    {
        public cPic()
        {
            InitializeComponent();
        }

        /// <summary>
        /// marks if the form has finished loading
        /// </summary>
        bool CompletedForm_Load = false;

        /// <summary>
        /// called when the form has finished loading
        /// used with locking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cPic_Load(object sender, EventArgs e)
        {
            CompletedForm_Load = true;
        }

        /// <summary>
        /// called to make sure the combobox shows the correct
        /// imagelayout for this cPic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cPicLayoutToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            if (this.BackgroundImageLayout == ImageLayout.Center)
            {
                cPicLayoutComboBox.SelectedIndex = 0;
            }
            else if (this.BackgroundImageLayout == ImageLayout.None)
            {
                cPicLayoutComboBox.SelectedIndex = 1;
            }
            else if (this.BackgroundImageLayout == ImageLayout.Stretch)
            {
                cPicLayoutComboBox.SelectedIndex = 2;
            }
            else if (this.BackgroundImageLayout == ImageLayout.Tile)
            {
                cPicLayoutComboBox.SelectedIndex = 3;
            }
            else if (this.BackgroundImageLayout == ImageLayout.Zoom)
            {
                cPicLayoutComboBox.SelectedIndex = 4;
            }
        }

        /// <summary>
        /// changing the combobox results in a change to the background image layout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cPicLayoutComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string string_layout = "";

            if (cPicLayoutComboBox.SelectedIndex == 0)
            {
                this.BackgroundImageLayout = ImageLayout.Center;
                string_layout = "Center";
            }
            else if (cPicLayoutComboBox.SelectedIndex == 1)
            {
                this.BackgroundImageLayout = ImageLayout.None;
                string_layout = "None";
            }
            else if (cPicLayoutComboBox.SelectedIndex == 2)
            {
                this.BackgroundImageLayout = ImageLayout.Stretch;
                string_layout = "Stretch";
            }
            else if (cPicLayoutComboBox.SelectedIndex == 3)
            {
                this.BackgroundImageLayout = ImageLayout.Tile;
                string_layout = "Tile";
            }
            else if (cPicLayoutComboBox.SelectedIndex == 4)
            {
                this.BackgroundImageLayout = ImageLayout.Zoom;
                string_layout = "Zoom";
            }

            replaceSetting(new[] { "cPic", this.Name, "ImageLayout" }, new[] { "cPic", this.Name, "ImageLayout", string_layout });

        }

        /// <summary>
        /// used to save the new size of the form to settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cPic_ResizeEnd(object sender, EventArgs e)
        {
            if (CompletedForm_Load == true)
            {
                //handle resize
                replaceSetting(new string[] {"cPic", this.Name, "Size"}, new string[] {"cPic", this.Name, "Size", this.Size.Width.ToString(), this.Size.Height.ToString()});

                //handle move
                replaceSetting(new string[] {"cPic", this.Name, "Location"}, new string[] {"cPic", this.Name, "Location", this.Location.X.ToString(), this.Location.Y.ToString()});
            }
        }
    }
}
