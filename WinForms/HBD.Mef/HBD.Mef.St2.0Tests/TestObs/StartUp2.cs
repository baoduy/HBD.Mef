using System.Composition;

namespace HBD.Mef.StTests.TestObs
{
    [Shared, Export(typeof(StartUp2))]
    [Export(typeof(IPlugin))]
    public class StartUp2: IPlugin
    {
    }
}
