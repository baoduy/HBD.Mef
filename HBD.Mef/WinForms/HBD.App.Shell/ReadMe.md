[![Build status](https://ci.appveyor.com/api/projects/status/p3htgsp0emq38ixh)](https://ci.appveyor.com/project/baoduy/hbd-app-shell)
[![License](https://img.shields.io/github/license/mashape/apistatus.svg)](https://opensource.org/licenses/MIT)
[![NuGet](https://img.shields.io/nuget/v/HBD.App.Shell.svg?maxAge=2592000)](https://www.nuget.org/packages/HBD.App.Shell/)

##### Nuget Package:
>PM> *Install-Package HBD.App.Shell*

# [HBD.App.Shell](https://drunkcoding.net/the-workspace-for-console-application/)
I would like to share the Console Shell application that provides the core foundation for the console application.
It allows building a loosely coupled, module-able and maintainable application.

# How does it works?
This Shell application had been developed based on [HBD.Mef](https://github.com/baoduy/HBD.Mef) library that will automatic load all modules in the defined folder at run time.
This application is suitable for those who want to develop a batch job that will be triggered by the scheduler and execute the particular plugin based on the input parameters.

# Quick Start

## 1. Add New Module
- Open Visual Studio and create a new Class Library project example project name is **Job.Module**.
- Install the latest version of **HBD.App.Shell** from Nuget.
- Add a new class named **Job1** and inherited from HBD.Mef.ConsoleApp.**ConsoleModuleBase** and
then implement 2 abstract methods (**Initialize** and **Run(params string[] args)**) as below.
Remember to add the **ModuleExport** attribute into your Job1 class.
```csharp
[HBD.Mef.Core.Modularity.ModuleExport("Job1", typeof(Job1))]
[PartCreationPolicy(CreationPolicy.Shared)]
public class Job1 : HBD.Mef.ConsoleApp.ConsoleModuleBase
{
        public override void Initialize()
        {
        }

        public override void Run(params string[] args)
        {
            Console.WriteLine($"{this.GetType().Name} was ran with parameters '{string.Join(",", args)}'");
            Console.Read();

            this.Logger.Info(message);
        }
}
```

The function of this job is writing a message to the console and log file.

## 2. Setup Publish Shell folder.
- Copy folder **[Solution Directory]\packages\HBD.App.Shell\tools\App.Shell** to **[Solution Directory]\Publish\App.Shell**
## 3. Update project's properties.
- Right click to the project select Properties
- Add below code into Post-build event
```batch
if not exist "$(SolutionDir)Publish\App.Shell\Modules\$(ProjectName)" mkdir "$(SolutionDir)Publish\App.Shell\Modules\$(ProjectName)" 
xcopy "$(TargetDir)*.*" "$(SolutionDir)Publish\App.Shell\Modules\$(ProjectName)" /s /y /r
```
- Set Start external program under Debug to **Publish\App.Shell\HBD.App.Shell.exe** 
- And set Working directory to **Publish\App.Shell** folder.
- And then set the Commnand line arguments is **-job1 param1 param2 param3**.

![Job Module Debug](https://raw.githubusercontent.com/baoduy/Images/master/Wordpress/HBD.App.Shell/Debug_Setting.PNG)

3. Add Module config and Debug your module.
- Add new **Module_Job_Demo.json** file with the following content.
```json
//this file name convention is Module_[Your project Name]
{
  "Name": "Demo Job Module",
  "Description": "This is demo job module for App Shell",
  "Version": "1.0.0-beta1",
  "IsEnabled": true, //or false
  //The list of assemblies that you want to be exposed to the EMF.
  "AssemplyFiles": [ "Job.Module.dll" ]
}
```
- Set Build action of this config file as below

![Job Module Json](https://raw.githubusercontent.com/baoduy/Images/master/Wordpress/HBD.App.Shell/Module_Config_BuildAction.PNG)

- Run your module, the Shell application will display as below.

![App Shell Job1](https://raw.githubusercontent.com/baoduy/Images/master/Wordpress/HBD.App.Shell/Run_job1.PNG)

# How to run multi jobs with parameters
As the **Command line arguments** above you will see the first parameter is the JobName and then follow by the parameters for that job.
If you want to execute multi-job you can pass the **Command line arguments** following this format: **-JobName1 param1 param2 param3 -JobName2 param1 param2 param3**

This is a screenshot when executing multi jobs.
![App Shell Job1 Job2](https://raw.githubusercontent.com/baoduy/Images/master/Wordpress/HBD.App.Shell/Run_job1_job2.PNG)

><span style="color:red">*If the Environment is **Debug** the console window will keep open unstil you press a enter key.*</span>

# Source code
You can download the **Job.Module** source code here in [Github - Job.Module](https://github.com/baoduy/Job.Module-for-HBD.App.Shell)
And the source code of **HBD.App.Shell** in here [Github - HBD.App.Shell](https://github.com/baoduy/HBD.App.Shell)

>*Hope, this Shell will help you to develop a plugin-able application faster and easier.
>Your feedback and ideas are much appreciated.
>Please feel free to drop me an email with your opinion.*

# Nuget Dependences
- [CommonServiceLocator](https://www.nuget.org/packages/CommonServiceLocator/)
- [HBD.Framework](https://www.nuget.org/packages/HBD.Framework/)
- [HBD.Mef](https://www.nuget.org/packages/HBD.Mef/)
- [log4net](https://www.nuget.org/packages/log4net/)
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/)
- [Prism.Core](https://www.nuget.org/packages/Prism.Core/)
- [Prism.Wpf](https://www.nuget.org/packages/Prism.Wpf/)

*Thanks for your reading*
- *[drunkcoding@outlook.com](mailto:drunkcoding@outlook)*
- *[drunkcoding](https://drunkcoding.net)*
- *[github](https://github.com/baoduy)*
