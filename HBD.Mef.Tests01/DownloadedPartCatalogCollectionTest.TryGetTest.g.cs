using Microsoft.Pex.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Modularity;
using System.ComponentModel.Composition.Primitives;
using HBD.Mef.Modularity;
using Microsoft.Pex.Framework.Generated;
// <auto-generated>
// This file contains automatically generated tests.
// Do not modify this file manually.
// 
// If the contents of this file becomes outdated, you can delete it.
// For example, if it no longer compiles.
// </auto-generated>
using System;

namespace HBD.Mef.Modularity.Tests
{
    public partial class DownloadedPartCatalogCollectionTest
    {

        [TestMethodAttribute]
        [PexGeneratedBy(typeof(DownloadedPartCatalogCollectionTest))]
        [PexRaisedException(typeof(ArgumentNullException))]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryGetTestThrowsArgumentNullException261()
        {
            using (PexDisposableContext disposables = PexDisposableContext.Create())
            {
                DownloadedPartCatalogCollection downloadedPartCatalogCollection;
                bool b;
                downloadedPartCatalogCollection =
                  DownloadedPartCatalogCollectionFactory.Create();
                ComposablePartCatalog composablePartCatalog = (ComposablePartCatalog)null;
                b = this.TryGetTest(downloadedPartCatalogCollection,
                                    (ModuleInfo)null, out composablePartCatalog);
                disposables.Add((IDisposable)composablePartCatalog);
                disposables.Dispose();
            }
        }

        [TestMethodAttribute]
        [PexGeneratedBy(typeof(DownloadedPartCatalogCollectionTest))]
        public void TryGetTest556()
        {
            using (PexDisposableContext disposables = PexDisposableContext.Create())
            {
                DownloadedPartCatalogCollection downloadedPartCatalogCollection;
                ModuleInfo moduleInfo;
                bool b;
                downloadedPartCatalogCollection =
                  DownloadedPartCatalogCollectionFactory.Create();
                string[] ss = new string[0];
                moduleInfo = ModuleInfoFactory.Create((string)null, (string)null, ss,
                                                      InitializationMode.WhenAvailable, (string)null, ModuleState.NotStarted);
                ComposablePartCatalog composablePartCatalog = (ComposablePartCatalog)null;
                b = this.TryGetTest
                        (downloadedPartCatalogCollection, moduleInfo, out composablePartCatalog);
                disposables.Add((IDisposable)composablePartCatalog);
                disposables.Dispose();
                PexAssert.AreEqual<bool>(false, b);
                PexAssert.IsNotNull((object)downloadedPartCatalogCollection);
                PexAssert.IsNull((object)composablePartCatalog);
            }
        }

        [TestMethodAttribute]
        [PexGeneratedBy(typeof(DownloadedPartCatalogCollectionTest))]
        public void TryGetTest246()
        {
            using (PexDisposableContext disposables = PexDisposableContext.Create())
            {
                DownloadedPartCatalogCollection downloadedPartCatalogCollection;
                ModuleInfo moduleInfo;
                bool b;
                downloadedPartCatalogCollection =
                  DownloadedPartCatalogCollectionFactory.Create();
                string[] ss = new string[1];
                moduleInfo = ModuleInfoFactory.Create((string)null, (string)null, ss,
                                                      InitializationMode.WhenAvailable, (string)null, ModuleState.NotStarted);
                ComposablePartCatalog composablePartCatalog = (ComposablePartCatalog)null;
                b = this.TryGetTest
                        (downloadedPartCatalogCollection, moduleInfo, out composablePartCatalog);
                disposables.Add((IDisposable)composablePartCatalog);
                disposables.Dispose();
                PexAssert.AreEqual<bool>(false, b);
                PexAssert.IsNotNull((object)downloadedPartCatalogCollection);
                PexAssert.IsNull((object)composablePartCatalog);
            }
        }

        [TestMethodAttribute]
        [PexGeneratedBy(typeof(DownloadedPartCatalogCollectionTest))]
        public void TryGetTest121()
        {
            using (PexDisposableContext disposables = PexDisposableContext.Create())
            {
                DownloadedPartCatalogCollection downloadedPartCatalogCollection;
                ModuleInfo moduleInfo;
                bool b;
                downloadedPartCatalogCollection =
                  DownloadedPartCatalogCollectionFactory.Create();
                string[] ss = new string[5];
                moduleInfo = ModuleInfoFactory.Create((string)null, (string)null, ss,
                                                      InitializationMode.WhenAvailable, (string)null, ModuleState.NotStarted);
                ComposablePartCatalog composablePartCatalog = (ComposablePartCatalog)null;
                b = this.TryGetTest
                        (downloadedPartCatalogCollection, moduleInfo, out composablePartCatalog);
                disposables.Add((IDisposable)composablePartCatalog);
                disposables.Dispose();
                PexAssert.AreEqual<bool>(false, b);
                PexAssert.IsNotNull((object)downloadedPartCatalogCollection);
                PexAssert.IsNull((object)composablePartCatalog);
            }
        }

