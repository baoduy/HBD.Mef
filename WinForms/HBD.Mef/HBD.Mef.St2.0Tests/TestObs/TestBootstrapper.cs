using System.Composition.Convention;
using HBD.Mef.Catalogs;
using System;
using HBD.Mef.Hosting;

namespace HBD.Mef.StTests.TestObs
{
    class TestBootstrapper : StandardBootstrapper
    {
        protected override ExtendedContainerConfiguration CreateContainerConfiguration()
        {
            return base.CreateContainerConfiguration()
                .WithInstance<IPlugin>("Duy", () => new DuyPlugin());
        }

        protected override void ConfigureCatalog()
        {
            base.ConfigureCatalog();
            this.Catalogs.Add(new MultiDirectoriesCatalog(new[] { AppContext.BaseDirectory }, System.IO.SearchOption.AllDirectories, CreateConventionBuilder()));
        }

        private ConventionBuilder CreateConventionBuilder()
        {
            var conventions = new ConventionBuilder();

            conventions
                .ForTypesDerivedFrom<IPlugin>()
                .Export()
                .Export<IPlugin>()
                .Shared();

            return conventions;
        }
    }
}
