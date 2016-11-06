# HBD.Mef
I would like to share the Mef library for Console and Winforms application.
This library allows creating a Bootstrapper for your allocation and load add-in library at runtime dynamically.

## What is Mef
Mef is short name of Managed Extensibility Framework had been introduced in the .NET Framework 4.0.

If you are working on Prism for WPF you will see the advance of the Mef
that allow building allow applications to isolate and manage extensions.
MEF's focus is on discoverability, extensibility, and portability.

## Why Mef
The Mef is a library for creating lightweight, extensible applications. 
**It allows application developers to discover and use extensions with no configuration required.**
It also lets extension developers easily encapsulate code and avoid fragile hard dependencies. MEF not only allows extensions to be reused within applications but across applications as well.
[(Click here to read more about Mef).](https://msdn.microsoft.com/en-us/library/dd460648(v=vs.110).aspx)

## What's supporting by HBD.Mef
### 1. Bootstrapper 
- The core bootstrapper is HBD.Core.**MefBootstrapper** contains needed API that allows creating Logger instance, create AggregateCatalog, Composition Container, and Initialize Application.
### 2. Logging
- The HBD.Mef.Core.Logging.**TextFileLogger** had been defined as default logger for this bootstrapper by using **log4net** library in Nuget.
- Default Log file location will be in the **StartUp\Logs\Log.log**. However, you can customize it by overwriting the **CreateLogger()** method and pass the new location as a constructor parameter.
```csharp
protected override ILoggerFacade CreateLogger() 
    => new TextFileLogger("New Log Location Here");
```
### 3. Console application
- The HBD.Mef.ConsoleApp.**MefConsoleAppBootstrapper** is a dedicated bootstrapper for **Console application**. The folder **ConsoleApp** maybe split into the separate project in future once it had so many features added.
### 4. Winforms application
- The HBD.Mef.WinForms.**MefWinFormBootstrapper** is a dedicated bootstrapper for **Window Forms application** as well. The folder **WinForms** maybe split into the separate project in future once it had so many features added.
### 5. Shell configuration
- The **ShellConfig** in HBD.Mef.Core.Configuration had been defined for shell application configuration purpose.
That allows setting the Title, Logo, Environment Name, Module and Backup folder location as well as the list of external dlls that need to be imported into the Mef when starting the application.
```csharp
public class ShellConfig
{
        //The nam of the application.
        string Name{get;set;}

        // The title of Main Form.
        string Title{get;set;}

        // The icon will be displayed on Main Forms icon.
        string Logo{get;set;}

        // The Name of Environment if you have multi environment. 
        //This one may be display on the Main Form title as format {Title} - {Environment}
        string Environment { get; set; }

        //The backup location of the Modules. 
        //The old module folder will be backed up in this folders.
        string BackupModulePath { get; set; }

        // The location of the Modules that will be imported when the app start.
        string ModulePath { get; set; }

        // The binaries that will be imported to Mef when the app start.
        IList<string> ImportedBinaries { get; set; }
}
```
### 6. Module Configuration.
- The **ModuleConfig** in HBD.Mef.Core.Configuration had been defined for Module configuration purpose.
That allows setting the name, description, AssemplyFiles and Status of the Module.
```csharp
public class ModuleConfig
{
        // The name of Module
        public string Name{get;set;}

        // The description of Module
        public string Description{get;set;}

        // The version of the module. This should not be null.
        public string Version {get;set;}

        // The costume Assemblies that will be loaded into Mef. 
        //If this list is empty whole folders
        // will be loaded.
        public IList<string> AssemplyFiles { get; }

        // The flag to Disable or Enable the Module.
        public bool IsEnabled{get;set;}

        // Allows this Module to be managed by using Module Manager.
        public bool AllowToManage{get;set;}

        [JsonIgnore]
        public string Directory { get; set; }

        [JsonIgnore]
        public string ConfigFile { get; set; }

        [JsonIgnore]
        public string InValidMessage { get; set; }

        [JsonIgnore]
        public bool IsValid { get; set; } = true;
}
 ```
### 7. The helper classes.
1. The HBD.Mef.Common.**JsonConfigHelper** is a helper class that helps to read json config file into an object and vise versa.
All loaded config objects will be cached into an internal ConcurrentDictionary for future usage.
So if you call RadConfig method twice for the same config file. The second call will return the object in the caching dictionary instead of read data from the file again.

2. The HBD.Mef.**ShellConfigManager** is a helper class that helps to manage the Shell and Modules configuration files.
- **ShellConfig** property: The Shell.json config file from the StartUp location of the application will be read into this property.
All changes of the ShellConfig object will be monitored and saved back to the config file once method *SaveChanges* had been called.
- **Modules** property: This is a collection of all config files of all modules that will be loaded and monitored.
All changes of the ModuleConfig will be saved back to the corresponding config files once method *SaveChanges* had been called.
- **SaveChanges** method: that will save all changes of the **ShellConfig** and **Modules** back to the corresponding config files.
- **UndoChanges** method: undo all changes of the **ShellConfig** and **Modules**.

## How to use HBE.Mef
### 1. Console Application.
1. Open visual studio and create new Console application project
2. Create a new class named Bootstrapper and make it inherited from **HBD.Mef.ConsoleApp.MefConsoleAppBootstrapper** as example below.
```csharp
internal class Bootstrapper : MefConsoleAppBootstrapper
{
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            Logger.Info("Add Bootstrapper Assembly");
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));

            Logger.Info("Import Shell Binaries");
            ShellConfigManager.ImportShellBinaries(AggregateCatalog);

            Logger.Info("Import Module Binaries");
            ShellConfigManager.ImportModuleBinaries(AggregateCatalog);
        }

        public override void Run(params string[] args)
        {
            try
            {
                base.Run(args);
                //Implement your app logic here.
            }
            catch (Exception ex)
            {
                Logger?.Exception(ex);
            }
        }
}
```
3. Update the Program.cs to create the above Bootstrapper and execute the Run(args) method as below code.
```csharp
internal class Program
{
        private static void Main(string[] args) => new Bootstrapper().Run(args);
}
```

**[You can download source code if HBD.Mef here](https://github.com/baoduy/HBD.Mef)**

>PS: *Your feedback and contribution are much appreciated.
>Please feel free to submit your opinions into github issues management or email to me*
>
>*Thanks and Regards
>[hoangbaoduy@gmail.com](mailto:hoangbaoduy@gmail.com)*