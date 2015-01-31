//This file is part of cDashboard
//cDashboard - An information-based overlay for Microsoft Windows
//cRViewer - A viewer and editor for all cReminders
//(C) Charles Machalow under the MIT License
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
    public partial class cRViewer : cForm
    {
        /// <summary>
        /// constructor
        /// </summary>
        public cRViewer()
        {
            InitializeComponent();
        }

        #region Events and Overrides
        /// <summary>
        /// form load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cRViewer_Load(object sender, EventArgs e)
        {
            updateDGV();
            CompletedForm_Load = true;
        }

        /// <summary>
        /// grabs all relevant Windows Messages
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            formResizer(ref m);
        }

        /// <summary>
        /// allow moving the form via mouse down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menustrip_MouseDown(object sender, MouseEventArgs e)
        {
            dragForm(e);
        }

        /// <summary>
        /// drag form by label_title
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_title_MouseDown(object sender, MouseEventArgs e)
        {
            dragForm(e);
        }
        #endregion

        /// <summary>
        /// close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// updates the DGV with all cReminders
        /// </summary>
        public void updateDGV()
        {
            dgv.Rows.Clear();
            SortedDictionary<long, string> cReminders = ((cDashboard)this.Parent).getDictCReminders();
            foreach (KeyValuePair<long, string> cR in cReminders)
            {
                dgv.Rows.Add(new DateTime(cR.Key).ToString(), cR.Value, "Remove", cR.Key.ToString());
            }
        }

        /// <summary>
        /// replaces the setting for cReminders from the updated dict
        /// </summary>
        /// <param name="cReminders"></param>
        private void saveCRemindersToSettingsFromDict(ref SortedDictionary<long, string> cReminders)
        {
            List<string> find = new List<string>();
            find.Add("cReminders");

            List<string> replace = new List<string>();
            replace.Add("cReminders");

            foreach (KeyValuePair<long, string> cR in cReminders)
            {
                replace.Add(cR.Key.ToString());
                replace.Add(cR.Value);
            }

            replaceSetting(find, replace);
        }

        /// <summary>
        /// called when a cell is clicked, will be used to remove cReminders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //make sure we are in the button column
            if (e.ColumnIndex != 2)
                return;

            SortedDictionary<long, string> cReminders = ((cDashboard)this.Parent).getDictCReminders();
            cReminders.Remove(new DateTime(Convert.ToInt64(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value)).Ticks);
           
            saveCRemindersToSettingsFromDict(ref cReminders);

            ((cDashboard)this.Parent).updateDictCReminders();
        }

        /// <summary>
        /// add a new cReminder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plusStripMenuItem1_Click(object sender, EventArgs e)
        {
            //represents the a unique time stamp for use as name of form
            long long_unique_timestamp = DateTime.Now.Ticks;

            cReminder cReminder_new = new cReminder();
            cReminder_new.Name = long_unique_timestamp.ToString();
            cReminder_new.Location = new Point(10, 25);
            cReminder_new.TopLevel = false;
            cReminder_new.Parent = this.Parent;

            this.Parent.Controls.Add(cReminder_new);
            cReminder_new.Show();
            cReminder_new.BringToFront();
        }

    }
}
