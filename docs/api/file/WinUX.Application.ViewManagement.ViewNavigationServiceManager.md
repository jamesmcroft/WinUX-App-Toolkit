---
layout: subpage
title: ViewNavigationServiceManager
permalink: /ref/winux-application-viewmanagement-viewnavigationservicemanager
---

Provides a simple to use manager for view ViewNavigationServiceManagers.

## NuGet

Exists within the [WinUX.UWP](https://www.nuget.org/packages/WinUX.UWP/) NuGet package.

## Syntax

```csharp
public class ViewNavigationServiceManager
```

## Fields

| Name | Type | Description |
|---|---|---|
| Current | ViewServiceManager<INavigationService> | Gets a static instance of the manager. |
| Services | IReadonlyDictionary<int, INavigationService> | Gets the collection of registered services. |

## Events

| Name | Type | Description |
|---|---|---|
| ServiceChanged | ServiceChangedEventHandler<int, INavigationService> | Invoked when a service has changed for a given view ID. |

## Static Methods

### INavigationService GetForView()

Gets the INavigationService instance for the current view if registered.

#### Example usage

```csharp
public void NavigateToMainView()
{
    var navigationService = ViewNavigationServiceManager.GetForView();
    navigationService.Navigate(typeof(MainView), "Parameter", new CommonNavigationTransitionInfo());
}
```

### INavigationService RegisterOrUpdateForView(INavigationService)

Registers a INavigationService service for the current view and returns the registered service.

#### Parameters

| Name | Type | Description |
|---|---|---|
| service | INavigationService | The service to register. |

#### Example usage

```csharp
public void Register()
{
    var viewFrame = Window.Current.Content as Frame;
    ViewNavigationServiceManager.RegisterOrUpdateForView(new NavigationService(viewFrame));
}
```

## Methods

### INavigationService RegisterOrUpdate(int, INavigationService)

Registers or updates a INavigationService service in the manager for the specified view ID and returns the registered service.

#### Parameters

| Name | Type | Description |
|---|---|---|
| key | int | The view ID as a key to register with. |
| service | INavigationService | The service to register. |

#### Example usage

```csharp
public void Register()
{
    var viewFrame = Window.Current.Content as Frame;
    var viewId = ApplicationView.GetForCurrentView().Id;

    ViewNavigationServiceManager.Current.RegisterOrUpdate(viewId, new NavigationService(viewFrame));
}
```

### INavigationService Get(int)

Gets a registered service from the manager if exists.

#### Parameters

| Name | Type | Description |
|---|---|---|
| key | int | The view ID to retrieve a service for. |

#### Example usage

```csharp
public void NavigateToMainView()
{
    var viewId = ApplicationView.GetForCurrentView().Id;

    var navigationService = ViewNavigationServiceManager.Current.Get(viewId);
    navigationService.Navigate(typeof(MainView), "Parameter", new CommonNavigationTransitionInfo());
}
```

### bool Unregister(int)

Unregisters a service from the manager.

#### Parameters

| Name | Type | Description |
|---|---|---|
| key | int | The view ID to unregister a service for. |

#### Example usage

```csharp
public void Cleanup()
{
    var viewId = ApplicationView.GetForCurrentView().Id;
    var unregistered = ViewNavigationServiceManager.Current.Unregister(viewId);
}
```