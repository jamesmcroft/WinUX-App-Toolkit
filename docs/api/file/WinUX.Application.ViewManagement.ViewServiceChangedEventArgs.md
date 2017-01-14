---
layout: subpage
title: ViewServiceChangedEventArgs
permalink: /ref/winux-application-viewmanagement-viewservicechangedeventargs
---

Provides event arguments for a changed view service.

## NuGet

Exists within the [WinUX.UWP](https://www.nuget.org/packages/WinUX.UWP/) NuGet package.

## Inheritance
- EventArgs
    - **ViewServiceChangedEventArgs**

## Syntax

```csharp
public sealed class ViewServiceChangedEventArgs<TService> : EventArgs
```

## Fields

| Name | Type | Description |
|---|---|---|
| Key | int | Gets the key relating to the changed service. |
| Service | TService | Gets the service that has changed. |