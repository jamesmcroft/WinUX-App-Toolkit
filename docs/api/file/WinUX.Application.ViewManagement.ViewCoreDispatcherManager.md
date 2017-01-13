---
layout: subpage
title: ViewCoreDispatcherManager
permalink: /ref/winux-application-viewmanagement-viewcoredispatchermanager
---

Provides a simple to use manager for view CoreDispatchers.

## NuGet

Exists within the [WinUX.UWP](https://www.nuget.org/packages/WinUX.UWP/) NuGet package.

## Syntax

```csharp
public class ViewCoreDispatcherManager
```

## Fields

| Name | Type | Description |
|---|---|---|
| Current | ViewServiceManager<CoreDispatcher> | Gets a static instance of the manager. |
| Services | IReadonlyDictionary<int, CoreDispatcher> | Gets the collection of registered services. |

## Events

| Name | Type | Description |
|---|---|---|
| ServiceChanged | ServiceChangedEventHandler<int, CoreDispatcher> | Invoked when a service has changed for a given view ID. |

## Static Methods

### CoreDispatcher GetForView()

Gets the CoreDispatcher instance for the current view if registered.

#### Example usage

```csharp
public async Task UpdateAsync()
{
    var dispatcher = ViewCoreDispatcherManager.GetForView();
    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
        // Update logic
    });
}
```

### CoreDispatcher RegisterOrUpdateForView(CoreDispatcher)

Registers a CoreDispatcher service for the current view and returns the registered service.

#### Parameters

| Name | Type | Description |
|---|---|---|
| service | CoreDispatcher | The service to register. |

#### Example usage

```csharp
public void Register()
{
    var dispatcher = Window.Current.Dispatcher;
    ViewCoreDispatcherManager.RegisterOrUpdateForView(dispatcher);
}
```

## Methods

### CoreDispatcher RegisterOrUpdate(int, CoreDispatcher)

Registers or updates a CoreDispatcher service in the manager for the specified view ID and returns the registered service.

#### Parameters

| Name | Type | Description |
|---|---|---|
| key | int | The view ID as a key to register with. |
| service | CoreDispatcher | The service to register. |

#### Example usage

```csharp
public void Register()
{
    var dispatcher = Window.Current.Dispatcher;
    var viewId = ApplicationView.GetForCurrentView().Id;

    ViewCoreDispatcherManager.Current.RegisterOrUpdate(viewId, dispatcher);
}
```

### CoreDispatcher Get(int)

Gets a registered service from the manager if exists.

#### Parameters

| Name | Type | Description |
|---|---|---|
| key | int | The view ID to retrieve a service for. |

#### Example usage

```csharp
public async Task UpdateAsync()
{
    var viewId = ApplicationView.GetForCurrentView().Id;

    var dispatcher = ViewCoreDispatcherManager.Current.Get(viewId);
    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
        // Update logic
    });
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
    var unregistered = ViewCoreDispatcherManager.Current.Unregister(viewId);
}
```