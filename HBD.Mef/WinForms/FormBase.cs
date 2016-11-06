using System.ComponentModel.Composition;
using System.Windows.Forms;
using Microsoft.Practices.ServiceLocation;
using Prism.Logging;

namespace HBD.Mef.WinForms
{
    public partial class FormBase : Form
    {
        public FormBase()
        {
            InitializeComponent();
            AutoValidate = AutoValidate.EnableAllowFocusChange;
        }

        [Import]
        public IServiceLocator ContainerService { protected get; set; }

        [Import]
        public ILoggerFacade Logger { protected get; set; }

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