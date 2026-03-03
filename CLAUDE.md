# CLAUDE.md - UniFi.Net

## Project Overview

UniFi.Net is a .NET client library for Ubiquiti's UniFi APIs. It provides typed clients for three UniFi product APIs:

- **Network** (`UniFi.Net.Network`) - Local UniFi Controller / Network API
- **Site Manager** (`UniFi.Net.SiteManager`) - Cloud-based device management via `api.ui.com`
- **Access** (`UniFi.Net.Access`) - UniFi Access door/space control devices

API documentation: https://developer.ui.com

## Solution Structure

```
UniFi.Net.slnx                          # Modern VS solution format
src/
  UniFi.Net.Core/                       # Shared project (.shproj) - serialization utilities
  UniFi.Net.Network/                    # Network API client (targets net8.0;net9.0)
  UniFi.Net.SiteManager/               # Site Manager API client (targets net8.0)
  UniFi.Net.Access/                     # Access API client (targets net8.0)
tests/
  UniFi.Net.Network.Tests/             # xUnit tests (net8.0)
  UniFi.Net.TestHarness/               # Integration test console app (net8.0)
```

## Build & Test

```bash
dotnet build
dotnet test
dotnet pack
```

CI/CD is in `.github/workflows/`. Only the Network project has CI pipelines currently.

## Architecture & Patterns

### Client Pattern

Each API module follows the same structure:
- **Interface** (e.g. `INetworkClient`) - defines all public operations
- **Client class** (e.g. `NetworkClient`) - implements the interface
- **Two constructors**: one taking `IHttpClientFactory` (for DI), one taking `Uri host` + `string apiKey` (standalone use)
- **HttpClientConfigurator** - internal static class configuring `HttpClient` base address, auth headers, User-Agent
- **ServiceCollectionExtensions** - `AddUniFi*Client(Action<UniFiConfig>)` extension methods in the `Microsoft.Extensions.DependencyInjection` namespace
- **UniFiConfig** - record with `required string Host` and `required string ApiKey`

The Access module uses a `ClientBase` abstract class with multiple specialized clients (UserClient, SpaceClient, DeviceClient, etc.), each with its own interface.

### Authentication

- **Network & Site Manager**: `X-API-KEY` header
- **Access**: `Authorization: Bearer {apiKey}` header

### API Path Prefixes

- Network: `proxy/network/integration/v1/`
- Site Manager: `v1/` (stable) or `ea/` (early access)
- Access: `api/v1/developer/`

### Models

- Use **positional record types** with PascalCase properties
- Related records grouped in the same file (e.g. `Device.cs` contains `Device`, `DeviceUplink`, `DeviceFeatures`, etc.)
- Responses: `PagedResponse<T>` (Network), `DataResponse<T>` / `PagedResponse<T>` (Site Manager)

### Serialization

- **System.Text.Json** exclusively (no Newtonsoft.Json)
- Shared project `UniFi.Net.Core` provides `SnakeCaseNamingPolicy` and `SnakeCaseEnumConverter`
- `JsonContent.Create` for request bodies, `ReadFromJsonAsync<T>` for response deserialization

### Error Handling

Each module has its own exception hierarchy:
- `UniFiNetworkException` (with `StatusCode`, `StatusName`, `Timestamp`, `RequestPath`, `RequestId`)
- `UniFiSiteManagerException` (with `StatusCode`, `Code`, `TraceId`)
- Derived types: `NotFoundException`, `UnauthorizedException`
- Error responses are deserialized from JSON and mapped to exceptions via pattern matching on status code

### Filtering (Network API)

The Network API supports a filter expression system:
- `IFilter` interface with `ToString()` producing filter strings like `name.eq('value')`
- Concrete types: `EqualityFilter<T>`, `GreaterThanFilter<T>`, `LessThanFilter<T>`, `LikeFilter<T>`, `InFilter<T>`, `NullFilter`
- Logical operators: `AndFilter`, `OrFilter`

## Coding Conventions

Enforced via `.editorconfig`:
- **4-space indentation**, 2-space for XML/JSON/YAML
- **File-scoped namespaces** (`namespace X;`)
- **Primary constructors** preferred
- **PascalCase** for all public members, constants, and static fields
- **Interfaces** prefixed with `I`
- **Allman brace style** (braces on new lines)
- **Sort system directives first**
- **Nullable reference types** enabled everywhere
- **Implicit usings** enabled
- **Line endings**: CRLF
- All public types must have **XML documentation comments**
- `GenerateDocumentationFile` is enabled in all projects

### Method Naming

- Async methods use `Async` suffix in Site Manager and Access (e.g. `ListHostsAsync`)
- Network client methods do **not** use the `Async` suffix (e.g. `ListSites`, `GetDevice`)
- List methods: `List{Resource}[Async]`
- Single-item gets: `Get{Resource}[Async]`
- Action methods: verb form (e.g. `PowerCyclePort`, `RestartDevice`)
- All async methods accept `CancellationToken cancellationToken = default` as the last parameter

## Dependencies

- `Microsoft.Extensions.DependencyInjection`
- `Microsoft.Extensions.Http`
- `Microsoft.Extensions.Options`
- Network project uses framework-conditional package versions (8.x for net8.0, 9.x for net9.0)

## NuGet Packaging

All source projects are packable. Packages include:
- README.md and LICENSE
- Symbol packages (snupkg)
- Deterministic builds with embedded sources
