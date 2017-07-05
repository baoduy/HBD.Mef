using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using HBD.Mef.WinForms;
using HBD.Mef.WinForms.Services;
using HBD.Mef.Shell.Configuration;

namespace HBD.WinForms.ModuleManagement.Plugin
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class EditModuleView : ViewBase
    {
        public EditModuleView()
        {
            InitializeComponent();
            toolStrip1.ImageScalingSize = new Size(16, 16);
            Text = string.Format(Resource.EditModuleView_Title, string.Empty);
        }

        [Import]
        private IShellConfigManager ShellConfigManager { get; set; }

        public ModuleConfig Module { get; set; }

        protected override void OnIsActiveChanged(EventArgs e)
        {
            base.OnIsActiveChanged(e);
            DataBind();
        }

        private void DataBind()
        {
            if (Module == null)
            {
                this.ShowErrorMessage("Module can not be NULL.");
                Enabled = false;
                return;
            }

            Text = string.Format(Resource.EditModuleView_Title, Module.Name);
            data_Version.Text = Module.Version;
            bindingSource.DataSource = Module;

            tableLayoutPanel1.Enabled =  Module.AllowToManage;
            btn_Save.Enabled = Module.AllowToManage;
            data_AllowToManage.Visible = !Module.AllowToManage;

            StatusService.SetStatus("Module is ready for edit.");
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (!Validate()) return;

            ShellConfigManager.SaveChanges(Logger);
            this.ShowInfoMessage("The changes had been saved.");
            StatusService.SetStatus($"Saved the changes of Module {Module.Name}");
            Close();
        }

        private void btn_VIewFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            => Process.Start(Module.Directory);

        private void btn_Close_Click(object sender, EventArgs e)
        {
            ShellConfigManager.UndoChanges();
            Close();
        }

        private void Close()
        {
            var tb = ContainerService.GetInstance<IMainViewService>();
            tb.DeactivateView(this);
            StatusService.SetStatus("Edit view had been closed.");
        }
    }
}