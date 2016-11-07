using Microsoft.Practices.ServiceLocation;
using Prism.Logging;
using Prism.Modularity;
using System.ComponentModel.Composition;

namespace HBD.Mef.ConsoleApp
{
    public abstract class ConsoleModuleBase : IModule
    {
        [Import]
        public ILoggerFacade Logger { protected get; set; }

        [Import]
        public IServiceLocator ContainerService { protected get; set; }

        public abstract void Initialize();

        public abstract void Run(params string[] args);
    }
}