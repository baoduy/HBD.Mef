#region using

using HBD.Framework.Core;
using System.Diagnostics;

#endregion

namespace HBD.Mef.Shell.Navigation
{
    [DebuggerDisplay("Name = {Name}, DisplayIndex = {DisplayIndex}")]
    public class MenuInfo : MenuInfoBase, IChildrentable<MenuInfoCollection>
    {
        private MenuInfoCollection _children;

        public MenuInfo(IMenuInfoCollection parent) : base(parent)
        {
            Children = new MenuInfoCollection();
        }

        public MenuInfoCollection Children
        {
            get { return _children; }
            protected set { SetValue(ref _children, value); }
        }
    }
}