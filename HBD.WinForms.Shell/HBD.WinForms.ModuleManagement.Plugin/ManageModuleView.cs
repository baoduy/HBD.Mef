using System;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HBD.Mef.WinForms;
using HBD.Mef.WinForms.Services;
using HBD.Mef.Shell.Configuration;

namespace HBD.WinForms.ModuleManagement.Plugin
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ManageModuleView : ViewBase
    {
        [Import]
        private IShellConfigManager ShellConfigManager { get; set; }

        public ManageModuleView()
        {
            InitializeComponent();
            toolStrip1.ImageScalingSize = new Size(16, 16);
        }

        public override string Text => Resource.ManageModuleView_Title;

        private void BindingData()
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.DataSource = ShellConfigManager.Modules.ToList();
        }

        protected override void OnIsActiveChanged(EventArgs e)
        {
            base.OnIsActiveChanged(e);
            StatusService.SetStatus(IsActive ? Resource.Status_ViewReady : Resource.Status_ViewClosed);

            BindingData();
        }

        private void dataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
            => EditModule();

        private void ctm_Edit_Click(object sender, EventArgs e)
            => EditModule();

        private void EditModule()
        {
            if (dataGridView.CurrentCell == null) return;

            var tab = ContainerService.GetInstance<IMainViewService>();
            var editView = ContainerService.GetInstance<EditModuleView>();
            editView.Module = dataGridView.CurrentCell.OwningRow.DataBoundItem as ModuleConfig;
            tab.ActiveView(editView);
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            using (var fr = ContainerService.GetInstance<AddModuleDialog>())
            {
                fr.ShowDialog(this);
            }
        }
    }
}