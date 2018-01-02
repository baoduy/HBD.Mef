#region using

using System.ComponentModel;
using HBD.Framework.Core;
using HBD.Mef.Shell.Core;
using System.Diagnostics;

#endregion

namespace HBD.Mef.Shell.Navigation
{
    [DebuggerDisplay("Name = {Name}, DisplayIndex = {DisplayIndex}")]
    public abstract class MenuInfoBase : Iconable, IMenuInfo, ITitleObject, IToolTipable, INamedObject
    {
        private MenuAlignment _alignment = MenuAlignment.Left;
        private int _displayIndex;
        private DisplayMode _displayMode = DisplayMode.IconAndText;
        private string _name;
        private IMenuInfoCollection _parent;
        private IPermissionValidationInfo _permissionValidation;
        private string _title;
        private string _toolTip;

        protected MenuInfoBase(IMenuInfoCollection parent)
        {
            _parent = parent;
        }

        [DefaultValue(DisplayMode.IconAndText)]
        public DisplayMode DisplayMode
        {
            get { return _displayMode; }
            set { SetValue(ref _displayMode, value); }
        }

        public IMenuInfoCollection Parent
        {
            get { return _parent; }
            protected set { SetValue(ref _parent, value); }
        }

        [DefaultValue(MenuAlignment.Left)]
        public MenuAlignment Alignment
        {
            get { return _alignment; }
            set { SetValue(ref _alignment, value); }
        }

        public int DisplayIndex
        {
            get { return _displayIndex; }
            set { SetValue(ref _displayIndex, value); }
        }

        public IPermissionValidationInfo PermissionValidation
        {
            get { return _permissionValidation; }
            set { SetValue(ref _permissionValidation, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetValue(ref _name, value); }
        }

        [DefaultValue("")]
        public string Title
        {
            get { return _title; }
            set { SetValue(ref _title, value); }
        }

        [DefaultValue("")]
        public string ToolTip
        {
            get { return _toolTip; }
            set { SetValue(ref _toolTip, value); }
        }
    }
}