namespace cDashboard
{
    partial class cMote
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cMote));
            this.button_previous = new System.Windows.Forms.Button();
            this.button_next = new System.Windows.Forms.Button();
            this.button_play_pause = new System.Windows.Forms.Button();
            this.button_close = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.button_test = new System.Windows.Forms.Button();
            this.picturebox_albumart = new System.Windows.Forms.PictureBox();
            this.button_vol_down = new System.Windows.Forms.Button();
            this.button_vol_up = new System.Windows.Forms.Button();
            this.button_vol_mute = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picturebox_albumart)).BeginInit();
            this.SuspendLayout();
            // 
            // button_previous
            // 
            this.button_previous.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_previous.BackgroundImage")));
            this.button_previous.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_previous.FlatAppearance.BorderSize = 0;
            this.button_previous.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_previous.Location = new System.Drawing.Point(7, 26);
            this.button_previous.Name = "button_previous";
            this.button_previous.Size = new System.Drawing.Size(65, 53);
            this.button_previous.TabIndex = 3;
            this.toolTip.SetToolTip(this.button_previous, "Previous Track");
            this.button_previous.UseVisualStyleBackColor = true;
            this.button_previous.Click += new System.EventHandler(this.button_previous_Click);
            // 
            // button_next
            // 
            this.button_next.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_next.BackgroundImage")));
            this.button_next.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_next.FlatAppearance.BorderSize = 0;
            this.button_next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_next.Location = new System.Drawing.Point(182, 26);
            this.button_next.Name = "button_next";
            this.button_next.Size = new System.Drawing.Size(65, 53);
            this.button_next.TabIndex = 2;
            this.toolTip.SetToolTip(this.button_next, "Next Track");
            this.button_next.UseVisualStyleBackColor = true;
            this.button_next.Click += new System.EventHandler(this.button_next_Click);
            // 
            // button_play_pause
            // 
            this.button_play_pause.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_play_pause.BackgroundImage")));
            this.button_play_pause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_play_pause.FlatAppearance.BorderSize = 0;
            this.button_play_pause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_play_pause.Location = new System.Drawing.Point(86, 16);
            this.button_play_pause.Name = "button_play_pause";
            this.button_play_pause.Size = new System.Drawing.Size(90, 72);
            this.button_play_pause.TabIndex = 1;
            this.toolTip.SetToolTip(this.button_play_pause, "Play/Pause");
            this.button_play_pause.UseVisualStyleBackColor = true;
            this.button_play_pause.Click += new System.EventHandler(this.button_play_pause_Click);
            // 
            // button_close
            // 
            this.button_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_close.ForeColor = System.Drawing.Color.White;
            this.button_close.Location = new System.Drawing.Point(236, 1);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(20, 22);
            this.button_close.TabIndex = 0;
            this.button_close.Text = "X";
            this.toolTip.SetToolTip(this.button_close, "Close cMote");
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // button_test
            // 
            this.button_test.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_test.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_test.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_test.ForeColor = System.Drawing.Color.White;
            this.button_test.Location = new System.Drawing.Point(210, 1);
            this.button_test.Name = "button_test";
            this.button_test.Size = new System.Drawing.Size(20, 22);
            this.button_test.TabIndex = 4;
            this.button_test.Text = "T";
            this.toolTip.SetToolTip(this.button_test, "Close cMote");
            this.button_test.UseVisualStyleBackColor = true;
            this.button_test.Visible = false;
            this.button_test.Click += new System.EventHandler(this.button_test_Click);
            // 
            // picturebox_albumart
            // 
            this.picturebox_albumart.Location = new System.Drawing.Point(140, 1);
            this.picturebox_albumart.Name = "picturebox_albumart";
            this.picturebox_albumart.Size = new System.Drawing.Size(36, 34);
            this.picturebox_albumart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picturebox_albumart.TabIndex = 5;
            this.picturebox_albumart.TabStop = false;
            this.picturebox_albumart.Visible = false;
            // 
            // button_vol_down
            // 
            this.button_vol_down.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_vol_down.BackgroundImage")));
            this.button_vol_down.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_vol_down.FlatAppearance.BorderSize = 0;
            this.button_vol_down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_vol_down.Location = new System.Drawing.Point(67, 79);
            this.button_vol_down.Name = "button_vol_down";
            this.button_vol_down.Size = new System.Drawing.Size(40, 37);
            this.button_vol_down.TabIndex = 6;
            this.toolTip.SetToolTip(this.button_vol_down, "Volume Down");
            this.button_vol_down.UseCompatibleTextRendering = true;
            this.button_vol_down.UseVisualStyleBackColor = true;
            this.button_vol_down.Click += new System.EventHandler(this.button_vol_down_Click);
            // 
            // button_vol_up
            // 
            this.button_vol_up.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_vol_up.BackgroundImage")));
            this.button_vol_up.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_vol_up.FlatAppearance.BorderSize = 0;
            this.button_vol_up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_vol_up.Location = new System.Drawing.Point(149, 79);
            this.button_vol_up.Name = "button_vol_up";
            this.button_vol_up.Size = new System.Drawing.Size(40, 37);
            this.button_vol_up.TabIndex = 7;
            this.toolTip.SetToolTip(this.button_vol_up, "Volume Up");
            this.button_vol_up.UseVisualStyleBackColor = true;
            this.button_vol_up.Click += new System.EventHandler(this.button_vol_up_Click);
            // 
            // button_vol_mute
            // 
            this.button_vol_mute.AutoSize = true;
            this.button_vol_mute.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_vol_mute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_vol_mute.FlatAppearance.BorderSize = 0;
            this.button_vol_mute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_vol_mute.Font = new System.Drawing.Font("Arial Narrow", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_vol_mute.ForeColor = System.Drawing.Color.White;
            this.button_vol_mute.Location = new System.Drawing.Point(105, 76);
            this.button_vol_mute.Name = "button_vol_mute";
            this.button_vol_mute.Size = new System.Drawing.Size(51, 43);
            this.button_vol_mute.TabIndex = 8;
            this.button_vol_mute.Text = "🔇";
            this.button_vol_mute.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolTip.SetToolTip(this.button_vol_mute, "Volume Mute");
            this.button_vol_mute.UseVisualStyleBackColor = true;
            this.button_vol_mute.Click += new System.EventHandler(this.button_vol_mute_Click);
            // 
            // cMote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(257, 118);
            this.Controls.Add(this.button_vol_up);
            this.Controls.Add(this.button_play_pause);
            this.Controls.Add(this.button_vol_down);
            this.Controls.Add(this.button_vol_mute);
            this.Controls.Add(this.picturebox_albumart);
            this.Controls.Add(this.button_test);
            this.Controls.Add(this.button_previous);
            this.Controls.Add(this.button_next);
            this.Controls.Add(this.button_close);
            this.Name = "cMote";
            this.Text = "cMote";
            this.Load += new System.EventHandler(this.cMote_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cMote_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.picturebox_albumart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.Button button_play_pause;
        private System.Windows.Forms.Button button_next;
        private System.Windows.Forms.Button button_previous;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button button_test;
        private System.Windows.Forms.PictureBox picturebox_albumart;
        private System.Windows.Forms.Button button_vol_down;
        private System.Windows.Forms.Button button_vol_up;
        private System.Windows.Forms.Button button_vol_mute;
    }
}