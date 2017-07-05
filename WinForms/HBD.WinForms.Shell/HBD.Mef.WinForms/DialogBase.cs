#region using

using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using HBD.Mef.Shell.Services;
using HBD.Mef.WinForms.Services;

#endregion

namespace HBD.Mef.WinForms
{
    [ToolboxItem(false)]
    public class DialogBase : FormBase
    {
        protected DialogBase()
        {
        }

        [Import]
        public IShellStatusService StatusService { protected get; set; }

        [Import]
        public ILoadingDialogService LoadingDialodService { protected get; set; }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.KeyCode == Keys.Escape) Close();
        }
    }
}