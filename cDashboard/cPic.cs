//This file is part of cDashboard
//cPic - A picture viewer for cDashboard
//(C) Charles Machalow 2014 under the MIT License
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
                replaceSetting(new string[] { "cPic", this.Name, "Size" }, new string[] { "cPic", this.Name, "Size", this.Size.Width.ToString(), this.Size.Height.ToString() });

                //handle move
                replaceSetting(new string[] { "cPic", this.Name, "Location" }, new string[] { "cPic", this.Name, "Location", this.Location.X.ToString(), this.Location.Y.ToString() });
            }
        }


        /// <summary>
        /// Opens slideshow manager datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slideshowManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //boolean flip
            dgv_sm.Visible = !dgv_sm.Visible;

            //set menu item text
            if (dgv_sm.Visible)
            {
                //manually memory management to ensure no file locks
                this.BackgroundImage.Dispose();
                this.BackgroundImage = null;

                slideshowManagerToolStripMenuItem.Text = "Hide Slideshow Manager";
                setupDGV();
            }
            else
            {
                //clear up some memory
                dgv_sm.Rows.Clear();

                slideshowManagerToolStripMenuItem.Text = "Show Slideshow Manager";

                //set cPic_new's background image equal to a random image in its folder
                Random r = new Random();
                string[] files = System.IO.Directory.GetFiles(SETTINGS_LOCATION + this.Name);
                this.BackgroundImage = Image.FromFile(files[r.Next(files.Length)]);
            }
        }

        /// <summary>
        /// get images/buttons/etc into datagridview
        /// </summary>
        private void setupDGV()
        {
            //initial clearing of DGV
            dgv_sm.Rows.Clear();

            //grab images from folder and display them along with a delete button
            foreach (FileInfo f in (new DirectoryInfo(SETTINGS_LOCATION + this.Name).GetFiles()))
            {
                //add all files to dgv
                using (Image img = Image.FromFile(f.FullName))
                {
                    dgv_sm.Rows.Add(new Bitmap(img), "Delete Image", f.FullName);
                }

                //default row height of 100 pixels
                dgv_sm.Rows[dgv_sm.Rows.Count - 1].Height = 100;
            }

            //add the add image row
            dgv_sm.Rows.Add(new Bitmap(1, 1), "Add Image", "");
        }

        /// <summary>
        /// called when a cell is clicked, will be used to delete / add images
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_sm_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if sender control is not in the button column, return
            if (e.ColumnIndex != 1)
                return;

            //special handling if this is an add image click
            //eg the last row button has been clicked
            if (e.RowIndex == dgv_sm.RowCount - 1)
            {
                addImageToCPic();
                return;
            }

            string image_location = dgv_sm.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString();
            dgv_sm.Rows[e.RowIndex].Dispose();
            File.Delete(image_location);

            //reset dgv
            setupDGV();

            //remove cPic if no images to show
            if (dgv_sm.RowCount == 1)
            {
                MessageBox.Show("There are no more images for this cPic and as a result it is being removed.");
                this.Close();
            }
        }

        /// <summary>
        /// called to add image to this cPic
        /// </summary>
        private void addImageToCPic()
        {
            OpenFileDialog openFileDialog1 = ((cDashboard)this.ParentForm).openFileDialog1;
            //set multiselect to true
            openFileDialog1.Multiselect = true;

            //setup the OpenFileDialog to only accept images
            openFileDialog1.Filter = "Image Files (*.bmp, *.jpg, *.png, *.tiff, *.gif)|*.bmp;*.jpg;*.png;*.tiff;*.gif";

            //change title of openFileDialog1
            openFileDialog1.Title = "Select more image(s) for cPic...";

            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                foreach (string file in openFileDialog1.FileNames)
                {
                    System.IO.File.Copy(file, SETTINGS_LOCATION + file.Substring(file.LastIndexOf("\\") + 1), false);

                    //sleeping for 1 millisecond eliminates chance of multiple completion on same tick
                    System.Threading.Thread.Sleep(1);

                    System.IO.File.Move(SETTINGS_LOCATION + file.Substring(file.LastIndexOf("\\") + 1), SETTINGS_LOCATION + this.Name + "\\" + DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(file));
                }
            }
            //make sure everything is placed properly
            setupDGV();
        }

    }
}
