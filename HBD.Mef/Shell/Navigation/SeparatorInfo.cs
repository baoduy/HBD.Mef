#region using

using System;
using HBD.Framework.Core;
using HBD.Mef.Shell.Core;

#endregion

namespace HBD.Mef.Shell.Navigation
{
    public sealed class SeparatorInfo : NotifyPropertyChange, IMenuInfo
    {
        private int _displayIndex;

        public SeparatorInfo(IMenuInfoCollection parent)
        {
            Parent = parent;
        }

        public MenuAlignment Alignment { get; set; } = MenuAlignment.Left;

        public int DisplayIndex
        {
            get { return _displayIndex; }
            set { SetValue(ref _displayIndex, value); }
        }

        public IPermissionValidationInfo PermissionValidation
        {
            get { return null; }
            set { throw new NotSupportedException("PermissionValidation for SeparatorInfo"); }
        }

        public IMenuInfoCollection Parent { get; }
    }
}