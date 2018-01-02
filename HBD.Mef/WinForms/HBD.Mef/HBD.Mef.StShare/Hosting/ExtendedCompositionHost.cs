using HBD.Framework.Core;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.Composition.Hosting.Core;
using System.Linq;

namespace HBD.Mef.Hosting
{
    public sealed class ExtendedCompositionHost : CompositionContext, IDisposable
    {
        internal ExtendedCompositionHost(CompositionHost compositionHost, IReadOnlyCollection<IInstanceInfo> singletons)
        {
            Guard.ArgumentIsNotNull(compositionHost, nameof(compositionHost));
            Guard.ArgumentIsNotNull(singletons, nameof(singletons));

            CompositionHost = compositionHost;
            Singletons = singletons;
        }

        private CompositionHost CompositionHost { get; }
        private IReadOnlyCollection<IInstanceInfo> Singletons { get; }

        public void Dispose()
        {
            CompositionHost.Dispose();
        }

        public override bool TryGetExport(CompositionContract contract, out object export)
        {
            if (CompositionHost.TryGetExport(contract, out export))
                return true;

            var name = contract.ContractName ?? contract.ContractType.FullName;
            var type = contract.ContractType;

            var found = Singletons.FirstOrDefault(a => a.ContractName == name 
                && a.ContractType == type);

            if (found == null)
                return false;

            export = found.Instance;
            this.SatisfyImports(export);

            return true;
        }
    }
}
