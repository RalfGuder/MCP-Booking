# Phase 2a: Booking-Tools Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Implement 8 MCP tools for the Bookings API area (list, get, create, update, delete, approve, pending, note) using vertical slices through all Clean Architecture layers.

**Architecture:** Each tool is a vertical slice: Domain Entity/Interface, Application UseCase/DTO, Infrastructure Repository, Server Tool. Foundation types (Booking entity, IBookingRepository, BookingDto, BookingApiClient extensions, localization) are created first, then each tool is TDD'd as a complete slice.

**Tech Stack:** .NET 10, ModelContextProtocol SDK, MSTest.Sdk, Moq, Shouldly, System.Text.Json

**Spec:** [2026-04-12-phase2a-bookings-design.md](../specs/2026-04-12-phase2a-bookings-design.md)

---

### Task 1: Domain Foundation — Booking Entity, IBookingRepository, BookingDto

**Files:**
- Create: `src/McpBooking.Domain/Entities/Booking.cs`
- Create: `src/McpBooking.Domain/Interfaces/IBookingRepository.cs`
- Create: `src/McpBooking.Application/DTOs/BookingDto.cs`

- [ ] **Step 1: Create Booking entity**

Create `src/McpBooking.Domain/Entities/Booking.cs`:

```csharp
// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.Text.Json;
using System.Text.Json.Serialization;

namespace McpBooking.Domain.Entities;

/// <summary>
/// Represents a booking in the WP Booking Calendar system.
/// </summary>
public class Booking
{
    /// <summary>
    /// Gets or sets the unique identifier of the booking.
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the resource (booking type) ID.
    /// </summary>
    [JsonPropertyName("booking_type")]
    public int BookingType { get; set; }

    /// <summary>
    /// Gets or sets the list of booked dates.
    /// </summary>
    [JsonPropertyName("dates")]
    public List<string> Dates { get; set; } = [];

    /// <summary>
    /// Gets or sets the submitted form data as a raw JSON element.
    /// </summary>
    [JsonPropertyName("form_data")]
    public JsonElement? FormData { get; set; }

    /// <summary>
    /// Gets or sets the booking status (e.g. pending, approved, trash).
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sort date of the booking.
    /// </summary>
    [JsonPropertyName("sort_date")]
    public string? SortDate { get; set; }

    /// <summary>
    /// Gets or sets the last modification date of the booking.
    /// </summary>
    [JsonPropertyName("modification_date")]
    public string? ModificationDate { get; set; }

    /// <summary>
    /// Gets or sets whether the booking is new/unread.
    /// </summary>
    [JsonPropertyName("is_new")]
    public bool? IsNew { get; set; }

    /// <summary>
    /// Gets or sets the note attached to the booking.
    /// </summary>
    [JsonPropertyName("note")]
    public string? Note { get; set; }
}
```

- [ ] **Step 2: Create IBookingRepository interface**

Create `src/McpBooking.Domain/Interfaces/IBookingRepository.cs`:

```csharp
// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Domain.Entities;

namespace McpBooking.Domain.Interfaces;

/// <summary>
/// Defines the contract for booking data access operations.
/// </summary>
public interface IBookingRepository
{
    /// <summary>
    /// Retrieves a filtered, paginated list of bookings.
    /// </summary>
    /// <param name="page">The page number (1-based).</param>
    /// <param name="perPage">The number of items per page (1-100).</param>
    /// <param name="resourceId">Optional filter by resource ID.</param>
    /// <param name="status">Optional filter by status.</param>
    /// <param name="dateFrom">Optional filter: start date (ISO 8601).</param>
    /// <param name="dateTo">Optional filter: end date (ISO 8601).</param>
    /// <param name="isNew">Optional filter by new/unread status.</param>
    /// <param name="search">Optional keyword search.</param>
    /// <param name="orderBy">Optional sort field.</param>
    /// <param name="order">Optional sort direction (ASC/DESC).</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>A read-only list of bookings.</returns>
    Task<IReadOnlyList<Booking>> ListAsync(int page, int perPage,
        int? resourceId = null, string? status = null,
        string? dateFrom = null, string? dateTo = null,
        bool? isNew = null, string? search = null,
        string? orderBy = null, string? order = null,
        CancellationToken ct = default);

    /// <summary>
    /// Retrieves a single booking by its identifier.
    /// </summary>
    /// <param name="id">The booking ID.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>The booking, or <see langword="null"/> if not found.</returns>
    Task<Booking?> GetAsync(int id, CancellationToken ct = default);

    /// <summary>
    /// Creates a new booking.
    /// </summary>
    /// <param name="bookingType">The resource/booking type ID.</param>
    /// <param name="formDataJson">Form data as a JSON string.</param>
    /// <param name="datesJson">Dates as a JSON array string.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>The created booking.</returns>
    Task<Booking> CreateAsync(int bookingType, string formDataJson,
        string datesJson, CancellationToken ct = default);

    /// <summary>
    /// Updates an existing booking.
    /// </summary>
    /// <param name="id">The booking ID.</param>
    /// <param name="formDataJson">Optional updated form data as JSON.</param>
    /// <param name="bookingType">Optional new booking type.</param>
    /// <param name="status">Optional new status.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>The updated booking.</returns>
    Task<Booking> UpdateAsync(int id, string? formDataJson = null,
        int? bookingType = null, string? status = null,
        CancellationToken ct = default);

    /// <summary>
    /// Deletes a booking by its identifier.
    /// </summary>
    /// <param name="id">The booking ID.</param>
    /// <param name="ct">A cancellation token.</param>
    Task DeleteAsync(int id, CancellationToken ct = default);

    /// <summary>
    /// Approves a booking.
    /// </summary>
    /// <param name="id">The booking ID.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>The approved booking.</returns>
    Task<Booking> ApproveAsync(int id, CancellationToken ct = default);

    /// <summary>
    /// Sets a booking to pending status.
    /// </summary>
    /// <param name="id">The booking ID.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>The updated booking.</returns>
    Task<Booking> SetPendingAsync(int id, CancellationToken ct = default);

    /// <summary>
    /// Updates the note attached to a booking.
    /// </summary>
    /// <param name="id">The booking ID.</param>
    /// <param name="note">The note text.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>The updated booking.</returns>
    Task<Booking> UpdateNoteAsync(int id, string note,
        CancellationToken ct = default);
}
```

- [ ] **Step 3: Create BookingDto**

Create `src/McpBooking.Application/DTOs/BookingDto.cs`:

```csharp
// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.Text.Json;

namespace McpBooking.Application.DTOs;

/// <summary>
/// Data transfer object representing a booking.
/// </summary>
/// <param name="Id">The unique identifier of the booking.</param>
/// <param name="BookingType">The resource/booking type ID.</param>
/// <param name="Dates">The list of booked dates.</param>
/// <param name="FormData">The submitted form data as a raw JSON element.</param>
/// <param name="Status">The booking status.</param>
/// <param name="SortDate">The sort date, or <see langword="null"/> if not set.</param>
/// <param name="ModificationDate">The modification date, or <see langword="null"/> if not set.</param>
/// <param name="IsNew">Whether the booking is new/unread, or <see langword="null"/> if not set.</param>
/// <param name="Note">The note, or <see langword="null"/> if not set.</param>
public record BookingDto(
    int Id, int BookingType, List<string> Dates,
    JsonElement? FormData, string Status,
    string? SortDate, string? ModificationDate,
    bool? IsNew, string? Note);
```

- [ ] **Step 4: Verify build**

Run: `dotnet build`
Expected: Build succeeds with 0 errors.

- [ ] **Step 5: Commit**

```bash
git add src/McpBooking.Domain/Entities/Booking.cs src/McpBooking.Domain/Interfaces/IBookingRepository.cs src/McpBooking.Application/DTOs/BookingDto.cs
git commit -m "feat: add Booking entity, IBookingRepository, and BookingDto"
```

---

### Task 2: Infrastructure Foundation — BookingApiClient Extensions and Strings

**Files:**
- Modify: `src/McpBooking.Infrastructure/Http/BookingApiClient.cs`
- Modify: `src/McpBooking.Infrastructure/Properties/Strings.cs`

- [ ] **Step 1: Add PostAsync, PutAsync, DeleteAsync to BookingApiClient**

Add these methods to `src/McpBooking.Infrastructure/Http/BookingApiClient.cs` after the existing `GetAsync<T>` method:

```csharp
    /// <summary>
    /// Sends a POST request with a JSON body and deserializes the response.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response into.</typeparam>
    /// <param name="path">The relative path to request.</param>
    /// <param name="body">The object to serialize as JSON in the request body.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>The deserialized response value, or <see langword="null"/> if the body is empty.</returns>
    public async Task<T?> PostAsync<T>(string path, object body, CancellationToken ct = default)
    {
        var response = await _httpClient.PostAsJsonAsync(path, body, ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>(ct);
    }

    /// <summary>
    /// Sends a POST request without a body and deserializes the response.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response into.</typeparam>
    /// <param name="path">The relative path to request.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>The deserialized response value, or <see langword="null"/> if the body is empty.</returns>
    public async Task<T?> PostAsync<T>(string path, CancellationToken ct = default)
    {
        var response = await _httpClient.PostAsync(path, null, ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>(ct);
    }

    /// <summary>
    /// Sends a PUT request with a JSON body and deserializes the response.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response into.</typeparam>
    /// <param name="path">The relative path to request.</param>
    /// <param name="body">The object to serialize as JSON in the request body.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>The deserialized response value, or <see langword="null"/> if the body is empty.</returns>
    public async Task<T?> PutAsync<T>(string path, object body, CancellationToken ct = default)
    {
        var response = await _httpClient.PutAsJsonAsync(path, body, ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>(ct);
    }

    /// <summary>
    /// Sends a DELETE request to the specified path.
    /// </summary>
    /// <param name="path">The relative path to request.</param>
    /// <param name="ct">A cancellation token.</param>
    public async Task DeleteAsync(string path, CancellationToken ct = default)
    {
        var response = await _httpClient.DeleteAsync(path, ct);
        response.EnsureSuccessStatusCode();
    }
```

- [ ] **Step 2: Add booking API path constants to Infrastructure Strings.cs**

Add these properties to `src/McpBooking.Infrastructure/Properties/Strings.cs` before the `Decode` method. Use `echo -n "<value>" | base64` to compute each Base64 string.

```csharp
    /// <summary>
    /// Gets the relative path of the bookings endpoint.
    /// </summary>
    // "bookings"
    internal static string ApiBookingsPath => Decode("Ym9va2luZ3M=");

    /// <summary>
    /// Gets the format string for a single booking by ID.
    /// </summary>
    // "bookings/{0}"
    internal static string ApiBookingByIdPath => Decode("Ym9va2luZ3Mve3swfQ==");

    /// <summary>
    /// Gets the format string for the booking approve endpoint.
    /// </summary>
    // "bookings/{0}/approve"
    internal static string ApiBookingApprovePath => Decode("Ym9va2luZ3Mve3swfS9hcHByb3Zl");

    /// <summary>
    /// Gets the format string for the booking pending endpoint.
    /// </summary>
    // "bookings/{0}/pending"
    internal static string ApiBookingPendingPath => Decode("Ym9va2luZ3Mve3swfS9wZW5kaW5n");

    /// <summary>
    /// Gets the format string for the booking note endpoint.
    /// </summary>
    // "bookings/{0}/note"
    internal static string ApiBookingNotePath => Decode("Ym9va2luZ3Mve3swfS9ub3Rl");

    /// <summary>
    /// Gets the query string format for booking list filters with ampersand prefix.
    /// </summary>
    // "&resource_id={0}"
    internal static string QueryResourceIdFormat => Decode("JnJlc291cmNlX2lkPXswfQ==");

    /// <summary>
    /// Gets the query string format for status filter.
    /// </summary>
    // "&status={0}"
    internal static string QueryStatusFormat => Decode("JnN0YXR1cz17MH0=");

    /// <summary>
    /// Gets the query string format for date_from filter.
    /// </summary>
    // "&date_from={0}"
    internal static string QueryDateFromFormat => Decode("JmRhdGVfZnJvbT17MH0=");

    /// <summary>
    /// Gets the query string format for date_to filter.
    /// </summary>
    // "&date_to={0}"
    internal static string QueryDateToFormat => Decode("JmRhdGVfdG89ezB9");

    /// <summary>
    /// Gets the query string format for is_new filter.
    /// </summary>
    // "&is_new={0}"
    internal static string QueryIsNewFormat => Decode("JmlzX25ldz17MH0=");

    /// <summary>
    /// Gets the query string format for search filter.
    /// </summary>
    // "&search={0}"
    internal static string QuerySearchFormat => Decode("JnNlYXJjaD17MH0=");

    /// <summary>
    /// Gets the query string format for orderby filter.
    /// </summary>
    // "&orderby={0}"
    internal static string QueryOrderByFormat => Decode("Jm9yZGVyYnk9ezB9");

    /// <summary>
    /// Gets the query string format for order filter.
    /// </summary>
    // "&order={0}"
    internal static string QueryOrderFormat => Decode("Jm9yZGVyPXswfQ==");
```

