using HBD.Mef.Logging;
using HBD.Mef.Modularity;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace HBD.MefTests.TestObs
{
    class DuyPlugin : IPlugin
    {
        [Import(AllowDefault =true)]
        public CompositionContainer Container { get; set; }

        [Import(AllowDefault = true)]
        public ILogger Logger { get; set; }

        public void Initialize()
        {
            
        }
    }
}
