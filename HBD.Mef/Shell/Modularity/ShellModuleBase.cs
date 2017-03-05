#region

using HBD.Framework;
using HBD.Mef.Core.Logging;
using HBD.Mef.Core.Modularity;
using HBD.Mef.Shell.Navigation;
using HBD.Mef.Shell.Services;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;

#endregion

namespace HBD.Mef.Shell.Modularity
{
    /// <summary>
    ///     The ShellModuleBase that share for both WindowForms and WPF.
    /// </summary>
    public abstract class ShellModuleBase : ModuleBase
    {
        public override void Initialize()
        {
            try
            {
                Logger.Info("Try to get IMenuInfoCollection");
                var menuSet = ContainerService.GetInstance<IShellMenuService>();
                MenuConfiguration(menuSet);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }

            try
            {
                Logger.Info("Try to get IStartupViewCollection");
                var startViews = ContainerService.TryResolve<IStartupViewCollection>();
                startViews.AddRange(GetStartUpViewTypes());
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        protected virtual IEnumerable<IViewInfo> GetStartUpViewTypes()
        {
            yield break;
        }

        protected virtual void MenuConfiguration(IShellMenuService menuSet)
        {
        }
    }
}