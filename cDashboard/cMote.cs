//This file is part of cDashboard
//cMote - A media controller for cDashboard
//(C) Charles Machalow 2014 under the MIT License
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace cDashboard
{
    public partial class cMote : cForm
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public cMote()
        {
            InitializeComponent();
        }

        /// <summary>
        /// standard form loading method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cMote_Load(object sender, EventArgs e)
        {
            CompletedForm_Load = true;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        /// <summary>
        /// sends a virtual key stroke for the key 'key'
        /// </summary>
        /// <param name="key"></param>
        private void cSendKey(byte key)
        {
            keybd_event(key, 0, 0x0001 /*KEYEVENTF_EXTENDEDKEY*/, 0);
            keybd_event(key, 0, 0x0002 /*KEYEVENTF_KEYUP*/, 0);
        }

        /// <summary>
        /// send a global play/pause command via the Windows API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_play_pause_Click(object sender, EventArgs e)
        {
            cSendKey(0xB3 /*VK_MEDIA_PLAY_PAUSE*/);
        }

        /// <summary>
        /// send a global next command via the Windows API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_next_Click(object sender, EventArgs e)
        {
            cSendKey(0xB0 /*VK_MEDIA_NEXT_TRACK*/);
        }

        /// <summary>
        /// send a global prev command via the Windows API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_previous_Click(object sender, EventArgs e)
        {
            cSendKey(0xB1 /*VK_MEDIA_PREV_TRACK*/);
        }

        /// <summary>
        /// closes this cForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Dragging
        /// <summary>
        /// if mouse is down on the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cMote_MouseDown(object sender, MouseEventArgs e)
        {
            dragForm(e);
        }
        #endregion
    }
}
