namespace cDashboard
{
    partial class cKBSetup
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
            this.label_combo = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveShortcutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goBackToDefaultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitWithoutSavingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.typeYourShortcutCombinationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_combo
            // 
            this.label_combo.Font = new System.Drawing.Font("Arial Narrow", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_combo.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label_combo.Location = new System.Drawing.Point(-3, 24);
            this.label_combo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_combo.Name = "label_combo";
            this.label_combo.Size = new System.Drawing.Size(284, 17);
            this.label_combo.TabIndex = 0;
            this.label_combo.Text = "Combination:";
            this.label_combo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.typeYourShortcutCombinationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(278, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.fileToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveShortcutToolStripMenuItem,
            this.goBackToDefaultsToolStripMenuItem,
            this.exitWithoutSavingToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Arial Narrow", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(49, 22);
            this.fileToolStripMenuItem.Text = "Exit...";
            // 
            // saveShortcutToolStripMenuItem
            // 
            this.saveShortcutToolStripMenuItem.Name = "saveShortcutToolStripMenuItem";
            this.saveShortcutToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.saveShortcutToolStripMenuItem.Text = "Save Shortcut";
            this.saveShortcutToolStripMenuItem.Click += new System.EventHandler(this.saveShortcutToolStripMenuItem_Click);
            // 
            // goBackToDefaultsToolStripMenuItem
            // 
            this.goBackToDefaultsToolStripMenuItem.Name = "goBackToDefaultsToolStripMenuItem";
            this.goBackToDefaultsToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.goBackToDefaultsToolStripMenuItem.Text = "Go Back To Defaults";
            this.goBackToDefaultsToolStripMenuItem.Click += new System.EventHandler(this.goBackToDefaultsToolStripMenuItem_Click);
            // 
            // exitWithoutSavingToolStripMenuItem
            // 
            this.exitWithoutSavingToolStripMenuItem.Name = "exitWithoutSavingToolStripMenuItem";
            this.exitWithoutSavingToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.exitWithoutSavingToolStripMenuItem.Text = "Exit Without Saving";
            this.exitWithoutSavingToolStripMenuItem.Click += new System.EventHandler(this.exitWithoutSavingToolStripMenuItem_Click);
            // 
            // typeYourShortcutCombinationToolStripMenuItem
            // 
            this.typeYourShortcutCombinationToolStripMenuItem.Font = new System.Drawing.Font("Arial Narrow", 10.125F, System.Drawing.FontStyle.Bold);
            this.typeYourShortcutCombinationToolStripMenuItem.Name = "typeYourShortcutCombinationToolStripMenuItem";
            this.typeYourShortcutCombinationToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.typeYourShortcutCombinationToolStripMenuItem.Text = "Type your shortcut, one key at a time.";
            // 
            // cKBSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(278, 45);
            this.Controls.Add(this.label_combo);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "cKBSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Fade Keyboard Shortcut Setup";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.cKBSetup_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cKBSetup_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_combo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveShortcutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goBackToDefaultsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitWithoutSavingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem typeYourShortcutCombinationToolStripMenuItem;

    }
}