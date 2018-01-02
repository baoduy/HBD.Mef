#region using

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HBD.Framework;
using HBD.Mef.Logging;
using HBD.Mef.Shell;
using HBD.Mef.Shell.Configuration;
using HBD.Mef.Shell.Core;
using HBD.Mef.Shell.Navigation;
using HBD.Mef.Shell.Services;
using HBD.Mef.WinForms;
using HBD.Mef.WinForms.Core;
using HBD.Mef.WinForms.Services;

#endregion

namespace HBD.WinForms.Shell
{
    [Export(typeof(IShellStatusService))]
    [Export(typeof(IMainViewService))]
    [Export(typeof(FrMain))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class FrMain : FormBase, IShellStatusService, IMainViewService
    {
        public FrMain()
        {
            InitializeComponent();
        }

        [Import]
        private IShellConfigManager ShellConfigManager { get; set; }

        [Import]
        private IShellMenuService MenuSet { get; set; }

        [Import]
        private IStartupViewCollection StartupViews { get; set; }

        public IReadOnlyCollection<UserControl> Views
            => tabControlManager.TabPages.OfType<TabPage>().Where(t => t.Controls.Count > 0)
                .Select(t => t.Controls[0] as UserControl).ToReadOnly();

        public void SetStatus(string status)
        {
            SetStatus(status, Color.Empty);
        }

        public void Set(IStatusInfo status)
        {
            var st = status as StatusInfo;

            if (st != null && st.BackgroundColor != Color.Empty)
                lb_Status.ForeColor = st.BackgroundColor;

            lb_Status.Text = status.Message;
        }

        public void SetStatus(string status, Color backgroundColor)
        {
            Set(new StatusInfo {Message = status, BackgroundColor = backgroundColor});
        }

        //private void ShowDialog<TDialog>() where TDialog : Form, new()
        //{
        //    using (var fr = new TDialog())
        //    {
        //        fr.ShowDialog(this);
        //    }
        //}

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {
                //Set Shell Info;
                Text = ShellConfigManager.ShellConfig.Title;
                Icon = new Icon(Functions.GetIconPath(ShellConfigManager.ShellConfig.Logo));
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            try
            {
                InitializeMenu();

                LoadStartUpViews();
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private void InitializeMenu()
        {
            MenuSet.Menu(Resource.Menu_ShellHome)
                .DisplayAt(0)
                .WithIcon(Resource.Shell)
                .Children
                    .AddSeparator()
                    .AddNavigation(Resource.Menu_Exit)
                    .WithIcon(Resource.Exit)
                    .ForAction(Close);

            MenuSet.BindingToMenu(menuStrip.Items, MenuAction);
        }

        private void LoadStartUpViews()
        {
            LoadViews(StartupViews.ToArray());
        }

        private void MenuAction(ToolStripItem sender)
        {
            try
            {
                var action = sender.Tag as INavigationInfo;
                if (action == null || action.NavigationParameters.Count <= 0)
                {
                    var message = $"Cannot found the action for {sender.Text}";
                    Logger.Exception(message);
                    this.ShowErrorMessage(message);
                    return;
                }

                foreach (var item in action.NavigationParameters)
                {
                    if (item == null) continue;
                    if (item is ActionNavigationParameter)
                        ((ActionNavigationParameter) item).Action?.Invoke();
                    else if (item is IViewInfo)
                        LoadView(item as IViewInfo);
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                this.ShowErrorMessage(ex);
            }
        }

        private void LoadViews(params IViewInfo[] viewInfos)
        {
            viewInfos.ForEach(v => LoadView(v));
        }

        private UserControl LoadView(IViewInfo viewInfo)
        {
            if (tabControlManager.IsExisted(viewInfo.ViewType))
            {
                tabControlManager.Select(viewInfo.ViewType);
                return tabControlManager.SelectedView;
            }

            var control = ContainerService.GetInstance(viewInfo.ViewType, viewInfo.ViewName) as UserControl;

            if (control == null)
            {
                var message = $"The control of {viewInfo.ViewType.Name} is not found.";
                Logger.Exception(message);
                this.ShowErrorMessage(message);
            }

            tabControlManager.Add(control);

            return control;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

#if !DEBUG
            e.Cancel = this.ShowConfirmationMessage(Resource.Message_FormClosing) != DialogResult.Yes;
#endif
        }

        #region IMainViewService

        public void ActiveView(UserControl control)
        {
            tabControlManager.Add(control);
        }

        public UserControl ActiveView(Type viewType, string viewName = null)
        {
            return LoadView(new ViewInfo(viewName, viewType));
        }

        public UserControl ActiveView(IViewInfo viewInfo)
        {
            return LoadView(viewInfo);
        }

        public void DeactivateView(UserControl control)
        {
            tabControlManager.Remove(control);
        }

        public void DeactivateView(Type viewType, string viewName = null)
        {
            DeactivateView(new ViewInfo(viewName, viewType));
        }

        public void DeactivateView(IViewInfo viewInfo)
        {
            if (viewInfo.ViewType != null)
            {
                tabControlManager.Remove(viewInfo.ViewType);
            }
            else
            {
                var control = ContainerService.GetInstance(viewInfo.ViewType, viewInfo.ViewName) as UserControl;
                tabControlManager.Remove(control);
            }
        }

        #endregion IMainViewService
    }
}