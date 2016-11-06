using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using HBD.Mef.Core.Navigation;

namespace HBD.Mef.WinForms
{
    public static class MenuExtensions
    {
        public static void BindingToMenu(this IEnumerable<IMenuInfo> @this, ToolStripItemCollection menuCollection,
            Action<ToolStripItem> menuClickAction = null)
        {
            if ((@this == null) || (menuCollection == null)) return;

            foreach (var item in @this)
            {
                var m = menuCollection.Add(item, menuClickAction);

                var n = item as MenuInfo;
                if (n == null) continue;
                var tm = m as ToolStripMenuItem;
                if (tm == null) continue;

                n.Children.BindingToMenu(tm.DropDownItems, menuClickAction);
            }
        }

        private static ToolStripItem Add(this ToolStripItemCollection @this, IMenuInfo item,
            Action<ToolStripItem> menuClickAction = null)
        {
            if ((@this == null) || (item == null)) return null;

            ToolStripItem m;
            if (item is SeparatorInfo)
                m = new ToolStripSeparator();
            else
            {
                var n = item as MenuInfoBase;
                m = @this.Add(n.Title);
                m.ToolTipText = n.ToolTip;

                if (n.Icon != null)
                {
                    var icon = n.Icon as Image;
                    m.Image = icon ?? Functions.LoadImage(n.Icon.ToString());
                }

                m.Alignment = n.Alignment == MenuAlignment.Right
                    ? ToolStripItemAlignment.Right
                    : ToolStripItemAlignment.Left;

                if (item is NavigationInfo)
                    m.Click += (s, e) => menuClickAction?.Invoke(m);
            }

            m.Tag = item;
            @this.Add(m);
            return m;
        }
    }
}