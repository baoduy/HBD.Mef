using HBD.Mef.Modularity;
// <copyright file="DownloadedPartCatalogCollectionFactory.cs" company="Duy Hoang">Copyright © Microsoft 2016</copyright>

using System;
using Microsoft.Pex.Framework;

namespace HBD.Mef.Modularity
{
    /// <summary>A factory for HBD.Mef.Modularity.DownloadedPartCatalogCollection instances</summary>
    public static partial class DownloadedPartCatalogCollectionFactory
    {
        /// <summary>A factory for HBD.Mef.Modularity.DownloadedPartCatalogCollection instances</summary>
        [PexFactoryMethod(typeof(DownloadedPartCatalogCollection))]
        public static DownloadedPartCatalogCollection Create()
        {
            DownloadedPartCatalogCollection downloadedPartCatalogCollection
       = new DownloadedPartCatalogCollection();
            return downloadedPartCatalogCollection;

            // TODO: Edit factory method of DownloadedPartCatalogCollection
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
