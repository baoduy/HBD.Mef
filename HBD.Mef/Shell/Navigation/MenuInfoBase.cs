#region

using HBD.Framework.Core;
using HBD.Mef.Shell.Core;
using System.ComponentModel;

#endregion

namespace HBD.Mef.Shell.Navigation
{
    public abstract class MenuInfoBase : Iconable, IMenuInfo, ITitleObject, IToolTipable, INamedObject
    {
        private MenuAlignment _alignment = MenuAlignment.Left;
        private DisplayMode _displayMode = DisplayMode.IconAndText;
        private IMenuInfoCollection _parent;
        private string _title;
        private string _toolTip;
        private string _name;
        private int _displayIndex;
        private IPermissionValidationInfo _permissionValidation;

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

        public string Name
        {
            get { return _name; }
            set { this.SetValue(ref _name, value); }
        }

        public int DisplayIndex
        {
            get { return _displayIndex; }
            set { this.SetValue(ref _displayIndex, value); }
        }

        public IPermissionValidationInfo PermissionValidation
        {
            get { return _permissionValidation; }
            set { this.SetValue(ref _permissionValidation, value); }
        }
    }
}