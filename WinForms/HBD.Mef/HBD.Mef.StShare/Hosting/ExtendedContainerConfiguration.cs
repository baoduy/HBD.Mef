using HBD.Framework.Core;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;

namespace HBD.Mef.Hosting
{
    /// <summary>
    /// This is extension for ContainerConfiguration that allow to imports singleton instances.
    /// </summary>
    public class ExtendedContainerConfiguration : ContainerConfiguration
    {
        readonly List<IInstanceInfo> Singletons;

        public ExtendedContainerConfiguration() => Singletons = new List<IInstanceInfo>();

        public ExtendedContainerConfiguration WithInstance<TInterface>(Func<TInterface> selector)
            => WithInstance(typeof(TInterface).FullName, selector);

        public ExtendedContainerConfiguration WithInstance<TInterface>(string name, Func<TInterface> selector)
        {
            Guard.ArgumentIsNotNull(name, nameof(name));
            Guard.ArgumentIsNotNull(selector, nameof(selector));

            var type = typeof(TInterface);
            var info = new InstanceInfo<TInterface>(name, type, selector);

            if (Singletons.Contains(info))
                throw new ArgumentException($"The instance of {type.Name} already resisted.");

            Singletons.Add(info);

            return this;
        }

        public new CompositionContext CreateContainer()
        {
            var container = base.CreateContainer();
            return new ExtendedCompositionHost(container, Singletons);
        }
    }
}
