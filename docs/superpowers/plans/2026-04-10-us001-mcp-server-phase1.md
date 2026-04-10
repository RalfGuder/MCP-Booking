# US-001 MCP-Server Phase 1 — Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Build a working MCP server in C# that connects to the WP Booking Calendar REST API and exposes `list_resources` as the first tool.

**Architecture:** Clean Architecture with 4 layers (Domain, Application, Infrastructure, Server). The MCP C# SDK handles protocol/transport. A typed HttpClient calls the WordPress API with Basic Auth. Each layer is a separate .NET project.

**Tech Stack:** .NET 10.0, MCP C# SDK (`ModelContextProtocol`), xUnit, Moq, Shouldly, `IHttpClientFactory`

---

## File Structure

```
MCP-Booking/
├── MCP-Booking.sln
├── Directory.Build.props
├── src/
│   ├── McpBooking.Domain/
│   │   ├── McpBooking.Domain.csproj
│   │   ├── Entities/
│   │   │   └── Resource.cs
│   │   └── Interfaces/
│   │       └── IResourceRepository.cs
│   ├── McpBooking.Application/
│   │   ├── McpBooking.Application.csproj
│   │   ├── DTOs/
│   │   │   └── ResourceDto.cs
│   │   └── UseCases/
│   │       └── ListResourcesUseCase.cs
│   ├── McpBooking.Infrastructure/
│   │   ├── McpBooking.Infrastructure.csproj
│   │   ├── Configuration/
│   │   │   └── BookingApiOptions.cs
│   │   ├── Http/
│   │   │   └── BookingApiClient.cs
│   │   ├── Repositories/
│   │   │   └── ResourceRepository.cs
│   │   └── DependencyInjection.cs
│   └── McpBooking.Server/
│       ├── McpBooking.Server.csproj
│       ├── Program.cs
│       └── Tools/
│           └── ListResourcesTool.cs
├── tests/
│   ├── McpBooking.Domain.Tests/
│   │   └── McpBooking.Domain.Tests.csproj
│   ├── McpBooking.Application.Tests/
│   │   ├── McpBooking.Application.Tests.csproj
│   │   └── UseCases/
│   │       └── ListResourcesUseCaseTests.cs
│   ├── McpBooking.Infrastructure.Tests/
│   │   ├── McpBooking.Infrastructure.Tests.csproj
│   │   └── Repositories/
│   │       └── ResourceRepositoryTests.cs
│   └── McpBooking.Server.Tests/
│       ├── McpBooking.Server.Tests.csproj
│       └── Tools/
│           └── ListResourcesToolTests.cs
└── docs/
```

---

### Task 1: Solution scaffolding with Directory.Build.props

**Files:**
- Create: `Directory.Build.props`
- Create: `MCP-Booking.sln`
- Create: all 8 `.csproj` files (4 src + 4 tests)

- [ ] **Step 1: Create Directory.Build.props**

Create file `Directory.Build.props` in the repo root:

```xml
<Project>
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>
</Project>
```

- [ ] **Step 2: Create Domain project**

Run:
```bash
dotnet new classlib -n McpBooking.Domain -o src/McpBooking.Domain
```
Expected: Project created. Delete the auto-generated `Class1.cs`:
```bash
rm src/McpBooking.Domain/Class1.cs
```

Edit `src/McpBooking.Domain/McpBooking.Domain.csproj` to remove TargetFramework (inherited from Directory.Build.props):

```xml
<Project Sdk="Microsoft.NET.Sdk">
</Project>
```

- [ ] **Step 3: Create Application project**

Run:
```bash
dotnet new classlib -n McpBooking.Application -o src/McpBooking.Application
rm src/McpBooking.Application/Class1.cs
```

Edit `src/McpBooking.Application/McpBooking.Application.csproj`:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
    <ProjectReference Include="..\McpBooking.Domain\McpBooking.Domain.csproj" />
  </ItemGroup>
</Project>
```

- [ ] **Step 4: Create Infrastructure project**

Run:
```bash
dotnet new classlib -n McpBooking.Infrastructure -o src/McpBooking.Infrastructure
rm src/McpBooking.Infrastructure/Class1.cs
```

Edit `src/McpBooking.Infrastructure/McpBooking.Infrastructure.csproj`:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.Http" Version="10.0.*" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\McpBooking.Domain\McpBooking.Domain.csproj" />
    <ProjectReference Include="..\McpBooking.Application\McpBooking.Application.csproj" />
  </ItemGroup>
</Project>
```

