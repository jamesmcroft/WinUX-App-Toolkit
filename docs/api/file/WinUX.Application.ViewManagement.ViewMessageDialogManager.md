---
layout: subpage
title: ViewMessageDialogManager
permalink: /ref/winux-application-viewmanagement-viewmessagedialogmanager
---

Provides a simple to use manager for a view's [MessageDialogManager](winux-messaging-dialogs-messagedialogmanager).

## NuGet

Exists within the [WinUX.UWP](https://www.nuget.org/packages/WinUX.UWP/) NuGet package.

## Inheritance
- [ViewServiceManager](winux-application-viewmanagement-viewservicemanager)
    - **ViewMessageDialogManager**

## Syntax

```csharp
public class ViewMessageDialogManager : ViewServiceManager<MessageDialogManager>
```

## Fields

| Name | Type | Description |
|---|---|---|
| Current | ViewServiceManager<MessageDialogManager> | Gets a static instance of the manager. |
| Services | IReadonlyDictionary<int, MessageDialogManager> | Gets the collection of registered services. |

## Events

| Name | Type | Description |
|---|---|---|
| ServiceChanged | ServiceChangedEventHandler<int, MessageDialogManager> | Invoked when a service has changed for a given view ID. |

## Static Methods

### MessageDialogManager GetForView()

Gets the MessageDialogManager instance for the current view if registered.

#### Example usage

```csharp
public async Task ShowHelloWorldDialogAsync()
{
    var messageDialogManager = ViewMessageDialogManager.GetForView();
    await messageDialogManager.ShowAsync("Hello, World!");
}
```

### MessageDialogManager RegisterOrUpdateForView(MessageDialogManager)

Registers a MessageDialogManager service for the current view and returns the registered service.

#### Parameters

| Name | Type | Description |
|---|---|---|
| service | MessageDialogManager | The service to register. |

#### Example usage

```csharp
public void Register()
{
    var dispatcher = Window.Current.Dispatcher;
    var messageDialogManager = new MessageDialogManager(dispatcher);
    ViewMessageDialogManager.RegisterOrUpdateForView(messageDialogManager);
}
```

## Methods

### MessageDialogManager RegisterOrUpdate(int, MessageDialogManager)

Registers or updates a MessageDialogManager service in the manager for the specified view ID and returns the registered service.

#### Parameters

| Name | Type | Description |
|---|---|---|
| key | int | The view ID as a key to register with. |
| service | MessageDialogManager | The service to register. |

#### Example usage

```csharp
public void Register()
{
    var dispatcher = Window.Current.Dispatcher;
    var messageDialogManager = new MessageDialogManager(dispatcher);
    var viewId = ApplicationView.GetForCurrentView().Id;

    ViewMessageDialogManager.Current.RegisterOrUpdate(viewId, messageDialogManager);
}
```

### MessageDialogManager Get(int)

Gets a registered service from the manager if exists.

#### Parameters

| Name | Type | Description |
|---|---|---|
| key | int | The view ID to retrieve a service for. |

#### Example usage

```csharp
public async Task ShowHelloWorldDialogAsync()
{
    var viewId = ApplicationView.GetForCurrentView().Id;

    var messageDialogManager = ViewMessageDialogManager.Current.Get(viewId);
    await messageDialogManager.ShowAsync("Hello, World!");
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
    var unregistered = ViewMessageDialogManager.Current.Unregister(viewId);
}
```