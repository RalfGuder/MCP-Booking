# Design: US-001 MCP-Server — Phase 1

**Datum:** 2026-04-10
**Scope:** Phase 1 — Kerninfrastruktur + Tool `list_resources`
**Status:** Entwurf

## Zusammenfassung

MCP-Server in C# (.NET 10.0), der die WP Booking Calendar REST API des Kulturvereins Milower Land e.V. als MCP-Tools für KI-Assistenten bereitstellt. Phase 1 implementiert die Kerninfrastruktur (MCP-Protokoll, Authentifizierung, Konfiguration, Fehlerbehandlung) und ein erstes Tool (`list_resources`).

## Technologie-Entscheidungen

| Thema | Entscheidung |
|-------|-------------|
| MCP-Implementierung | Offizielles MCP C# SDK (`ModelContextProtocol` NuGet) |
| Target-Framework | `net10.0` |
| Authentifizierung | WordPress Application Passwords (Basic Auth) |
| HTTP-Client | Typed HttpClient via `IHttpClientFactory` |
| Mocking-Framework | Moq |
| Assertion-Framework | Shouldly |
| Test-Framework | xUnit |
| Architektur | Clean Architecture (4 Schichten) |

## Projektstruktur

```
MCP-Booking/
├── MCP-Booking.sln
├── Directory.Build.props              # net10.0, Nullable, ImplicitUsings
├── src/
│   ├── McpBooking.Domain/             # Entities, Interfaces — keine NuGet-Pakete
│   ├── McpBooking.Application/        # Use Cases, DTOs — referenziert Domain
│   ├── McpBooking.Infrastructure/     # HTTP-Client, Repositories
│   │   └── NuGet: Microsoft.Extensions.Http
│   └── McpBooking.Server/            # MCP-Einstiegspunkt
│       └── NuGet: ModelContextProtocol, Microsoft.Extensions.Hosting
├── tests/
│   ├── McpBooking.Domain.Tests/
│   ├── McpBooking.Application.Tests/
│   ├── McpBooking.Infrastructure.Tests/
│   └── McpBooking.Server.Tests/
│   (alle: xunit, xunit.runner.visualstudio, Microsoft.NET.Test.Sdk, Shouldly, Moq)
└── docs/
```

**Projektreferenzen:**
- `Application` → `Domain`
- `Infrastructure` → `Domain`, `Application`
- `Server` → `Domain`, `Application`, `Infrastructure`
- Jedes Test-Projekt → das zu testende Projekt

## Architektur

```
+----------------------------------------------+
|          McpBooking.Server                   |
|   Program.cs, Tools/, Fehlerbehandlung       |
|   NuGet: ModelContextProtocol                |
+----------------------------------------------+
|          McpBooking.Infrastructure           |
|   BookingApiClient, ResourceRepository       |
|   BookingApiOptions, DependencyInjection     |
+----------------------------------------------+
|          McpBooking.Application              |
|   ListResourcesUseCase, ResourceDto          |
+----------------------------------------------+
|          McpBooking.Domain                   |
|   Resource (Entity), IResourceRepository     |
+----------------------------------------------+
```

Dependency Rule: Abhängigkeiten zeigen immer nach innen. Infrastructure implementiert Domain-Interfaces (Dependency Inversion). DI verbindet die Schichten zur Laufzeit.

## Domain-Schicht

### Entity: `Resource`

```csharp
namespace McpBooking.Domain.Entities;

public class Resource
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Cost { get; set; }
    public int? Visitors { get; set; }
}
```

### Interface: `IResourceRepository`

```csharp
namespace McpBooking.Domain.Interfaces;

public interface IResourceRepository
{
    Task<IReadOnlyList<Resource>> ListAsync(int page = 1, int perPage = 20, CancellationToken ct = default);
}
```

Für Phase 1 existiert nur `Resource` und `IResourceRepository.ListAsync`. Weitere Entities und Methoden kommen in Phase 2.

## Application-Schicht

### DTO: `ResourceDto`

```csharp
namespace McpBooking.Application.DTOs;

public record ResourceDto(int Id, string Title, string? Cost, int? Visitors);
```

### Use Case: `ListResourcesUseCase`

```csharp
namespace McpBooking.Application.UseCases;

public class ListResourcesUseCase
{
    private readonly IResourceRepository _repository;

    public ListResourcesUseCase(IResourceRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ResourceDto>> ExecuteAsync(
        int page = 1, int perPage = 20, CancellationToken ct = default)
    {
        var resources = await _repository.ListAsync(page, perPage, ct);
        return resources.Select(r => new ResourceDto(r.Id, r.Title, r.Cost, r.Visitors)).ToList();
    }
}
```

- Records für DTOs (immutable, Wertesemantik)
- Mapping Entity → DTO im Use Case
- `CancellationToken` wird durchgereicht

## Infrastructure-Schicht

### Konfiguration: `BookingApiOptions`

```csharp
namespace McpBooking.Infrastructure.Configuration;

public class BookingApiOptions
{
    public string BaseUrl { get; set; } = "https://kv-milowerland.de/wp-json/wpbc/v1";
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
```

