//This file is part of cDashboard
//cDashboard - An information-based overlay for Microsoft Windows
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
            getSpotifyInfoViaThread();

            try
            {
                if (getSpecificSetting(new string[] { "cMote", this.Name, "SpotifyIntegration" })[0] == "False")
                {
                    checkbox_spotify_integration.Checked = false;
                }
                else
                {
                    checkbox_spotify_integration.Checked = true;
                }
            }
            catch (ArgumentOutOfRangeException) //the only real exception would be that this value doesn't exist in the settings
            {
                replaceSetting(new string[] { "cMote", this.Name, "SpotifyIntegration" }, new string[] { "cMote", this.Name, "SpotifyIntegration", "False" });
            }

            CompletedForm_Load = true;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        /// <summary>
        /// sends a virtual key stroke for the key 'key'
        /// </summary>
        /// <param name="key"></param>
        private void cSendKey(uint key)
        {
            Process spotify = getSpotifyProcess();
            string spotify_window_title = "";
            if (spotify != null)
                spotify_window_title = spotify.MainWindowTitle;

            SendMessage(this.Handle, 0x319 /*WM_APPCOMMAND*/, this.Handle, (IntPtr)((int)key << 16));

            if (spotify != null)
            {
                int x = 0;
                //if we check for a change 20 times and it is the same, then it probably is the same song
                while (getSpotifyProcess().MainWindowTitle == spotify_window_title && x < 20)
                {
                    //busy waiting (not very good but eh...)
                    x++;
                }
            }
        }

        #region Buttons
        /// <summary>
        /// send a global play/pause command via the Windows API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_play_pause_Click(object sender, EventArgs e)
        {
            cSendKey(14 /*PlayPause*/);
            getSpotifyInfoViaThread();
        }

        /// <summary>
        /// send a global next command via the Windows API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_next_Click(object sender, EventArgs e)
        {
            cSendKey(11 /*MediaNext*/);
            getSpotifyInfoViaThread();
        }

        /// <summary>
        /// send a global prev command via the Windows API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_previous_Click(object sender, EventArgs e)
        {
            cSendKey(12 /*MediaPrevious*/);
            getSpotifyInfoViaThread();
        }

        /// <summary>
        /// send a global mute command via the Windows API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_vol_mute_Click(object sender, EventArgs e)
        {
            cSendKey(8 /*VolumeMute*/);
        }

        /// <summary>
        /// send a volume up command via Windows API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_vol_up_Click(object sender, EventArgs e)
        {
            cSendKey(10 /*VolumeUp*/);
        }

        /// <summary>
        /// send a volume down command via Windows API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_vol_down_Click(object sender, EventArgs e)
        {
            cSendKey(9 /*VolumeDown*/);
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

        #region Spotify Integration

        /// <summary>
        /// returns the spotify process
        /// </summary>
        /// <returns></returns>
        private Process getSpotifyProcess()
        {
            if (Process.GetProcessesByName("Spotify").Length > 0)
                return Process.GetProcessesByName("Spotify")[0];
            else
                return null;
        }

        /// <summary>
        /// update SpotifyIntegration in the settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkbox_spotify_integration_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbox_spotify_integration.Checked)
            {
                replaceSetting(new string[] { "cMote", this.Name, "SpotifyIntegration" }, new string[] { "cMote", this.Name, "SpotifyIntegration", "True" });
                getSpotifyInfo();
            }
            else
            {
                replaceSetting(new string[] { "cMote", this.Name, "SpotifyIntegration" }, new string[] { "cMote", this.Name, "SpotifyIntegration", "False" });
                picturebox_albumart.Image = null;
                this.Size = new Size(257, 118);
                return;
            }
        }

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
        /// gets Spotify information via API, threaded
        /// </summary>
        public void getSpotifyInfoViaThread()
        {
            System.Threading.Thread t1 = new System.Threading.Thread(getSpotifyInfo);
            t1.Start();
        }

        /// <summary>
        /// gets Spotify information via API
        /// </summary>
        private void getSpotifyInfo()
        {
            //cancel if spotify integration is not checked
            if (!checkbox_spotify_integration.Checked)
                return;

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
                    Dictionary<string, dynamic> dict = getDictFromJsonUrl("https://api.spotify.com/v1/search?q=track:" + song + "%20artist:" + artist + "&type=track");

                    if (dict == null)
                    {
                        if (picturebox_albumart.InvokeRequired)
                        {
                            picturebox_albumart.Invoke(new MethodInvoker(delegate
                            {
                                picturebox_albumart.Image = null;
                                this.Size = new Size(257, 118);
                            }));
                        }
                        else
                        {
                            picturebox_albumart.Image = null;
                            this.Size = new Size(257, 118);
                        }

                        return;
                    }

                    //set true when we have gotten an img from the API
                    bool gotImg = false;

                    foreach (var i in dict["tracks"]["items"])
                    {
                        if (i["artists"][0]["name"] == artist)
                        {
                            string img_url = i["album"]["images"][0]["url"];
                            if (picturebox_albumart.InvokeRequired)
                            {
                                picturebox_albumart.Invoke(new MethodInvoker(delegate
                                {
                                    picturebox_albumart.Load(img_url);
                                    picturebox_albumart.Tag = img_url;
                                }));
                            }
                            else
                            {
                                picturebox_albumart.Load(img_url);
                                picturebox_albumart.Tag = img_url;
                            }

                            gotImg = true;
                            break;
                        }
                    }

                    //blank out album art, other things, if we can't find it via API
                    if (!gotImg)
                    {
                        if (picturebox_albumart.InvokeRequired)
                        {
                            picturebox_albumart.Invoke(new MethodInvoker(delegate
                            {
                                picturebox_albumart.Image = null;
                                this.Size = new Size(257, 118);
                            }));
                        }
                        else
                        {
                            picturebox_albumart.Image = null;
                            this.Size = new Size(257, 118);
                        }
                        return;
                    }
                    else
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                                this.Size = new Size(257, 161);
                                label_songname.Text = song;
                                label_artist.Text = artist;
                            }));
                        }
                        else
                        {
                            this.Size = new Size(257, 161);
                            label_songname.Text = song;
                            label_artist.Text = artist;
                        }
                        return;
                    }
                }
            }
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    picturebox_albumart.Image = null;
                    this.Size = new Size(257, 118);
                }));
            }
            else
            {
                picturebox_albumart.Image = null;
                this.Size = new Size(257, 118);
            }

        }
        #endregion

        /// <summary>
        /// show huge cover art in cPic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picturebox_albumart_Click(object sender, EventArgs e)
        {
            //represents the a unique time stamp for use as name of form / image
            long long_unique_timestamp = DateTime.Now.Ticks;

            string file = picturebox_albumart.Tag.ToString();

            //get cover art
            WebClient Client = new WebClient();
            //the only exception is connection failed, so just abort.
            try
            {
                Client.DownloadFile(file, SETTINGS_LOCATION + "tmp");
                Client.Dispose();
            }
            catch (Exception) 
            {
                return;
            }

            //make directory
            System.IO.Directory.CreateDirectory(SETTINGS_LOCATION + long_unique_timestamp.ToString());

            System.IO.File.Move(SETTINGS_LOCATION + "tmp", SETTINGS_LOCATION + long_unique_timestamp.ToString() + "\\" + DateTime.Now.Ticks.ToString());
            cPic cPic_new = new cPic();
            cPic_new.Name = long_unique_timestamp.ToString();
            cPic_new.Location = new Point(10, 25);
            cPic_new.Size = new Size(640, 640);
            cPic_new.TopLevel = false;
            cPic_new.Parent = this.Parent;
            cPic_new.Tag = 1;

            //properly name, randomize files
            randomizeFiles(cPic_new.Name);

            //set cPic_new's background image equal to image number 1 from the folder
            cPic_new.BackgroundImage = Image.FromFile(SETTINGS_LOCATION + cPic_new.Name + "\\1");

            //changed default behavior to zoom
            cPic_new.BackgroundImageLayout = ImageLayout.Zoom;

            this.Parent.Controls.Add(cPic_new);
            cPic_new.Show();
            cPic_new.BringToFront();

            //add the cPic to settings
            System.IO.StreamWriter sw = new System.IO.StreamWriter(SETTINGS_LOCATION + "cDash Settings.cDash", true);
            sw.WriteLine("cPic;" + long_unique_timestamp.ToString() + ";FolderName;" + long_unique_timestamp.ToString());
            sw.WriteLine("cPic;" + long_unique_timestamp.ToString() + ";Size;350;400");
            sw.WriteLine("cPic;" + long_unique_timestamp.ToString() + ";Location;10;25");
            sw.WriteLine("cPic;" + long_unique_timestamp.ToString() + ";ImageLayout;Zoom");
            sw.Close();

        }

    }
}
