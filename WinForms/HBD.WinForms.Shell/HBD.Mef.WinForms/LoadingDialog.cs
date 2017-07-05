#region using

using System;
using System.ComponentModel;
using System.Windows.Forms;

#endregion

namespace HBD.Mef.WinForms
{
    public partial class LoadingDialog : Form
    {
        public LoadingDialog(string message = null)
        {
            InitializeComponent();
            LoadingMessage = message;
        }

        public bool IsShown { get; private set; }

        [DefaultValue("Loading...")]
        public string LoadingMessage
        {
            get { return loadingControl1.Message; }
            set { loadingControl1.Message = value; }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            IsShown = true;
            CenterToParent();
        }
    }
}