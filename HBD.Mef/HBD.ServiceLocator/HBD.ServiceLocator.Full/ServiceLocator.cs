using System;
using System.ComponentModel.Composition.Hosting;

namespace HBD
{
    public static partial class ServiceLocator
    {
        /// <summary>
        /// Initialize a IServiceLocator from Mef. 
        /// </summary>
        /// <param name="serviceBuilder"></param>
        public static void SetServiceLocator(Func<CompositionContainer> serviceBuilder)
        {
            if (serviceBuilder == null)
                throw new ArgumentNullException(nameof(serviceBuilder));

            serviceLocatorBuilder = () => new MefServiceLocator(serviceBuilder.Invoke());
        }
    }
}
