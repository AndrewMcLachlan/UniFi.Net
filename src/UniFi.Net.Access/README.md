# UniFi.Net.Access
[![NuGet](https://img.shields.io/nuget/v/UniFi.Net.Access.svg?style=flat-square)](https://www.nuget.org/packages/UniFi.Net.Access/)
[![GitHub](https://img.shields.io/github/license/AndrewMcLachlan/UniFi.Net.svg?style=flat-square)](https://github.com/AndrewMcLachlan/UniFi.Net)

An unofficial .NET client for the UniFi Network Controller API, designed to work with UniFi OS and the UniFi Network application.

This library provides a simple and intuitive way to interact with the UniFi Network Controller, allowing you to manage devices, networks, clients, and more.

## Getting Started

To get started with UniFi.NET.Acess, you can install the package via NuGet:
```bash
dotnet add package UniFi.Net.Access
```

## Usage

Here's a basic example of how to use UniFi.NET to connect to your UniFi Network Controller and retrieve a list of devices:

```csharp
using UniFi.Client;
using UniFi.Client.Models;

var client = new UniFiAccessClient("https://your-unifi-controller:8443", "apikey");
```

Using Dependency Injection (DI) is also supported:
```csharp
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddUniFiAccessClient(options => 
{
    options.Host = "https://your-unifi-controller:8443";
    options.ApiKey = "apiKey";
});
```