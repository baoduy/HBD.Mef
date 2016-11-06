using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using HBD.Framework;
using HBD.Mef.WinForms.Services;
using Microsoft.Practices.ServiceLocation;
using Prism;
using Prism.Logging;

namespace HBD.Mef.WinForms
{
    public partial class ViewBase : UserControl, INotifyPropertyChanged, IActiveAware
    {
        public ViewBase()
        {
            InitializeComponent();
            if (!DesignMode)
                Visible = IsActive;
        }

        [Import]
        public IServiceLocator ContainerService { protected get; set; }

        [Import]
        public ILoggerFacade Logger { protected get; set; }

        [Import]
        public IStatusBarService StatusService { protected get; set; }

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

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
            => OnPropertyChanged(propertyName);

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
            // ReSharper disable once ExplicitCallerInfoArgument
            => RaisePropertyChanged(ExtractPropertyName(propertyExpression));

        #endregion INotifyPropertyChanged

        protected virtual string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
            => propertyExpression.ExtractPropertyName();

        #endregion INotifyPropertyChanged
    }
}