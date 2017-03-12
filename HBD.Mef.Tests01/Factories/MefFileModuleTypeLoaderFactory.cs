using System.ComponentModel.Composition.Hosting;
using HBD.Mef.Modularity;
// <copyright file="MefFileModuleTypeLoaderFactory.cs" company="Duy Hoang">Copyright © Microsoft 2016</copyright>

using System;
using Microsoft.Pex.Framework;

namespace HBD.Mef.Modularity
{
    /// <summary>A factory for HBD.Mef.Modularity.MefFileModuleTypeLoader instances</summary>
    public static partial class MefFileModuleTypeLoaderFactory
    {
        /// <summary>A factory for HBD.Mef.Modularity.MefFileModuleTypeLoader instances</summary>
        [PexFactoryMethod(typeof(MefFileModuleTypeLoader))]
        public static MefFileModuleTypeLoader Create(AggregateCatalog value_aggregateCatalog)
        {
            MefFileModuleTypeLoader mefFileModuleTypeLoader = new MefFileModuleTypeLoader();
            mefFileModuleTypeLoader.AggregateCatalog = value_aggregateCatalog;
            return mefFileModuleTypeLoader;

            // TODO: Edit factory method of MefFileModuleTypeLoader
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
