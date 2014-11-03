namespace cDashboard
{
    partial class cReminder
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
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.button_set = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.label_instructions = new System.Windows.Forms.Label();
            this.datetimepicker = new System.Windows.Forms.DateTimePicker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rtb_message = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_set
            // 
            this.button_set.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_set.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_set.ForeColor = System.Drawing.Color.White;
            this.button_set.Location = new System.Drawing.Point(158, 117);
            this.button_set.Name = "button_set";
            this.button_set.Size = new System.Drawing.Size(156, 23);
            this.button_set.TabIndex = 3;
            this.button_set.Text = "Set";
            this.toolTip.SetToolTip(this.button_set, "Set this cReminder");
            this.button_set.UseVisualStyleBackColor = true;
            this.button_set.Click += new System.EventHandler(this.button_set_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_cancel.ForeColor = System.Drawing.Color.White;
            this.button_cancel.Location = new System.Drawing.Point(0, 117);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(156, 23);
            this.button_cancel.TabIndex = 6;
            this.button_cancel.Text = "Cancel";
            this.toolTip.SetToolTip(this.button_cancel, "Cancel setting this cReminder");
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // label_instructions
            // 
            this.label_instructions.AutoSize = true;
            this.label_instructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_instructions.ForeColor = System.Drawing.Color.White;
            this.label_instructions.Location = new System.Drawing.Point(-2, 7);
            this.label_instructions.Name = "label_instructions";
            this.label_instructions.Size = new System.Drawing.Size(245, 13);
            this.label_instructions.TabIndex = 4;
            this.label_instructions.Text = "Set Date, Time, Message for cReminder...";
            this.label_instructions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_instructions_MouseDown);
            // 
            // datetimepicker
            // 
            this.datetimepicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.datetimepicker.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datetimepicker.CalendarMonthBackground = System.Drawing.Color.Transparent;
            this.datetimepicker.CustomFormat = "dddd MMMM dd, yyyy -  hh:mm tt ";
            this.datetimepicker.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datetimepicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datetimepicker.Location = new System.Drawing.Point(0, 21);
            this.datetimepicker.Name = "datetimepicker";
            this.datetimepicker.Size = new System.Drawing.Size(314, 25);
            this.datetimepicker.TabIndex = 2;
            this.toolTip.SetToolTip(this.datetimepicker, "Set time, date for cReminder");
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(314, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.menuStrip1_MouseDown);
            // 
            // xToolStripMenuItem
            // 
            this.xToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.xToolStripMenuItem.Name = "xToolStripMenuItem";
            this.xToolStripMenuItem.Size = new System.Drawing.Size(27, 20);
            this.xToolStripMenuItem.Text = "X";
            this.xToolStripMenuItem.ToolTipText = "Close this, cancel";
            this.xToolStripMenuItem.Click += new System.EventHandler(this.xToolStripMenuItem_Click);
            // 
            // rtb_message
            // 
            this.rtb_message.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_message.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_message.Location = new System.Drawing.Point(0, 47);
            this.rtb_message.Name = "rtb_message";
            this.rtb_message.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtb_message.Size = new System.Drawing.Size(314, 67);
            this.rtb_message.TabIndex = 5;
            this.rtb_message.Text = "";
            this.toolTip.SetToolTip(this.rtb_message, "Message text");
            // 
            // cReminder
            // 
            this.AcceptButton = this.button_set;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(314, 143);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.label_instructions);
            this.Controls.Add(this.button_set);
            this.Controls.Add(this.datetimepicker);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.rtb_message);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "cReminder";
            this.Text = "cReminder";
            this.Load += new System.EventHandler(this.cReminder_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cReminder_MouseDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker datetimepicker;
        private System.Windows.Forms.Button button_set;
        private System.Windows.Forms.Label label_instructions;
        private System.Windows.Forms.RichTextBox rtb_message;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.ToolTip toolTip;

    }
}