        [TestMethodAttribute]
        [PexGeneratedBy(typeof(DownloadedPartCatalogCollectionTest))]
        [ExpectedExceptionAttribute(typeof(ArgumentNullException))]
        public void TryGetTestThrowsArgumentNullException26101()
        {
            using (PexDisposableContext disposables = PexDisposableContext.Create())
            {
                DownloadedPartCatalogCollection downloadedPartCatalogCollection;
                bool b;
                downloadedPartCatalogCollection =
                  DownloadedPartCatalogCollectionFactory.Create();
                ComposablePartCatalog composablePartCatalog = (ComposablePartCatalog)null;
                b = this.TryGetTest(downloadedPartCatalogCollection,
                                    (ModuleInfo)null, out composablePartCatalog);
                disposables.Add((IDisposable)composablePartCatalog);
                disposables.Dispose();
            }
        }

        [TestMethodAttribute]
        [PexGeneratedBy(typeof(DownloadedPartCatalogCollectionTest))]
        public void TryGetTest55601()
        {
            using (PexDisposableContext disposables = PexDisposableContext.Create())
            {
                DownloadedPartCatalogCollection downloadedPartCatalogCollection;
                ModuleInfo moduleInfo;
                bool b;
                downloadedPartCatalogCollection =
                  DownloadedPartCatalogCollectionFactory.Create();
                string[] ss = new string[0];
                moduleInfo = ModuleInfoFactory.Create((string)null, (string)null, ss,
                                                      InitializationMode.WhenAvailable, (string)null, ModuleState.NotStarted);
                ComposablePartCatalog composablePartCatalog = (ComposablePartCatalog)null;
                b = this.TryGetTest
                        (downloadedPartCatalogCollection, moduleInfo, out composablePartCatalog);
                disposables.Add((IDisposable)composablePartCatalog);
                disposables.Dispose();
                PexAssert.AreEqual<bool>(false, b);
                PexAssert.IsNotNull((object)downloadedPartCatalogCollection);
                PexAssert.IsNull((object)composablePartCatalog);
            }
        }

        [TestMethodAttribute]
        [PexGeneratedBy(typeof(DownloadedPartCatalogCollectionTest))]
        public void TryGetTest24601()
        {
            using (PexDisposableContext disposables = PexDisposableContext.Create())
            {
                DownloadedPartCatalogCollection downloadedPartCatalogCollection;
                ModuleInfo moduleInfo;
                bool b;
                downloadedPartCatalogCollection =
                  DownloadedPartCatalogCollectionFactory.Create();
                string[] ss = new string[1];
                moduleInfo = ModuleInfoFactory.Create((string)null, (string)null, ss,
                                                      InitializationMode.WhenAvailable, (string)null, ModuleState.NotStarted);
                ComposablePartCatalog composablePartCatalog = (ComposablePartCatalog)null;
                b = this.TryGetTest
                        (downloadedPartCatalogCollection, moduleInfo, out composablePartCatalog);
                disposables.Add((IDisposable)composablePartCatalog);
                disposables.Dispose();
                PexAssert.AreEqual<bool>(false, b);
                PexAssert.IsNotNull((object)downloadedPartCatalogCollection);
                PexAssert.IsNull((object)composablePartCatalog);
            }
        }

        [TestMethodAttribute]
        [PexGeneratedBy(typeof(DownloadedPartCatalogCollectionTest))]
        public void TryGetTest12101()
        {
            using (PexDisposableContext disposables = PexDisposableContext.Create())
            {
                DownloadedPartCatalogCollection downloadedPartCatalogCollection;
                ModuleInfo moduleInfo;
                bool b;
                downloadedPartCatalogCollection =
                  DownloadedPartCatalogCollectionFactory.Create();
                string[] ss = new string[5];
                moduleInfo = ModuleInfoFactory.Create((string)null, (string)null, ss,
                                                      InitializationMode.WhenAvailable, (string)null, ModuleState.NotStarted);
                ComposablePartCatalog composablePartCatalog = (ComposablePartCatalog)null;
                b = this.TryGetTest
                        (downloadedPartCatalogCollection, moduleInfo, out composablePartCatalog);
                disposables.Add((IDisposable)composablePartCatalog);
                disposables.Dispose();
                PexAssert.AreEqual<bool>(false, b);
                PexAssert.IsNotNull((object)downloadedPartCatalogCollection);
                PexAssert.IsNull((object)composablePartCatalog);
            }
        }

