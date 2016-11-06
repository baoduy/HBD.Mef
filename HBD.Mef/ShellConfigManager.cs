﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using HBD.Framework;
using HBD.Framework.Core;
using HBD.Mef.Common;
using HBD.Mef.Core.Configuration;
using HBD.Mef.Core.Logging;
using Newtonsoft.Json;
using Prism.Logging;

namespace HBD.Mef
{
    /// <summary>
    ///     The configuration manager for the shell plugin-able application (Console, Winforms and WPF
    ///     application using Mef version controller)
    /// </summary>
    public static class ShellConfigManager
    {
        /// <summary>
        ///     Default Shell config file name.
        /// </summary>
        public const string ShellConfigFile = "Shell.json";

        private static ChangeTrackingEntry<ShellConfig> _shellConfigEntry;

        private static ModuleConfigCollection _modules;

        /// <summary>
        ///     The Shell.json config file must be in the Startup location of the application.
        /// </summary>
        public static ShellConfig ShellConfig
        {
            get
            {
                if (_shellConfigEntry != null) return _shellConfigEntry.Entity;
                _shellConfigEntry =
                    new ChangeTrackingEntry<ShellConfig>(JsonConfigHelper.ReadConfig<ShellConfig>(ShellConfigFile));
                return _shellConfigEntry.Entity;
            }
        }

        public static ModuleConfigCollection Modules
        {
            get { return Funcs.GetOrLoad(ref _modules, () => LoadModuleFrom(ShellConfig.ModulePath)); }
        }

        /// <summary>
        ///     Load module configuration from Location.
        /// </summary>
        /// <param name="rootDirectory"></param>
        /// <returns></returns>
        private static ModuleConfigCollection LoadModuleFrom(string rootDirectory)
        {
            var modules = new List<ModuleConfig>();
            var modulePath = Path.GetFullPath(rootDirectory);

            if (!Directory.Exists(modulePath))
                throw new DirectoryNotFoundException(modulePath);

            foreach (var directory in Directory.GetDirectories(modulePath))
            {
                var configFile = Directory.GetFiles(directory, "Module*.json").FirstOrDefault();
                if (configFile.IsNullOrEmpty())
                {
                    modules.Add(new ModuleConfig
                    {
                        InValidMessage = "Module config file is not found.",
                        //assume that the module folder still valid event there is no config file.
                        IsValid = true,
                        Directory = directory,
                        //Assume that config file name is "Module_[FolderName].json"
                        ConfigFile = "Module_" + Path.GetDirectoryName(directory) + ".json"
                    });
                    continue;
                }

                // ReSharper disable once AssignNullToNotNullAttribute
                var module = JsonConvert.DeserializeObject<ModuleConfig>(File.ReadAllText(configFile));
                module.Directory = directory;
                module.ConfigFile = Path.GetFileName(configFile);
                modules.Add(module);
            }

            return new ModuleConfigCollection(modules);
        }

        /// <summary>
        ///     Import the binaries that had been configured in the ShellConfig.ImportedBinaries.
        /// </summary>
        /// <param name="catalog"></param>
        public static void ImportShellBinaries(AggregateCatalog catalog)
        {
            Guard.ArgumentIsNotNull(catalog, nameof(catalog));
            //Import additional binaries
            if (ShellConfig.ImportedBinaries == null) return;

            foreach (var s in ShellConfig.ImportedBinaries)
            {
                var file = Path.GetFullPath(File.Exists(s) ? s : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, s));

                if (!File.Exists(file))
                    throw new FileNotFoundException(file);

                var ac = Assembly.LoadFile(file);
                catalog.Catalogs.Add(new AssemblyCatalog(ac));
            }
        }

        /// <summary>
        ///     The modules configuration will be loaded based on the config in the Shell.ModulePath and
        ///     import all the require binaries to the Mef.
        /// </summary>
        /// <param name="catalog"></param>
        public static void ImportModuleBinaries(AggregateCatalog catalog)
        {
            Guard.ArgumentIsNotNull(catalog, nameof(catalog));

            //Load all Binaries from Module folder into Mef if the AssemplyFiles is NULL or Empty.
            foreach (var item in Modules.Where(m => m.IsEnabled && m.IsValid && (m.AssemplyFiles.Count <= 0)))
                catalog.Catalogs.Add(new DirectoryCatalog(item.Directory));

            //Load the Binaries from Module into Mef that had been specified at the AssemplyFiles property.
            foreach (var item in Modules.Where(m => m.IsEnabled && m.IsValid && (m.AssemplyFiles.Count > 0)))
            {
                var notFound = item.AssemplyFiles.FirstOrDefault(f => !File.Exists(item.Directory + "\\" + f));
                if (notFound.IsNotNull())
                {
                    item.InValidMessage = $"{notFound} is not found.";
                    item.IsValid = false;
                    continue;
                }

                var assemblies = from f in item.AssemplyFiles
                    let a = Assembly.LoadFrom(item.Directory + "\\" + f)
                    select new AssemblyCatalog(a);

                catalog.Catalogs.AddRange(assemblies);
            }
        }

        public static void SaveChanges(ILoggerFacade logger = null)
        {
            if (_shellConfigEntry?.IsChanged == true)
            {
                JsonConfigHelper.SaveConfig(_shellConfigEntry.Entity, ShellConfigFile);
                _shellConfigEntry.AcceptChanges();
            }

            foreach (var item in Modules.ChangedItems)
                if (!item.AllowToManage)
                {
                    logger?.Warn(
                        $"Undo the changes of Module {item.Name} because it is not Allow To Manage (AllowToManage = false).");
                    Modules.UndoChanges(item);
                }
                else
                {
                    logger?.Info($"Save the changes for Module {item.Name}");
                    JsonConfigHelper.SaveConfig(item, Path.Combine(item.Directory, item.ConfigFile));
                }
            Modules.AcceptChanges();
        }

        public static void UndoChanges()
        {
            if (_shellConfigEntry?.IsChanged == true)
                _shellConfigEntry.UndoChanges();

            Modules?.UndoChanges();
        }
    }
}