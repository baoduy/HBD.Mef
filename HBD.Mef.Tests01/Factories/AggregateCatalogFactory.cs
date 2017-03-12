using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.Hosting;
// <copyright file="AggregateCatalogFactory.cs" company="Duy Hoang">Copyright © Microsoft 2016</copyright>

using System;
using Microsoft.Pex.Framework;

namespace System.ComponentModel.Composition.Hosting
{
    /// <summary>A factory for System.ComponentModel.Composition.Hosting.AggregateCatalog instances</summary>
    public static partial class AggregateCatalogFactory
    {
        /// <summary>A factory for System.ComponentModel.Composition.Hosting.AggregateCatalog instances</summary>
        [PexFactoryMethod(typeof(AggregateCatalog))]
        public static AggregateCatalog Create(IEnumerable<ComposablePartCatalog> catalogs_iEnumerable)
        {
            AggregateCatalog aggregateCatalog = new AggregateCatalog(catalogs_iEnumerable);
            return aggregateCatalog;

            // TODO: Edit factory method of AggregateCatalog
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
