using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using HBD.Framework;
using HBD.Mef.Core.Logging;
using HBD.Mef.Core.Navigation;
using Microsoft.Practices.ServiceLocation;
using Prism.Logging;
using Prism.Modularity;

namespace HBD.Mef.Core.Modularity
{
    public abstract class ModuleBase : IModule
    {
        [Import]
        protected IServiceLocator ServiceLocator { get; set; }

        [Import]
        protected ILoggerFacade Logger { get; set; }

        public virtual void Initialize()
        {
            try
            {
                Logger.Info("Try to get IMenuInfoCollection");
                var menuSet = ServiceLocator.GetInstance<IMenuInfoCollection>();
                MenuConfiguration(menuSet);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }

            try
            {
                Logger.Info("Try to get IStartupViewCollection");
                var startViews = ServiceLocator.TryResolve<IStartupViewCollection>();
                startViews.AddRange(GetStartUpViewTypes());
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        protected abstract IEnumerable<IViewInfo> GetStartUpViewTypes();

        protected abstract void MenuConfiguration(IMenuInfoCollection menuSet);
    }
}