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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cPic));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cPicLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cPicLayoutComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.slideshowManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgv_sm = new System.Windows.Forms.DataGridView();
            this.Images = new System.Windows.Forms.DataGridViewImageColumn();
            this.DeleteButtons = new System.Windows.Forms.DataGridViewButtonColumn();
            this.FileLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_sm)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Black;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.xToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            this.menuStrip1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.menuStrip1_MouseDown);
            this.menuStrip1.MouseLeave += new System.EventHandler(this.menuStrip1_MouseLeave);
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cPicLayoutToolStripMenuItem,
            this.slideshowManagerToolStripMenuItem});
            this.FileToolStripMenuItem.Font = new System.Drawing.Font("Wingdings 3", 8.25F);
            this.FileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
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
            this.cPicLayoutToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
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
            // slideshowManagerToolStripMenuItem
            // 
            this.slideshowManagerToolStripMenuItem.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slideshowManagerToolStripMenuItem.Name = "slideshowManagerToolStripMenuItem";
            this.slideshowManagerToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.slideshowManagerToolStripMenuItem.Text = "Show Slideshow Manager";
            this.slideshowManagerToolStripMenuItem.Click += new System.EventHandler(this.slideshowManagerToolStripMenuItem_Click);
            // 
            // xToolStripMenuItem
            // 
            this.xToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.xToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.xToolStripMenuItem.Name = "xToolStripMenuItem";
            this.xToolStripMenuItem.Size = new System.Drawing.Size(27, 20);
            this.xToolStripMenuItem.Text = "X";
            this.xToolStripMenuItem.Click += new System.EventHandler(this.xToolStripMenuItem_Click);
            // 
            // dgv_sm
            // 
            this.dgv_sm.AllowUserToAddRows = false;
            this.dgv_sm.AllowUserToDeleteRows = false;
            this.dgv_sm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_sm.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_sm.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_sm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_sm.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Images,
            this.DeleteButtons,
            this.FileLocation});
            this.dgv_sm.Location = new System.Drawing.Point(3, 24);
            this.dgv_sm.Name = "dgv_sm";
            this.dgv_sm.RowHeadersVisible = false;
            this.dgv_sm.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_sm.ShowCellErrors = false;
            this.dgv_sm.ShowRowErrors = false;
            this.dgv_sm.Size = new System.Drawing.Size(278, 236);
            this.dgv_sm.TabIndex = 1;
            this.dgv_sm.Visible = false;
            this.dgv_sm.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_sm_CellContentClick);
            // 
            // Images
            // 
            this.Images.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = null;
            this.Images.DefaultCellStyle = dataGridViewCellStyle1;
            this.Images.FillWeight = 194.9239F;
            this.Images.HeaderText = "Images";
            this.Images.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Images.Name = "Images";
            // 
            // DeleteButtons
            // 
            this.DeleteButtons.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.DeleteButtons.FillWeight = 5.076141F;
            this.DeleteButtons.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteButtons.HeaderText = "";
            this.DeleteButtons.Name = "DeleteButtons";
            this.DeleteButtons.Width = 5;
            // 
            // FileLocation
            // 
            this.FileLocation.HeaderText = "FileLocation";
            this.FileLocation.Name = "FileLocation";
            this.FileLocation.Visible = false;
            // 
            // cPic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.dgv_sm);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "cPic";
            this.Text = "cPic";
            this.TransparencyKey = System.Drawing.Color.Black;
            this.Load += new System.EventHandler(this.cPic_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cPic_MouseDown);
            this.MouseEnter += new System.EventHandler(this.cPic_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.cPic_MouseLeave);
            this.Resize += new System.EventHandler(this.cPic_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_sm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cPicLayoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox cPicLayoutComboBox;
        private System.Windows.Forms.ToolStripMenuItem slideshowManagerToolStripMenuItem;
        private System.Windows.Forms.DataGridViewImageColumn Images;
        private System.Windows.Forms.DataGridViewButtonColumn DeleteButtons;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileLocation;
        public System.Windows.Forms.DataGridView dgv_sm;
        private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem;
        public System.Windows.Forms.MenuStrip menuStrip1;
    }
}