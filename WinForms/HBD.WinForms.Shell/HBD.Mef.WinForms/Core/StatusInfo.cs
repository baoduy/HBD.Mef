#region using

using System.Drawing;
using HBD.Framework.Core;
using HBD.Mef.Shell.Core;

#endregion

namespace HBD.Mef.WinForms.Core
{
    public class StatusInfo : Iconable, IStatusInfo
    {
        private Color _backgroundColor;
        private string _message;
        private Color _messageColor;

        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { SetValue(ref _backgroundColor, value); }
        }

        public Color MessageColor
        {
            get { return _messageColor; }
            set { SetValue(ref _messageColor, value); }
        }

        public string Message
        {
            get { return _message; }
            set { SetValue(ref _message, value); }
        }
    }
}