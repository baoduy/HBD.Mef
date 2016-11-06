using HBD.Framework;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Policy;

namespace HBD.Mef.Core.Modularity
{
    public class DirectoryModuleCatalog : ModuleCatalog
    {
        /// <summary>
        ///     Directory containing modules to search for.
        /// </summary>
        public string ModulePath { get; set; }

        /// <summary>
        ///     Drives the main logic of building the child domain and searching for the assemblies.
        /// </summary>
        protected override void InnerLoad()
        {
            if (string.IsNullOrEmpty(ModulePath))
                throw new InvalidOperationException();
            if (!Directory.Exists(ModulePath))
                throw new InvalidOperationException(ModulePath);
            var domain = BuildChildDomain(AppDomain.CurrentDomain);
            try
            {
                var stringList = new List<string>();
                var collection = AppDomain.CurrentDomain.GetAssemblies().Cast<Assembly>().Where(assembly =>
                {
                    if (!(assembly is AssemblyBuilder) &&
                        (assembly.GetType().FullName != "System.Reflection.Emit.InternalAssemblyBuilder"))
                        return !string.IsNullOrEmpty(assembly.Location);
                    return false;
                }).Select(assembly => assembly.Location);
                stringList.AddRange(collection);

                var type = typeof(InnerModuleInfoLoader);

                var moduleInfoLoader =
                    // ReSharper disable once AssignNullToNotNullAttribute
                    (InnerModuleInfoLoader)domain.CreateInstanceFrom(type.Assembly.Location, type.FullName).Unwrap();
                InnerModuleInfoLoader.LoadAssemblies(stringList);
                Items.AddRange(InnerModuleInfoLoader.GetModuleInfos(ModulePath));
            }
            finally
            {
                AppDomain.Unload(domain);
            }
        }

        protected virtual AppDomain BuildChildDomain(AppDomain parentDomain)
        {
            if (parentDomain == null)
                throw new ArgumentNullException(nameof(parentDomain));
            return AppDomain.CreateDomain("DiscoveryRegion", new Evidence(parentDomain.Evidence),
                parentDomain.SetupInformation);
        }

        private class InnerModuleInfoLoader : MarshalByRefObject
        {
            internal static ModuleInfo[] GetModuleInfos(string path)
            {
                var directory = new DirectoryInfo(path);
                ResolveEventHandler resolveEventHandler = (sender, args) => OnReflectionOnlyResolve(args, directory);
                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += resolveEventHandler;
                var type =
                    AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies()
                        .First(asm => asm.FullName == typeof(IModule).Assembly.FullName)
                        .GetType(typeof(IModule).FullName);
                var array = GetNotAllreadyLoadedModuleInfos(directory, type).ToArray();
                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= resolveEventHandler;
                return array;
            }

            private static IEnumerable<ModuleInfo> GetNotAllreadyLoadedModuleInfos(DirectoryInfo directory,
                Type moduleType)
            {
                var source = new List<FileInfo>();
                var alreadyLoadedAssemblies = AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies();
                foreach (
                    var fileInfo in
                    directory.GetFiles("*.dll")
                        .Where(
                            file =>
                                alreadyLoadedAssemblies.FirstOrDefault(
                                    assembly =>
                                        string.Compare(Path.GetFileName(assembly.Location), file.Name,
                                            StringComparison.OrdinalIgnoreCase) == 0) == null))
                    try
                    {
                        Assembly.ReflectionOnlyLoadFrom(fileInfo.FullName);
                        source.Add(fileInfo);
                    }
                    catch (BadImageFormatException)
                    {
                    }
                return
                    source.SelectMany(
                        file =>
                            Assembly.ReflectionOnlyLoadFrom(file.FullName)
                                .GetExportedTypes()
                                .Where(moduleType.IsAssignableFrom)
                                .Where(t => t != moduleType)
                                .Where(t => !t.IsAbstract)
                                .Select(CreateModuleInfo));
            }

            private static Assembly OnReflectionOnlyResolve(ResolveEventArgs args, DirectoryInfo directory)
            {
                var assembly =
                    AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies()
                        .FirstOrDefault(
                            asm => string.Equals(asm.FullName, args.Name, StringComparison.OrdinalIgnoreCase));
                if (assembly != null)
                    return assembly;
                var assemblyName = new AssemblyName(args.Name);
                var str = Path.Combine(directory.FullName, assemblyName.Name + ".dll");
                if (File.Exists(str))
                    return Assembly.ReflectionOnlyLoadFrom(str);
                return Assembly.ReflectionOnlyLoad(args.Name);
            }

            internal static void LoadAssemblies(IEnumerable<string> assemblies)
            {
                foreach (var assembly in assemblies)
                    try
                    {
                        Assembly.ReflectionOnlyLoadFrom(assembly);
                    }
                    catch (FileNotFoundException)
                    {
                    }
            }

            private static ModuleInfo CreateModuleInfo(Type type)
            {
                var name1 = type.Name;
                var flag = false;
                var customAttributeData1 =
                    CustomAttributeData.GetCustomAttributes(type)
                        .FirstOrDefault(
                            // ReSharper disable once PossibleNullReferenceException
                            cad => cad.Constructor.DeclaringType.FullName == typeof(ModuleAttribute).FullName);
                if (customAttributeData1 != null)
                    // ReSharper disable once PossibleNullReferenceException
                    foreach (var namedArgument in customAttributeData1.NamedArguments)
                    {
                        var name2 = namedArgument.MemberInfo.Name;
                        if (name2 != "ModuleName")
                            if (name2 != "OnDemand")
                            {
                                if (name2 == "StartupLoaded")
                                    flag = !(bool)namedArgument.TypedValue.Value;
                            }
                            else
                                flag = (bool)namedArgument.TypedValue.Value;
                        else
                            name1 = (string)namedArgument.TypedValue.Value;
                    }
                var stringList = CustomAttributeData.GetCustomAttributes(type)
                    // ReSharper disable once PossibleNullReferenceException
                    .Where(cad => cad.Constructor.DeclaringType.FullName == typeof(ModuleDependencyAttribute).FullName)
                    .Select(customAttributeData2 => (string)customAttributeData2.ConstructorArguments[0].Value).ToList();

                var moduleInfo = new ModuleInfo(name1, type.AssemblyQualifiedName)
                {
                    InitializationMode = (InitializationMode)(flag ? 1 : 0),
                    Ref = type.Assembly.CodeBase
                };
                moduleInfo.DependsOn.AddRange(stringList);
                return moduleInfo;
            }
        }
    }
}