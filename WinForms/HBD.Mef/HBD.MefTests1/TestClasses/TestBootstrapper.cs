using HBD.Mef;
using HBD.Mef.Catalogs;
using HBD.Mef.Logging;
using HBD.Mef.Shell.Configuration;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace HBD.MefTests.TestClasses
{
    public class TestBootstrapper : MefBootstrapper
    {
        private IShellConfigManager ShellConfigManager { get; } = new ShellConfigManager();

        protected override ILogger CreateLogger()
            => new Trace2FileLogger($"Logs\\Log_{Guid.NewGuid()}.log");

        static TestBootstrapper _default;
        public static TestBootstrapper Default
        {
            get
            {
                if (_default != null) return _default;
                _default = new TestBootstrapper();
                _default.Run();
                return _default;
            }
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            var rfc = CreateReflectionContext();

            var ctg = rfc == null
                ? new MultiDirectoriesCatalog(new[] { "Modules" }, System.IO.SearchOption.AllDirectories)
                : new MultiDirectoriesCatalog(new[] { "Modules" }, System.IO.SearchOption.AllDirectories, rfc);

            AggregateCatalog.Catalogs.Add(ctg);
            //AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(TestBootstrapper).Assembly, rfc));
        }

        protected override void RegisterExternalObjects()
        {
            base.RegisterExternalObjects();
            Container.ComposeExportedValue(ShellConfigManager);
        }
    }
}
