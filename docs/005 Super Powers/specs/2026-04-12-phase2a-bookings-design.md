# Design: Phase 2a — Booking-Tools

**Datum:** 2026-04-12
**Scope:** 8 MCP-Tools fuer den Bookings-API-Bereich
**Status:** Entwurf
**Vorgaenger:** [2026-04-10-us001-mcp-server-design.md](2026-04-10-us001-mcp-server-design.md) (Phase 1)

## Verknuepfte Artefakte

### User Stories

- [US-006 Tool: list_bookings](../../001%20User%20Stories/US-006-list-bookings.md)
- [US-007 Tool: create_booking](../../001%20User%20Stories/US-007-create-booking.md)
- [US-008 Tool: get_booking](../../001%20User%20Stories/US-008-get-booking.md)
- [US-009 Tool: update_booking](../../001%20User%20Stories/US-009-update-booking.md)
- [US-010 Tool: delete_booking](../../001%20User%20Stories/US-010-delete-booking.md)
- [US-011 Tool: approve_booking](../../001%20User%20Stories/US-011-approve-booking.md)
- [US-012 Tool: set_booking_pending](../../001%20User%20Stories/US-012-set-booking-pending.md)
- [US-013 Tool: update_booking_note](../../001%20User%20Stories/US-013-update-booking-note.md)

### Use Cases

- [UC-001 Buchungen auflisten](../../002%20Use%20Cases/UC-001-list-bookings.md)
- [UC-002 Buchung erstellen](../../002%20Use%20Cases/UC-002-create-booking.md)
- [UC-003 Buchung abrufen](../../002%20Use%20Cases/UC-003-get-booking.md)
- [UC-004 Buchung aktualisieren](../../002%20Use%20Cases/UC-004-update-booking.md)
- [UC-005 Buchung loeschen](../../002%20Use%20Cases/UC-005-delete-booking.md)
- [UC-006 Buchung genehmigen](../../002%20Use%20Cases/UC-006-approve-booking.md)
- [UC-007 Buchung auf Pending setzen](../../002%20Use%20Cases/UC-007-set-booking-pending.md)
- [UC-008 Buchungsnotiz aktualisieren](../../002%20Use%20Cases/UC-008-update-booking-note.md)

### Requirements

| Requirement | Titel |
|-------------|-------|
| [REQ-011](../../003%20Requirements/REQ-011-list-bookings.md) | Tool: list_bookings |
| [REQ-012](../../003%20Requirements/REQ-012-create-booking.md) | Tool: create_booking |
| [REQ-013](../../003%20Requirements/REQ-013-get-booking.md) | Tool: get_booking |
| [REQ-014](../../003%20Requirements/REQ-014-update-booking.md) | Tool: update_booking |
| [REQ-015](../../003%20Requirements/REQ-015-delete-booking.md) | Tool: delete_booking |
| [REQ-016](../../003%20Requirements/REQ-016-approve-booking.md) | Tool: approve_booking |
| [REQ-017](../../003%20Requirements/REQ-017-set-booking-pending.md) | Tool: set_booking_pending |
| [REQ-018](../../003%20Requirements/REQ-018-update-booking-note.md) | Tool: update_booking_note |

### Test Cases

| Test Case | Titel |
|-----------|-------|
| [TC-001](../../004%20Test%20Cases/TC-001-list-bookings.md) | list_bookings |
| [TC-002](../../004%20Test%20Cases/TC-002-create-booking.md) | create_booking |
| [TC-003](../../004%20Test%20Cases/TC-003-get-booking.md) | get_booking |
| [TC-004](../../004%20Test%20Cases/TC-004-update-booking.md) | update_booking |
| [TC-005](../../004%20Test%20Cases/TC-005-delete-booking.md) | delete_booking |
| [TC-006](../../004%20Test%20Cases/TC-006-approve-booking.md) | approve_booking |
| [TC-007](../../004%20Test%20Cases/TC-007-set-booking-pending.md) | set_booking_pending |
| [TC-008](../../004%20Test%20Cases/TC-008-update-booking-note.md) | update_booking_note |

## Zusammenfassung