### Typed HttpClient: `BookingApiClient`

```csharp
namespace McpBooking.Infrastructure.Http;

public class BookingApiClient
{
    private readonly HttpClient _httpClient;

    public BookingApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T?> GetAsync<T>(string path, CancellationToken ct = default)
    {
        var response = await _httpClient.GetAsync(path, ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>(ct);
    }
}
```

### Repository: `ResourceRepository`

```csharp
namespace McpBooking.Infrastructure.Repositories;

public class ResourceRepository : IResourceRepository
{
    private readonly BookingApiClient _client;

    public ResourceRepository(BookingApiClient client)
    {
        _client = client;
    }

    public async Task<IReadOnlyList<Resource>> ListAsync(
        int page = 1, int perPage = 20, CancellationToken ct = default)
    {
        var resources = await _client.GetAsync<List<Resource>>(
            $"/resources?page={page}&per_page={perPage}", ct);
        return resources ?? [];
    }
}
```

### DI-Registration: `DependencyInjection`

```csharp
namespace McpBooking.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, BookingApiOptions options)
    {
        services.AddSingleton(options);
        services.AddHttpClient<BookingApiClient>(client =>
        {
            client.BaseAddress = new Uri(options.BaseUrl);
            var credentials = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{options.Username}:{options.Password}"));
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", credentials);
        });
        services.AddScoped<IResourceRepository, ResourceRepository>();
        return services;
    }
}
```

- `IHttpClientFactory` verwaltet Lifecycle und Connection-Pooling
- Basic Auth Header einmalig beim Client-Setup konfiguriert
- `EnsureSuccessStatusCode()` wirft `HttpRequestException` bei 4xx/5xx
- Extension Method `AddInfrastructure()` kapselt die DI-Registrierung

## Presentation-Schicht (MCP-Server)

### Einstiegspunkt: `Program.cs`

```csharp
using McpBooking.Application.UseCases;
using McpBooking.Infrastructure;
using McpBooking.Infrastructure.Configuration;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

var options = new BookingApiOptions
{
    BaseUrl = Environment.GetEnvironmentVariable("WPBC_API_URL")
              ?? "https://kv-milowerland.de/wp-json/wpbc/v1",
    Username = Environment.GetEnvironmentVariable("WPBC_USERNAME")
              ?? throw new InvalidOperationException("WPBC_USERNAME ist nicht gesetzt."),
    Password = Environment.GetEnvironmentVariable("WPBC_PASSWORD")
              ?? throw new InvalidOperationException("WPBC_PASSWORD ist nicht gesetzt.")
};

builder.Services.AddInfrastructure(options);
builder.Services.AddScoped<ListResourcesUseCase>();

builder.Services.AddMcpServer(mcp =>
{
    mcp.WithStdioTransport();
    mcp.WithToolsFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();
await app.RunAsync();
```

### MCP-Tool: `ListResourcesTool`

```csharp
namespace McpBooking.Server.Tools;

[McpServerToolType]
public class ListResourcesTool
{
    private readonly ListResourcesUseCase _useCase;

    public ListResourcesTool(ListResourcesUseCase useCase)
    {
        _useCase = useCase;
    }

    [McpServerTool("list_resources"), Description("Listet alle buchbaren Ressourcen auf.")]
    public async Task<string> ExecuteAsync(
        [Description("Seite (Standard: 1)")] int page = 1,
        [Description("Einträge pro Seite (Standard: 20, Max: 100)")] int perPage = 20,
        CancellationToken ct = default)
    {
        if (page < 1) return "Fehler: page muss >= 1 sein.";
        if (perPage < 1 || perPage > 100) return "Fehler: per_page muss zwischen 1 und 100 liegen.";

        try
        {
            var resources = await _useCase.ExecuteAsync(page, perPage, ct);
            return JsonSerializer.Serialize(resources, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            });
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            return "Fehler: Authentifizierung fehlgeschlagen. Bitte API-Zugangsdaten prüfen.";
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Forbidden)
        {
            return "Fehler: Keine Berechtigung für diese Aktion.";
        }
        catch (HttpRequestException)
        {
            return "Fehler: Die Booking API ist nicht erreichbar.";
        }
    }
}
```

- Konfiguration über Umgebungsvariablen: `WPBC_API_URL`, `WPBC_USERNAME`, `WPBC_PASSWORD`
- Fehlende Pflicht-Credentials → sofortige Exception beim Start (Fail Fast)
- `[McpServerToolType]` und `[McpServerTool]` Attribute für automatische Tool-Erkennung
- Parametervalidierung (REQ-006) direkt im Tool vor Use-Case-Aufruf
- Fehlerbehandlung (REQ-005) übersetzt HTTP-Exceptions in lesbare Fehlermeldungen
- JSON-Ausgabe mit Snake-Case-Naming

## Fehlerbehandlung (REQ-005)