        [TestMethodAttribute]
        [PexGeneratedBy(typeof(DownloadedPartCatalogCollectionTest))]
        [ExpectedExceptionAttribute(typeof(ArgumentNullException))]
        public void TryGetTestThrowsArgumentNullException26102()
        {
            using (PexDisposableContext disposables = PexDisposableContext.Create())
            {
                DownloadedPartCatalogCollection downloadedPartCatalogCollection;
                bool b;
                downloadedPartCatalogCollection =
                  DownloadedPartCatalogCollectionFactory.Create();
                ComposablePartCatalog composablePartCatalog = (ComposablePartCatalog)null;
                b = this.TryGetTest(downloadedPartCatalogCollection,
                                    (ModuleInfo)null, out composablePartCatalog);
                disposables.Add((IDisposable)composablePartCatalog);
                disposables.Dispose();
            }
        }

        [TestMethodAttribute]
        [PexGeneratedBy(typeof(DownloadedPartCatalogCollectionTest))]
        public void TryGetTest55602()
        {
            using (PexDisposableContext disposables = PexDisposableContext.Create())
            {
                DownloadedPartCatalogCollection downloadedPartCatalogCollection;
                ModuleInfo moduleInfo;
                bool b;
                downloadedPartCatalogCollection =
                  DownloadedPartCatalogCollectionFactory.Create();
                string[] ss = new string[0];
                moduleInfo = ModuleInfoFactory.Create((string)null, (string)null, ss,
                                                      InitializationMode.WhenAvailable, (string)null, ModuleState.NotStarted);
                ComposablePartCatalog composablePartCatalog = (ComposablePartCatalog)null;
                b = this.TryGetTest
                        (downloadedPartCatalogCollection, moduleInfo, out composablePartCatalog);
                disposables.Add((IDisposable)composablePartCatalog);
                disposables.Dispose();
                PexAssert.AreEqual<bool>(false, b);
                PexAssert.IsNotNull((object)downloadedPartCatalogCollection);
                PexAssert.IsNull((object)composablePartCatalog);
            }
        }

        [TestMethodAttribute]
        [PexGeneratedBy(typeof(DownloadedPartCatalogCollectionTest))]
        public void TryGetTest24602()
        {
            using (PexDisposableContext disposables = PexDisposableContext.Create())
            {
                DownloadedPartCatalogCollection downloadedPartCatalogCollection;
                ModuleInfo moduleInfo;
                bool b;
                downloadedPartCatalogCollection =
                  DownloadedPartCatalogCollectionFactory.Create();
                string[] ss = new string[1];
                moduleInfo = ModuleInfoFactory.Create((string)null, (string)null, ss,
                                                      InitializationMode.WhenAvailable, (string)null, ModuleState.NotStarted);
                ComposablePartCatalog composablePartCatalog = (ComposablePartCatalog)null;
                b = this.TryGetTest
                        (downloadedPartCatalogCollection, moduleInfo, out composablePartCatalog);
                disposables.Add((IDisposable)composablePartCatalog);
                disposables.Dispose();
                PexAssert.AreEqual<bool>(false, b);
                PexAssert.IsNotNull((object)downloadedPartCatalogCollection);
                PexAssert.IsNull((object)composablePartCatalog);
            }
        }

