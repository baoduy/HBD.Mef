using HBD.Framework.Core;

namespace HBD.Mef.Core.Navigation
{
    public abstract class MenuInfoBase : Iconable, IMenuInfo, ITitleObject, IToolTipable
    {
        private MenuAlignment _alignment = MenuAlignment.Left;
        private IMenuInfoCollection _parent;
        private string _title;
        private string _toolTip;

        protected MenuInfoBase(IMenuInfoCollection parent)
        {
            _parent = parent;
        }

        public IMenuInfoCollection Parent
        {
            get { return _parent; }
            protected set { SetValue(ref _parent, value); }
        }

        public MenuAlignment Alignment
        {
            get { return _alignment; }
            set { SetValue(ref _alignment, value); }
        }

        public string Title
        {
            get { return _title; }
            set { SetValue(ref _title, value); }
        }

        public string ToolTip
        {
            get { return _toolTip; }
            set { SetValue(ref _toolTip, value); }
        }
    }
}