Phase 2a implementiert 8 MCP-Tools fuer den Bookings-Bereich der WP Booking Calendar API. Die Tools folgen dem in Phase 1 etablierten Pattern (vertikale Slices durch alle 4 Clean-Architecture-Schichten) und werden nacheinander per TDD implementiert. Der `BookingApiClient` wird um POST, PUT und DELETE erweitert, sodass er fuer alle folgenden Phasen wiederverwendbar ist.

## Implementierungsansatz

**Vertikale Slices:** Jedes Tool wird vollstaendig ueber alle 4 Schichten implementiert, eins nach dem anderen.

**Reihenfolge:** list_bookings, get_booking, create_booking, update_booking, delete_booking, approve_booking, set_booking_pending, update_booking_note.

**Begruendung:** Die GET-Tools (list, get) kommen zuerst, da sie keine Erweiterung des `BookingApiClient` benoetigen und die `Booking`-Entity etablieren. Danach folgen die schreibenden Tools, die nacheinander POST, PUT und DELETE einfuehren.

## Domain-Schicht

### Entity: Booking

```csharp
using System.Text.Json;
using System.Text.Json.Serialization;

namespace McpBooking.Domain.Entities;

public class Booking
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("booking_type")]
    public int BookingType { get; set; }

    [JsonPropertyName("dates")]
    public List<string> Dates { get; set; } = [];

    [JsonPropertyName("form_data")]
    public JsonElement? FormData { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("sort_date")]
    public string? SortDate { get; set; }

    [JsonPropertyName("modification_date")]
    public string? ModificationDate { get; set; }

    [JsonPropertyName("is_new")]
    public bool? IsNew { get; set; }

    [JsonPropertyName("note")]
    public string? Note { get; set; }
}
```

`FormData` als `JsonElement?` bewahrt die volle JSON-Struktur ohne Datenverlust. Die Formularfelder variieren je nach Buchungstyp und werden nicht typisiert.

### Interface: IBookingRepository

```csharp
using McpBooking.Domain.Entities;

namespace McpBooking.Domain.Interfaces;

public interface IBookingRepository
{
    Task<IReadOnlyList<Booking>> ListAsync(int page, int perPage,
        int? resourceId = null, string? status = null,
        string? dateFrom = null, string? dateTo = null,
        bool? isNew = null, string? search = null,
        string? orderBy = null, string? order = null,
        CancellationToken ct = default);

    Task<Booking?> GetAsync(int id, CancellationToken ct = default);

    Task<Booking> CreateAsync(int bookingType, string formDataJson,
        string datesJson, CancellationToken ct = default);

    Task<Booking> UpdateAsync(int id, string? formDataJson = null,
        int? bookingType = null, string? status = null,
        CancellationToken ct = default);

    Task DeleteAsync(int id, CancellationToken ct = default);

    Task<Booking> ApproveAsync(int id, CancellationToken ct = default);

    Task<Booking> SetPendingAsync(int id, CancellationToken ct = default);

    Task<Booking> UpdateNoteAsync(int id, string note,
        CancellationToken ct = default);
}
```

## Application-Schicht

### DTO: BookingDto

```csharp
using System.Text.Json;

namespace McpBooking.Application.DTOs;

public record BookingDto(
    int Id, int BookingType, List<string> Dates,
    JsonElement? FormData, string Status,
    string? SortDate, string? ModificationDate,
    bool? IsNew, string? Note);
```

### Use Cases

8 Use-Case-Klassen, alle im Namespace `McpBooking.Application.UseCases`:

| Klasse | Repository-Methode | Rueckgabe |
|--------|--------------------|-----------|
| `ListBookingsUseCase` | `ListAsync` | `IReadOnlyList<BookingDto>` |
| `GetBookingUseCase` | `GetAsync` | `BookingDto?` |
| `CreateBookingUseCase` | `CreateAsync` | `BookingDto` |
| `UpdateBookingUseCase` | `UpdateAsync` | `BookingDto` |
| `DeleteBookingUseCase` | `DeleteAsync` | (void) |
| `ApproveBookingUseCase` | `ApproveAsync` | `BookingDto` |
| `SetBookingPendingUseCase` | `SetPendingAsync` | `BookingDto` |
| `UpdateBookingNoteUseCase` | `UpdateNoteAsync` | `BookingDto` |

Jeder Use Case:
- Bekommt `IBookingRepository` per Constructor Injection
- Hat eine `virtual async Task<...> ExecuteAsync(...)` Methode
- Mappt `Booking` Entity zu `BookingDto` (ausser `DeleteBookingUseCase`)
- Reicht `CancellationToken` durch

