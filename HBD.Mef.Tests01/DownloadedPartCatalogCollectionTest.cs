using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using Prism.Modularity;
// <copyright file="DownloadedPartCatalogCollectionTest.cs" company="Duy Hoang">Copyright © Microsoft 2016</copyright>

using System;
using HBD.Mef.Modularity;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBD.Mef.Modularity.Tests
{
    /// <summary>This class contains parameterized unit tests for DownloadedPartCatalogCollection</summary>
    [TestClassAttribute]
    [PexClass(typeof(DownloadedPartCatalogCollection))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class DownloadedPartCatalogCollectionTest
    {

        /// <summary>Test stub for Add(ModuleInfo, ComposablePartCatalog)</summary>
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public void AddTest(
            [PexAssumeUnderTest]DownloadedPartCatalogCollection target,
            ModuleInfo moduleInfo,
            ComposablePartCatalog catalog
        )
        {
            target.Add(moduleInfo, catalog);
            // TODO: add assertions to method DownloadedPartCatalogCollectionTest.AddTest(DownloadedPartCatalogCollection, ModuleInfo, ComposablePartCatalog)
        }

        /// <summary>Test stub for Clear()</summary>
        [PexMethod]
        public void ClearTest([PexAssumeUnderTest]DownloadedPartCatalogCollection target)
        {
            target.Clear();
            // TODO: add assertions to method DownloadedPartCatalogCollectionTest.ClearTest(DownloadedPartCatalogCollection)
        }

        /// <summary>Test stub for Remove(ModuleInfo)</summary>
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public void RemoveTest([PexAssumeUnderTest]DownloadedPartCatalogCollection target, ModuleInfo moduleInfo)
        {
            target.Remove(moduleInfo);
            // TODO: add assertions to method DownloadedPartCatalogCollectionTest.RemoveTest(DownloadedPartCatalogCollection, ModuleInfo)
        }

        /// <summary>Test stub for TryGet(ModuleInfo, ComposablePartCatalog&amp;)</summary>
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public bool TryGetTest(
            [PexAssumeUnderTest]DownloadedPartCatalogCollection target,
            ModuleInfo moduleInfo,
            out ComposablePartCatalog catalog
        )
        {
            bool result = target.TryGet(moduleInfo, out catalog);
            return result;
            // TODO: add assertions to method DownloadedPartCatalogCollectionTest.TryGetTest(DownloadedPartCatalogCollection, ModuleInfo, ComposablePartCatalog&)
        }

        /// <summary>Test stub for Get(ModuleInfo)</summary>
        [PexMethod(MaxRunsWithoutNewTests = 200)]
        [PexAllowedException(typeof(ArgumentNullException))]
        [PexAllowedException(typeof(KeyNotFoundException))]
        public ComposablePartCatalog GetTest([PexAssumeUnderTest]DownloadedPartCatalogCollection target, ModuleInfo moduleInfo)
        {
            ComposablePartCatalog result = target.Get(moduleInfo);
            return result;
            // TODO: add assertions to method DownloadedPartCatalogCollectionTest.GetTest(DownloadedPartCatalogCollection, ModuleInfo)
        }
    }
}
