---
layout: subpage
title: WinUX.Application.AppLauncher
permalink: /ref/winux-application-applauncher
---

Provides a simple mechanism for launching external applications.

## Syntax

```csharp
public static class AppLauncher
```

## Methods

### LaunchAsync(Uri, string, bool)

Launches an application with the specified application ID using the specified uri URI.

#### Parameters

| Name | Type | Description |
|---|---|---|
| uri | Uri | The URI/query to pass to the launching application. |
| applicationPackageFamilyName | string | The package family name of the application to launch. |
| promptToLaunch | bool | A value indicating whether to show a prompt when launching the application. |

#### Example usage

```csharp
public async Task OpenBingMapsAsync(double lat, double lon)
{
    var uri = new Uri($"bingmaps:?cp={lat}~{lon}");
    await AppLauncher.LaunchAsync(uri, AppPackageFamilyNames.BingMaps, false);
}
```