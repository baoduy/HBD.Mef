using HBD.Mef.Logging;
using System.Composition;

namespace HBD.Mef.StTests.TestObs
{
    class DuyPlugin : IPlugin
    {
        [Import(AllowDefault =true)]
        public CompositionContext Container { get; set; }

        [Import(AllowDefault = true)]
        public ILogger Logger { get; set; }
    }
}
