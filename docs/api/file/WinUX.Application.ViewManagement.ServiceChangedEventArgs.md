---
layout: subpage
title: ServiceChangedEventArgs
permalink: /ref/winux-application-viewmanagement-servicechangedeventargs
---

Provides event arguments for a changed service.

## NuGet

Exists within the [WinUX.Common](https://www.nuget.org/packages/WinUX.Common/) NuGet package.

## Syntax

```csharp
public sealed class ServiceChangedEventArgs<TKey, TService>
```

## Fields

| Name | Type | Description |
|---|---|---|
| Key | TKey | Gets the key relating to the changed service. |
| Service | TService | Gets the service that has changed. |