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
            this.label_instr = new System.Windows.Forms.Label();
            this.label_combo = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveShortcutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goBackToDefaultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitWithoutSavingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_instr
            // 
            this.label_instr.BackColor = System.Drawing.Color.Transparent;
            this.label_instr.Font = new System.Drawing.Font("Arial Narrow", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_instr.Location = new System.Drawing.Point(135, -3);
            this.label_instr.Name = "label_instr";
            this.label_instr.Size = new System.Drawing.Size(417, 40);
            this.label_instr.TabIndex = 0;
            this.label_instr.Text = "Type your shortcut combination.";
            this.label_instr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_combo
            // 
            this.label_combo.Font = new System.Drawing.Font("Arial Narrow", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_combo.Location = new System.Drawing.Point(4, 191);
            this.label_combo.Name = "label_combo";
            this.label_combo.Size = new System.Drawing.Size(568, 33);
            this.label_combo.TabIndex = 1;
            this.label_combo.Text = "Combination:";
            this.label_combo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(555, 39);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveShortcutToolStripMenuItem,
            this.goBackToDefaultsToolStripMenuItem,
            this.exitWithoutSavingToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Arial Narrow", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(84, 35);
            this.fileToolStripMenuItem.Text = "Exit...";
            // 
            // saveShortcutToolStripMenuItem
            // 
            this.saveShortcutToolStripMenuItem.Name = "saveShortcutToolStripMenuItem";
            this.saveShortcutToolStripMenuItem.Size = new System.Drawing.Size(303, 36);
            this.saveShortcutToolStripMenuItem.Text = "Save Shortcut";
            this.saveShortcutToolStripMenuItem.Click += new System.EventHandler(this.saveShortcutToolStripMenuItem_Click);
            // 
            // goBackToDefaultsToolStripMenuItem
            // 
            this.goBackToDefaultsToolStripMenuItem.Name = "goBackToDefaultsToolStripMenuItem";
            this.goBackToDefaultsToolStripMenuItem.Size = new System.Drawing.Size(303, 36);
            this.goBackToDefaultsToolStripMenuItem.Text = "Go Back To Defaults";
            this.goBackToDefaultsToolStripMenuItem.Click += new System.EventHandler(this.goBackToDefaultsToolStripMenuItem_Click);
            // 
            // exitWithoutSavingToolStripMenuItem
            // 
            this.exitWithoutSavingToolStripMenuItem.Name = "exitWithoutSavingToolStripMenuItem";
            this.exitWithoutSavingToolStripMenuItem.Size = new System.Drawing.Size(303, 36);
            this.exitWithoutSavingToolStripMenuItem.Text = "Exit Without Saving";
            this.exitWithoutSavingToolStripMenuItem.Click += new System.EventHandler(this.exitWithoutSavingToolStripMenuItem_Click);
            // 
            // cKBSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 233);
            this.Controls.Add(this.label_combo);
            this.Controls.Add(this.label_instr);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
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

        private System.Windows.Forms.Label label_instr;
        private System.Windows.Forms.Label label_combo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveShortcutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goBackToDefaultsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitWithoutSavingToolStripMenuItem;

    }
}