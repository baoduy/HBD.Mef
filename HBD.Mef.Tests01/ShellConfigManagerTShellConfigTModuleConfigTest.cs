using Prism.Logging;
using System.ComponentModel.Composition.Hosting;
// <copyright file="ShellConfigManagerTShellConfigTModuleConfigTest.cs" company="Duy Hoang">Copyright © Microsoft 2016</copyright>

using System;
using HBD.Mef.Shell.Configuration;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBD.Mef.Shell.Configuration.Tests
{
    /// <summary>This class contains parameterized unit tests for ShellConfigManager`2</summary>
    [TestClassAttribute]
    [PexClass(typeof(ShellConfigManager<, >))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ShellConfigManagerTShellConfigTModuleConfigTest
    {

        /// <summary>Test stub for ImportModuleBinaries(AggregateCatalog)</summary>
        [PexGenericArguments(typeof(ShellConfig), typeof(ModuleConfig))]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public void ImportModuleBinariesTest<TShellConfig, TModuleConfig>([PexAssumeNotNull]ShellConfigManager<TShellConfig, TModuleConfig> target, AggregateCatalog catalog)
            where TShellConfig : ShellConfig, new()
            where TModuleConfig : ModuleConfig, new()
        {
            target.ImportModuleBinaries(catalog);
            // TODO: add assertions to method ShellConfigManagerTShellConfigTModuleConfigTest.ImportModuleBinariesTest(ShellConfigManager`2<!!0,!!1>, AggregateCatalog)
        }

        /// <summary>Test stub for ImportShellBinaries(AggregateCatalog)</summary>
        [PexGenericArguments(typeof(ShellConfig), typeof(ModuleConfig))]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public void ImportShellBinariesTest<TShellConfig, TModuleConfig>([PexAssumeNotNull]ShellConfigManager<TShellConfig, TModuleConfig> target, AggregateCatalog catalog)
            where TShellConfig : ShellConfig, new()
            where TModuleConfig : ModuleConfig, new()
        {
            target.ImportShellBinaries(catalog);
            // TODO: add assertions to method ShellConfigManagerTShellConfigTModuleConfigTest.ImportShellBinariesTest(ShellConfigManager`2<!!0,!!1>, AggregateCatalog)
        }

        /// <summary>Test stub for SaveChanges(ILoggerFacade)</summary>
        [PexGenericArguments(typeof(ShellConfig), typeof(ModuleConfig))]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public void SaveChangesTest<TShellConfig, TModuleConfig>([PexAssumeNotNull]ShellConfigManager<TShellConfig, TModuleConfig> target, ILoggerFacade logger)
            where TShellConfig : ShellConfig, new()
            where TModuleConfig : ModuleConfig, new()
        {
            target.SaveChanges(logger);
            // TODO: add assertions to method ShellConfigManagerTShellConfigTModuleConfigTest.SaveChangesTest(ShellConfigManager`2<!!0,!!1>, ILoggerFacade)
        }

        /// <summary>Test stub for SaveChanges(!1)</summary>
        [PexGenericArguments(typeof(ShellConfig), typeof(ModuleConfig))]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public void SaveChangesTest01<TShellConfig, TModuleConfig>([PexAssumeNotNull]ShellConfigManager<TShellConfig, TModuleConfig> target, TModuleConfig module)
            where TShellConfig : ShellConfig, new()
            where TModuleConfig : ModuleConfig, new()
        {
            target.SaveChanges(module);
            // TODO: add assertions to method ShellConfigManagerTShellConfigTModuleConfigTest.SaveChangesTest01(ShellConfigManager`2<!!0,!!1>, !!1)
        }

        /// <summary>Test stub for UndoChanges()</summary>
        [PexGenericArguments(typeof(ShellConfig), typeof(ModuleConfig))]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public void UndoChangesTest<TShellConfig, TModuleConfig>([PexAssumeNotNull]ShellConfigManager<TShellConfig, TModuleConfig> target)
            where TShellConfig : ShellConfig, new()
            where TModuleConfig : ModuleConfig, new()
        {
            target.UndoChanges();
            // TODO: add assertions to method ShellConfigManagerTShellConfigTModuleConfigTest.UndoChangesTest(ShellConfigManager`2<!!0,!!1>)
        }

        /// <summary>Test stub for UndoChanges(!1)</summary>
        [PexGenericArguments(typeof(ShellConfig), typeof(ModuleConfig))]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public void UndoChangesTest01<TShellConfig, TModuleConfig>([PexAssumeNotNull]ShellConfigManager<TShellConfig, TModuleConfig> target, TModuleConfig module)
            where TShellConfig : ShellConfig, new()
            where TModuleConfig : ModuleConfig, new()
        {
            target.UndoChanges(module);
            // TODO: add assertions to method ShellConfigManagerTShellConfigTModuleConfigTest.UndoChangesTest01(ShellConfigManager`2<!!0,!!1>, !!1)
        }

        /// <summary>Test stub for get_Modules()</summary>
        [PexGenericArguments(typeof(ShellConfig), typeof(ModuleConfig))]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public ModuleConfigCollection<TModuleConfig> ModulesGetTest<TShellConfig, TModuleConfig>([PexAssumeNotNull]ShellConfigManager<TShellConfig, TModuleConfig> target)
            where TShellConfig : ShellConfig, new()
            where TModuleConfig : ModuleConfig, new()
        {
            ModuleConfigCollection<TModuleConfig> result = target.Modules;
            return result;
            // TODO: add assertions to method ShellConfigManagerTShellConfigTModuleConfigTest.ModulesGetTest(ShellConfigManager`2<!!0,!!1>)
        }

        /// <summary>Test stub for get_ShellConfig()</summary>
        [PexGenericArguments(typeof(ShellConfig), typeof(ModuleConfig))]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public TShellConfig ShellConfigGetTest<TShellConfig, TModuleConfig>([PexAssumeNotNull]ShellConfigManager<TShellConfig, TModuleConfig> target)
            where TShellConfig : ShellConfig, new()
            where TModuleConfig : ModuleConfig, new()
        {
            TShellConfig result = target.ShellConfig;
            return result;
            // TODO: add assertions to method ShellConfigManagerTShellConfigTModuleConfigTest.ShellConfigGetTest(ShellConfigManager`2<!!0,!!1>)
        }
    }
}