Mapping-Logik (identisch in allen Use Cases ausser Delete):

```csharp
private static BookingDto ToDto(Booking b) => new(
    b.Id, b.BookingType, b.Dates, b.FormData, b.Status,
    b.SortDate, b.ModificationDate, b.IsNew, b.Note);
```

## Infrastructure-Schicht

### BookingApiClient-Erweiterung

Drei neue Methoden neben dem bestehenden `GetAsync<T>`:

```csharp
public async Task<T?> PostAsync<T>(string path, object body, CancellationToken ct = default)
{
    var response = await _httpClient.PostAsJsonAsync(path, body, ct);
    response.EnsureSuccessStatusCode();
    return await response.Content.ReadFromJsonAsync<T>(ct);
}

public async Task<T?> PostAsync<T>(string path, CancellationToken ct = default)
{
    var response = await _httpClient.PostAsync(path, null, ct);
    response.EnsureSuccessStatusCode();
    return await response.Content.ReadFromJsonAsync<T>(ct);
}

public async Task<T?> PutAsync<T>(string path, object body, CancellationToken ct = default)
{
    var response = await _httpClient.PutAsJsonAsync(path, body, ct);
    response.EnsureSuccessStatusCode();
    return await response.Content.ReadFromJsonAsync<T>(ct);
}

public async Task DeleteAsync(string path, CancellationToken ct = default)
{
    var response = await _httpClient.DeleteAsync(path, ct);
    response.EnsureSuccessStatusCode();
}
```

### BookingRepository

Implementiert `IBookingRepository`. Alle Methoden nutzen `BookingApiClient`:

| Methode | HTTP | Pfad |
|---------|------|------|
| `ListAsync` | GET | `/bookings?page=...&per_page=...&...` |
| `GetAsync` | GET | `/bookings/{id}` |
| `CreateAsync` | POST | `/bookings` |
| `UpdateAsync` | PUT | `/bookings/{id}` |
| `DeleteAsync` | DELETE | `/bookings/{id}` |
| `ApproveAsync` | POST | `/bookings/{id}/approve` |
| `SetPendingAsync` | POST | `/bookings/{id}/pending` |
| `UpdateNoteAsync` | PUT | `/bookings/{id}/note` |

`ListAsync` baut den Query-String dynamisch aus den optionalen Filtern auf. Alle String-Werte werden mit `Uri.EscapeDataString` kodiert.

`CreateAsync` parst `formDataJson` und `datesJson` intern zu `JsonElement` und baut das Request-Body-Objekt.

### DI-Registration

In `DependencyInjection.AddInfrastructure()`:

```csharp
services.AddScoped<IBookingRepository, BookingRepository>();
```

### Strings.cs (Infrastructure)

Neue Base64-codierte Konstanten fuer API-Pfade:

- `ApiBookingsPath` -> `/bookings`
- `ApiBookingByIdPath` -> `/bookings/{0}`
- `ApiBookingApprovePath` -> `/bookings/{0}/approve`
- `ApiBookingPendingPath` -> `/bookings/{0}/pending`
- `ApiBookingNotePath` -> `/bookings/{0}/note`

## Server-Schicht

### MCP-Tools

8 Tool-Klassen im Namespace `McpBooking.Server.Tools`:

| Klasse | MCP-Name | Description |
|--------|----------|-------------|
| `ListBookingsTool` | `list_bookings` | "Listet Buchungen mit optionalen Filtern auf." |
| `GetBookingTool` | `get_booking` | "Ruft eine einzelne Buchung ab." |
| `CreateBookingTool` | `create_booking` | "Erstellt eine neue Buchung." |
| `UpdateBookingTool` | `update_booking` | "Aktualisiert eine bestehende Buchung." |
| `DeleteBookingTool` | `delete_booking` | "Loescht eine Buchung." |
| `ApproveBookingTool` | `approve_booking` | "Genehmigt eine Buchung." |
| `SetBookingPendingTool` | `set_booking_pending` | "Setzt eine Buchung auf ausstehend." |
| `UpdateBookingNoteTool` | `update_booking_note` | "Aktualisiert die Notiz einer Buchung." |

### Tool-Parameter