| HTTP-Status | Fehlermeldung |
|-------------|---------------|
| 401 Unauthorized | „Authentifizierung fehlgeschlagen. Bitte API-Zugangsdaten prüfen." |
| 403 Forbidden | „Keine Berechtigung für diese Aktion." |
| 404 Not Found | „[Entität] mit ID [id] nicht gefunden." |
| 5xx | „Serverfehler bei der Booking API." |
| Netzwerkfehler | „Die Booking API ist nicht erreichbar." |
| Validierungsfehler | „Fehler: [Parameter] muss [Bedingung] sein." |

Fehler werden als String-Rückgabe im Tool geliefert (kein Exception-Wurf an den MCP-Host).

## Konfiguration (REQ-004)

| Umgebungsvariable | Pflicht | Standardwert |
|-------------------|---------|-------------|
| `WPBC_API_URL` | nein | `https://kv-milowerland.de/wp-json/wpbc/v1` |
| `WPBC_USERNAME` | ja | — (Fail Fast bei fehlend) |
| `WPBC_PASSWORD` | ja | — (Fail Fast bei fehlend) |

## Test-Strategie

| Projekt | Testet | Mocking | Tests (Phase 1) |
|---------|--------|---------|-----------------|
| Domain.Tests | — | — | 0 (keine Logik) |
| Application.Tests | Use Case Mapping, Delegation | Moq (IResourceRepository) | ~3 |
| Infrastructure.Tests | HTTP-Aufrufe, Deserialisierung | MockHttpMessageHandler | ~3 |
| Server.Tests | Parametervalidierung, Fehlerbehandlung, JSON-Output | Moq (ListResourcesUseCase) | ~4 |

### Application.Tests — Beispieltests

```csharp
[Fact]
public async Task ExecuteAsync_ReturnsResources_MappedToDtos()
{
    var mock = new Mock<IResourceRepository>();
    mock.Setup(r => r.ListAsync(1, 20, It.IsAny<CancellationToken>()))
        .ReturnsAsync(new List<Resource>
        {
            new() { Id = 1, Title = "Gemeindesaal", Cost = "50", Visitors = 30 }
        });
    var useCase = new ListResourcesUseCase(mock.Object);

    var result = await useCase.ExecuteAsync();

    result.Count.ShouldBe(1);
    result[0].Title.ShouldBe("Gemeindesaal");
    result[0].Id.ShouldBe(1);
}

[Fact]
public async Task ExecuteAsync_EmptyList_ReturnsEmptyCollection()
{
    var mock = new Mock<IResourceRepository>();
    mock.Setup(r => r.ListAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(new List<Resource>());
    var useCase = new ListResourcesUseCase(mock.Object);

    var result = await useCase.ExecuteAsync();

    result.ShouldBeEmpty();
}
```

### Server.Tests — Beispieltests

```csharp
[Fact]
public async Task ExecuteAsync_InvalidPage_ReturnsError()
{
    var tool = new ListResourcesTool(Mock.Of<ListResourcesUseCase>());

    var result = await tool.ExecuteAsync(page: 0);

    result.ShouldContain("page muss >= 1 sein");
}

[Fact]
public async Task ExecuteAsync_AuthError_ReturnsReadableMessage()
{
    var mock = new Mock<ListResourcesUseCase>(Mock.Of<IResourceRepository>());
    mock.Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
        .ThrowsAsync(new HttpRequestException("Unauthorized", null, HttpStatusCode.Unauthorized));
    var tool = new ListResourcesTool(mock.Object);

    var result = await tool.ExecuteAsync();

    result.ShouldContain("Authentifizierung fehlgeschlagen");
}
```

## Abgedeckte Requirements

| Requirement | Abgedeckt durch |
|-------------|----------------|
| REQ-001 MCP-Protokoll | McpBooking.Server + MCP SDK |
| REQ-002 stdio-Transport | `mcp.WithStdioTransport()` in Program.cs |
| REQ-003 Authentifizierung | `BookingApiOptions` + Basic Auth Header in DI |
| REQ-004 Konfiguration | Umgebungsvariablen in Program.cs |
| REQ-005 Fehlerbehandlung | catch-Blöcke in ListResourcesTool |
| REQ-006 Parametervalidierung | Validierung in ListResourcesTool.ExecuteAsync |
| REQ-007 Paginierung | page/perPage Parameter in ListAsync |
| REQ-008 HTTP-Client | BookingApiClient + IHttpClientFactory |
| REQ-009 Clean Architecture | 4 Projekte, Dependency Rule eingehalten |
| REQ-010 TDD | xUnit + Moq + Shouldly, 4 Test-Projekte |
| REQ-019 list_resources | ListResourcesTool + ListResourcesUseCase + ResourceRepository |

## Phase-2-Erweiterbarkeit

Neue Tools folgen dem gleichen Muster:
1. Entity + Repository-Methode in Domain
2. Use Case + DTO in Application
3. Repository-Implementierung in Infrastructure
4. Tool-Klasse in Server

Die Architektur ist so aufgebaut, dass jedes neue Tool unabhängig hinzugefügt werden kann, ohne bestehende Klassen zu ändern (Open/Closed Principle).
