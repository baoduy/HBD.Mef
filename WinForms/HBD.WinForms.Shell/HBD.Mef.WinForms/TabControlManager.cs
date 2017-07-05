#region using

using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HBD.Framework.Core;
using HBD.Mef.WinForms.Core;

#endregion

namespace HBD.Mef.WinForms
{
    public partial class TabControlManager : TabControl
    {
        public TabControlManager()
        {
            InitializeComponent();
        }

        public TabControlManager(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public virtual UserControl SelectedView => SelectedTab?.Controls[0] as UserControl;
        //private IDictionary<UserControl, TabPage> _controls = new Dictionary<UserControl, TabPage>();

        public virtual void Add(UserControl control)
        {
            Guard.ArgumentIsNotNull(control, nameof(control));
            var tab = GetTabPageBycontrolType(control.GetType());
            if (tab != null)
            {
                SelectedTab = tab;
                return;
            }

            SuspendLayout();

            tab = new TabPage
            {
                Text = string.IsNullOrEmpty(control.Text) ? control.GetType().Name : control.Text,
                Name = $"Tab_{control.Name}"
            };

            control.Dock = DockStyle.Fill;

            tab.Controls.Add(control);

            TabPages.Add(tab);
            SelectedTab = tab;

            ResumeLayout();

            var activeAware = control.CastAs<IActiveAware>();
            if (activeAware != null)
                activeAware.IsActive = true;
        }

        public void Add<TView>() where TView : UserControl, new()
        {
            var tab = GetTabPageBycontrolType(typeof(TView));
            if (tab != null)
            {
                SelectedTab = tab;
                return;
            }

            Add(new TView());
        }

        public void Add(Type controlType)
        {
            var tab = GetTabPageBycontrolType(controlType);
            if (tab != null)
            {
                SelectedTab = tab;
                return;
            }

            Add(Activator.CreateInstance(controlType) as UserControl);
        }

        private TabPage GetTabPageBycontrolType(Type controlType)
            =>
                TabPages.Cast<TabPage>()
                    .FirstOrDefault(t => t.Controls.OfType<Control>().Any(c => c.GetType() == controlType));

        public bool IsExisted<TView>() where TView : UserControl
            => IsExisted(typeof(TView));

        public bool IsExisted(Type controlType)
        {
            Guard.ArgumentIsNotNull(controlType, nameof(controlType));
            return GetTabPageBycontrolType(controlType) != null;
        }

        public bool IsExisted(UserControl control)
            => IsExisted(control?.GetType());

        public void Select<TView>() where TView : UserControl
            => Select(typeof(TView));

        public void Select(Type controlType)
        {
            Guard.ArgumentIsNotNull(controlType, nameof(controlType));
            var tab = GetTabPageBycontrolType(controlType);
            if (tab == null) return;
            SelectedTab = tab;
        }

        public void Select(UserControl control)
            => Select(control?.GetType());

        public bool Remove(UserControl control) => Remove(control.GetType());

        public bool Remove(Type controlType)
        {
            var tab = GetTabPageBycontrolType(controlType);
            return Remove(tab);
        }

        protected virtual bool Remove(TabPage tab)
        {
            if (tab == null || tab.IsDisposed) return false;

            TabPages.Remove(tab);

            var activaeAware = tab.Controls.OfType<IActiveAware>().FirstOrDefault();
            if (activaeAware != null)
                activaeAware.IsActive = false;

            if (TabPages.Count > 0)
                SelectedTab = TabPages[TabCount - 1];
            tab.Dispose();

            return true;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (Parent != null)
            {
                Parent.Controls.Add(btn_Close);
                btn_Close.BringToFront();
            }

            base.OnVisibleChanged(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            btn_Close.Location = new Point(Location.X + Width - btn_Close.Width, Location.Y - 1);
            base.OnSizeChanged(e);
        }

        protected override void OnSelected(TabControlEventArgs e)
        {
            base.OnSelected(e);
            toolTip1.SetToolTip(btn_Close, $"Close {SelectedTab?.Text} tab.");
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            btn_Close.Visible = TabPages.Count > 0;
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            btn_Close.Visible = TabPages.Count > 1;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (SelectedTab == null) return;
            Remove(SelectedTab);
        }
    }
}