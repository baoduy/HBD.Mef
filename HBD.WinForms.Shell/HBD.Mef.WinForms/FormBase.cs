#region using

using System.ComponentModel.Composition;
using System.Windows.Forms;
using HBD.Mef.WinForms.Services;
using Microsoft.Practices.ServiceLocation;
using HBD.Mef.Logging;

#endregion

namespace HBD.Mef.WinForms
{
    public class FormBase : Form
    {
        public FormBase()
        {
            AutoValidate = AutoValidate.EnableAllowFocusChange;
        }

        [Import]
        public IServiceLocator ContainerService { protected get; set; }

        [Import]
        public ILogger Logger { protected get; set; }

        [Import]
        public IMessageBoxService MessageBoxService { protected get; set; }

        /// <summary>
        ///     This code allow to close the from when children control are validating.
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x10) // The upper right "X" was clicked
            {
                ActiveControl = null;
                AutoValidate = AutoValidate.Disable;
            }
            base.WndProc(ref m);
        }
    }
}