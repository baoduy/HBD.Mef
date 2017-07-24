[![License](https://img.shields.io/github/license/mashape/apistatus.svg)](https://opensource.org/licenses/MIT)
[![NuGet](https://img.shields.io/nuget/v/HBD.ServiceLocator.svg?maxAge=2592000)](https://www.nuget.org/packages/HBD.ServiceLocator/)

##### Nuget Package:
>PM> *Install-Package HBD.ServiceLocator*
>
# HBD.ServiceLocator
Similar to CommonServiceLocator in nuget. However, this implementation of IServiceProvider using Mef and widely supports for all .Net Framework from 4.5 to Standard 1.6 and 2.0.

With this implementation it provides the easy way to share the instance of Mef container to all the modules, libraries in separate projects regardless how the Mef container had been initialized.

If you want to use the custom Dependency Injection *(Example: Ninject, Unity or AutoFact)* you can create your own IServiceLocator for your container.

![HBD.ServiceLocator](https://raw.githubusercontent.com/baoduy/Images/master/Wordpress/HBD.ServiceLocator/HBD.ServiceLocator.png)

The is a various way to register the DI container.
- Register a **IServiceLocator**, an *ArgumentNullException* will be thrown if the new provider is Null.
```csharp
ServiceLocator.SetServiceLocator(IServiceLocator newProvider);
```

- Lazy load register. If the real instance of IServiceLocator is not available at the registration time the ServiceLocator allows you to register an Func of IServiceLocator, This function won't be executed until the Current property is accessing.
```csharp
ServiceLocator.SetServiceLocator(()=> newProvider);
```
- In this library, There is a **MefServiceLocator** had been provided allow to register a Mef container directly to the ServiceLocator. Your Mef container will be wrapped into MefServiceLocator automatically.
```csharp
ServiceLocator.SetServiceLocator(()=> yourMefContainer);
```

The flag **IsServiceLocatorSet** is allowed to indicate whether the provider had been set or not.
If the provider is lazy load method and the instance is not available at the accessing time, the *InvalidOperationException* will be thrown.
The lazy load method will be executed again and again until the not null instance of IServiceLocator or Mef Container is returned.

Using the ServiceLocator is simple access to the Static Current property.
```csharp
ServiceLocator.Current.GetInstance<YourCLass>();
```

>Please note that The null value will be return if there is no satisfy instance found.