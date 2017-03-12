using HBD.Mef.Shell.Configuration;
using System.Xml;
using System.ComponentModel.Composition.Hosting;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Configuration;
using System.Text;
using System.Globalization;
using System.IO;
using Prism.Logging;
using Microsoft.Pex.Framework.Using;
using System;
using Microsoft.Pex.Framework.Suppression;
// <copyright file="PexAssemblyInfo.cs" company="Duy Hoang">Copyright © Microsoft 2016</copyright>
using Microsoft.Pex.Framework.Coverage;
using Microsoft.Pex.Framework.Creatable;
using Microsoft.Pex.Framework.Instrumentation;
using Microsoft.Pex.Framework.Settings;
using Microsoft.Pex.Framework.Validation;

// Microsoft.Pex.Framework.Settings
[assembly: PexAssemblySettings(TestFramework = "MSTestv2")]

// Microsoft.Pex.Framework.Instrumentation
[assembly: PexAssemblyUnderTest("HBD.Mef")]
[assembly: PexInstrumentAssembly("log4net")]
[assembly: PexInstrumentAssembly("System.Core")]
[assembly: PexInstrumentAssembly("Newtonsoft.Json")]
[assembly: PexInstrumentAssembly("HBD.Framework")]
[assembly: PexInstrumentAssembly("System.ComponentModel.Composition")]
[assembly: PexInstrumentAssembly("Prism")]
[assembly: PexInstrumentAssembly("Microsoft.Practices.ServiceLocation")]
[assembly: PexInstrumentAssembly("Prism.Wpf")]

// Microsoft.Pex.Framework.Creatable
[assembly: PexCreatableFactoryForDelegates]

// Microsoft.Pex.Framework.Validation
[assembly: PexAllowedContractRequiresFailureAtTypeUnderTestSurface]
[assembly: PexAllowedXmlDocumentedException]

// Microsoft.Pex.Framework.Coverage
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "log4net")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "System.Core")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "Newtonsoft.Json")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "HBD.Framework")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "System.ComponentModel.Composition")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "Prism")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "Microsoft.Practices.ServiceLocation")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "Prism.Wpf")]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(Environment))]
[assembly: PexUseType(typeof(EmptyLogger))]
[assembly: PexUseType(typeof(DebugLogger))]
[assembly: PexSuppressStaticFieldStore("Prism.Properties.Resources", "resourceMan")]
[assembly: PexInstrumentType(typeof(TimeZoneInfo))]
[assembly: PexInstrumentType(typeof(AppDomain))]
[assembly: PexInstrumentType(typeof(Path))]
[assembly: PexInstrumentType(typeof(TextInfo))]
[assembly: PexInstrumentType("mscorlib", "Microsoft.Win32.Win32Native")]
[assembly: PexInstrumentType(typeof(Math))]
[assembly: PexSuppressStaticFieldStore(typeof(Encoding), "defaultEncoding")]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(ConfigurationManager))]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(Attribute))]
[assembly: PexSuppressUninstrumentedMethodFromType("System.CultureAwareComparer")]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(EncodingProvider))]
[assembly: PexSuppressUninstrumentedMethodFromType("System.Text.InternalEncoderBestFitFallback")]
[assembly: PexSuppressUninstrumentedMethodFromType("System.Text.InternalDecoderBestFitFallback")]
[assembly: PexSuppressUninstrumentedMethodFromType("System.Runtime.Versioning.BinaryCompatibility")]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(TextWriter))]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(DateTimeFormatInfo))]
[assembly: PexSuppressUninstrumentedMethodFromType("System.DateTimeFormat")]
[assembly: PexSuppressUninstrumentedMethodFromType("System.IO.PathInternal")]
[assembly: PexSuppressUninstrumentedMethodFromType("System.AppContextSwitches")]
[assembly: PexSuppressUninstrumentedMethodFromType("System.IO.PathHelper")]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(OperatingSystem))]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(EncoderReplacementFallback))]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(DecoderReplacementFallback))]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(EncoderFallback))]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(DecoderFallback))]
[assembly: PexSuppressUninstrumentedMethodFromType("System.Text.UTF8Encoding+UTF8Encoder")]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(EncoderExceptionFallback))]
[assembly: PexSuppressUninstrumentedMethodFromType("System.Mda+StreamWriterBufferedDataLost")]
[assembly: PexInstrumentType(typeof(Directory))]
[assembly: PexInstrumentType(typeof(FileStream))]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(SafeHandleZeroOrMinusOneIsInvalid))]
[assembly: PexSuppressUninstrumentedMethodFromType("Microsoft.Win32.Win32Native")]
[assembly: PexSuppressStaticFieldStore("System.Diagnostics.TraceInternal", "autoFlush")]
[assembly: PexSuppressStaticFieldStore(typeof(StreamWriter), "_UTF8NoBOM")]
[assembly: PexSuppressStaticFieldStore("System.Diagnostics.TraceInternal", "indentLevel")]
[assembly: PexInstrumentType(typeof(DirectoryInfo))]
[assembly: PexInstrumentType(typeof(File))]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(SafeHandle))]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(FileSystemInfo))]
[assembly: PexInstrumentType(typeof(FileSystemInfo))]
[assembly: PexUseType(typeof(CompositionScopeDefinition))]
[assembly: PexUseType(typeof(Lazy<,>), "System.ComponentModel.Composition.Hosting.CatalogExportProvider+CatalogChangeProxy")]
[assembly: PexUseType(typeof(FilteredCatalog))]
[assembly: PexUseType(typeof(AggregateCatalog))]
[assembly: PexUseType(typeof(AssemblyCatalog))]
[assembly: PexUseType(typeof(TypeCatalog))]
[assembly: PexUseType(typeof(ApplicationCatalog))]
[assembly: PexUseType(typeof(DirectoryCatalog))]
[assembly: PexSuppressExplorableEvent("System.Collections.Generic.ObjectEqualityComparer`1")]
[assembly: PexSuppressExplorableEvent("System.Collections.Generic.GenericEqualityComparer`1")]
[assembly: PexSuppressStaticFieldStore(typeof(Uri), "s_ConfigInitializing")]
[assembly: PexSuppressStaticFieldStore(typeof(Uri), "s_initLock")]
[assembly: PexSuppressStaticFieldStore("System.SR", "loader")]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(AppDomainSetup))]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(GCHandle))]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(TextInfo))]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(RuntimeEnvironment))]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(XmlReaderSettings))]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(XmlReader))]
[assembly: PexSuppressUninstrumentedMethodFromType("System.Globalization.EncodingTable")]
[assembly: PexSuppressUninstrumentedMethodFromType("System.Globalization.CodePageDataItem")]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(Decoder))]
[assembly: PexSuppressUninstrumentedMethodFromType("System.Text.UTF8Encoding+UTF8Decoder")]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(Buffer))]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(EncoderFallbackBuffer))]
[assembly: PexInstrumentAssembly("System.Xml")]
[assembly: PexSuppressUninstrumentedMethodFromType("System.Xml.XmlTextReaderImpl")]
[assembly: PexInstrumentType(typeof(DecoderFallbackBuffer))]
[assembly: PexSuppressExplorableEvent(typeof(AggregateCatalog))]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(Marshal))]
[assembly: PexSuppressUninstrumentedMethodFromType(typeof(DecoderReplacementFallbackBuffer))]
[assembly: PexInstrumentType(typeof(Marshal))]
[assembly: PexUseType(typeof(ShellConfigManager))]

