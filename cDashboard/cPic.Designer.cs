namespace cDashboard
{
    partial class cPic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cPic));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cPicLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cPicLayoutComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cPicLayoutToolStripMenuItem});
            this.FileToolStripMenuItem.Font = new System.Drawing.Font("Wingdings 3", 8.25F);
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(28, 20);
            this.FileToolStripMenuItem.Text = "p";
            // 
            // cPicLayoutToolStripMenuItem
            // 
            this.cPicLayoutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cPicLayoutComboBox});
            this.cPicLayoutToolStripMenuItem.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cPicLayoutToolStripMenuItem.Name = "cPicLayoutToolStripMenuItem";
            this.cPicLayoutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cPicLayoutToolStripMenuItem.Text = "cPic Layout";
            this.cPicLayoutToolStripMenuItem.DropDownOpened += new System.EventHandler(this.cPicLayoutToolStripMenuItem_DropDownOpened);
            // 
            // cPicLayoutComboBox
            // 
            this.cPicLayoutComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cPicLayoutComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cPicLayoutComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cPicLayoutComboBox.Items.AddRange(new object[] {
            "Center",
            "None",
            "Stretch",
            "Tile",
            "Zoom"});
            this.cPicLayoutComboBox.MergeIndex = 1;
            this.cPicLayoutComboBox.Name = "cPicLayoutComboBox";
            this.cPicLayoutComboBox.Size = new System.Drawing.Size(121, 23);
            this.cPicLayoutComboBox.SelectedIndexChanged += new System.EventHandler(this.cPicLayoutComboBox_SelectedIndexChanged);
            // 
            // cPic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "cPic";
            this.Text = "cPic";
            this.Load += new System.EventHandler(this.cPic_Load);
            this.ResizeEnd += new System.EventHandler(this.cPic_ResizeEnd);
            this.Move += new System.EventHandler(this.cPic_Move);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cPicLayoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox cPicLayoutComboBox;
    }
}