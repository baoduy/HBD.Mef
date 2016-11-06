﻿using HBD.Framework.Core;

namespace HBD.Mef.Core.Navigation
{
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