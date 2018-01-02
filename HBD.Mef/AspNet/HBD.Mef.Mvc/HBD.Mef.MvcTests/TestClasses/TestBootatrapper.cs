#region using

using System.ComponentModel.Composition.Hosting;
using HBD.Mef.Mvc;

#endregion

namespace HBD.Mef.MvcTests.TestClasses
{
    internal class TestBootatrapper : MvcBootstrapper
    {
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            AggregateCatalog.Catalogs.Add(
                new AssemblyCatalog(typeof(MvcBootstrapper).Assembly, CreateReflectionContext()));
            AggregateCatalog.Catalogs.Add(
                new AssemblyCatalog(typeof(TestBootatrapper).Assembly, CreateReflectionContext()));
        }
    }
}