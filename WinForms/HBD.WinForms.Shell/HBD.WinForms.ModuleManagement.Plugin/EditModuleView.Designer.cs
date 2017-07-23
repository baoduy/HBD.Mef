namespace HBD.WinForms.ModuleManagement.Plugin
{
    partial class EditModuleView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditModuleView));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_Close = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.data_Version = new System.Windows.Forms.ToolStripLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.data_Description = new System.Windows.Forms.TextBox();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.data_Name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.data_IsEnabled = new System.Windows.Forms.CheckBox();
            this.data_InvalidMessage = new System.Windows.Forms.Label();
            this.data_AllowToManage = new System.Windows.Forms.Label();
            this.validationManager1 = new HBD.WinForms.Validation.ValidationManager(this.components);
            this.data_Name_RequiredValidatior1 = new HBD.WinForms.Validation.RequiredValidatior(this.components);
            this.btn_ViewFolder = new System.Windows.Forms.LinkLabel();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.validationManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_Close,
            this.toolStripSeparator1,
            this.btn_Save,
            this.toolStripLabel1,
            this.data_Version});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(917, 39);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_Close
            // 
            this.btn_Close.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btn_Close.Image = global::HBD.WinForms.ModuleManagement.Plugin.Resource.Close;
            this.btn_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(99, 36);
            this.btn_Close.Text = "Close";
            this.btn_Close.ToolTipText = "Close without changes";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // btn_Save
            // 
            this.btn_Save.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btn_Save.Image = global::HBD.WinForms.ModuleManagement.Plugin.Resource.Save;
            this.btn_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(92, 36);
            this.btn_Save.Text = "Save";
            this.btn_Save.ToolTipText = "Save and Close";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(167, 36);
            this.toolStripLabel1.Text = "Module version: ";
            // 
            // data_Version
            // 
            this.data_Version.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.data_Version.Name = "data_Version";
            this.data_Version.Size = new System.Drawing.Size(0, 36);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.Controls.Add(this.data_Description, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.data_Name, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.data_IsEnabled, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.data_InvalidMessage, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.data_AllowToManage, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 39);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(917, 392);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // data_Description
            // 
            this.data_Description.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Description", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.data_Description.Dock = System.Windows.Forms.DockStyle.Fill;
            this.data_Description.Location = new System.Drawing.Point(163, 73);
            this.data_Description.Margin = new System.Windows.Forms.Padding(6, 12, 6, 6);
            this.data_Description.Name = "data_Description";
            this.data_Description.Size = new System.Drawing.Size(711, 29);
            this.data_Description.TabIndex = 3;
            this.validationManager1.SetValidators(this.data_Description, new HBD.WinForms.Base.IValidation[0]);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(6, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(18);
            this.label1.Size = new System.Drawing.Size(100, 61);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // data_Name
            // 
            this.data_Name.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Name", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.data_Name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.data_Name.Location = new System.Drawing.Point(163, 12);
            this.data_Name.Margin = new System.Windows.Forms.Padding(6, 12, 6, 6);
            this.data_Name.Name = "data_Name";
            this.data_Name.Size = new System.Drawing.Size(711, 29);
            this.data_Name.TabIndex = 1;
            this.validationManager1.SetValidators(this.data_Name, new HBD.WinForms.Base.IValidation[] {
            ((HBD.WinForms.Base.IValidation)(this.data_Name_RequiredValidatior1))});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(6, 61);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(18);
            this.label2.Size = new System.Drawing.Size(145, 61);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description";
            // 
            // data_IsEnabled
            // 
            this.data_IsEnabled.AutoSize = true;
            this.data_IsEnabled.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.bindingSource, "IsEnabled", true));
            this.data_IsEnabled.Location = new System.Drawing.Point(163, 140);
            this.data_IsEnabled.Margin = new System.Windows.Forms.Padding(6, 18, 6, 18);
            this.data_IsEnabled.Name = "data_IsEnabled";
            this.data_IsEnabled.Size = new System.Drawing.Size(110, 29);
            this.data_IsEnabled.TabIndex = 5;
            this.data_IsEnabled.Text = "Enabled";
            this.data_IsEnabled.UseVisualStyleBackColor = true;
            // 
            // data_InvalidMessage
            // 
            this.data_InvalidMessage.AutoEllipsis = true;
            this.data_InvalidMessage.AutoSize = true;
            this.data_InvalidMessage.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "InValidMessage", true));
            this.data_InvalidMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.data_InvalidMessage.Location = new System.Drawing.Point(163, 247);
            this.data_InvalidMessage.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.data_InvalidMessage.Name = "data_InvalidMessage";
            this.data_InvalidMessage.Padding = new System.Windows.Forms.Padding(0, 18, 18, 18);
            this.data_InvalidMessage.Size = new System.Drawing.Size(711, 61);
            this.data_InvalidMessage.TabIndex = 8;
            this.data_InvalidMessage.Text = "Invalid Message";
            // 
            // data_AllowToManage
            // 
            this.data_AllowToManage.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.data_AllowToManage, 2);
            this.data_AllowToManage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.data_AllowToManage.Enabled = false;
            this.data_AllowToManage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.data_AllowToManage.Location = new System.Drawing.Point(163, 205);
            this.data_AllowToManage.Margin = new System.Windows.Forms.Padding(6, 18, 6, 18);
            this.data_AllowToManage.Name = "data_AllowToManage";
            this.data_AllowToManage.Size = new System.Drawing.Size(748, 24);
            this.data_AllowToManage.TabIndex = 10;
            this.data_AllowToManage.Text = "This module is not allow to changes.";
            // 
            // validationManager1
            // 
            this.validationManager1.ContainerControl = this;
            this.validationManager1.Icon = ((System.Drawing.Icon)(resources.GetObject("validationManager1.Icon")));
            // 
            // data_Name_RequiredValidatior1
            // 
            this.data_Name_RequiredValidatior1.ErrorMessage = "Name is required.";
            this.data_Name_RequiredValidatior1.ValidationControl = this.data_Name;
            this.data_Name_RequiredValidatior1.ValidationProperty = "Text";
            // 
            // btn_ViewFolder
            // 
            this.btn_ViewFolder.AutoSize = true;
            this.btn_ViewFolder.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_ViewFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ViewFolder.Location = new System.Drawing.Point(735, 431);
            this.btn_ViewFolder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btn_ViewFolder.Name = "btn_ViewFolder";
            this.btn_ViewFolder.Padding = new System.Windows.Forms.Padding(0, 10, 9, 10);
            this.btn_ViewFolder.Size = new System.Drawing.Size(182, 44);
            this.btn_ViewFolder.TabIndex = 10;
            this.btn_ViewFolder.TabStop = true;
            this.btn_ViewFolder.Text = "View module folder";
            this.btn_ViewFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_ViewFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btn_VIewFolder_LinkClicked);
            // 
            // EditModuleView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_ViewFolder);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "EditModuleView";
            this.Size = new System.Drawing.Size(917, 924);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.validationManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox data_Name;
        private System.Windows.Forms.TextBox data_Description;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox data_IsEnabled;
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.Label data_InvalidMessage;
        private Validation.ValidationManager validationManager1;
        private Validation.RequiredValidatior data_Name_RequiredValidatior1;
        private System.Windows.Forms.ToolStripButton btn_Save;
        private System.Windows.Forms.ToolStripButton btn_Close;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label data_AllowToManage;
        private System.Windows.Forms.LinkLabel btn_ViewFolder;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel data_Version;
    }
}
