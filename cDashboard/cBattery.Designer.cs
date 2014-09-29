namespace cDashboard
{
    partial class cBattery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cBattery));
            this.label_battery_remaining = new System.Windows.Forms.Label();
            this.button_close = new System.Windows.Forms.Button();
            this.label_battery_percentage = new System.Windows.Forms.Label();
            this.panel_battery = new System.Windows.Forms.Panel();
            this.pictureBox_battery = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_battery)).BeginInit();
            this.SuspendLayout();
            // 
            // label_battery_remaining
            // 
            this.label_battery_remaining.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_battery_remaining.ForeColor = System.Drawing.Color.White;
            this.label_battery_remaining.Location = new System.Drawing.Point(63, 48);
            this.label_battery_remaining.Name = "label_battery_remaining";
            this.label_battery_remaining.Size = new System.Drawing.Size(103, 20);
            this.label_battery_remaining.TabIndex = 6;
            this.label_battery_remaining.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_battery_remaining.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_battery_remaining_MouseDown);
            // 
            // button_close
            // 
            this.button_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_close.BackColor = System.Drawing.Color.Transparent;
            this.button_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_close.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_close.ForeColor = System.Drawing.Color.White;
            this.button_close.Location = new System.Drawing.Point(108, 1);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(15, 17);
            this.button_close.TabIndex = 4;
            this.button_close.Text = "X";
            this.button_close.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.button_close.UseVisualStyleBackColor = false;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // label_battery_percentage
            // 
            this.label_battery_percentage.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_battery_percentage.ForeColor = System.Drawing.Color.White;
            this.label_battery_percentage.Location = new System.Drawing.Point(62, 28);
            this.label_battery_percentage.Name = "label_battery_percentage";
            this.label_battery_percentage.Size = new System.Drawing.Size(56, 20);
            this.label_battery_percentage.TabIndex = 1;
            this.label_battery_percentage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_battery_percentage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_battery_percentage_MouseDown);
            // 
            // panel_battery
            // 
            this.panel_battery.BackColor = System.Drawing.Color.White;
            this.panel_battery.Location = new System.Drawing.Point(18, 23);
            this.panel_battery.Name = "panel_battery";
            this.panel_battery.Size = new System.Drawing.Size(27, 45);
            this.panel_battery.TabIndex = 3;
            this.panel_battery.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_battery_MouseDown);
            // 
            // pictureBox_battery
            // 
            this.pictureBox_battery.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_battery.Image")));
            this.pictureBox_battery.Location = new System.Drawing.Point(-27, 5);
            this.pictureBox_battery.Name = "pictureBox_battery";
            this.pictureBox_battery.Size = new System.Drawing.Size(114, 74);
            this.pictureBox_battery.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_battery.TabIndex = 2;
            this.pictureBox_battery.TabStop = false;
            this.pictureBox_battery.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_battery_MouseDown);
            // 
            // cBattery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(124, 87);
            this.Controls.Add(this.label_battery_remaining);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.label_battery_percentage);
            this.Controls.Add(this.panel_battery);
            this.Controls.Add(this.pictureBox_battery);
            this.Name = "cBattery";
            this.Text = "cBattery";
            this.Load += new System.EventHandler(this.cBattery_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cBattery_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_battery)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_battery_percentage;
        private System.Windows.Forms.PictureBox pictureBox_battery;
        private System.Windows.Forms.Panel panel_battery;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.Label label_battery_remaining;
    }
}