- [ ] **Step 5: Create Server project**

Run:
```bash
dotnet new console -n McpBooking.Server -o src/McpBooking.Server
```

Edit `src/McpBooking.Server/McpBooking.Server.csproj`:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="ModelContextProtocol" Version="0.*" />
    <PackageReference Include="Microsoft.Extensions.Hosting" Version="10.0.*" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\McpBooking.Domain\McpBooking.Domain.csproj" />
    <ProjectReference Include="..\McpBooking.Application\McpBooking.Application.csproj" />
    <ProjectReference Include="..\McpBooking.Infrastructure\McpBooking.Infrastructure.csproj" />
  </ItemGroup>
</Project>
```

- [ ] **Step 6: Create four test projects**

Run:
```bash
dotnet new xunit -n McpBooking.Domain.Tests -o tests/McpBooking.Domain.Tests
dotnet new xunit -n McpBooking.Application.Tests -o tests/McpBooking.Application.Tests
dotnet new xunit -n McpBooking.Infrastructure.Tests -o tests/McpBooking.Infrastructure.Tests
dotnet new xunit -n McpBooking.Server.Tests -o tests/McpBooking.Server.Tests
```

Delete auto-generated test files:
```bash
rm tests/McpBooking.Domain.Tests/UnitTest1.cs
rm tests/McpBooking.Application.Tests/UnitTest1.cs
rm tests/McpBooking.Infrastructure.Tests/UnitTest1.cs
rm tests/McpBooking.Server.Tests/UnitTest1.cs
```

Edit each test `.csproj` to add Shouldly, Moq, and the project reference. Remove TargetFramework (inherited).

`tests/McpBooking.Domain.Tests/McpBooking.Domain.Tests.csproj`:
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
    <PackageReference Include="coverlet.collector" Version="6.*" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.*" />
    <PackageReference Include="xunit" Version="2.*" />
    <PackageReference Include="xunit.runner.visualstudio" Version="3.*" />
    <PackageReference Include="Shouldly" Version="4.*" />
    <PackageReference Include="Moq" Version="4.*" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\..\src\McpBooking.Domain\McpBooking.Domain.csproj" />
  </ItemGroup>
</Project>
```

`tests/McpBooking.Application.Tests/McpBooking.Application.Tests.csproj`:
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
    <PackageReference Include="coverlet.collector" Version="6.*" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.*" />
    <PackageReference Include="xunit" Version="2.*" />
    <PackageReference Include="xunit.runner.visualstudio" Version="3.*" />
    <PackageReference Include="Shouldly" Version="4.*" />
    <PackageReference Include="Moq" Version="4.*" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\..\src\McpBooking.Application\McpBooking.Application.csproj" />
  </ItemGroup>
</Project>
```

`tests/McpBooking.Infrastructure.Tests/McpBooking.Infrastructure.Tests.csproj`:
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
    <PackageReference Include="coverlet.collector" Version="6.*" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.*" />
    <PackageReference Include="xunit" Version="2.*" />
    <PackageReference Include="xunit.runner.visualstudio" Version="3.*" />
    <PackageReference Include="Shouldly" Version="4.*" />
    <PackageReference Include="Moq" Version="4.*" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\..\src\McpBooking.Infrastructure\McpBooking.Infrastructure.csproj" />
  </ItemGroup>
</Project>
```

`tests/McpBooking.Server.Tests/McpBooking.Server.Tests.csproj`:
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
    <PackageReference Include="coverlet.collector" Version="6.*" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.*" />
    <PackageReference Include="xunit" Version="2.*" />
    <PackageReference Include="xunit.runner.visualstudio" Version="3.*" />
    <PackageReference Include="Shouldly" Version="4.*" />
    <PackageReference Include="Moq" Version="4.*" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\..\src\McpBooking.Server\McpBooking.Server.csproj" />
  </ItemGroup>
