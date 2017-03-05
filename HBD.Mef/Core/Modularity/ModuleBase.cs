#region

using System.ComponentModel.Composition;
using Microsoft.Practices.ServiceLocation;
using Prism.Logging;
using Prism.Modularity;

#endregion

namespace HBD.Mef.Core.Modularity
{
    public abstract class ModuleBase : IModule
    {
        [Import]
        protected IServiceLocator ContainerService { get; set; }

        [Import]
        protected ILoggerFacade Logger { get; set; }

        public abstract void Initialize();
    }
}