        [TestMethodAttribute]
        [PexGeneratedBy(typeof(DownloadedPartCatalogCollectionTest))]
        public void TryGetTest12102()
        {
            using (PexDisposableContext disposables = PexDisposableContext.Create())
            {
                DownloadedPartCatalogCollection downloadedPartCatalogCollection;
                ModuleInfo moduleInfo;
                bool b;
                downloadedPartCatalogCollection =
                  DownloadedPartCatalogCollectionFactory.Create();
                string[] ss = new string[5];
                moduleInfo = ModuleInfoFactory.Create((string)null, (string)null, ss,
                                                      InitializationMode.WhenAvailable, (string)null, ModuleState.NotStarted);
                ComposablePartCatalog composablePartCatalog = (ComposablePartCatalog)null;
                b = this.TryGetTest
                        (downloadedPartCatalogCollection, moduleInfo, out composablePartCatalog);
                disposables.Add((IDisposable)composablePartCatalog);
                disposables.Dispose();
                PexAssert.AreEqual<bool>(false, b);
                PexAssert.IsNotNull((object)downloadedPartCatalogCollection);
                PexAssert.IsNull((object)composablePartCatalog);
            }
        }

[TestMethodAttribute]
[PexGeneratedBy(typeof(DownloadedPartCatalogCollectionTest))]
[ExpectedExceptionAttribute(typeof(ArgumentNullException))]
public void TryGetTestThrowsArgumentNullException26103()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      DownloadedPartCatalogCollection downloadedPartCatalogCollection;
      bool b;
      downloadedPartCatalogCollection =
        DownloadedPartCatalogCollectionFactory.Create();
      ComposablePartCatalog composablePartCatalog = (ComposablePartCatalog)null;
      b = this.TryGetTest(downloadedPartCatalogCollection, 
                          (ModuleInfo)null, out composablePartCatalog);
      disposables.Add((IDisposable)composablePartCatalog);
      disposables.Dispose();
    }
}

[TestMethodAttribute]
[PexGeneratedBy(typeof(DownloadedPartCatalogCollectionTest))]
public void TryGetTest55603()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      DownloadedPartCatalogCollection downloadedPartCatalogCollection;
      ModuleInfo moduleInfo;
      bool b;
      downloadedPartCatalogCollection =
        DownloadedPartCatalogCollectionFactory.Create();
      string[] ss = new string[0];
      moduleInfo = ModuleInfoFactory.Create((string)null, (string)null, ss, 
                                            InitializationMode.WhenAvailable, (string)null, ModuleState.NotStarted);
      ComposablePartCatalog composablePartCatalog = (ComposablePartCatalog)null;
      b = this.TryGetTest
              (downloadedPartCatalogCollection, moduleInfo, out composablePartCatalog);
      disposables.Add((IDisposable)composablePartCatalog);
      disposables.Dispose();
      PexAssert.AreEqual<bool>(false, b);
      PexAssert.IsNotNull((object)downloadedPartCatalogCollection);
      PexAssert.IsNull((object)composablePartCatalog);
    }
}

[TestMethodAttribute]
[PexGeneratedBy(typeof(DownloadedPartCatalogCollectionTest))]
public void TryGetTest24603()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      DownloadedPartCatalogCollection downloadedPartCatalogCollection;
      ModuleInfo moduleInfo;
      bool b;
      downloadedPartCatalogCollection =
        DownloadedPartCatalogCollectionFactory.Create();
      string[] ss = new string[1];
      moduleInfo = ModuleInfoFactory.Create((string)null, (string)null, ss, 
                                            InitializationMode.WhenAvailable, (string)null, ModuleState.NotStarted);
      ComposablePartCatalog composablePartCatalog = (ComposablePartCatalog)null;
      b = this.TryGetTest
              (downloadedPartCatalogCollection, moduleInfo, out composablePartCatalog);
      disposables.Add((IDisposable)composablePartCatalog);
      disposables.Dispose();
      PexAssert.AreEqual<bool>(false, b);
      PexAssert.IsNotNull((object)downloadedPartCatalogCollection);
      PexAssert.IsNull((object)composablePartCatalog);
    }
}

[TestMethodAttribute]
[PexGeneratedBy(typeof(DownloadedPartCatalogCollectionTest))]
public void TryGetTest12103()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      DownloadedPartCatalogCollection downloadedPartCatalogCollection;
      ModuleInfo moduleInfo;
      bool b;
      downloadedPartCatalogCollection =
        DownloadedPartCatalogCollectionFactory.Create();
      string[] ss = new string[5];
      moduleInfo = ModuleInfoFactory.Create((string)null, (string)null, ss, 
                                            InitializationMode.WhenAvailable, (string)null, ModuleState.NotStarted);
      ComposablePartCatalog composablePartCatalog = (ComposablePartCatalog)null;
      b = this.TryGetTest
              (downloadedPartCatalogCollection, moduleInfo, out composablePartCatalog);
      disposables.Add((IDisposable)composablePartCatalog);
      disposables.Dispose();
      PexAssert.AreEqual<bool>(false, b);
      PexAssert.IsNotNull((object)downloadedPartCatalogCollection);
      PexAssert.IsNull((object)composablePartCatalog);
    }
}
    }
}
