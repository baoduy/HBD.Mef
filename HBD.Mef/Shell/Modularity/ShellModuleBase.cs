#region using

using System;
using System.Collections.Generic;
using HBD.Framework;
using HBD.Mef.Logging;
using HBD.Mef.Modularity;
using HBD.Mef.Shell.Navigation;
using HBD.Mef.Shell.Services;

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
                var startViews = ContainerService.GetInstance<IStartupViewCollection>();
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