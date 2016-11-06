using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using HBD.Mef.WinForms.Services;

namespace HBD.Mef.WinForms
{
    [ToolboxItem(false)]
    public class DialogBase : FormBase
    {
        protected DialogBase()
        {
        }

        [Import]
        public IStatusBarService StatusService { protected get; set; }

        [Import]
        public ILoadingDialodService LoadingDialodService { protected get; set; }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.KeyCode == Keys.Escape) Close();
        }
    }
}