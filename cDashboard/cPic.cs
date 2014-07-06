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
using System.Runtime.InteropServices;

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

        #region Form Dragging and Resizing

        //import both the SendMessage method and the ReleaseCapture method from user32.dll
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        /// <summary>
        /// calls method for moving form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            dragForm(e);
        }

        /// <summary>
        /// calls method for moving form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cPic_MouseDown(object sender, MouseEventArgs e)
        {
            dragForm(e);
        }

        /// <summary>
        /// calls the user32.dll to move the form
        /// </summary>
        /// <param name="e"></param>
        private void dragForm(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //removes mouse capture from current object, and allows it to be sent elsewhere
                ReleaseCapture();

                //WM_NCLBUTTONDOWN = 0xA1
                //HT_CAPTION = 0x2
                //send a windows message that the left mouse button is down on the titlebar 
                SendMessage(this.Handle, 0xA1, 0x2, 0);
            }
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

                //hide menustrip
                menuStrip1.Visible = false;
            }
        }

        #endregion

        #region Hiding MenuStrip and Other MenuStrip Things

        /// <summary>
        /// called if the mouse leaves the main form area
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cPic_MouseLeave(object sender, EventArgs e)
        {
            //if the mouse is anywhere on the form, don't hide the menustrip
            if (this.GetChildAtPoint(this.PointToClient(MousePosition)) == null)
            {
                menuStrip1.Visible = false;
            }
        }

        /// <summary>
        /// called when mouse enters the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cPic_MouseEnter(object sender, EventArgs e)
        {
            menuStrip1.Visible = true;
        }

        /// <summary>
        /// if the menustrip is left, hide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuStrip1_MouseLeave(object sender, EventArgs e)
        {
            //if the mouse is anywhere on the form, don't hide the menustrip
            if (this.GetChildAtPoint(this.PointToClient(MousePosition)) == null)
            {
                menuStrip1.Visible = false;
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
                setupDGV(false);
            }
            else
            {
                //clear up some memory
                dgv_sm.Rows.Clear();
                System.GC.Collect(); //I know its bad but eh, it works

                randomizeFiles(this.Name);

                slideshowManagerToolStripMenuItem.Text = "Show Slideshow Manager";

                //reset background image to what it was via tag
                this.BackgroundImage = Image.FromFile(SETTINGS_LOCATION + this.Name + "\\1");
            }
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
        /// get images/buttons/etc into datagridview
        /// </summary>
        private void setupDGV(bool randomize)
        {
            //initial clearing of DGV
            dgv_sm.Rows.Clear();
            //re randomize files (only on add or delete)
            if (randomize)
            {
                randomizeFiles(this.Name);
                this.Tag = 1;
            }
            //grab images from folder and display them along with a delete button
            foreach (FileInfo f in (new DirectoryInfo(SETTINGS_LOCATION + this.Name).GetFiles()))
            {
                //add all files to dgv
                using (Image img = Image.FromFile(f.FullName))
                {
                    dgv_sm.Rows.Add(new Bitmap(img), "Delete Image", f.FullName);
                    img.Dispose();
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
            if (e.RowIndex == dgv_sm.Rows.Count - 1)
            {
                addImageToCPic();
            }
            else
            {
                //remove file, row
                string image_location = dgv_sm.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString();
                dgv_sm.Rows.RemoveAt(e.RowIndex);
                File.Delete(image_location);

                //remove cPic if no images to show
                if (dgv_sm.RowCount == 1)
                {
                    MessageBox.Show("There are no more images for this cPic and as a result it is being removed.");
                    this.Close();
                }
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
                    //sleeping for 5 millisecond eliminates chance of multiple completion on same tick
                    System.Threading.Thread.Sleep(5);

                    string tmp_ident = DateTime.Now.Ticks.ToString();
                    string copyto = SETTINGS_LOCATION + this.Name + "\\" + tmp_ident;
                    System.IO.File.Copy(file, copyto, true);

                    //add to dgv
                    using (Image img = Image.FromFile(copyto))
                    {
                        dgv_sm.Rows.Insert(dgv_sm.Rows.Count - 1, new Bitmap(img), "Delete Image", copyto);
                        img.Dispose();
                    }

                    //fix sizing
                    dgv_sm.Rows[dgv_sm.Rows.Count - 2].Height = 100;
                }
            }
        }

        /// <summary>
        /// (x) close button, closes form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        /// <summary>
        /// grabs all relevant Windows Messages
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            //WINDOWS API CODES 
            //WM_NCHITTEST = 0x84
            //HTLEFT = 10
            //HTRIGHT = 11
            //HTTOP = 12
            //HTTOPLEFT = 13
            //HTTOPRIGHT = 14
            //HTBOTTOM = 15
            //HTBOTTOMLEFT = 16
            //HTBOTTOMRIGHT = 17
            if (m.Msg == 0x84)
            {
                //get the lower 4 bits
                int x = (int)(m.LParam.ToInt64() & 0xFFFF);
                //get higher 4 bits
                int y = (int)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);
                Point pt = PointToClient(new Point(x, y));

                //top
                if (pt.Y <= 10 && ClientSize.Height >= 25)
                {
                    m.Result = (IntPtr)(12);
                }

                //bottom
                if (pt.Y >= ClientSize.Height - 10 && ClientSize.Height >= 25)
                {
                    m.Result = (IntPtr)(15);
                }

                //left
                if (pt.X <= 15 && ClientSize.Height >= 25)
                {
                    m.Result = (IntPtr)(10);
                }

                //right
                if (pt.X >= ClientSize.Width - 15 && ClientSize.Height >= 25)
                {
                    m.Result = (IntPtr)(11);
                }

                //bottom right
                if (pt.X >= ClientSize.Width - 25 && pt.Y >= ClientSize.Height - 25 && ClientSize.Height >= 25)
                {
                    m.Result = (IntPtr)(IsMirrored ? 16 : 17);
                }

                //bottom left
                if (pt.X <= 25 && pt.Y >= ClientSize.Height - 25 && ClientSize.Height >= 25)
                {
                    m.Result = (IntPtr)(IsMirrored ? 17 : 16);
                }

                //top left
                if (pt.X <= 15 && pt.Y <= 15 && ClientSize.Height >= 25)
                {
                    m.Result = (IntPtr)(IsMirrored ? 14 : 13);
                }

                //top right
                //this also handles small forms
                if (pt.X >= ClientSize.Width - 25 && pt.Y <= 25)
                {
                    m.Result = (IntPtr)(IsMirrored ? 13 : 14);
                }

                //we results between 10 and 17, so return
                if (m.Result.ToInt32() <= 17 && m.Result.ToInt32() >= 10)
                {
                    //hiding the menustrip again is a failsafe, it will prevent most chances for error
                    menuStrip1.Visible = false; 
                    return;
                }
            }

            //if this isn't the message we are modifying, let it go
            base.WndProc(ref m);
        }
    }
}