</Project>
```

- [ ] **Step 7: Create the solution and add all projects**

Run:
```bash
dotnet new sln -n MCP-Booking
dotnet sln add src/McpBooking.Domain/McpBooking.Domain.csproj --solution-folder src
dotnet sln add src/McpBooking.Application/McpBooking.Application.csproj --solution-folder src
dotnet sln add src/McpBooking.Infrastructure/McpBooking.Infrastructure.csproj --solution-folder src
dotnet sln add src/McpBooking.Server/McpBooking.Server.csproj --solution-folder src
dotnet sln add tests/McpBooking.Domain.Tests/McpBooking.Domain.Tests.csproj --solution-folder tests
dotnet sln add tests/McpBooking.Application.Tests/McpBooking.Application.Tests.csproj --solution-folder tests
dotnet sln add tests/McpBooking.Infrastructure.Tests/McpBooking.Infrastructure.Tests.csproj --solution-folder tests
dotnet sln add tests/McpBooking.Server.Tests/McpBooking.Server.Tests.csproj --solution-folder tests
```

- [ ] **Step 8: Verify build and test**

Run:
```bash
dotnet build
```
Expected: `Build succeeded. 0 Warning(s) 0 Error(s)`

Run:
```bash
dotnet test
```
Expected: All test projects discovered, 0 tests run (no test files yet), no errors.

- [ ] **Step 9: Commit**

```bash
git add Directory.Build.props MCP-Booking.sln src/ tests/
git commit -m "feat: scaffold solution with 8 projects (Clean Architecture)

4 source projects (Domain, Application, Infrastructure, Server) and
4 test projects. Directory.Build.props sets net10.0, Nullable, ImplicitUsings."
```

---

### Task 2: Domain layer — Resource entity and IResourceRepository

**Files:**
- Create: `src/McpBooking.Domain/Entities/Resource.cs`
- Create: `src/McpBooking.Domain/Interfaces/IResourceRepository.cs`

- [ ] **Step 1: Create Resource entity**

Create file `src/McpBooking.Domain/Entities/Resource.cs`:

```csharp
using System.Text.Json.Serialization;

namespace McpBooking.Domain.Entities;

public class Resource
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("cost")]
    public string? Cost { get; set; }

    [JsonPropertyName("visitors")]
    public int? Visitors { get; set; }
}
```

- [ ] **Step 2: Create IResourceRepository interface**

Create file `src/McpBooking.Domain/Interfaces/IResourceRepository.cs`:

```csharp
using McpBooking.Domain.Entities;

namespace McpBooking.Domain.Interfaces;

public interface IResourceRepository
{
    Task<IReadOnlyList<Resource>> ListAsync(int page = 1, int perPage = 20, CancellationToken ct = default);
}
```

- [ ] **Step 3: Verify build**

Run:
```bash
dotnet build src/McpBooking.Domain
```
Expected: `Build succeeded.`

- [ ] **Step 4: Commit**

```bash
git add src/McpBooking.Domain/
git commit -m "feat: add Resource entity and IResourceRepository interface"
```

---

### Task 3: Application layer — ResourceDto and ListResourcesUseCase (TDD)

**Files:**
- Create: `src/McpBooking.Application/DTOs/ResourceDto.cs`
- Create: `src/McpBooking.Application/UseCases/ListResourcesUseCase.cs`
- Create: `tests/McpBooking.Application.Tests/UseCases/ListResourcesUseCaseTests.cs`

- [ ] **Step 1: Write the failing tests**

Create file `tests/McpBooking.Application.Tests/UseCases/ListResourcesUseCaseTests.cs`:

```csharp
using McpBooking.Application.DTOs;
using McpBooking.Application.UseCases;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;
using Moq;
using Shouldly;

namespace McpBooking.Application.Tests.UseCases;

