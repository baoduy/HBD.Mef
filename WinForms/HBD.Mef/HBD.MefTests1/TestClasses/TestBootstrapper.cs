using HBD.Mef;
using HBD.Mef.Logging;
using HBD.Mef.Shell.Configuration;

namespace HBD.MefTests.TestClasses
{
    public class TestBootstrapper : MefBootstrapper
    {
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            var mm = new ShellConfigManager();
            var rfc = CreateReflectionContext();

            Logger.Info("Import Shell Binaries");
            mm.ImportShellBinaries(AggregateCatalog, rfc);

            Logger.Info("Import Module Binaries");
            mm.ImportModuleBinaries(AggregateCatalog, rfc);
        }
    }
}
