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
            // cMote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(257, 101);
            this.Controls.Add(this.button_previous);
            this.Controls.Add(this.button_next);
            this.Controls.Add(this.button_play_pause);
            this.Controls.Add(this.button_close);
            this.Name = "cMote";
            this.Text = "cMote";
            this.Load += new System.EventHandler(this.cMote_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cMote_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.Button button_play_pause;
        private System.Windows.Forms.Button button_next;
        private System.Windows.Forms.Button button_previous;
        private System.Windows.Forms.ToolTip toolTip;
    }
}