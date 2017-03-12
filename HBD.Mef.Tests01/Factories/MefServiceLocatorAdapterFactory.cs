using System.ComponentModel.Composition.Hosting;
using HBD.Mef.Services;
using System;
using Microsoft.Pex.Framework;

namespace HBD.Mef.Services
{
    /// <summary>A factory for HBD.Mef.Services.MefServiceLocatorAdapter instances</summary>
    public static partial class MefServiceLocatorAdapterFactory
    {
        /// <summary>A factory for HBD.Mef.Services.MefServiceLocatorAdapter instances</summary>
        [PexFactoryMethod(typeof(MefServiceLocatorAdapter))]
        public static MefServiceLocatorAdapter Create(CompositionContainer compositionContainer_compositionContainer)
        {
            MefServiceLocatorAdapter mefServiceLocatorAdapter
       = new MefServiceLocatorAdapter(compositionContainer_compositionContainer);
            return mefServiceLocatorAdapter;

            // TODO: Edit factory method of MefServiceLocatorAdapter
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
