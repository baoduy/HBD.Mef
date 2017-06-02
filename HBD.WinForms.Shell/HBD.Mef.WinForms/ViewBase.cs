#region using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using HBD.Framework;
using HBD.Mef.Shell.Services;
using HBD.Mef.WinForms.Core;
using HBD.Mef.WinForms.Services;
using Microsoft.Practices.ServiceLocation;
using HBD.Mef.Logging;

#endregion

namespace HBD.Mef.WinForms
{
    public class ViewBase : UserControl, INotifyPropertyChanged, IActiveAware, INavigationAware
    {
        public ViewBase()
        {
            if (!DesignMode)
                Visible = IsActive;
        }

        [Import(AllowDefault = true, AllowRecomposition = true)]
        public IServiceLocator ContainerService { protected get; set; }

        [Import(AllowDefault = true, AllowRecomposition = true)]
        public ILogger Logger { protected get; set; }

        [Import(AllowDefault = true, AllowRecomposition = true)]
        public IShellStatusService StatusService { protected get; set; }

        [Import(AllowDefault = true, AllowRecomposition = true)]
        public IMessageBoxService MessageBoxService { protected get; set; }

        [Import(AllowDefault = true, AllowRecomposition = true)]
        public INavigationManager NavigationManager { protected get; set; }

        #region NavigationAware

        public virtual void OnNavigatedTo(WinformNavigationContext navigationContext)
        {
        }

        #endregion

        #region IActiveAware

        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set
            {
                if (isActive == value) return;
                isActive = value;
                OnIsActiveChanged(EventArgs.Empty);
            }
        }

        public event EventHandler IsActiveChanged;

        protected virtual void OnIsActiveChanged(EventArgs e)
        {
            Visible = IsActive;

            IsActiveChanged?.Invoke(this, e);
        }

        #endregion IActiveAware

        #region INotifyPropertyChanged

        protected void SetValue<T>(ref T member, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(member, value)) return;

            member = value;
            // ReSharper disable once ExplicitCallerInfoArgument
            RaisePropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
            => OnPropertyChanged(propertyName);

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
            // ReSharper disable once ExplicitCallerInfoArgument
            => RaisePropertyChanged(ExtractPropertyName(propertyExpression));

        protected virtual string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
            => propertyExpression.ExtractPropertyName();

        #endregion INotifyPropertyChanged
    }
}