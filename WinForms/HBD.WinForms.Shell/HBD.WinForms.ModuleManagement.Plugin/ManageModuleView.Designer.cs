namespace HBD.WinForms.ModuleManagement.Plugin
{
    partial class ManageModuleView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_Add = new System.Windows.Forms.ToolStripButton();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.col_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Des = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_IsEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctm_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_Add});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1165, 37);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_Add
            // 
            this.btn_Add.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btn_Add.Image = global::HBD.WinForms.ModuleManagement.Plugin.Resource.Add;
            this.btn_Add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(160, 34);
            this.btn_Add.Text = "Add Module";
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_Name,
            this.col_Des,
            this.col_Version,
            this.col_IsEnabled});
            this.dataGridView.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 37);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(1165, 886);
            this.dataGridView.TabIndex = 1;
            this.dataGridView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView_MouseDoubleClick);
            // 
            // col_Name
            // 
            this.col_Name.DataPropertyName = "Name";
            this.col_Name.HeaderText = "Name";
            this.col_Name.Name = "col_Name";
            this.col_Name.ReadOnly = true;
            // 
            // col_Des
            // 
            this.col_Des.DataPropertyName = "Description";
            this.col_Des.HeaderText = "Description";
            this.col_Des.Name = "col_Des";
            this.col_Des.ReadOnly = true;
            // 
            // col_Version
            // 
            this.col_Version.DataPropertyName = "Version";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Version.DefaultCellStyle = dataGridViewCellStyle2;
            this.col_Version.HeaderText = "Version";
            this.col_Version.Name = "col_Version";
            this.col_Version.ReadOnly = true;
            // 
            // col_IsEnabled
            // 
            this.col_IsEnabled.DataPropertyName = "IsEnabled";
            this.col_IsEnabled.HeaderText = "Enabled";
            this.col_IsEnabled.Name = "col_IsEnabled";
            this.col_IsEnabled.ReadOnly = true;
            this.col_IsEnabled.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_IsEnabled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctm_Edit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(122, 38);
            // 
            // ctm_Edit
            // 
            this.ctm_Edit.Name = "ctm_Edit";
            this.ctm_Edit.Size = new System.Drawing.Size(121, 34);
            this.ctm_Edit.Text = "&Edit";
            this.ctm_Edit.Click += new System.EventHandler(this.ctm_Edit_Click);
            // 
            // ManageModuleView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "ManageModuleView";
            this.Size = new System.Drawing.Size(1165, 923);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ToolStripButton btn_Add;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ctm_Edit;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Des;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Version;
        private System.Windows.Forms.DataGridViewCheckBoxColumn col_IsEnabled;
    }
}