**list_bookings:**
- `page` (int, default 1) — Seite
- `perPage` (int, default 20) — Eintraege pro Seite (Max: 100)
- `resourceId` (int?, optional) — Filter nach Ressourcen-ID
- `status` (string?, optional) — Filter nach Status (pending/approved/trash)
- `dateFrom` (string?, optional) — Filter ab Datum (ISO 8601)
- `dateTo` (string?, optional) — Filter bis Datum (ISO 8601)
- `isNew` (bool?, optional) — Filter nach neu/ungelesen
- `search` (string?, optional) — Stichwortsuche
- `orderBy` (string?, optional) — Sortierung (booking_id/sort_date/modification_date)
- `order` (string?, optional) — Richtung (ASC/DESC)

**get_booking, delete_booking, approve_booking, set_booking_pending:**
- `id` (int) — Buchungs-ID

**create_booking:**
- `bookingType` (int) — Ressourcen-/Buchungstyp-ID
- `formDataJson` (string) — Formulardaten als JSON-String
- `datesJson` (string) — Buchungsdaten als JSON-Array-String

**update_booking:**
- `id` (int) — Buchungs-ID
- `formDataJson` (string?, optional) — Formulardaten als JSON-String
- `bookingType` (int?, optional) — Neuer Buchungstyp
- `status` (string?, optional) — Neuer Status

**update_booking_note:**
- `id` (int) — Buchungs-ID
- `note` (string) — Notiztext

### Validierung

Jedes Tool validiert seine Parameter vor dem Use-Case-Aufruf:

| Regel | Betroffene Tools |
|-------|-----------------|
| `page >= 1` | list_bookings |
| `perPage` zwischen 1 und 100 | list_bookings |
| `id >= 1` | get, update, delete, approve, pending, note |
| `bookingType >= 1` | create; update (nur wenn gesetzt) |
| `formDataJson` ist gueltiges JSON | create; update (nur wenn gesetzt) |
| `datesJson` ist gueltiges JSON-Array | create |
| `note` nicht leer | update_booking_note |

Bei Validierungsfehlern wird eine lokalisierte Fehlermeldung zurueckgegeben (kein Exception-Wurf).

### Fehlerbehandlung

Identisch zum Phase-1-Pattern in jedem Tool:

| HTTP-Status | Messages.resx-Key | Deutsche Meldung |
|-------------|-------------------|-----------------|
| 401 | `ErrorAuthenticationFailed` | (bestehend) |
| 403 | `ErrorForbidden` | (bestehend) |
| 404 | `ErrorBookingNotFound` | "Buchung mit ID {0} nicht gefunden." |
| 5xx | `ErrorServerError` | (bestehend) |
| Netzwerk | `ErrorApiUnreachable` | (bestehend) |

### Lokalisierung

Neue Eintraege in `Messages.resx` (DE, EN, FR, ES):

| Key | DE | EN |
|-----|----|----|
| `ErrorBookingNotFound` | Buchung mit ID {0} nicht gefunden. | Booking with ID {0} not found. |
| `ErrorInvalidId` | ID muss >= 1 sein. | ID must be >= 1. |
| `ErrorInvalidBookingType` | Buchungstyp muss >= 1 sein. | Booking type must be >= 1. |
| `ErrorInvalidJson` | {0} enthaelt kein gueltiges JSON. | {0} does not contain valid JSON. |
| `ErrorNoteEmpty` | Notiz darf nicht leer sein. | Note must not be empty. |
| `SuccessBookingDeleted` | Buchung {0} wurde geloescht. | Booking {0} has been deleted. |

### Program.cs

8 neue Use-Case-Registrierungen:

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

## Test-Strategie

### Application.Tests (~20 Tests)

Pro Use Case 2-3 Tests:
- **Mapping:** Entity-Felder korrekt zu DTO gemappt
- **Delegation:** Repository mit korrekten Parametern aufgerufen
- **Leer/Null:** Leere Liste bzw. null-Rueckgabe korrekt behandelt

Mock: `IBookingRepository` via Moq.

### Infrastructure.Tests (~12 Tests)

- **Query-Aufbau:** `ListAsync` baut korrekte Query-Strings mit allen Filtern
- **Deserialisierung:** JSON-Antwort mit `form_data` als `JsonElement` korrekt deserialisiert
- **POST/PUT-Body:** Request-Body enthaelt korrekte Felder und Struktur
- **DELETE:** Korrekte URL, kein Response-Body erwartet
- **Fehler-Propagation:** HTTP 4xx/5xx werden als `HttpRequestException` durchgereicht

