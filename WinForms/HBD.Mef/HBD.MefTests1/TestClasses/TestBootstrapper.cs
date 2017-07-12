using HBD.Mef;
using HBD.Mef.Catalogs;
using HBD.Mef.Logging;
using System;

namespace HBD.MefTests.TestClasses
{
    public class TestBootstrapper : MefBootstrapper
    {
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
        }
    }
}
