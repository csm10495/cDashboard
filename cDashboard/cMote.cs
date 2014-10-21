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
using System.Net;
using System.Web.Script.Serialization;

namespace cDashboard
{
    public partial class cMote : cForm
    {
        Dictionary<string, dynamic> dict;

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
            this.Size = new Size(257, 118);
            getSpotifyInfo();
            CompletedForm_Load = true;
        }

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        /// <summary>
        /// sends a virtual key stroke for the key 'key'
        /// </summary>
        /// <param name="key"></param>
        private void cSendKey(byte key)
        {
            keybd_event(key, 0, 0x0001 /*KEYEVENTF_EXTENDEDKEY*/ | 0, 0); ;
            //MessageBox.Show("DANG!");    //Why does this still fix the problem
            keybd_event(key, 0, 0x0001 /*KEYEVENTF_EXTENDEDKEY*/ | 0x0002 /*KEYEVENTF_KEYUP*/, 0);

            // System.Threading.Thread.Sleep(3000);
        }

        #region Buttons
        /// <summary>
        /// send a global play/pause command via the Windows API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_play_pause_Click(object sender, EventArgs e)
        {
            cSendKey(0xB3 /*VK_MEDIA_PLAY_PAUSE*/);
            getSpotifyInfo();
            button_test.PerformClick();
        }

        /// <summary>
        /// send a global next command via the Windows API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_next_Click(object sender, EventArgs e)
        {
            cSendKey(0xB0 /*VK_MEDIA_NEXT_TRACK*/);
            getSpotifyInfo();
            button_test.PerformClick();

        }

        /// <summary>
        /// send a global prev command via the Windows API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_previous_Click(object sender, EventArgs e)
        {
            cSendKey(0xB1 /*VK_MEDIA_PREV_TRACK*/);
            getSpotifyInfo();
            button_test.PerformClick();
        }

        /// <summary>
        /// send a global mute command via the Windows API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_vol_mute_Click(object sender, EventArgs e)
        {
            cSendKey(0xAD /*VK_VOLUME_MUTE*/);
        }

        /// <summary>
        /// send a volume up command via Windows API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_vol_up_Click(object sender, EventArgs e)
        {
            cSendKey(0xAF /*VK_VOLUME_UP*/);
        }

        /// <summary>
        /// send a volume down command via Windows API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_vol_down_Click(object sender, EventArgs e)
        {
            cSendKey(0xAE /*VK_VOLUME_DOWN*/);
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
        #endregion

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

        /// <summary>
        /// grabs JSON from URL
        /// returns null on fail
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private Dictionary<string, dynamic> getDictFromJsonUrl(string url)
        {
            //predeclare before try/catch
            WebRequest request;
            WebResponse response;
            System.IO.StreamReader reader;
            Dictionary<string, dynamic> dict;

            //try/catch is used to detect connectivity
            //any catch should be the result of a lack of internet access
            try
            {
                request = WebRequest.Create(url);
                response = request.GetResponse();
                reader = new System.IO.StreamReader(response.GetResponseStream());
                string string_url_text = reader.ReadToEnd();
                dict = new JavaScriptSerializer().Deserialize<Dictionary<string, dynamic>>(string_url_text);
            }
            catch
            {
                return null;
            }

            return dict;
        }

        #region Spotify Integration (STASH'd)
        /// <summary>
        /// testing album artwork data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_test_Click(object sender, EventArgs e)
        {
            getSpotifyInfo();
        }

        /// <summary>
        /// gets Spotify information via API
        /// </summary>
        private void getSpotifyInfo()
        {
            //get Spotify process
            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                //short circuit eval
                //at this point process == spotify
                if (process.MainWindowTitle.Length >= 7 && process.MainWindowTitle.Substring(0, 7) == "Spotify" && process.ProcessName.ToLower() == "spotify" && process.MainWindowTitle != "Spotify")
                {

                    string artist_and_song = process.MainWindowTitle.Substring(process.MainWindowTitle.IndexOf(" - ") + 3);
                    string artist = artist_and_song.Substring(0, artist_and_song.IndexOf(" – "));     //need to handle error (happens when we catch spotify between songs or without a title)
                    string song = artist_and_song.Substring(artist.Length + 3);

                    //Spotify API call
                    dict = getDictFromJsonUrl("https://api.spotify.com/v1/search?q=" + song + "&type=track");

                    if (dict == null)
                    {
                        picturebox_albumart.Image = null;
                        this.Size = new Size(257, 118);
                        return;
                    }

                    //set true when we have gotten an img from the API
                    bool gotImg = false;

                    foreach (var i in dict["tracks"]["items"])
                    {
                        if (i["artists"][0]["name"] == artist)
                        {
                            string img_url = i["album"]["images"][0]["url"];
                            picturebox_albumart.Load(img_url);
                            gotImg = true;
                            break;
                        }
                    }

                    //blank out album art, other things, if we can't find it via API
                    if (!gotImg)
                    {
                        picturebox_albumart.Image = null;
                        this.Size = new Size(257, 118);
                        return;
                    }
                    else
                    {
                        this.Size = new Size(257, 161);
                        label_songname.Text = song;
                        label_artist.Text = artist;
                        return;
                    }
                }
            }
            picturebox_albumart.Image = null;
            this.Size = new Size(257, 118);
        }
        #endregion


        private void bg_request_DoWork(object sender, DoWorkEventArgs e)
        {

        }
    }
}
