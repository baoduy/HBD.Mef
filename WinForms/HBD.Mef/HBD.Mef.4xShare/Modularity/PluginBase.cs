#region using

using System.ComponentModel.Composition;
using HBD.Mef.Logging;
using HBD.ServiceLocator;

#endregion

namespace HBD.Mef.Modularity
{
    public abstract class PluginBase : IPlugin
    {
        [Import(AllowRecomposition = true, AllowDefault = true)]
        protected IServiceLocator ContainerService { get; set; }

        [Import(AllowRecomposition = true, AllowDefault = true)]
        protected ILogger Logger { get; set; }

        public abstract void Initialize();
    }
}