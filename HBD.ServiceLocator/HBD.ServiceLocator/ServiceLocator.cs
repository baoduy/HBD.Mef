using System;

#if NETSTANDARD2_0 || NETSTANDARD1_6
using System.Composition;
#endif

namespace HBD
{
    public static partial class ServiceLocator
    {
        private static IServiceLocator currentProvider;
        private static Func<IServiceLocator> serviceLocatorBuilder;

        public static IServiceLocator Current => GetOrInitialize();

        private static IServiceLocator GetOrInitialize()
        {
            if (currentProvider == null && serviceLocatorBuilder == null)
                throw new InvalidOperationException("Service Locator has not been provided.");
            if (currentProvider != null) return currentProvider;

            return currentProvider = serviceLocatorBuilder.Invoke();
        }

#if NETSTANDARD2_0 || NETSTANDARD1_6
        /// <summary>
        /// Initialize a IServiceLocator from Mef.
        /// </summary>
        /// <param name="service"></param>
        public static void SetServiceLocator(Func<CompositionContext> serviceBuilder)
            => serviceLocatorBuilder = () => new MefServiceLocator(serviceBuilder.Invoke());
#endif

        public static void SetServiceLocator(IServiceLocator newProvider)
            => currentProvider = newProvider ?? throw new ArgumentNullException(nameof(newProvider));

        public static void SetServiceLocator(Func<IServiceLocator> newProviderBuilder)
          => serviceLocatorBuilder = newProviderBuilder ?? throw new ArgumentNullException(nameof(newProviderBuilder));
    }
}
