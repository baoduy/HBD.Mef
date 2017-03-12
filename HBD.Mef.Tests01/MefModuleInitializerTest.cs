using System.ComponentModel.Composition.Hosting;
using Prism.Logging;
using Microsoft.Practices.ServiceLocation;
// <copyright file="MefModuleInitializerTest.cs" company="Duy Hoang">Copyright © Microsoft 2016</copyright>

using System;
using HBD.Mef.Modularity;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBD.Mef.Modularity.Tests
{
    /// <summary>This class contains parameterized unit tests for MefModuleInitializer</summary>
    [TestClassAttribute]
    [PexClass(typeof(MefModuleInitializer))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class MefModuleInitializerTest
    {

        /// <summary>Test stub for .ctor(IServiceLocator, ILoggerFacade, DownloadedPartCatalogCollection, AggregateCatalog)</summary>
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public MefModuleInitializer ConstructorTest(
            IServiceLocator serviceLocator,
            ILoggerFacade loggerFacade,
            DownloadedPartCatalogCollection downloadedPartCatalogs,
            AggregateCatalog aggregateCatalog
        )
        {
            MefModuleInitializer target =
                                 new MefModuleInitializer(serviceLocator, loggerFacade, downloadedPartCatalogs, aggregateCatalog);
            return target;
            // TODO: add assertions to method MefModuleInitializerTest.ConstructorTest(IServiceLocator, ILoggerFacade, DownloadedPartCatalogCollection, AggregateCatalog)
        }
    }
}
