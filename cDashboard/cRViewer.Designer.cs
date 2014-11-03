namespace cDashboard
{
    partial class cRViewer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menustrip = new System.Windows.Forms.MenuStrip();
            this.xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.column_date_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_remove_creminder = new System.Windows.Forms.DataGridViewButtonColumn();
            this.column_ticks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label_title = new System.Windows.Forms.Label();
            this.plusStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menustrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // menustrip
            // 
            this.menustrip.BackColor = System.Drawing.Color.Transparent;
            this.menustrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xToolStripMenuItem,
            this.plusStripMenuItem1});
            this.menustrip.Location = new System.Drawing.Point(0, 0);
            this.menustrip.Name = "menustrip";
            this.menustrip.Size = new System.Drawing.Size(284, 26);
            this.menustrip.TabIndex = 0;
            this.menustrip.Text = "menuStrip1";
            this.menustrip.MouseDown += new System.Windows.Forms.MouseEventHandler(this.menustrip_MouseDown);
            // 
            // xToolStripMenuItem
            // 
            this.xToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xToolStripMenuItem.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.xToolStripMenuItem.Name = "xToolStripMenuItem";
            this.xToolStripMenuItem.Size = new System.Drawing.Size(29, 22);
            this.xToolStripMenuItem.Text = "X";
            this.xToolStripMenuItem.Click += new System.EventHandler(this.xToolStripMenuItem_Click);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgv.BackgroundColor = System.Drawing.Color.Black;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.column_date_time,
            this.column_message,
            this.column_remove_creminder,
            this.column_ticks});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.Location = new System.Drawing.Point(1, 27);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.ShowCellErrors = false;
            this.dgv.ShowCellToolTips = false;
            this.dgv.ShowEditingIcon = false;
            this.dgv.Size = new System.Drawing.Size(282, 340);
            this.dgv.TabIndex = 1;
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            // 
            // column_date_time
            // 
            this.column_date_time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.column_date_time.HeaderText = "Date and Time";
            this.column_date_time.MinimumWidth = 100;
            this.column_date_time.Name = "column_date_time";
            this.column_date_time.ReadOnly = true;
            this.column_date_time.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // column_message
            // 
            this.column_message.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.column_message.HeaderText = "Message";
            this.column_message.MinimumWidth = 90;
            this.column_message.Name = "column_message";
            this.column_message.ReadOnly = true;
            this.column_message.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // column_remove_creminder
            // 
            this.column_remove_creminder.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.column_remove_creminder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.column_remove_creminder.HeaderText = "Remove?";
            this.column_remove_creminder.Name = "column_remove_creminder";
            this.column_remove_creminder.ReadOnly = true;
            this.column_remove_creminder.Width = 66;
            // 
            // column_ticks
            // 
            this.column_ticks.HeaderText = "Ticks";
            this.column_ticks.Name = "column_ticks";
            this.column_ticks.ReadOnly = true;
            this.column_ticks.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.column_ticks.Visible = false;
            this.column_ticks.Width = 44;
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_title.Location = new System.Drawing.Point(1, 6);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(83, 20);
            this.label_title.TabIndex = 2;
            this.label_title.Text = "cReminders";
            this.label_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_title_MouseDown);
            // 
            // plusStripMenuItem1
            // 
            this.plusStripMenuItem1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.plusStripMenuItem1.BackColor = System.Drawing.Color.Black;
            this.plusStripMenuItem1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plusStripMenuItem1.ForeColor = System.Drawing.Color.White;
            this.plusStripMenuItem1.Name = "plusStripMenuItem1";
            this.plusStripMenuItem1.Size = new System.Drawing.Size(29, 22);
            this.plusStripMenuItem1.Text = "+";
            this.plusStripMenuItem1.Click += new System.EventHandler(this.plusStripMenuItem1_Click);
            // 
            // cRViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(284, 368);
            this.Controls.Add(this.label_title);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.menustrip);
            this.ForeColor = System.Drawing.Color.White;
            this.MainMenuStrip = this.menustrip;
            this.MinimumSize = new System.Drawing.Size(100, 75);
            this.Name = "cRViewer";
            this.Text = "cRViewer";
            this.Load += new System.EventHandler(this.cRViewer_Load);
            this.menustrip.ResumeLayout(false);
            this.menustrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menustrip;
        private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_date_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_message;
        private System.Windows.Forms.DataGridViewButtonColumn column_remove_creminder;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_ticks;
        private System.Windows.Forms.ToolStripMenuItem plusStripMenuItem1;
    }
}