public class ListResourcesUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ReturnsResources_MappedToDtos()
    {
        var mock = new Mock<IResourceRepository>();
        mock.Setup(r => r.ListAsync(1, 20, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Resource>
            {
                new() { Id = 1, Title = "Gemeindesaal", Cost = "50", Visitors = 30 },
                new() { Id = 2, Title = "Vereinsraum", Cost = null, Visitors = null }
            });
        var useCase = new ListResourcesUseCase(mock.Object);

        var result = await useCase.ExecuteAsync();

        result.Count.ShouldBe(2);
        result[0].ShouldBe(new ResourceDto(1, "Gemeindesaal", "50", 30));
        result[1].ShouldBe(new ResourceDto(2, "Vereinsraum", null, null));
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

    [Fact]
    public async Task ExecuteAsync_PassesPaginationParameters()
    {
        var mock = new Mock<IResourceRepository>();
        mock.Setup(r => r.ListAsync(3, 50, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Resource>());
        var useCase = new ListResourcesUseCase(mock.Object);

        await useCase.ExecuteAsync(page: 3, perPage: 50);

        mock.Verify(r => r.ListAsync(3, 50, It.IsAny<CancellationToken>()), Times.Once);
    }
}
```

- [ ] **Step 2: Run tests to verify they fail**

Run:
```bash
dotnet test tests/McpBooking.Application.Tests --verbosity normal
```
Expected: FAIL — `ListResourcesUseCase` and `ResourceDto` do not exist yet.

- [ ] **Step 3: Create ResourceDto**

Create file `src/McpBooking.Application/DTOs/ResourceDto.cs`:

```csharp
namespace McpBooking.Application.DTOs;

public record ResourceDto(int Id, string Title, string? Cost, int? Visitors);
```

- [ ] **Step 4: Create ListResourcesUseCase**

Create file `src/McpBooking.Application/UseCases/ListResourcesUseCase.cs`:

```csharp
using McpBooking.Application.DTOs;
using McpBooking.Domain.Interfaces;

namespace McpBooking.Application.UseCases;

public class ListResourcesUseCase
{
    private readonly IResourceRepository _repository;

    public ListResourcesUseCase(IResourceRepository repository)
    {
        _repository = repository;
    }

    public virtual async Task<IReadOnlyList<ResourceDto>> ExecuteAsync(
        int page = 1, int perPage = 20, CancellationToken ct = default)
    {
        var resources = await _repository.ListAsync(page, perPage, ct);
        return resources.Select(r => new ResourceDto(r.Id, r.Title, r.Cost, r.Visitors)).ToList();
    }
}
```

Note: `virtual` on `ExecuteAsync` is required so Moq can mock it in Server.Tests later.

- [ ] **Step 5: Run tests to verify they pass**

Run:
```bash
dotnet test tests/McpBooking.Application.Tests --verbosity normal
```
Expected: 3 tests passed.

- [ ] **Step 6: Commit**

```bash
git add src/McpBooking.Application/ tests/McpBooking.Application.Tests/
git commit -m "feat: add ListResourcesUseCase with DTO mapping (TDD)

3 tests: mapping correctness, empty list, pagination pass-through."
```

---

### Task 4: Infrastructure layer — BookingApiOptions, BookingApiClient, ResourceRepository (TDD)

**Files:**
- Create: `src/McpBooking.Infrastructure/Configuration/BookingApiOptions.cs`
- Create: `src/McpBooking.Infrastructure/Http/BookingApiClient.cs`
- Create: `src/McpBooking.Infrastructure/Repositories/ResourceRepository.cs`
- Create: `src/McpBooking.Infrastructure/DependencyInjection.cs`
- Create: `tests/McpBooking.Infrastructure.Tests/Repositories/ResourceRepositoryTests.cs`

- [ ] **Step 1: Write the failing tests**

Create file `tests/McpBooking.Infrastructure.Tests/Repositories/ResourceRepositoryTests.cs`:

```csharp
using System.Net;
using System.Text.Json;
using McpBooking.Domain.Entities;
using McpBooking.Infrastructure.Http;
using McpBooking.Infrastructure.Repositories;
using Moq;
using Moq.Protected;
using Shouldly;

namespace McpBooking.Infrastructure.Tests.Repositories;

public class ResourceRepositoryTests
{
    private static (ResourceRepository repo, Mock<HttpMessageHandler> handlerMock) CreateRepoWithMockHttp(
        HttpStatusCode statusCode, string responseBody)
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(responseBody, System.Text.Encoding.UTF8, "application/json")
            });

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("https://example.com/wp-json/wpbc/v1")
        };
        var apiClient = new BookingApiClient(httpClient);
        var repo = new ResourceRepository(apiClient);
        return (repo, handlerMock);
    }

    [Fact]
    public async Task ListAsync_ReturnsDeserializedResources()
    {
        var json = JsonSerializer.Serialize(new[]
        {
            new { id = 1, title = "Gemeindesaal", cost = "50", visitors = 30 },
            new { id = 2, title = "Vereinsraum", cost = (string?)null, visitors = (int?)null }
        });
        var (repo, _) = CreateRepoWithMockHttp(HttpStatusCode.OK, json);

        var result = await repo.ListAsync();

        result.Count.ShouldBe(2);
        result[0].Id.ShouldBe(1);
        result[0].Title.ShouldBe("Gemeindesaal");
        result[0].Cost.ShouldBe("50");
        result[0].Visitors.ShouldBe(30);
        result[1].Id.ShouldBe(2);
        result[1].Title.ShouldBe("Vereinsraum");
    }

    [Fact]
    public async Task ListAsync_EmptyArray_ReturnsEmptyList()
    {
        var (repo, _) = CreateRepoWithMockHttp(HttpStatusCode.OK, "[]");

        var result = await repo.ListAsync();

        result.ShouldBeEmpty();
    }

    [Fact]
    public async Task ListAsync_SendsCorrectQueryParameters()
    {
        var (repo, handlerMock) = CreateRepoWithMockHttp(HttpStatusCode.OK, "[]");

        await repo.ListAsync(page: 3, perPage: 50);

        handlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.RequestUri!.PathAndQuery.Contains("page=3") &&
                req.RequestUri.PathAndQuery.Contains("per_page=50")),
            ItExpr.IsAny<CancellationToken>());
    }
}
```

- [ ] **Step 2: Run tests to verify they fail**

Run:
```bash
dotnet test tests/McpBooking.Infrastructure.Tests --verbosity normal
```
Expected: FAIL — `BookingApiClient` and `ResourceRepository` do not exist yet.

- [ ] **Step 3: Create BookingApiOptions**

Create file `src/McpBooking.Infrastructure/Configuration/BookingApiOptions.cs`:

```csharp
namespace McpBooking.Infrastructure.Configuration;

