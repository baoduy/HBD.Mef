using System.ComponentModel;
using System.Windows.Forms;

namespace HBD.Mef.WinForms
{
    public partial class LoadingControl : UserControl
    {
        public LoadingControl()
        {
            InitializeComponent();
        }

        [DefaultValue("Loading...")]
        public string Message
        {
            get { return data_Message.Text; }
            set { data_Message.Text = value; }
        }

        public void BringToCenter()
        {
            if (Parent == null) return;
            Left = (Parent.ClientSize.Width - Width)/2;
            Top = (Parent.ClientSize.Height - Height)/2;
        }
    }
}