Mock: `MockHttpMessageHandler`.

### Server.Tests (~30 Tests)

Pro Tool 3-5 Tests:
- **Happy Path:** Gueltige Parameter liefern JSON-Ausgabe
- **Validierung:** Ungueltige Parameter liefern lokalisierte Fehlermeldung
- **Auth-Fehler:** 401 liefert lesbare Meldung
- **Not Found:** 404 liefert "Buchung mit ID X nicht gefunden"
- **Server-Fehler:** 5xx liefert Serverfehler-Meldung

Mock: Use Cases via Moq.

### Gesamterwartung

~62 neue Tests, die bestehenden 10 Tests bleiben unveraendert.

## Dateien-Uebersicht

### Neue Dateien (20)

| Schicht | Datei |
|---------|-------|
| Domain | `src/McpBooking.Domain/Entities/Booking.cs` |
| Domain | `src/McpBooking.Domain/Interfaces/IBookingRepository.cs` |
| Application | `src/McpBooking.Application/DTOs/BookingDto.cs` |
| Application | `src/McpBooking.Application/UseCases/ListBookingsUseCase.cs` |
| Application | `src/McpBooking.Application/UseCases/GetBookingUseCase.cs` |
| Application | `src/McpBooking.Application/UseCases/CreateBookingUseCase.cs` |
| Application | `src/McpBooking.Application/UseCases/UpdateBookingUseCase.cs` |
| Application | `src/McpBooking.Application/UseCases/DeleteBookingUseCase.cs` |
| Application | `src/McpBooking.Application/UseCases/ApproveBookingUseCase.cs` |
| Application | `src/McpBooking.Application/UseCases/SetBookingPendingUseCase.cs` |
| Application | `src/McpBooking.Application/UseCases/UpdateBookingNoteUseCase.cs` |
| Infrastructure | `src/McpBooking.Infrastructure/Repositories/BookingRepository.cs` |
| Server | `src/McpBooking.Server/Tools/ListBookingsTool.cs` |
| Server | `src/McpBooking.Server/Tools/GetBookingTool.cs` |
| Server | `src/McpBooking.Server/Tools/CreateBookingTool.cs` |
| Server | `src/McpBooking.Server/Tools/UpdateBookingTool.cs` |
| Server | `src/McpBooking.Server/Tools/DeleteBookingTool.cs` |
| Server | `src/McpBooking.Server/Tools/ApproveBookingTool.cs` |
| Server | `src/McpBooking.Server/Tools/SetBookingPendingTool.cs` |
| Server | `src/McpBooking.Server/Tools/UpdateBookingNoteTool.cs` |

### Geaenderte Dateien (7+)

| Datei | Aenderung |
|-------|-----------|
| `src/McpBooking.Infrastructure/Http/BookingApiClient.cs` | POST/PUT/DELETE Methoden |
| `src/McpBooking.Infrastructure/DependencyInjection.cs` | BookingRepository registrieren |
| `src/McpBooking.Infrastructure/Properties/Strings.cs` | API-Pfad-Konstanten |
| `src/McpBooking.Server/Program.cs` | 8 Use Cases registrieren |
| `src/McpBooking.Server/Properties/Messages.resx` | 6 neue Eintraege |
| `src/McpBooking.Server/Properties/Messages.en.resx` | 6 neue Eintraege |
| `src/McpBooking.Server/Properties/Messages.fr.resx` | 6 neue Eintraege |
| `src/McpBooking.Server/Properties/Messages.es.resx` | 6 neue Eintraege |

## Konventionen (aus Phase 1 uebernommen)

- XML-Docs auf allen public Members (CS1591 als Error)
- Harte Strings in `Properties/Strings.cs` (Base64-codiert)
- Lokalisierte Fehlermeldungen via `Messages.resx` (DE, EN, FR, ES)
- MSTest.Sdk, Moq, Shouldly
- Snake-Case JSON-Ausgabe (`JsonNamingPolicy.SnakeCaseLower`)
- `CancellationToken` in allen async Methoden
- `virtual` auf Use-Case-Methoden (fuer Moq)
- Gemeinsame `JsonSerializerOptions` als `static readonly` Feld