public class BookingApiOptions
{
    public string BaseUrl { get; set; } = "https://kv-milowerland.de/wp-json/wpbc/v1";
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
```

- [ ] **Step 4: Create BookingApiClient**

Create file `src/McpBooking.Infrastructure/Http/BookingApiClient.cs`:

```csharp
using System.Net.Http.Json;

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

- [ ] **Step 5: Create ResourceRepository**

Create file `src/McpBooking.Infrastructure/Repositories/ResourceRepository.cs`:

```csharp
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;
using McpBooking.Infrastructure.Http;

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

- [ ] **Step 6: Create DependencyInjection extension**

Create file `src/McpBooking.Infrastructure/DependencyInjection.cs`:

```csharp
using System.Net.Http.Headers;
using System.Text;
using McpBooking.Domain.Interfaces;
using McpBooking.Infrastructure.Configuration;
using McpBooking.Infrastructure.Http;
using McpBooking.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

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

- [ ] **Step 7: Run tests to verify they pass**

Run:
```bash
dotnet test tests/McpBooking.Infrastructure.Tests --verbosity normal
```
Expected: 3 tests passed.

- [ ] **Step 8: Commit**

```bash
git add src/McpBooking.Infrastructure/ tests/McpBooking.Infrastructure.Tests/
git commit -m "feat: add Infrastructure layer with HTTP client and ResourceRepository (TDD)

BookingApiClient (typed HttpClient), ResourceRepository, BookingApiOptions,
DI extension. 3 tests: deserialization, empty list, query parameters."
```

---

### Task 5: Server layer — ListResourcesTool with MCP registration (TDD)

**Files:**
- Create: `src/McpBooking.Server/Tools/ListResourcesTool.cs`
- Modify: `src/McpBooking.Server/Program.cs`
- Create: `tests/McpBooking.Server.Tests/Tools/ListResourcesToolTests.cs`

- [ ] **Step 1: Write the failing tests**

Create file `tests/McpBooking.Server.Tests/Tools/ListResourcesToolTests.cs`:

```csharp
using System.Net;
using System.Text.Json;
using McpBooking.Application.DTOs;
using McpBooking.Application.UseCases;
using McpBooking.Domain.Interfaces;
using McpBooking.Server.Tools;
using Moq;
using Shouldly;

namespace McpBooking.Server.Tests.Tools;

public class ListResourcesToolTests
{
    private static ListResourcesTool CreateToolWithMockUseCase(
        IReadOnlyList<ResourceDto>? result = null, Exception? exception = null)
    {
        var useCaseMock = new Mock<ListResourcesUseCase>(Mock.Of<IResourceRepository>());
        if (exception != null)
        {
            useCaseMock.Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);
        }
        else
        {
            useCaseMock.Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result ?? new List<ResourceDto>());
        }
        return new ListResourcesTool(useCaseMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ValidParams_ReturnsJson()
    {
        var resources = new List<ResourceDto>
        {
            new(1, "Gemeindesaal", "50", 30)
        };
        var tool = CreateToolWithMockUseCase(resources);

        var result = await tool.ExecuteAsync();

        var doc = JsonDocument.Parse(result);
        doc.RootElement.GetArrayLength().ShouldBe(1);
        doc.RootElement[0].GetProperty("id").GetInt32().ShouldBe(1);
        doc.RootElement[0].GetProperty("title").GetString().ShouldBe("Gemeindesaal");
    }

    [Fact]
    public async Task ExecuteAsync_InvalidPage_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(page: 0);

        result.ShouldContain("page muss >= 1 sein");
    }

    [Fact]
    public async Task ExecuteAsync_InvalidPerPage_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(perPage: 101);

        result.ShouldContain("per_page muss zwischen 1 und 100 liegen");
    }

    [Fact]
    public async Task ExecuteAsync_AuthError_ReturnsReadableMessage()
    {
        var tool = CreateToolWithMockUseCase(
            exception: new HttpRequestException("Unauthorized", null, HttpStatusCode.Unauthorized));

        var result = await tool.ExecuteAsync();

        result.ShouldContain("Authentifizierung fehlgeschlagen");
    }
}
```

- [ ] **Step 2: Run tests to verify they fail**

Run:
```bash
dotnet test tests/McpBooking.Server.Tests --verbosity normal
```
Expected: FAIL — `ListResourcesTool` does not exist yet.

- [ ] **Step 3: Create ListResourcesTool**

Create file `src/McpBooking.Server/Tools/ListResourcesTool.cs`:

```csharp
using System.ComponentModel;
using System.Net;
using System.Text.Json;
using McpBooking.Application.UseCases;
using ModelContextProtocol.Server;

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

- [ ] **Step 4: Run tests to verify they pass**

Run:
```bash
dotnet test tests/McpBooking.Server.Tests --verbosity normal
```
Expected: 4 tests passed.

- [ ] **Step 5: Commit**

```bash
git add src/McpBooking.Server/Tools/ tests/McpBooking.Server.Tests/
git commit -m "feat: add ListResourcesTool with validation and error handling (TDD)

4 tests: JSON output, invalid page, invalid perPage, auth error message."
```

---

### Task 6: Program.cs — MCP server entry point and DI wiring

**Files:**
- Modify: `src/McpBooking.Server/Program.cs`

- [ ] **Step 1: Write Program.cs**

Replace `src/McpBooking.Server/Program.cs` with:

```csharp
using McpBooking.Application.UseCases;
using McpBooking.Infrastructure;
using McpBooking.Infrastructure.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

var options = new BookingApiOptions
{
    BaseUrl = Environment.GetEnvironmentVariable("WPBC_API_URL")
              ?? "https://kv-milowerland.de/wp-json/wpbc/v1",
    Username = Environment.GetEnvironmentVariable("WPBC_USERNAME")
              ?? throw new InvalidOperationException("Umgebungsvariable WPBC_USERNAME ist nicht gesetzt."),
    Password = Environment.GetEnvironmentVariable("WPBC_PASSWORD")
              ?? throw new InvalidOperationException("Umgebungsvariable WPBC_PASSWORD ist nicht gesetzt.")
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

- [ ] **Step 2: Verify build**

Run:
```bash
dotnet build src/McpBooking.Server
```
Expected: `Build succeeded.`

- [ ] **Step 3: Commit**

```bash
git add src/McpBooking.Server/Program.cs
git commit -m "feat: add Program.cs with MCP server, DI wiring, and env config

Reads WPBC_API_URL, WPBC_USERNAME, WPBC_PASSWORD from environment.
Registers infrastructure, use case, and MCP server with stdio transport."
```

---

### Task 7: Full integration verification

- [ ] **Step 1: Run all tests**

Run:
```bash
dotnet test --verbosity normal
```
Expected: 10 tests passed across Application.Tests, Infrastructure.Tests, and Server.Tests.

- [ ] **Step 2: Verify the full build**

Run:
```bash
dotnet build
```
Expected: `Build succeeded. 0 Warning(s) 0 Error(s)`

- [ ] **Step 3: Verify server starts (and fails fast without credentials)**

Run:
```bash
dotnet run --project src/McpBooking.Server 2>&1 || true
```
Expected: `Unhandled exception. System.InvalidOperationException: Umgebungsvariable WPBC_USERNAME ist nicht gesetzt.`

This confirms the fail-fast behavior for missing credentials.

- [ ] **Step 4: Final commit**

```bash
git add -A
git commit -m "feat: complete US-001 Phase 1 — MCP server with list_resources tool

Clean Architecture (Domain, Application, Infrastructure, Server),
MCP C# SDK with stdio transport, WordPress Basic Auth, typed HttpClient,
10 passing tests (xUnit + Moq + Shouldly)."
```