- [ ] **Step 3: Verify build**

Run: `dotnet build`
Expected: Build succeeds with 0 errors.

- [ ] **Step 4: Commit**

```bash
git add src/McpBooking.Infrastructure/Http/BookingApiClient.cs src/McpBooking.Infrastructure/Properties/Strings.cs
git commit -m "feat: add POST/PUT/DELETE to BookingApiClient and booking path constants"
```

---

### Task 3: Localization — Messages.resx and Messages.Designer.cs

**Files:**
- Modify: `src/McpBooking.Server/Properties/Messages.resx`
- Modify: `src/McpBooking.Server/Properties/Messages.en.resx`
- Modify: `src/McpBooking.Server/Properties/Messages.fr.resx`
- Modify: `src/McpBooking.Server/Properties/Messages.es.resx`
- Modify: `src/McpBooking.Server/Properties/Messages.Designer.cs`

- [ ] **Step 1: Add entries to Messages.resx (German, default)**

Add before the closing `</root>` tag in `src/McpBooking.Server/Properties/Messages.resx`:

```xml
  <data name="ErrorBookingNotFound" xml:space="preserve">
    <value>Fehler: Buchung mit ID {0} nicht gefunden.</value>
  </data>
  <data name="ErrorInvalidId" xml:space="preserve">
    <value>Fehler: ID muss &gt;= 1 sein.</value>
  </data>
  <data name="ErrorInvalidBookingType" xml:space="preserve">
    <value>Fehler: Buchungstyp muss &gt;= 1 sein.</value>
  </data>
  <data name="ErrorInvalidJson" xml:space="preserve">
    <value>Fehler: {0} enthält kein gültiges JSON.</value>
  </data>
  <data name="ErrorNoteEmpty" xml:space="preserve">
    <value>Fehler: Notiz darf nicht leer sein.</value>
  </data>
  <data name="SuccessBookingDeleted" xml:space="preserve">
    <value>Buchung {0} wurde gelöscht.</value>
  </data>
```

- [ ] **Step 2: Add entries to Messages.en.resx**

Add before the closing `</root>` tag in `src/McpBooking.Server/Properties/Messages.en.resx`:

```xml
  <data name="ErrorBookingNotFound" xml:space="preserve">
    <value>Error: Booking with ID {0} not found.</value>
  </data>
  <data name="ErrorInvalidId" xml:space="preserve">
    <value>Error: ID must be &gt;= 1.</value>
  </data>
  <data name="ErrorInvalidBookingType" xml:space="preserve">
    <value>Error: Booking type must be &gt;= 1.</value>
  </data>
  <data name="ErrorInvalidJson" xml:space="preserve">
    <value>Error: {0} does not contain valid JSON.</value>
  </data>
  <data name="ErrorNoteEmpty" xml:space="preserve">
    <value>Error: Note must not be empty.</value>
  </data>
  <data name="SuccessBookingDeleted" xml:space="preserve">
    <value>Booking {0} has been deleted.</value>
  </data>
```

- [ ] **Step 3: Add entries to Messages.fr.resx**

Add before the closing `</root>` tag in `src/McpBooking.Server/Properties/Messages.fr.resx`:

```xml
  <data name="ErrorBookingNotFound" xml:space="preserve">
    <value>Erreur : Réservation avec l'ID {0} introuvable.</value>
  </data>
  <data name="ErrorInvalidId" xml:space="preserve">
    <value>Erreur : L'ID doit être &gt;= 1.</value>
  </data>
  <data name="ErrorInvalidBookingType" xml:space="preserve">
    <value>Erreur : Le type de réservation doit être &gt;= 1.</value>
  </data>
  <data name="ErrorInvalidJson" xml:space="preserve">
    <value>Erreur : {0} ne contient pas de JSON valide.</value>
  </data>
  <data name="ErrorNoteEmpty" xml:space="preserve">
    <value>Erreur : La note ne doit pas être vide.</value>
  </data>
  <data name="SuccessBookingDeleted" xml:space="preserve">
    <value>Réservation {0} supprimée.</value>
  </data>
```

- [ ] **Step 4: Add entries to Messages.es.resx**

Add before the closing `</root>` tag in `src/McpBooking.Server/Properties/Messages.es.resx`:

```xml
  <data name="ErrorBookingNotFound" xml:space="preserve">
    <value>Error: Reserva con ID {0} no encontrada.</value>
  </data>
  <data name="ErrorInvalidId" xml:space="preserve">
    <value>Error: ID debe ser &gt;= 1.</value>
  </data>
  <data name="ErrorInvalidBookingType" xml:space="preserve">
    <value>Error: Tipo de reserva debe ser &gt;= 1.</value>
  </data>
  <data name="ErrorInvalidJson" xml:space="preserve">
    <value>Error: {0} no contiene JSON válido.</value>
  </data>
  <data name="ErrorNoteEmpty" xml:space="preserve">
    <value>Error: La nota no debe estar vacía.</value>
  </data>
  <data name="SuccessBookingDeleted" xml:space="preserve">
    <value>Reserva {0} eliminada.</value>
  </data>
```

- [ ] **Step 5: Add properties to Messages.Designer.cs**

Add these properties to `src/McpBooking.Server/Properties/Messages.Designer.cs` before the closing `}`:

```csharp
    /// <summary>
    /// Gets the error message returned when a booking is not found (HTTP 404).
    /// </summary>
    internal static string ErrorBookingNotFound =>
        ResourceManager.GetString("ErrorBookingNotFound", ResourceCulture)!;

    /// <summary>
    /// Gets the error message returned when the id parameter is invalid.
    /// </summary>
    internal static string ErrorInvalidId =>
        ResourceManager.GetString("ErrorInvalidId", ResourceCulture)!;

    /// <summary>
    /// Gets the error message returned when the booking type parameter is invalid.
    /// </summary>
    internal static string ErrorInvalidBookingType =>
        ResourceManager.GetString("ErrorInvalidBookingType", ResourceCulture)!;

    /// <summary>
    /// Gets the error message returned when a JSON parameter is invalid.
    /// </summary>
    internal static string ErrorInvalidJson =>
        ResourceManager.GetString("ErrorInvalidJson", ResourceCulture)!;

    /// <summary>
    /// Gets the error message returned when the note parameter is empty.
    /// </summary>
    internal static string ErrorNoteEmpty =>
        ResourceManager.GetString("ErrorNoteEmpty", ResourceCulture)!;

    /// <summary>
    /// Gets the success message returned when a booking is deleted.
    /// </summary>
    internal static string SuccessBookingDeleted =>
        ResourceManager.GetString("SuccessBookingDeleted", ResourceCulture)!;
```

- [ ] **Step 6: Verify build**

Run: `dotnet build`
Expected: Build succeeds with 0 errors.

- [ ] **Step 7: Commit**

```bash
git add src/McpBooking.Server/Properties/
git commit -m "feat: add booking localization entries (DE, EN, FR, ES)"
```

---

### Task 4: list_bookings — Vertical Slice

**Files:**
- Create: `src/McpBooking.Application/UseCases/ListBookingsUseCase.cs`
- Create: `src/McpBooking.Infrastructure/Repositories/BookingRepository.cs`
- Create: `src/McpBooking.Server/Tools/ListBookingsTool.cs`
- Create: `tests/McpBooking.Application.Tests/UseCases/ListBookingsUseCaseTests.cs`
- Create: `tests/McpBooking.Infrastructure.Tests/Repositories/BookingRepositoryTests.cs`
- Create: `tests/McpBooking.Server.Tests/Tools/ListBookingsToolTests.cs`
- Modify: `src/McpBooking.Infrastructure/DependencyInjection.cs`

- [ ] **Step 1: Write Application test**

Create `tests/McpBooking.Application.Tests/UseCases/ListBookingsUseCaseTests.cs`:

```csharp
using System.Text.Json;
using McpBooking.Application.DTOs;
using McpBooking.Application.UseCases;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace McpBooking.Application.Tests.UseCases;

[TestClass]
public class ListBookingsUseCaseTests
{
    [TestMethod]
    public async Task ExecuteAsync_ReturnsBookings_MappedToDtos()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.ListAsync(1, 20, null, null, null, null, null, null, null, null, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Booking>
            {
                new()
                {
                    Id = 1, BookingType = 2, Dates = ["2026-05-01"],
                    FormData = JsonDocument.Parse("{\"name\":\"Max\"}").RootElement,
                    Status = "approved", SortDate = "2026-05-01", ModificationDate = "2026-04-10",
                    IsNew = false, Note = "Test"
                }
            });
        var useCase = new ListBookingsUseCase(mock.Object);

        var result = await useCase.ExecuteAsync(1, 20);

        result.Count.ShouldBe(1);
        result[0].Id.ShouldBe(1);
        result[0].BookingType.ShouldBe(2);
        result[0].Status.ShouldBe("approved");
        result[0].Note.ShouldBe("Test");
    }

    [TestMethod]
    public async Task ExecuteAsync_EmptyList_ReturnsEmptyCollection()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.ListAsync(It.IsAny<int>(), It.IsAny<int>(),
                It.IsAny<int?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>(),
                It.IsAny<bool?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Booking>());
        var useCase = new ListBookingsUseCase(mock.Object);

        var result = await useCase.ExecuteAsync(1, 20);

        result.ShouldBeEmpty();
    }

    [TestMethod]
    public async Task ExecuteAsync_PassesAllFilterParameters()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.ListAsync(2, 10, 5, "approved", "2026-05-01", "2026-05-31",
                true, "test", "sort_date", "ASC", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Booking>());
        var useCase = new ListBookingsUseCase(mock.Object);

        await useCase.ExecuteAsync(2, 10, 5, "approved", "2026-05-01", "2026-05-31",
            true, "test", "sort_date", "ASC");

        mock.Verify(r => r.ListAsync(2, 10, 5, "approved", "2026-05-01", "2026-05-31",
            true, "test", "sort_date", "ASC", It.IsAny<CancellationToken>()), Times.Once);
    }
}
```

- [ ] **Step 2: Run test to verify it fails**

Run: `dotnet test tests/McpBooking.Application.Tests`
Expected: FAIL — `ListBookingsUseCase` does not exist.

- [ ] **Step 3: Write ListBookingsUseCase**

Create `src/McpBooking.Application/UseCases/ListBookingsUseCase.cs`:

```csharp
// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Application.DTOs;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;

namespace McpBooking.Application.UseCases;

/// <summary>
/// Use case for retrieving a filtered, paginated list of bookings.
/// </summary>
public class ListBookingsUseCase
{
    private readonly IBookingRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListBookingsUseCase"/> class.
    /// </summary>
    /// <param name="repository">The booking repository used to retrieve data.</param>
    public ListBookingsUseCase(IBookingRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Retrieves a filtered, paginated list of bookings from the repository.
    /// </summary>
    /// <param name="page">The page number (1-based).</param>
    /// <param name="perPage">The number of items per page (1-100).</param>
    /// <param name="resourceId">Optional filter by resource ID.</param>
    /// <param name="status">Optional filter by status.</param>
    /// <param name="dateFrom">Optional filter: start date (ISO 8601).</param>
    /// <param name="dateTo">Optional filter: end date (ISO 8601).</param>
    /// <param name="isNew">Optional filter by new/unread status.</param>
    /// <param name="search">Optional keyword search.</param>
    /// <param name="orderBy">Optional sort field.</param>
    /// <param name="order">Optional sort direction (ASC/DESC).</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>A read-only list of booking data transfer objects.</returns>
    public virtual async Task<IReadOnlyList<BookingDto>> ExecuteAsync(
        int page, int perPage,
        int? resourceId = null, string? status = null,
        string? dateFrom = null, string? dateTo = null,
        bool? isNew = null, string? search = null,
        string? orderBy = null, string? order = null,
        CancellationToken ct = default)
    {
        var bookings = await _repository.ListAsync(page, perPage,
            resourceId, status, dateFrom, dateTo,
            isNew, search, orderBy, order, ct);
        return bookings.Select(ToDto).ToList();
    }

    private static BookingDto ToDto(Booking b) => new(
        b.Id, b.BookingType, b.Dates, b.FormData, b.Status,
        b.SortDate, b.ModificationDate, b.IsNew, b.Note);
}
```

- [ ] **Step 4: Run test to verify it passes**

Run: `dotnet test tests/McpBooking.Application.Tests`
Expected: All tests PASS.

- [ ] **Step 5: Write Infrastructure test**

Create `tests/McpBooking.Infrastructure.Tests/Repositories/BookingRepositoryTests.cs`:

```csharp
using System.Net;
using System.Text.Json;
using McpBooking.Infrastructure.Http;
using McpBooking.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Shouldly;

namespace McpBooking.Infrastructure.Tests.Repositories;

[TestClass]
public class BookingRepositoryTests
{
    private static (BookingRepository repo, Mock<HttpMessageHandler> handlerMock) CreateRepoWithMockHttp(
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
            BaseAddress = new Uri("https://example.com/wp-json/wpbc/v1/")
        };
        var apiClient = new BookingApiClient(httpClient);
        var repo = new BookingRepository(apiClient);
        return (repo, handlerMock);
    }

    [TestMethod]
    public async Task ListAsync_ReturnsDeserializedBookings()
    {
        var json = JsonSerializer.Serialize(new[]
        {
            new
            {
                id = 1, booking_type = 2, dates = new[] { "2026-05-01" },
                form_data = new { name = "Max" }, status = "approved",
                sort_date = "2026-05-01", modification_date = "2026-04-10",
                is_new = false, note = "Test"
            }
        });
        var (repo, _) = CreateRepoWithMockHttp(HttpStatusCode.OK, json);

        var result = await repo.ListAsync(1, 20);

        result.Count.ShouldBe(1);
        result[0].Id.ShouldBe(1);
        result[0].BookingType.ShouldBe(2);
        result[0].Status.ShouldBe("approved");
    }

    [TestMethod]
    public async Task ListAsync_SendsCorrectQueryWithFilters()
    {
        var (repo, handlerMock) = CreateRepoWithMockHttp(HttpStatusCode.OK, "[]");

        await repo.ListAsync(2, 10, resourceId: 5, status: "approved",
            dateFrom: "2026-05-01", dateTo: "2026-05-31");

        handlerMock.Protected().Verify(
            "SendAsync", Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.RequestUri!.PathAndQuery.Contains("page=2") &&
                req.RequestUri.PathAndQuery.Contains("per_page=10") &&
                req.RequestUri.PathAndQuery.Contains("resource_id=5") &&
                req.RequestUri.PathAndQuery.Contains("status=approved") &&
                req.RequestUri.PathAndQuery.Contains("date_from=2026-05-01") &&
                req.RequestUri.PathAndQuery.Contains("date_to=2026-05-31")),
            ItExpr.IsAny<CancellationToken>());
    }

    [TestMethod]
    public async Task ListAsync_EmptyArray_ReturnsEmptyList()
    {
        var (repo, _) = CreateRepoWithMockHttp(HttpStatusCode.OK, "[]");

        var result = await repo.ListAsync(1, 20);

        result.ShouldBeEmpty();
    }
}
```

- [ ] **Step 6: Run test to verify it fails**

Run: `dotnet test tests/McpBooking.Infrastructure.Tests`
Expected: FAIL — `BookingRepository` does not exist.

- [ ] **Step 7: Write BookingRepository with ListAsync**

Create `src/McpBooking.Infrastructure/Repositories/BookingRepository.cs`:

```csharp
// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;
using McpBooking.Infrastructure.Http;
using McpBooking.Infrastructure.Properties;

namespace McpBooking.Infrastructure.Repositories;

/// <summary>
/// Retrieves and manages bookings via the WP Booking Calendar REST API.
/// </summary>
public class BookingRepository : IBookingRepository
{
    private readonly BookingApiClient _client;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookingRepository"/> class.
    /// </summary>
    /// <param name="client">The API client used to send HTTP requests.</param>
    public BookingRepository(BookingApiClient client)
    {
        _client = client;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<Booking>> ListAsync(int page, int perPage,
        int? resourceId, string? status, string? dateFrom, string? dateTo,
        bool? isNew, string? search, string? orderBy, string? order,
        CancellationToken ct)
    {
        var query = $"{Strings.ApiBookingsPath}{string.Format(Strings.QueryPageFormat, page, perPage)}";
        if (resourceId.HasValue)
            query += string.Format(Strings.QueryResourceIdFormat, resourceId.Value);
        if (status != null)
            query += string.Format(Strings.QueryStatusFormat, Uri.EscapeDataString(status));
        if (dateFrom != null)
            query += string.Format(Strings.QueryDateFromFormat, Uri.EscapeDataString(dateFrom));
        if (dateTo != null)
            query += string.Format(Strings.QueryDateToFormat, Uri.EscapeDataString(dateTo));
        if (isNew.HasValue)
            query += string.Format(Strings.QueryIsNewFormat, isNew.Value.ToString().ToLowerInvariant());
        if (search != null)
            query += string.Format(Strings.QuerySearchFormat, Uri.EscapeDataString(search));
        if (orderBy != null)
            query += string.Format(Strings.QueryOrderByFormat, Uri.EscapeDataString(orderBy));
        if (order != null)
            query += string.Format(Strings.QueryOrderFormat, Uri.EscapeDataString(order));

        return await _client.GetAsync<List<Booking>>(query, ct) ?? [];
    }

    /// <inheritdoc />
    public async Task<Booking?> GetAsync(int id, CancellationToken ct)
    {
        return await _client.GetAsync<Booking>(
            string.Format(Strings.ApiBookingByIdPath, id), ct);
    }

    /// <inheritdoc />
    public async Task<Booking> CreateAsync(int bookingType, string formDataJson,
        string datesJson, CancellationToken ct)
    {
        var body = new
        {
            booking_type = bookingType,
            form_data = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(formDataJson),
            dates = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(datesJson)
        };
        return (await _client.PostAsync<Booking>(Strings.ApiBookingsPath, body, ct))!;
    }

    /// <inheritdoc />
    public async Task<Booking> UpdateAsync(int id, string? formDataJson,
        int? bookingType, string? status, CancellationToken ct)
    {
        var dict = new Dictionary<string, object>();
        if (formDataJson != null)
            dict["form_data"] = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(formDataJson);
        if (bookingType.HasValue)
            dict["booking_type"] = bookingType.Value;
        if (status != null)
            dict["status"] = status;

        return (await _client.PutAsync<Booking>(
            string.Format(Strings.ApiBookingByIdPath, id), dict, ct))!;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        await _client.DeleteAsync(
            string.Format(Strings.ApiBookingByIdPath, id), ct);
    }

    /// <inheritdoc />
    public async Task<Booking> ApproveAsync(int id, CancellationToken ct)
    {
        return (await _client.PostAsync<Booking>(
            string.Format(Strings.ApiBookingApprovePath, id), ct))!;
    }

    /// <inheritdoc />
    public async Task<Booking> SetPendingAsync(int id, CancellationToken ct)
    {
        return (await _client.PostAsync<Booking>(
            string.Format(Strings.ApiBookingPendingPath, id), ct))!;
    }

    /// <inheritdoc />
    public async Task<Booking> UpdateNoteAsync(int id, string note, CancellationToken ct)
    {
        return (await _client.PutAsync<Booking>(
            string.Format(Strings.ApiBookingNotePath, id), new { note }, ct))!;
    }
}
```

- [ ] **Step 8: Register IBookingRepository in DI**

Add to `src/McpBooking.Infrastructure/DependencyInjection.cs` after the existing `AddScoped<IResourceRepository, ResourceRepository>()` line:

```csharp
        services.AddScoped<IBookingRepository, BookingRepository>();
```

Also add the using at the top if not already present — it should already be covered since `IBookingRepository` is in the same namespace as `IResourceRepository`.

- [ ] **Step 9: Run test to verify it passes**

Run: `dotnet test tests/McpBooking.Infrastructure.Tests`
Expected: All tests PASS.

- [ ] **Step 10: Write Server test**

Create `tests/McpBooking.Server.Tests/Tools/ListBookingsToolTests.cs`:

```csharp
using System.Net;
using System.Text.Json;
using McpBooking.Application.DTOs;
using McpBooking.Application.UseCases;
using McpBooking.Domain.Interfaces;
using McpBooking.Server.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace McpBooking.Server.Tests.Tools;

[TestClass]
public class ListBookingsToolTests
{
    private static ListBookingsTool CreateToolWithMockUseCase(
        IReadOnlyList<BookingDto>? result = null, Exception? exception = null)
    {
        var useCaseMock = new Mock<ListBookingsUseCase>(Mock.Of<IBookingRepository>());
        if (exception != null)
        {
            useCaseMock.Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<int?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>(),
                    It.IsAny<bool?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);
        }
        else
        {
            useCaseMock.Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<int?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>(),
                    It.IsAny<bool?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(result ?? new List<BookingDto>());
        }
        return new ListBookingsTool(useCaseMock.Object);
    }

    [TestMethod]
    public async Task ExecuteAsync_ValidParams_ReturnsJson()
    {
        var bookings = new List<BookingDto>
        {
            new(1, 2, ["2026-05-01"], null, "approved", null, null, null, null)
        };
        var tool = CreateToolWithMockUseCase(bookings);

        var result = await tool.ExecuteAsync();

        var doc = JsonDocument.Parse(result);
        doc.RootElement.GetArrayLength().ShouldBe(1);
        doc.RootElement[0].GetProperty("id").GetInt32().ShouldBe(1);
    }

    [TestMethod]
    public async Task ExecuteAsync_InvalidPage_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(page: 0);

        result.ShouldContain("page");
    }

    [TestMethod]
    public async Task ExecuteAsync_InvalidPerPage_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(perPage: 101);

        result.ShouldContain("per_page");
    }

    [TestMethod]
    public async Task ExecuteAsync_AuthError_ReturnsReadableMessage()
    {
        var tool = CreateToolWithMockUseCase(
            exception: new HttpRequestException("Unauthorized", null, HttpStatusCode.Unauthorized));

        var result = await tool.ExecuteAsync();

        result.ShouldContain("Authentifizierung");
    }
}
```

- [ ] **Step 11: Run test to verify it fails**

Run: `dotnet test tests/McpBooking.Server.Tests`
Expected: FAIL — `ListBookingsTool` does not exist.

- [ ] **Step 12: Write ListBookingsTool**

Create `src/McpBooking.Server/Tools/ListBookingsTool.cs`:

```csharp
// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.ComponentModel;
using System.Net;
using System.Text.Json;
using McpBooking.Application.UseCases;
using McpBooking.Server.Properties;
using ModelContextProtocol.Server;

namespace McpBooking.Server.Tools;

/// <summary>
/// MCP tool that lists bookings with optional filters.
/// </summary>
[McpServerToolType]
public class ListBookingsTool
{
    private readonly ListBookingsUseCase _useCase;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListBookingsTool"/> class.
    /// </summary>
    /// <param name="useCase">The use case that retrieves the booking list.</param>
    public ListBookingsTool(ListBookingsUseCase useCase)
    {
        _useCase = useCase;
    }

    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    /// <summary>
    /// Lists bookings from the WP Booking Calendar API with optional filters.
    /// </summary>
    /// <param name="page">The page number (1-based, default: 1).</param>
    /// <param name="perPage">The number of items per page (1-100, default: 20).</param>
    /// <param name="resourceId">Optional filter by resource ID.</param>
    /// <param name="status">Optional filter by status (pending/approved/trash).</param>
    /// <param name="dateFrom">Optional filter: start date (ISO 8601).</param>
    /// <param name="dateTo">Optional filter: end date (ISO 8601).</param>
    /// <param name="isNew">Optional filter by new/unread status.</param>
    /// <param name="search">Optional keyword search.</param>
    /// <param name="orderBy">Optional sort field (booking_id/sort_date/modification_date).</param>
    /// <param name="order">Optional sort direction (ASC/DESC).</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>A JSON string with the list of bookings, or a localized error message.</returns>
    [McpServerTool(Name = "list_bookings"), Description("Listet Buchungen mit optionalen Filtern auf.")]
    public async Task<string> ExecuteAsync(
        [Description("Seite (Standard: 1)")] int page = 1,
        [Description("Einträge pro Seite (Standard: 20, Max: 100)")] int perPage = 20,
        [Description("Filter nach Ressourcen-ID")] int? resourceId = null,
        [Description("Filter nach Status (pending/approved/trash)")] string? status = null,
        [Description("Filter ab Datum (ISO 8601)")] string? dateFrom = null,
        [Description("Filter bis Datum (ISO 8601)")] string? dateTo = null,
        [Description("Filter nach neu/ungelesen")] bool? isNew = null,
        [Description("Stichwortsuche")] string? search = null,
        [Description("Sortierung (booking_id/sort_date/modification_date)")] string? orderBy = null,
        [Description("Richtung (ASC/DESC)")] string? order = null,
        CancellationToken ct = default)
    {
        if (page < 1) return Messages.ErrorPageInvalid;
        if (perPage < 1 || perPage > 100) return Messages.ErrorPerPageInvalid;

        try
        {
            var bookings = await _useCase.ExecuteAsync(page, perPage,
                resourceId, status, dateFrom, dateTo,
                isNew, search, orderBy, order, ct);
            return JsonSerializer.Serialize(bookings, s_jsonOptions);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            return Messages.ErrorAuthenticationFailed;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Forbidden)
        {
            return Messages.ErrorForbidden;
        }
        catch (HttpRequestException ex) when (ex.StatusCode >= HttpStatusCode.InternalServerError)
        {
            return Messages.ErrorServerError;
        }
        catch (HttpRequestException)
        {
            return Messages.ErrorApiUnreachable;
        }
    }
}
```

- [ ] **Step 13: Run all tests**

Run: `dotnet test`
Expected: All tests PASS (10 existing + new tests).

- [ ] **Step 14: Commit**

```bash
git add src/McpBooking.Application/UseCases/ListBookingsUseCase.cs src/McpBooking.Infrastructure/Repositories/BookingRepository.cs src/McpBooking.Infrastructure/DependencyInjection.cs src/McpBooking.Server/Tools/ListBookingsTool.cs tests/McpBooking.Application.Tests/UseCases/ListBookingsUseCaseTests.cs tests/McpBooking.Infrastructure.Tests/Repositories/BookingRepositoryTests.cs tests/McpBooking.Server.Tests/Tools/ListBookingsToolTests.cs
git commit -m "feat(US-006): add list_bookings tool with TDD"
```

---

### Task 5: get_booking — Vertical Slice

**Files:**
- Create: `src/McpBooking.Application/UseCases/GetBookingUseCase.cs`
- Create: `src/McpBooking.Server/Tools/GetBookingTool.cs`
- Create: `tests/McpBooking.Application.Tests/UseCases/GetBookingUseCaseTests.cs`
- Create: `tests/McpBooking.Server.Tests/Tools/GetBookingToolTests.cs`

Note: `BookingRepository.GetAsync` was already written in Task 4 (the full repository).

- [ ] **Step 1: Write Application test**

Create `tests/McpBooking.Application.Tests/UseCases/GetBookingUseCaseTests.cs`:

```csharp
using System.Text.Json;
using McpBooking.Application.UseCases;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace McpBooking.Application.Tests.UseCases;

[TestClass]
public class GetBookingUseCaseTests
{
    [TestMethod]
    public async Task ExecuteAsync_BookingExists_ReturnsMappedDto()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.GetAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking
            {
                Id = 1, BookingType = 2, Dates = ["2026-05-01"],
                Status = "approved", Note = "Test"
            });
        var useCase = new GetBookingUseCase(mock.Object);

        var result = await useCase.ExecuteAsync(1);

        result.ShouldNotBeNull();
        result!.Id.ShouldBe(1);
        result.Status.ShouldBe("approved");
    }

    [TestMethod]
    public async Task ExecuteAsync_BookingNotFound_ReturnsNull()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.GetAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Booking?)null);
        var useCase = new GetBookingUseCase(mock.Object);

        var result = await useCase.ExecuteAsync(999);

        result.ShouldBeNull();
    }
}
```

- [ ] **Step 2: Run test to verify it fails**

Run: `dotnet test tests/McpBooking.Application.Tests`
Expected: FAIL — `GetBookingUseCase` does not exist.

- [ ] **Step 3: Write GetBookingUseCase**

Create `src/McpBooking.Application/UseCases/GetBookingUseCase.cs`:

```csharp
// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Application.DTOs;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;

namespace McpBooking.Application.UseCases;

/// <summary>
/// Use case for retrieving a single booking by its identifier.
/// </summary>
public class GetBookingUseCase
{
    private readonly IBookingRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBookingUseCase"/> class.
    /// </summary>
    /// <param name="repository">The booking repository used to retrieve data.</param>
    public GetBookingUseCase(IBookingRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Retrieves a single booking from the repository.
    /// </summary>
    /// <param name="id">The booking ID.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>The booking DTO, or <see langword="null"/> if not found.</returns>
    public virtual async Task<BookingDto?> ExecuteAsync(int id, CancellationToken ct = default)
    {
        var booking = await _repository.GetAsync(id, ct);
        return booking is null ? null : ToDto(booking);
    }

    private static BookingDto ToDto(Booking b) => new(
        b.Id, b.BookingType, b.Dates, b.FormData, b.Status,
        b.SortDate, b.ModificationDate, b.IsNew, b.Note);
}
```

- [ ] **Step 4: Run test to verify it passes**

Run: `dotnet test tests/McpBooking.Application.Tests`
Expected: All tests PASS.

- [ ] **Step 5: Write Server test**

Create `tests/McpBooking.Server.Tests/Tools/GetBookingToolTests.cs`:

```csharp
using System.Net;
using System.Text.Json;
using McpBooking.Application.DTOs;
using McpBooking.Application.UseCases;
using McpBooking.Domain.Interfaces;
using McpBooking.Server.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace McpBooking.Server.Tests.Tools;

[TestClass]
public class GetBookingToolTests
{
    private static GetBookingTool CreateToolWithMockUseCase(
        BookingDto? result = null, Exception? exception = null)
    {
        var useCaseMock = new Mock<GetBookingUseCase>(Mock.Of<IBookingRepository>());
        if (exception != null)
        {
            useCaseMock.Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);
        }
        else
        {
            useCaseMock.Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);
        }
        return new GetBookingTool(useCaseMock.Object);
    }

    [TestMethod]
    public async Task ExecuteAsync_ValidId_ReturnsJson()
    {
        var booking = new BookingDto(1, 2, ["2026-05-01"], null, "approved", null, null, null, null);
        var tool = CreateToolWithMockUseCase(booking);

        var result = await tool.ExecuteAsync(1);

        var doc = JsonDocument.Parse(result);
        doc.RootElement.GetProperty("id").GetInt32().ShouldBe(1);
    }

    [TestMethod]
    public async Task ExecuteAsync_InvalidId_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(0);

        result.ShouldContain("ID");
    }

    [TestMethod]
    public async Task ExecuteAsync_NotFound_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase(result: null);

        var result = await tool.ExecuteAsync(999);

        result.ShouldContain("999");
    }

    [TestMethod]
    public async Task ExecuteAsync_AuthError_ReturnsReadableMessage()
    {
        var tool = CreateToolWithMockUseCase(
            exception: new HttpRequestException("Unauthorized", null, HttpStatusCode.Unauthorized));

        var result = await tool.ExecuteAsync(1);

        result.ShouldContain("Authentifizierung");
    }
}
```

- [ ] **Step 6: Run test to verify it fails**

Run: `dotnet test tests/McpBooking.Server.Tests`
Expected: FAIL — `GetBookingTool` does not exist.

- [ ] **Step 7: Write GetBookingTool**

Create `src/McpBooking.Server/Tools/GetBookingTool.cs`:

```csharp
// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.ComponentModel;
using System.Net;
using System.Text.Json;
using McpBooking.Application.UseCases;
using McpBooking.Server.Properties;
using ModelContextProtocol.Server;

namespace McpBooking.Server.Tools;

/// <summary>
/// MCP tool that retrieves a single booking by its ID.
/// </summary>
[McpServerToolType]
public class GetBookingTool
{
    private readonly GetBookingUseCase _useCase;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBookingTool"/> class.
    /// </summary>
    /// <param name="useCase">The use case that retrieves a booking.</param>
    public GetBookingTool(GetBookingUseCase useCase)
    {
        _useCase = useCase;
    }

    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    /// <summary>
    /// Retrieves a single booking from the WP Booking Calendar API.
    /// </summary>
    /// <param name="id">The booking ID.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>A JSON string with the booking, or a localized error message.</returns>
    [McpServerTool(Name = "get_booking"), Description("Ruft eine einzelne Buchung ab.")]
    public async Task<string> ExecuteAsync(
        [Description("Buchungs-ID")] int id,
        CancellationToken ct = default)
    {
        if (id < 1) return Messages.ErrorInvalidId;

        try
        {
            var booking = await _useCase.ExecuteAsync(id, ct);
            if (booking is null)
                return string.Format(Messages.ErrorBookingNotFound, id);
            return JsonSerializer.Serialize(booking, s_jsonOptions);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            return Messages.ErrorAuthenticationFailed;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Forbidden)
        {
            return Messages.ErrorForbidden;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return string.Format(Messages.ErrorBookingNotFound, id);
        }
        catch (HttpRequestException ex) when (ex.StatusCode >= HttpStatusCode.InternalServerError)
        {
            return Messages.ErrorServerError;
        }
        catch (HttpRequestException)
        {
            return Messages.ErrorApiUnreachable;
        }
    }
}
```

- [ ] **Step 8: Run all tests**

Run: `dotnet test`
Expected: All tests PASS.

- [ ] **Step 9: Commit**

```bash
git add src/McpBooking.Application/UseCases/GetBookingUseCase.cs src/McpBooking.Server/Tools/GetBookingTool.cs tests/McpBooking.Application.Tests/UseCases/GetBookingUseCaseTests.cs tests/McpBooking.Server.Tests/Tools/GetBookingToolTests.cs
git commit -m "feat(US-008): add get_booking tool with TDD"
```

---

### Task 6: create_booking — Vertical Slice

**Files:**
- Create: `src/McpBooking.Application/UseCases/CreateBookingUseCase.cs`
- Create: `src/McpBooking.Server/Tools/CreateBookingTool.cs`
- Create: `tests/McpBooking.Application.Tests/UseCases/CreateBookingUseCaseTests.cs`
- Create: `tests/McpBooking.Server.Tests/Tools/CreateBookingToolTests.cs`

- [ ] **Step 1: Write Application test**

Create `tests/McpBooking.Application.Tests/UseCases/CreateBookingUseCaseTests.cs`:

```csharp
using McpBooking.Application.UseCases;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace McpBooking.Application.Tests.UseCases;

[TestClass]
public class CreateBookingUseCaseTests
{
    [TestMethod]
    public async Task ExecuteAsync_ReturnsCreatedBookingAsDto()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.CreateAsync(2, "{\"name\":\"Max\"}", "[\"2026-05-01\"]", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking
            {
                Id = 42, BookingType = 2, Dates = ["2026-05-01"],
                Status = "pending"
            });
        var useCase = new CreateBookingUseCase(mock.Object);

        var result = await useCase.ExecuteAsync(2, "{\"name\":\"Max\"}", "[\"2026-05-01\"]");

        result.Id.ShouldBe(42);
        result.BookingType.ShouldBe(2);
        result.Status.ShouldBe("pending");
    }

    [TestMethod]
    public async Task ExecuteAsync_DelegatesCorrectParameters()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.CreateAsync(3, "{}", "[]", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking { Id = 1, Status = "pending" });
        var useCase = new CreateBookingUseCase(mock.Object);

        await useCase.ExecuteAsync(3, "{}", "[]");

        mock.Verify(r => r.CreateAsync(3, "{}", "[]", It.IsAny<CancellationToken>()), Times.Once);
    }
}
```

- [ ] **Step 2: Run test to verify it fails**

Run: `dotnet test tests/McpBooking.Application.Tests`
Expected: FAIL — `CreateBookingUseCase` does not exist.

- [ ] **Step 3: Write CreateBookingUseCase**

Create `src/McpBooking.Application/UseCases/CreateBookingUseCase.cs`:

```csharp
// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Application.DTOs;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;

namespace McpBooking.Application.UseCases;

/// <summary>
/// Use case for creating a new booking.
/// </summary>
public class CreateBookingUseCase
{
    private readonly IBookingRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateBookingUseCase"/> class.
    /// </summary>
    /// <param name="repository">The booking repository used to create bookings.</param>
    public CreateBookingUseCase(IBookingRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Creates a new booking via the repository.
    /// </summary>
    /// <param name="bookingType">The resource/booking type ID.</param>
    /// <param name="formDataJson">Form data as a JSON string.</param>
    /// <param name="datesJson">Dates as a JSON array string.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>The created booking as a DTO.</returns>
    public virtual async Task<BookingDto> ExecuteAsync(
        int bookingType, string formDataJson, string datesJson,
        CancellationToken ct = default)
    {
        var booking = await _repository.CreateAsync(bookingType, formDataJson, datesJson, ct);
        return ToDto(booking);
    }

    private static BookingDto ToDto(Booking b) => new(
        b.Id, b.BookingType, b.Dates, b.FormData, b.Status,
        b.SortDate, b.ModificationDate, b.IsNew, b.Note);
}
```

- [ ] **Step 4: Run test to verify it passes**

Run: `dotnet test tests/McpBooking.Application.Tests`
Expected: All tests PASS.

- [ ] **Step 5: Write Server test**

Create `tests/McpBooking.Server.Tests/Tools/CreateBookingToolTests.cs`:

```csharp
using System.Net;
using System.Text.Json;
using McpBooking.Application.DTOs;
using McpBooking.Application.UseCases;
using McpBooking.Domain.Interfaces;
using McpBooking.Server.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace McpBooking.Server.Tests.Tools;

[TestClass]
public class CreateBookingToolTests
{
    private static CreateBookingTool CreateToolWithMockUseCase(
        BookingDto? result = null, Exception? exception = null)
    {
        var useCaseMock = new Mock<CreateBookingUseCase>(Mock.Of<IBookingRepository>());
        if (exception != null)
        {
            useCaseMock.Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);
        }
        else
        {
            useCaseMock.Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result ?? new BookingDto(1, 1, [], null, "pending", null, null, null, null));
        }
        return new CreateBookingTool(useCaseMock.Object);
    }

    [TestMethod]
    public async Task ExecuteAsync_ValidParams_ReturnsJson()
    {
        var booking = new BookingDto(42, 2, ["2026-05-01"], null, "pending", null, null, null, null);
        var tool = CreateToolWithMockUseCase(booking);

        var result = await tool.ExecuteAsync(2, "{\"name\":\"Max\"}", "[\"2026-05-01\"]");

        var doc = JsonDocument.Parse(result);
        doc.RootElement.GetProperty("id").GetInt32().ShouldBe(42);
    }

    [TestMethod]
    public async Task ExecuteAsync_InvalidBookingType_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(0, "{}", "[]");

        result.ShouldContain("Buchungstyp");
    }

    [TestMethod]
    public async Task ExecuteAsync_InvalidFormDataJson_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(1, "not-json", "[]");

        result.ShouldContain("formDataJson");
    }

    [TestMethod]
    public async Task ExecuteAsync_InvalidDatesJson_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(1, "{}", "not-json");

        result.ShouldContain("datesJson");
    }

    [TestMethod]
    public async Task ExecuteAsync_AuthError_ReturnsReadableMessage()
    {
        var tool = CreateToolWithMockUseCase(
            exception: new HttpRequestException("Unauthorized", null, HttpStatusCode.Unauthorized));

        var result = await tool.ExecuteAsync(1, "{}", "[]");

        result.ShouldContain("Authentifizierung");
    }
}
```

- [ ] **Step 6: Run test to verify it fails**

Run: `dotnet test tests/McpBooking.Server.Tests`
Expected: FAIL — `CreateBookingTool` does not exist.

- [ ] **Step 7: Write CreateBookingTool**

Create `src/McpBooking.Server/Tools/CreateBookingTool.cs`:

```csharp
// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.ComponentModel;
using System.Net;
using System.Text.Json;
using McpBooking.Application.UseCases;
using McpBooking.Server.Properties;
using ModelContextProtocol.Server;

namespace McpBooking.Server.Tools;

/// <summary>
/// MCP tool that creates a new booking.
/// </summary>
[McpServerToolType]
public class CreateBookingTool
{
    private readonly CreateBookingUseCase _useCase;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateBookingTool"/> class.
    /// </summary>
    /// <param name="useCase">The use case that creates a booking.</param>
    public CreateBookingTool(CreateBookingUseCase useCase)
    {
        _useCase = useCase;
    }

    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    /// <summary>
    /// Creates a new booking in the WP Booking Calendar API.
    /// </summary>
    /// <param name="bookingType">The resource/booking type ID.</param>
    /// <param name="formDataJson">Form data as a JSON string.</param>
    /// <param name="datesJson">Booking dates as a JSON array string.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>A JSON string with the created booking, or a localized error message.</returns>
    [McpServerTool(Name = "create_booking"), Description("Erstellt eine neue Buchung.")]
    public async Task<string> ExecuteAsync(
        [Description("Ressourcen-/Buchungstyp-ID")] int bookingType,
        [Description("Formulardaten als JSON-String")] string formDataJson,
        [Description("Buchungsdaten als JSON-Array-String")] string datesJson,
        CancellationToken ct = default)
    {
        if (bookingType < 1) return Messages.ErrorInvalidBookingType;
        if (!IsValidJson(formDataJson))
            return string.Format(Messages.ErrorInvalidJson, "formDataJson");
        if (!IsValidJson(datesJson))
            return string.Format(Messages.ErrorInvalidJson, "datesJson");

        try
        {
            var booking = await _useCase.ExecuteAsync(bookingType, formDataJson, datesJson, ct);
            return JsonSerializer.Serialize(booking, s_jsonOptions);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            return Messages.ErrorAuthenticationFailed;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Forbidden)
        {
            return Messages.ErrorForbidden;
        }
        catch (HttpRequestException ex) when (ex.StatusCode >= HttpStatusCode.InternalServerError)
        {
            return Messages.ErrorServerError;
        }
        catch (HttpRequestException)
        {
            return Messages.ErrorApiUnreachable;
        }
    }

    private static bool IsValidJson(string json)
    {
        try
        {
            JsonDocument.Parse(json);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}
```

- [ ] **Step 8: Run all tests**

Run: `dotnet test`
Expected: All tests PASS.

- [ ] **Step 9: Commit**

```bash
git add src/McpBooking.Application/UseCases/CreateBookingUseCase.cs src/McpBooking.Server/Tools/CreateBookingTool.cs tests/McpBooking.Application.Tests/UseCases/CreateBookingUseCaseTests.cs tests/McpBooking.Server.Tests/Tools/CreateBookingToolTests.cs
git commit -m "feat(US-007): add create_booking tool with TDD"
```

---

### Task 7: update_booking — Vertical Slice

**Files:**
- Create: `src/McpBooking.Application/UseCases/UpdateBookingUseCase.cs`
- Create: `src/McpBooking.Server/Tools/UpdateBookingTool.cs`
- Create: `tests/McpBooking.Application.Tests/UseCases/UpdateBookingUseCaseTests.cs`
- Create: `tests/McpBooking.Server.Tests/Tools/UpdateBookingToolTests.cs`

- [ ] **Step 1: Write Application test**

Create `tests/McpBooking.Application.Tests/UseCases/UpdateBookingUseCaseTests.cs`:

```csharp
using McpBooking.Application.UseCases;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace McpBooking.Application.Tests.UseCases;

[TestClass]
public class UpdateBookingUseCaseTests
{
    [TestMethod]
    public async Task ExecuteAsync_ReturnsUpdatedBookingAsDto()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.UpdateAsync(1, null, null, "approved", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking { Id = 1, BookingType = 2, Status = "approved" });
        var useCase = new UpdateBookingUseCase(mock.Object);

        var result = await useCase.ExecuteAsync(1, status: "approved");

        result.Id.ShouldBe(1);
        result.Status.ShouldBe("approved");
    }

    [TestMethod]
    public async Task ExecuteAsync_DelegatesAllParameters()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.UpdateAsync(5, "{}", 3, "pending", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking { Id = 5, Status = "pending" });
        var useCase = new UpdateBookingUseCase(mock.Object);

        await useCase.ExecuteAsync(5, "{}", 3, "pending");

        mock.Verify(r => r.UpdateAsync(5, "{}", 3, "pending", It.IsAny<CancellationToken>()), Times.Once);
    }
}
```

- [ ] **Step 2: Run test to verify it fails**

Run: `dotnet test tests/McpBooking.Application.Tests`
Expected: FAIL — `UpdateBookingUseCase` does not exist.

- [ ] **Step 3: Write UpdateBookingUseCase**

Create `src/McpBooking.Application/UseCases/UpdateBookingUseCase.cs`:

```csharp
// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Application.DTOs;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;

namespace McpBooking.Application.UseCases;

/// <summary>
/// Use case for updating an existing booking.
/// </summary>
public class UpdateBookingUseCase
{
    private readonly IBookingRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateBookingUseCase"/> class.
    /// </summary>
    /// <param name="repository">The booking repository used to update bookings.</param>
    public UpdateBookingUseCase(IBookingRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Updates an existing booking via the repository.
    /// </summary>
    /// <param name="id">The booking ID.</param>
    /// <param name="formDataJson">Optional updated form data as JSON.</param>
    /// <param name="bookingType">Optional new booking type.</param>
    /// <param name="status">Optional new status.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>The updated booking as a DTO.</returns>
    public virtual async Task<BookingDto> ExecuteAsync(
        int id, string? formDataJson = null, int? bookingType = null,
        string? status = null, CancellationToken ct = default)
    {
        var booking = await _repository.UpdateAsync(id, formDataJson, bookingType, status, ct);
        return ToDto(booking);
    }

    private static BookingDto ToDto(Booking b) => new(
        b.Id, b.BookingType, b.Dates, b.FormData, b.Status,
        b.SortDate, b.ModificationDate, b.IsNew, b.Note);
}
```

- [ ] **Step 4: Run test to verify it passes**

Run: `dotnet test tests/McpBooking.Application.Tests`
Expected: All tests PASS.

- [ ] **Step 5: Write Server test**

Create `tests/McpBooking.Server.Tests/Tools/UpdateBookingToolTests.cs`:

```csharp
using System.Net;
using System.Text.Json;
using McpBooking.Application.DTOs;
using McpBooking.Application.UseCases;
using McpBooking.Domain.Interfaces;
using McpBooking.Server.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace McpBooking.Server.Tests.Tools;

[TestClass]
public class UpdateBookingToolTests
{
    private static UpdateBookingTool CreateToolWithMockUseCase(
        BookingDto? result = null, Exception? exception = null)
    {
        var useCaseMock = new Mock<UpdateBookingUseCase>(Mock.Of<IBookingRepository>());
        if (exception != null)
        {
            useCaseMock.Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<string?>(),
                    It.IsAny<int?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);
        }
        else
        {
            useCaseMock.Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<string?>(),
                    It.IsAny<int?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result ?? new BookingDto(1, 1, [], null, "approved", null, null, null, null));
        }
        return new UpdateBookingTool(useCaseMock.Object);
    }

    [TestMethod]
    public async Task ExecuteAsync_ValidParams_ReturnsJson()
    {
        var booking = new BookingDto(1, 2, ["2026-05-01"], null, "approved", null, null, null, null);
        var tool = CreateToolWithMockUseCase(booking);

        var result = await tool.ExecuteAsync(1, status: "approved");

        var doc = JsonDocument.Parse(result);
        doc.RootElement.GetProperty("id").GetInt32().ShouldBe(1);
    }

    [TestMethod]
    public async Task ExecuteAsync_InvalidId_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(0);

        result.ShouldContain("ID");
    }

    [TestMethod]
    public async Task ExecuteAsync_InvalidFormDataJson_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(1, formDataJson: "not-json");

        result.ShouldContain("formDataJson");
    }

    [TestMethod]
    public async Task ExecuteAsync_NotFound_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase(
            exception: new HttpRequestException("Not Found", null, HttpStatusCode.NotFound));

        var result = await tool.ExecuteAsync(999);

        result.ShouldContain("999");
    }
}
```

- [ ] **Step 6: Run test to verify it fails**

Run: `dotnet test tests/McpBooking.Server.Tests`
Expected: FAIL — `UpdateBookingTool` does not exist.

- [ ] **Step 7: Write UpdateBookingTool**

Create `src/McpBooking.Server/Tools/UpdateBookingTool.cs`:

```csharp
// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.ComponentModel;
using System.Net;
using System.Text.Json;
using McpBooking.Application.UseCases;
using McpBooking.Server.Properties;
using ModelContextProtocol.Server;

namespace McpBooking.Server.Tools;

/// <summary>
/// MCP tool that updates an existing booking.
/// </summary>
[McpServerToolType]
public class UpdateBookingTool
{
    private readonly UpdateBookingUseCase _useCase;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateBookingTool"/> class.
    /// </summary>
    /// <param name="useCase">The use case that updates a booking.</param>
    public UpdateBookingTool(UpdateBookingUseCase useCase)
    {
        _useCase = useCase;
    }

    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    /// <summary>
    /// Updates an existing booking in the WP Booking Calendar API.
    /// </summary>
    /// <param name="id">The booking ID.</param>
    /// <param name="formDataJson">Optional updated form data as JSON string.</param>
    /// <param name="bookingType">Optional new booking type.</param>
    /// <param name="status">Optional new status.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>A JSON string with the updated booking, or a localized error message.</returns>
    [McpServerTool(Name = "update_booking"), Description("Aktualisiert eine bestehende Buchung.")]
    public async Task<string> ExecuteAsync(
        [Description("Buchungs-ID")] int id,
        [Description("Formulardaten als JSON-String")] string? formDataJson = null,
        [Description("Neuer Buchungstyp")] int? bookingType = null,
        [Description("Neuer Status")] string? status = null,
        CancellationToken ct = default)
    {
        if (id < 1) return Messages.ErrorInvalidId;
        if (bookingType.HasValue && bookingType.Value < 1)
            return Messages.ErrorInvalidBookingType;
        if (formDataJson != null && !IsValidJson(formDataJson))
            return string.Format(Messages.ErrorInvalidJson, "formDataJson");

        try
        {
            var booking = await _useCase.ExecuteAsync(id, formDataJson, bookingType, status, ct);
            return JsonSerializer.Serialize(booking, s_jsonOptions);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            return Messages.ErrorAuthenticationFailed;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Forbidden)
        {
            return Messages.ErrorForbidden;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return string.Format(Messages.ErrorBookingNotFound, id);
        }
        catch (HttpRequestException ex) when (ex.StatusCode >= HttpStatusCode.InternalServerError)
        {
            return Messages.ErrorServerError;
        }
        catch (HttpRequestException)
        {
            return Messages.ErrorApiUnreachable;
        }
    }

    private static bool IsValidJson(string json)
    {
        try
        {
            JsonDocument.Parse(json);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}
```

- [ ] **Step 8: Run all tests**

Run: `dotnet test`
Expected: All tests PASS.

- [ ] **Step 9: Commit**

```bash
git add src/McpBooking.Application/UseCases/UpdateBookingUseCase.cs src/McpBooking.Server/Tools/UpdateBookingTool.cs tests/McpBooking.Application.Tests/UseCases/UpdateBookingUseCaseTests.cs tests/McpBooking.Server.Tests/Tools/UpdateBookingToolTests.cs
git commit -m "feat(US-009): add update_booking tool with TDD"
```

---

### Task 8: delete_booking — Vertical Slice

**Files:**
- Create: `src/McpBooking.Application/UseCases/DeleteBookingUseCase.cs`
- Create: `src/McpBooking.Server/Tools/DeleteBookingTool.cs`
- Create: `tests/McpBooking.Application.Tests/UseCases/DeleteBookingUseCaseTests.cs`
- Create: `tests/McpBooking.Server.Tests/Tools/DeleteBookingToolTests.cs`

- [ ] **Step 1: Write Application test**

Create `tests/McpBooking.Application.Tests/UseCases/DeleteBookingUseCaseTests.cs`:

```csharp
using McpBooking.Application.UseCases;
using McpBooking.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace McpBooking.Application.Tests.UseCases;

[TestClass]
public class DeleteBookingUseCaseTests
{
    [TestMethod]
    public async Task ExecuteAsync_DelegatesToRepository()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.DeleteAsync(5, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        var useCase = new DeleteBookingUseCase(mock.Object);

        await useCase.ExecuteAsync(5);

        mock.Verify(r => r.DeleteAsync(5, It.IsAny<CancellationToken>()), Times.Once);
    }
}
```

- [ ] **Step 2: Run test to verify it fails**

Run: `dotnet test tests/McpBooking.Application.Tests`
Expected: FAIL — `DeleteBookingUseCase` does not exist.

- [ ] **Step 3: Write DeleteBookingUseCase**

Create `src/McpBooking.Application/UseCases/DeleteBookingUseCase.cs`:

```csharp
// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Domain.Interfaces;

namespace McpBooking.Application.UseCases;

/// <summary>
/// Use case for deleting a booking.
/// </summary>
public class DeleteBookingUseCase
{
    private readonly IBookingRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteBookingUseCase"/> class.
    /// </summary>
    /// <param name="repository">The booking repository used to delete bookings.</param>
    public DeleteBookingUseCase(IBookingRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Deletes a booking via the repository.
    /// </summary>
    /// <param name="id">The booking ID.</param>
    /// <param name="ct">A cancellation token.</param>
    public virtual async Task ExecuteAsync(int id, CancellationToken ct = default)
    {
        await _repository.DeleteAsync(id, ct);
    }
}
```

- [ ] **Step 4: Run test to verify it passes**

Run: `dotnet test tests/McpBooking.Application.Tests`
Expected: All tests PASS.

- [ ] **Step 5: Write Server test**

Create `tests/McpBooking.Server.Tests/Tools/DeleteBookingToolTests.cs`:

```csharp
using System.Net;
using McpBooking.Application.UseCases;
using McpBooking.Domain.Interfaces;
using McpBooking.Server.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace McpBooking.Server.Tests.Tools;

[TestClass]
public class DeleteBookingToolTests
{
    private static DeleteBookingTool CreateToolWithMockUseCase(Exception? exception = null)
    {
        var useCaseMock = new Mock<DeleteBookingUseCase>(Mock.Of<IBookingRepository>());
        if (exception != null)
        {
            useCaseMock.Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);
        }
        else
        {
            useCaseMock.Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
        }
        return new DeleteBookingTool(useCaseMock.Object);
    }

    [TestMethod]
    public async Task ExecuteAsync_ValidId_ReturnsSuccessMessage()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(5);

        result.ShouldContain("5");
    }

    [TestMethod]
    public async Task ExecuteAsync_InvalidId_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(0);

        result.ShouldContain("ID");
    }

    [TestMethod]
    public async Task ExecuteAsync_NotFound_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase(
            exception: new HttpRequestException("Not Found", null, HttpStatusCode.NotFound));

        var result = await tool.ExecuteAsync(999);

        result.ShouldContain("999");
    }

    [TestMethod]
    public async Task ExecuteAsync_AuthError_ReturnsReadableMessage()
    {
        var tool = CreateToolWithMockUseCase(
            exception: new HttpRequestException("Unauthorized", null, HttpStatusCode.Unauthorized));

        var result = await tool.ExecuteAsync(1);

        result.ShouldContain("Authentifizierung");
    }
}
```

- [ ] **Step 6: Run test to verify it fails**

Run: `dotnet test tests/McpBooking.Server.Tests`
Expected: FAIL — `DeleteBookingTool` does not exist.

- [ ] **Step 7: Write DeleteBookingTool**

Create `src/McpBooking.Server/Tools/DeleteBookingTool.cs`:

```csharp
// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.ComponentModel;
using System.Net;
using McpBooking.Application.UseCases;
using McpBooking.Server.Properties;
using ModelContextProtocol.Server;

namespace McpBooking.Server.Tools;

/// <summary>
/// MCP tool that deletes a booking.
/// </summary>
[McpServerToolType]
public class DeleteBookingTool
{
    private readonly DeleteBookingUseCase _useCase;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteBookingTool"/> class.
    /// </summary>
    /// <param name="useCase">The use case that deletes a booking.</param>
    public DeleteBookingTool(DeleteBookingUseCase useCase)
    {
        _useCase = useCase;
    }

    /// <summary>
    /// Deletes a booking from the WP Booking Calendar API.
    /// </summary>
    /// <param name="id">The booking ID.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>A success message, or a localized error message.</returns>
    [McpServerTool(Name = "delete_booking"), Description("Löscht eine Buchung.")]
    public async Task<string> ExecuteAsync(
        [Description("Buchungs-ID")] int id,
        CancellationToken ct = default)
    {
        if (id < 1) return Messages.ErrorInvalidId;

        try
        {
            await _useCase.ExecuteAsync(id, ct);
            return string.Format(Messages.SuccessBookingDeleted, id);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            return Messages.ErrorAuthenticationFailed;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Forbidden)
        {
            return Messages.ErrorForbidden;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return string.Format(Messages.ErrorBookingNotFound, id);
        }
        catch (HttpRequestException ex) when (ex.StatusCode >= HttpStatusCode.InternalServerError)
        {
            return Messages.ErrorServerError;
        }
        catch (HttpRequestException)
        {
            return Messages.ErrorApiUnreachable;
        }
    }
}
```

- [ ] **Step 8: Run all tests**

Run: `dotnet test`
Expected: All tests PASS.

- [ ] **Step 9: Commit**

```bash
git add src/McpBooking.Application/UseCases/DeleteBookingUseCase.cs src/McpBooking.Server/Tools/DeleteBookingTool.cs tests/McpBooking.Application.Tests/UseCases/DeleteBookingUseCaseTests.cs tests/McpBooking.Server.Tests/Tools/DeleteBookingToolTests.cs
git commit -m "feat(US-010): add delete_booking tool with TDD"
```

---

### Task 9: approve_booking — Vertical Slice

**Files:**
- Create: `src/McpBooking.Application/UseCases/ApproveBookingUseCase.cs`
- Create: `src/McpBooking.Server/Tools/ApproveBookingTool.cs`
- Create: `tests/McpBooking.Application.Tests/UseCases/ApproveBookingUseCaseTests.cs`
- Create: `tests/McpBooking.Server.Tests/Tools/ApproveBookingToolTests.cs`

- [ ] **Step 1: Write Application test**

Create `tests/McpBooking.Application.Tests/UseCases/ApproveBookingUseCaseTests.cs`:

```csharp
using McpBooking.Application.UseCases;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace McpBooking.Application.Tests.UseCases;

[TestClass]
public class ApproveBookingUseCaseTests
{
    [TestMethod]
    public async Task ExecuteAsync_ReturnsApprovedBookingAsDto()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.ApproveAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking { Id = 1, BookingType = 2, Status = "approved" });
        var useCase = new ApproveBookingUseCase(mock.Object);

        var result = await useCase.ExecuteAsync(1);

        result.Id.ShouldBe(1);
        result.Status.ShouldBe("approved");
    }

    [TestMethod]
    public async Task ExecuteAsync_DelegatesToRepository()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.ApproveAsync(5, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking { Id = 5, Status = "approved" });
        var useCase = new ApproveBookingUseCase(mock.Object);

        await useCase.ExecuteAsync(5);

        mock.Verify(r => r.ApproveAsync(5, It.IsAny<CancellationToken>()), Times.Once);
    }
}
```

- [ ] **Step 2: Run test to verify it fails**

Run: `dotnet test tests/McpBooking.Application.Tests`
Expected: FAIL — `ApproveBookingUseCase` does not exist.

- [ ] **Step 3: Write ApproveBookingUseCase**

Create `src/McpBooking.Application/UseCases/ApproveBookingUseCase.cs`:

```csharp
// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Application.DTOs;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;

namespace McpBooking.Application.UseCases;

/// <summary>
/// Use case for approving a booking.
/// </summary>
public class ApproveBookingUseCase
{
    private readonly IBookingRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApproveBookingUseCase"/> class.
    /// </summary>
    /// <param name="repository">The booking repository used to approve bookings.</param>
    public ApproveBookingUseCase(IBookingRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Approves a booking via the repository.
    /// </summary>
    /// <param name="id">The booking ID.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>The approved booking as a DTO.</returns>
    public virtual async Task<BookingDto> ExecuteAsync(int id, CancellationToken ct = default)
    {
        var booking = await _repository.ApproveAsync(id, ct);
        return ToDto(booking);
    }

    private static BookingDto ToDto(Booking b) => new(
        b.Id, b.BookingType, b.Dates, b.FormData, b.Status,
        b.SortDate, b.ModificationDate, b.IsNew, b.Note);
}
```

- [ ] **Step 4: Run test to verify it passes**

Run: `dotnet test tests/McpBooking.Application.Tests`
Expected: All tests PASS.

- [ ] **Step 5: Write Server test**

Create `tests/McpBooking.Server.Tests/Tools/ApproveBookingToolTests.cs`:

```csharp
using System.Net;
using System.Text.Json;
using McpBooking.Application.DTOs;
using McpBooking.Application.UseCases;
using McpBooking.Domain.Interfaces;
using McpBooking.Server.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace McpBooking.Server.Tests.Tools;

[TestClass]
public class ApproveBookingToolTests
{
    private static ApproveBookingTool CreateToolWithMockUseCase(
        BookingDto? result = null, Exception? exception = null)
    {
        var useCaseMock = new Mock<ApproveBookingUseCase>(Mock.Of<IBookingRepository>());
        if (exception != null)
        {
            useCaseMock.Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);
        }
        else
        {
            useCaseMock.Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result ?? new BookingDto(1, 1, [], null, "approved", null, null, null, null));
        }
        return new ApproveBookingTool(useCaseMock.Object);
    }

    [TestMethod]
    public async Task ExecuteAsync_ValidId_ReturnsJson()
    {
        var booking = new BookingDto(1, 2, ["2026-05-01"], null, "approved", null, null, null, null);
        var tool = CreateToolWithMockUseCase(booking);

        var result = await tool.ExecuteAsync(1);

        var doc = JsonDocument.Parse(result);
        doc.RootElement.GetProperty("status").GetString().ShouldBe("approved");
    }

    [TestMethod]
    public async Task ExecuteAsync_InvalidId_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(0);

        result.ShouldContain("ID");
    }

    [TestMethod]
    public async Task ExecuteAsync_NotFound_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase(
            exception: new HttpRequestException("Not Found", null, HttpStatusCode.NotFound));

        var result = await tool.ExecuteAsync(999);

        result.ShouldContain("999");
    }
}
```

- [ ] **Step 6: Run test to verify it fails**

Run: `dotnet test tests/McpBooking.Server.Tests`
Expected: FAIL — `ApproveBookingTool` does not exist.

- [ ] **Step 7: Write ApproveBookingTool**

Create `src/McpBooking.Server/Tools/ApproveBookingTool.cs`:

```csharp
// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.ComponentModel;
using System.Net;
using System.Text.Json;
using McpBooking.Application.UseCases;
using McpBooking.Server.Properties;
using ModelContextProtocol.Server;

namespace McpBooking.Server.Tools;

/// <summary>
/// MCP tool that approves a booking.
/// </summary>
[McpServerToolType]
public class ApproveBookingTool
{
    private readonly ApproveBookingUseCase _useCase;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApproveBookingTool"/> class.
    /// </summary>
    /// <param name="useCase">The use case that approves a booking.</param>
    public ApproveBookingTool(ApproveBookingUseCase useCase)
    {
        _useCase = useCase;
    }

    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    /// <summary>
    /// Approves a booking in the WP Booking Calendar API.
    /// </summary>
    /// <param name="id">The booking ID.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>A JSON string with the approved booking, or a localized error message.</returns>
    [McpServerTool(Name = "approve_booking"), Description("Genehmigt eine Buchung.")]
    public async Task<string> ExecuteAsync(
        [Description("Buchungs-ID")] int id,
        CancellationToken ct = default)
    {
        if (id < 1) return Messages.ErrorInvalidId;

        try
        {
            var booking = await _useCase.ExecuteAsync(id, ct);
            return JsonSerializer.Serialize(booking, s_jsonOptions);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            return Messages.ErrorAuthenticationFailed;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Forbidden)
        {
            return Messages.ErrorForbidden;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return string.Format(Messages.ErrorBookingNotFound, id);
        }
        catch (HttpRequestException ex) when (ex.StatusCode >= HttpStatusCode.InternalServerError)
        {
            return Messages.ErrorServerError;
        }
        catch (HttpRequestException)
        {
            return Messages.ErrorApiUnreachable;
        }
    }
}
```

- [ ] **Step 8: Run all tests**

Run: `dotnet test`
Expected: All tests PASS.

- [ ] **Step 9: Commit**

```bash
git add src/McpBooking.Application/UseCases/ApproveBookingUseCase.cs src/McpBooking.Server/Tools/ApproveBookingTool.cs tests/McpBooking.Application.Tests/UseCases/ApproveBookingUseCaseTests.cs tests/McpBooking.Server.Tests/Tools/ApproveBookingToolTests.cs
git commit -m "feat(US-011): add approve_booking tool with TDD"
```

---

### Task 10: set_booking_pending — Vertical Slice

**Files:**
- Create: `src/McpBooking.Application/UseCases/SetBookingPendingUseCase.cs`
- Create: `src/McpBooking.Server/Tools/SetBookingPendingTool.cs`
- Create: `tests/McpBooking.Application.Tests/UseCases/SetBookingPendingUseCaseTests.cs`
- Create: `tests/McpBooking.Server.Tests/Tools/SetBookingPendingToolTests.cs`

- [ ] **Step 1: Write Application test**

Create `tests/McpBooking.Application.Tests/UseCases/SetBookingPendingUseCaseTests.cs`:

```csharp
using McpBooking.Application.UseCases;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace McpBooking.Application.Tests.UseCases;

[TestClass]
public class SetBookingPendingUseCaseTests
{
    [TestMethod]
    public async Task ExecuteAsync_ReturnsPendingBookingAsDto()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.SetPendingAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking { Id = 1, BookingType = 2, Status = "pending" });
        var useCase = new SetBookingPendingUseCase(mock.Object);

        var result = await useCase.ExecuteAsync(1);

        result.Id.ShouldBe(1);
        result.Status.ShouldBe("pending");
    }

    [TestMethod]
    public async Task ExecuteAsync_DelegatesToRepository()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.SetPendingAsync(5, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking { Id = 5, Status = "pending" });
        var useCase = new SetBookingPendingUseCase(mock.Object);

        await useCase.ExecuteAsync(5);

        mock.Verify(r => r.SetPendingAsync(5, It.IsAny<CancellationToken>()), Times.Once);
    }
}
```

- [ ] **Step 2: Run test to verify it fails**

Run: `dotnet test tests/McpBooking.Application.Tests`
Expected: FAIL — `SetBookingPendingUseCase` does not exist.

- [ ] **Step 3: Write SetBookingPendingUseCase**

Create `src/McpBooking.Application/UseCases/SetBookingPendingUseCase.cs`:

```csharp
// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Application.DTOs;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;

namespace McpBooking.Application.UseCases;

/// <summary>
/// Use case for setting a booking to pending status.
/// </summary>
public class SetBookingPendingUseCase
{
    private readonly IBookingRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="SetBookingPendingUseCase"/> class.
    /// </summary>
    /// <param name="repository">The booking repository.</param>
    public SetBookingPendingUseCase(IBookingRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Sets a booking to pending status via the repository.
    /// </summary>
    /// <param name="id">The booking ID.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>The updated booking as a DTO.</returns>
    public virtual async Task<BookingDto> ExecuteAsync(int id, CancellationToken ct = default)
    {
        var booking = await _repository.SetPendingAsync(id, ct);
        return ToDto(booking);
    }

    private static BookingDto ToDto(Booking b) => new(
        b.Id, b.BookingType, b.Dates, b.FormData, b.Status,
        b.SortDate, b.ModificationDate, b.IsNew, b.Note);
}
```

- [ ] **Step 4: Run test to verify it passes**

Run: `dotnet test tests/McpBooking.Application.Tests`
Expected: All tests PASS.

- [ ] **Step 5: Write Server test**

Create `tests/McpBooking.Server.Tests/Tools/SetBookingPendingToolTests.cs`:

```csharp
using System.Net;
using System.Text.Json;
using McpBooking.Application.DTOs;
using McpBooking.Application.UseCases;
using McpBooking.Domain.Interfaces;
using McpBooking.Server.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace McpBooking.Server.Tests.Tools;

[TestClass]
public class SetBookingPendingToolTests
{
    private static SetBookingPendingTool CreateToolWithMockUseCase(
        BookingDto? result = null, Exception? exception = null)
    {
        var useCaseMock = new Mock<SetBookingPendingUseCase>(Mock.Of<IBookingRepository>());
        if (exception != null)
        {
            useCaseMock.Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);
        }
        else
        {
            useCaseMock.Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result ?? new BookingDto(1, 1, [], null, "pending", null, null, null, null));
        }
        return new SetBookingPendingTool(useCaseMock.Object);
    }

    [TestMethod]
    public async Task ExecuteAsync_ValidId_ReturnsJson()
    {
        var booking = new BookingDto(1, 2, ["2026-05-01"], null, "pending", null, null, null, null);
        var tool = CreateToolWithMockUseCase(booking);

        var result = await tool.ExecuteAsync(1);

        var doc = JsonDocument.Parse(result);
        doc.RootElement.GetProperty("status").GetString().ShouldBe("pending");
    }

    [TestMethod]
    public async Task ExecuteAsync_InvalidId_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(0);

        result.ShouldContain("ID");
    }

    [TestMethod]
    public async Task ExecuteAsync_NotFound_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase(
            exception: new HttpRequestException("Not Found", null, HttpStatusCode.NotFound));

        var result = await tool.ExecuteAsync(999);

        result.ShouldContain("999");
    }
}
```

- [ ] **Step 6: Run test to verify it fails**

Run: `dotnet test tests/McpBooking.Server.Tests`
Expected: FAIL — `SetBookingPendingTool` does not exist.

- [ ] **Step 7: Write SetBookingPendingTool**

Create `src/McpBooking.Server/Tools/SetBookingPendingTool.cs`:

