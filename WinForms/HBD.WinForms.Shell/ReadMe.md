[![Build status](https://ci.appveyor.com/api/projects/status/6bfl1l110lg794mo)](https://ci.appveyor.com/project/baoduy/hbd-winforms-shell)
[![License](https://img.shields.io/github/license/mashape/apistatus.svg)](https://opensource.org/licenses/MIT)
[![NuGet](https://img.shields.io/nuget/v/HBD.WinForms.Shell.svg?maxAge=2592000)](https://www.nuget.org/packages/HBD.WinForms.Shell/)

##### Nuget Package:
>PM> *Install-Package HBD.WinForms.Shell*

# [HBD.WinForms.Shell](https://drunkcoding.net/the-workspace-for-windowforms)
I would like to share the Winforms Shell application, that provides the core foundation for the Winforms application.
With this foundation, it can be load the external modules from the pre-defined folders when it started. So that you can add-in and maintenance the modules easily.

# How it works?
This Shell application had been developed based on [HBD.Mef](https://github.com/baoduy/HBD.Mef) library to loading and executing external modules when it starts.
For more information about **HBD.Mef** you can found in [here](https://github.com/baoduy/HBD.Mef).

# Build-In Services
In this Shell there are a few services had been developed and exposed into Mef that helps you to interact with the Shell component.
1. The **IMainMenuService** will helps to add Menu Item into the Menu-bar of Main window.

![Menu Bar](https://raw.githubusercontent.com/baoduy/Images/master/Wordpress/HBD.WinForms.Shell/MenuBar.png)

2. The **IStatusBarService** will helps to set the status into the Status-Bar of Main window.

![Status Bar](https://raw.githubusercontent.com/baoduy/Images/master/Wordpress/HBD.WinForms.Shell/StatusBar.png)

3. The **IMainViewService** will help to interact with Tab Manager of the Main window.
That allow to add/remove UserControl into TabManager.

![Tab Manager](https://raw.githubusercontent.com/baoduy/Images/master/Wordpress/HBD.WinForms.Shell/TabManager.png)

4. The **NavigationManager** will provide the details in the below section.
# Quick Start - Develop a new module for HBD.WinForms.Shell

### 1. Add New Module
- Open Visual Studio and create a new Class Library project example project name is **Demo.Module**.
- Install the latest version of **HBD.WinForms.Shell** from Nuget.
- Add new class named **StartupDemoModule** and inherited from HBD.Mef.WinForms.Modularity.**WinFormModuleBase** and
then implement 2 abstract methods (**GetStartUpViewTypes** and **MenuConfiguration(IMainMenuService menuSet)**) as below.
Remember to add the **ModuleExport** attribute into your StartupDemoModule class.
```csharp
[HBD.Mef.Core.Modularity.ModuleExport(typeof(StartupDemoModule))]
[PartCreationPolicy(CreationPolicy.Shared)]
public class StartupDemoModule : HBD.Mef.WinForms.Modularity.WinFormModuleBase
{
        protected override IEnumerable<IViewInfo> GetStartUpViewTypes()
        {
            //Return the View1 as Startup View.
            //This view will be loaded automatically when application started.
            yield return new ViewInfo(typeof(View1));
        }

        protected override void MenuConfiguration(IMainMenuService menuSet)
        {
            //Add Main Menu for this module.
            menuSet.Menu("Demo")
                .WithIcon(Resources.DemoIcon)
                .WithToolTip("This is demo menu.")
                .Children
                    //Add Navigation for View1.
                    .AddNavigation("View 1")
                        .For(new ViewInfo(typeof(View1)))
                    //And this navigation for View 2.
                    .AndNavigation("View 2")
                        .For(new ViewInfo(typeof(View2)));
        }
}
```
- Add 2 new User Controls Named **View1** and **View2** as below.

![Demo Module Views](https://raw.githubusercontent.com/baoduy/Images/master/Wordpress/HBD.WinForms.Shell/DemoModule_Views.png)

- On each view overwrite the Text property and return the View Title and add the Export attribute as below.
```csharp
[Export]
public partial class View1: UserControl
{
    public override string Text => "View 1";

    ...
}

[Export]
public partial class View2: UserControl
{
    public override string Text => "View 2";

    ...
}
```
### 2. Setup Publish Shell folder.
- Copy folder **[Solution Directory]\packages\HBD.WinForms.Shell\tools\WinForms.Shell** to **[Solution Directory]\Publish\WinForms.Shell**
### 3. Update project's properties.
- Right click to the project select Properties
- Add below code into Post-build event
```batch
if not exist "$(SolutionDir)Publish\WinForms.Shell\Modules\$(ProjectName)" mkdir "$(SolutionDir)Publish\WinForms.Shell\Modules\$(ProjectName)" 
xcopy "$(TargetDir)*.*" "$(SolutionDir)Publish\WinForms.Shell\Modules\$(ProjectName)" /s /y /r
```
- Set Start external program under Debug to **Publish\WinForms.Shell\HBD.WinForms.Shell.exe** 
- And set Working directory to **Publish\WinForms.Shell** folder.

![Demo Module Debug](https://raw.githubusercontent.com/baoduy/Images/master/Wordpress/HBD.WinForms.Shell/DemoModule_Debug.png)

### 4. Add Module config and Debug your module.
- Add new **Module_Demo.json** file with the following content.
```json
//this file name convention is Module_[Your project Name]
{
  "Name": "Demo Module",
  "Description": "This is demo module for WinForms Shell",
  "Version": "1.0.0-beta1",
  "IsEnabled": true, //or false
  //The list of assemblies that you want to be exposed to the EMF.
  "AssemplyFiles": [ "Demo.Module.dll" ]
}
```
- Set Build action of this config file as below

![Demo Module Json](https://raw.githubusercontent.com/baoduy/Images/master/Wordpress/HBD.WinForms.Shell/DemoModule_Json.png)
- Run your module the Shell application will display as below.

![Demo Shell](https://raw.githubusercontent.com/baoduy/Images/master/Wordpress/HBD.WinForms.Shell/Demo_Shell.png)

# Navigation between the Views.
From version 1.0.1 a new interface had been added into *HBD.Mef.WinForms.Common* to provide the communication channel between the views. So that the view and navigate to the other view with the parameters.
#### 1. Implementation.
If your view wants to be navigated from the other view you need to implement the **INavigationAware** interface and handle **OnNavigatedTo(WinformNavigationContext navigationContext)** method, This method will be called when navigating from the other view.
- The interface
```csharp
public interface INavigationAware
{
    void OnNavigatedTo(WinformNavigationContext navigationContext);
}
```
- The view implementation
```csharp
[Export]
public partial class View2 : UserControl, INavigationAware
{
    public void OnNavigatedTo(WinformNavigationContext navigationContext)
    {
        this.label2.Text = navigationContext.NavigationParameters.First().Value.ToString();
    }
}
```
#### 2. Using NavigationManager
The sampble code below will handle the button event and navigate to the other view
```csharp
[Export]
public partial class View1 : UserControl
{
    [Import]
    public INavigationManager NavigationManager { protected get; set; }

    private void button1_Click(object sender, System.EventArgs e)
    {
        this.NavigationManager.NavigateTo(typeof(View2),
            new Dictionary<string, object> { ["Message"] = "Navigated from View 1" });
    }
}
```
![Navigation Manager](https://raw.githubusercontent.com/baoduy/Images/master/Wordpress/HBD.WinForms.Shell/NavigationManager.PNG)
When run the application and click the button. View2 will be open and display the message was passed from View1.

#### 3. Recommendation
There are 2 base classes had been provided in *HBD.Mef.WinForms* for your Forms and Views. So that when you create a new Form or View, it can be inherited from this form instead.

- The **FormBase**
```csharp
public class FormBase: Form
{
    [Import]
    public IServiceLocator ContainerService { protected get; set; }

    [Import]
    public ILoggerFacade Logger { protected get; set; }

    [Import]
    public IMessageBoxService MessageBoxService { protected get; set; }
}
```
- The **ViewBase**
```csharp
public class ViewBase : UserControl, INotifyPropertyChanged, IActiveAware,INavigationAware
{

    [Import(AllowDefault = true,AllowRecomposition = true)]
    public IServiceLocator ContainerService { protected get; set; }

    [Import(AllowDefault = true, AllowRecomposition = true)]
    public ILoggerFacade Logger { protected get; set; }

    [Import(AllowDefault = true, AllowRecomposition = true)]
    public IShellStatusService StatusService { protected get; set; }

    [Import(AllowDefault = true, AllowRecomposition = true)]
    public IMessageBoxService MessageBoxService { protected get; set; }

    [Import(AllowDefault = true, AllowRecomposition = true)]
    public INavigationManager NavigationManager { protected get; set; }

    #region IActiveAware
        ...
    #endregion

    #region INotifyPropertyChanged
        ...
    #endregion

    #region NavigationAware
        ...  
    #endregion
}
```
- The **Form** implementation
```csharp
public class MainForm : FormBase
{
    ...
}
```
- The **View** implementation
```csharp
[Export]
public partial class View1: ViewBase
{
    public override string Text => "View 1";
    ...   
}
```
# Source code
- [HBD.Windows.Shell](https://github.com/baoduy/HBD.WinForms.Shell) source code.
- [Demo.Module](https://github.com/baoduy/Demo.Module-For-WinForm.Shell) source code.

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