```csharp
// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.ComponentModel;
using System.Net;
using System.Text.Json;
using McpBooking.Application.UseCases;
using McpBooking.Server.Properties;
using ModelContextProtocol.Server;

namespace McpBooking.Server.Tools;

/// <summary>
/// MCP tool that sets a booking to pending status.
/// </summary>
[McpServerToolType]
public class SetBookingPendingTool
{
    private readonly SetBookingPendingUseCase _useCase;

    /// <summary>
    /// Initializes a new instance of the <see cref="SetBookingPendingTool"/> class.
    /// </summary>
    /// <param name="useCase">The use case that sets a booking to pending.</param>
    public SetBookingPendingTool(SetBookingPendingUseCase useCase)
    {
        _useCase = useCase;
    }

    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    /// <summary>
    /// Sets a booking to pending status in the WP Booking Calendar API.
    /// </summary>
    /// <param name="id">The booking ID.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>A JSON string with the updated booking, or a localized error message.</returns>
    [McpServerTool(Name = "set_booking_pending"), Description("Setzt eine Buchung auf ausstehend.")]
    public async Task<string> ExecuteAsync(
        [Description("Buchungs-ID")] int id,
        CancellationToken ct = default)
    {
        if (id < 1) return Messages.ErrorInvalidId;

        try
        {
            var booking = await _useCase.ExecuteAsync(id, ct);
            return JsonSerializer.Serialize(booking, s_jsonOptions);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            return Messages.ErrorAuthenticationFailed;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Forbidden)
        {
            return Messages.ErrorForbidden;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return string.Format(Messages.ErrorBookingNotFound, id);
        }
        catch (HttpRequestException ex) when (ex.StatusCode >= HttpStatusCode.InternalServerError)
        {
            return Messages.ErrorServerError;
        }
        catch (HttpRequestException)
        {
            return Messages.ErrorApiUnreachable;
        }
    }
}
```

- [ ] **Step 8: Run all tests**

Run: `dotnet test`
Expected: All tests PASS.

- [ ] **Step 9: Commit**

```bash
git add src/McpBooking.Application/UseCases/SetBookingPendingUseCase.cs src/McpBooking.Server/Tools/SetBookingPendingTool.cs tests/McpBooking.Application.Tests/UseCases/SetBookingPendingUseCaseTests.cs tests/McpBooking.Server.Tests/Tools/SetBookingPendingToolTests.cs
git commit -m "feat(US-012): add set_booking_pending tool with TDD"
```

---

### Task 11: update_booking_note — Vertical Slice

**Files:**
- Create: `src/McpBooking.Application/UseCases/UpdateBookingNoteUseCase.cs`
- Create: `src/McpBooking.Server/Tools/UpdateBookingNoteTool.cs`
- Create: `tests/McpBooking.Application.Tests/UseCases/UpdateBookingNoteUseCaseTests.cs`
- Create: `tests/McpBooking.Server.Tests/Tools/UpdateBookingNoteToolTests.cs`

- [ ] **Step 1: Write Application test**

Create `tests/McpBooking.Application.Tests/UseCases/UpdateBookingNoteUseCaseTests.cs`:

```csharp
using McpBooking.Application.UseCases;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace McpBooking.Application.Tests.UseCases;

[TestClass]
public class UpdateBookingNoteUseCaseTests
{
    [TestMethod]
    public async Task ExecuteAsync_ReturnsUpdatedBookingAsDto()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.UpdateNoteAsync(1, "Neue Notiz", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking { Id = 1, BookingType = 2, Status = "approved", Note = "Neue Notiz" });
        var useCase = new UpdateBookingNoteUseCase(mock.Object);

        var result = await useCase.ExecuteAsync(1, "Neue Notiz");

        result.Id.ShouldBe(1);
        result.Note.ShouldBe("Neue Notiz");
    }

    [TestMethod]
    public async Task ExecuteAsync_DelegatesCorrectParameters()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.UpdateNoteAsync(5, "Test", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking { Id = 5, Status = "approved", Note = "Test" });
        var useCase = new UpdateBookingNoteUseCase(mock.Object);

        await useCase.ExecuteAsync(5, "Test");

        mock.Verify(r => r.UpdateNoteAsync(5, "Test", It.IsAny<CancellationToken>()), Times.Once);
    }
}
```

- [ ] **Step 2: Run test to verify it fails**

Run: `dotnet test tests/McpBooking.Application.Tests`
Expected: FAIL — `UpdateBookingNoteUseCase` does not exist.

- [ ] **Step 3: Write UpdateBookingNoteUseCase**

Create `src/McpBooking.Application/UseCases/UpdateBookingNoteUseCase.cs`:

```csharp
// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Application.DTOs;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;

namespace McpBooking.Application.UseCases;

/// <summary>
/// Use case for updating the note attached to a booking.
/// </summary>
public class UpdateBookingNoteUseCase
{
    private readonly IBookingRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateBookingNoteUseCase"/> class.
    /// </summary>
    /// <param name="repository">The booking repository.</param>
    public UpdateBookingNoteUseCase(IBookingRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Updates the note of a booking via the repository.
    /// </summary>
    /// <param name="id">The booking ID.</param>
    /// <param name="note">The note text.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>The updated booking as a DTO.</returns>
    public virtual async Task<BookingDto> ExecuteAsync(
        int id, string note, CancellationToken ct = default)
    {
        var booking = await _repository.UpdateNoteAsync(id, note, ct);
        return ToDto(booking);
    }

    private static BookingDto ToDto(Booking b) => new(
        b.Id, b.BookingType, b.Dates, b.FormData, b.Status,
        b.SortDate, b.ModificationDate, b.IsNew, b.Note);
}
```

- [ ] **Step 4: Run test to verify it passes**

Run: `dotnet test tests/McpBooking.Application.Tests`
Expected: All tests PASS.

- [ ] **Step 5: Write Server test**

Create `tests/McpBooking.Server.Tests/Tools/UpdateBookingNoteToolTests.cs`:

```csharp
using System.Net;
using System.Text.Json;
using McpBooking.Application.DTOs;
using McpBooking.Application.UseCases;
using McpBooking.Domain.Interfaces;
using McpBooking.Server.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace McpBooking.Server.Tests.Tools;

[TestClass]
public class UpdateBookingNoteToolTests
{
    private static UpdateBookingNoteTool CreateToolWithMockUseCase(
        BookingDto? result = null, Exception? exception = null)
    {
        var useCaseMock = new Mock<UpdateBookingNoteUseCase>(Mock.Of<IBookingRepository>());
        if (exception != null)
        {
            useCaseMock.Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);
        }
        else
        {
            useCaseMock.Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result ?? new BookingDto(1, 1, [], null, "approved", null, null, null, "note"));
        }
        return new UpdateBookingNoteTool(useCaseMock.Object);
    }

    [TestMethod]
    public async Task ExecuteAsync_ValidParams_ReturnsJson()
    {
        var booking = new BookingDto(1, 2, ["2026-05-01"], null, "approved", null, null, null, "Neue Notiz");
        var tool = CreateToolWithMockUseCase(booking);

        var result = await tool.ExecuteAsync(1, "Neue Notiz");

        var doc = JsonDocument.Parse(result);
        doc.RootElement.GetProperty("note").GetString().ShouldBe("Neue Notiz");
    }

    [TestMethod]
    public async Task ExecuteAsync_InvalidId_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(0, "note");

        result.ShouldContain("ID");
    }

    [TestMethod]
    public async Task ExecuteAsync_EmptyNote_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(1, "");

        result.ShouldContain("Notiz");
    }

    [TestMethod]
    public async Task ExecuteAsync_NotFound_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase(
            exception: new HttpRequestException("Not Found", null, HttpStatusCode.NotFound));

        var result = await tool.ExecuteAsync(999, "note");

        result.ShouldContain("999");
    }
}
```

- [ ] **Step 6: Run test to verify it fails**

Run: `dotnet test tests/McpBooking.Server.Tests`
Expected: FAIL — `UpdateBookingNoteTool` does not exist.

- [ ] **Step 7: Write UpdateBookingNoteTool**

Create `src/McpBooking.Server/Tools/UpdateBookingNoteTool.cs`:

```csharp
// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.ComponentModel;
using System.Net;
using System.Text.Json;
using McpBooking.Application.UseCases;
using McpBooking.Server.Properties;
using ModelContextProtocol.Server;

namespace McpBooking.Server.Tools;

/// <summary>
/// MCP tool that updates the note attached to a booking.
/// </summary>
[McpServerToolType]
public class UpdateBookingNoteTool
{
    private readonly UpdateBookingNoteUseCase _useCase;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateBookingNoteTool"/> class.
    /// </summary>
    /// <param name="useCase">The use case that updates a booking note.</param>
    public UpdateBookingNoteTool(UpdateBookingNoteUseCase useCase)
    {
        _useCase = useCase;
    }

    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    /// <summary>
    /// Updates the note attached to a booking in the WP Booking Calendar API.
    /// </summary>
    /// <param name="id">The booking ID.</param>
    /// <param name="note">The note text.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>A JSON string with the updated booking, or a localized error message.</returns>
    [McpServerTool(Name = "update_booking_note"), Description("Aktualisiert die Notiz einer Buchung.")]
    public async Task<string> ExecuteAsync(
        [Description("Buchungs-ID")] int id,
        [Description("Notiztext")] string note,
        CancellationToken ct = default)
    {
        if (id < 1) return Messages.ErrorInvalidId;
        if (string.IsNullOrWhiteSpace(note)) return Messages.ErrorNoteEmpty;

        try
        {
            var booking = await _useCase.ExecuteAsync(id, note, ct);
            return JsonSerializer.Serialize(booking, s_jsonOptions);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            return Messages.ErrorAuthenticationFailed;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Forbidden)
        {
            return Messages.ErrorForbidden;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return string.Format(Messages.ErrorBookingNotFound, id);
        }
        catch (HttpRequestException ex) when (ex.StatusCode >= HttpStatusCode.InternalServerError)
        {
            return Messages.ErrorServerError;
        }
        catch (HttpRequestException)
        {
            return Messages.ErrorApiUnreachable;
        }
    }
}
```

- [ ] **Step 8: Run all tests**

Run: `dotnet test`
Expected: All tests PASS.

- [ ] **Step 9: Commit**

```bash
git add src/McpBooking.Application/UseCases/UpdateBookingNoteUseCase.cs src/McpBooking.Server/Tools/UpdateBookingNoteTool.cs tests/McpBooking.Application.Tests/UseCases/UpdateBookingNoteUseCaseTests.cs tests/McpBooking.Server.Tests/Tools/UpdateBookingNoteToolTests.cs
git commit -m "feat(US-013): add update_booking_note tool with TDD"
```

---

### Task 12: DI Wiring — Program.cs and Final Verification

**Files:**
- Modify: `src/McpBooking.Server/Program.cs`

- [ ] **Step 1: Register all 8 booking use cases in Program.cs**

Add after the existing `builder.Services.AddScoped<ListResourcesUseCase>();` line in `src/McpBooking.Server/Program.cs`:

```csharp
builder.Services.AddScoped<ListBookingsUseCase>();
builder.Services.AddScoped<GetBookingUseCase>();
builder.Services.AddScoped<CreateBookingUseCase>();
builder.Services.AddScoped<UpdateBookingUseCase>();
builder.Services.AddScoped<DeleteBookingUseCase>();
builder.Services.AddScoped<ApproveBookingUseCase>();
builder.Services.AddScoped<SetBookingPendingUseCase>();
builder.Services.AddScoped<UpdateBookingNoteUseCase>();
```

Also add the required usings at the top of `Program.cs`:

```csharp
using McpBooking.Application.UseCases;
```

This using likely already exists for `ListResourcesUseCase` — just verify it's there.

- [ ] **Step 2: Verify full build**

Run: `dotnet build`
Expected: Build succeeds with 0 errors and 0 warnings.

- [ ] **Step 3: Run all tests**

Run: `dotnet test`
Expected: All tests PASS (10 existing + ~52 new = ~62 total).

- [ ] **Step 4: Commit**

```bash
git add src/McpBooking.Server/Program.cs
git commit -m "feat: register all booking use cases in DI container"
```

- [ ] **Step 5: Verify final test count**

Run: `dotnet test --verbosity quiet`
Expected output should show the total test count (should be ~62+ tests, 0 failed).
