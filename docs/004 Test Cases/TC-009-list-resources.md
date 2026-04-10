---
id: "009"
title: Ressourcen auflisten
tags:
  - TestCase
  - Resources
status: implementiert
---

# TC-009: Ressourcen auflisten

**Use Case:** [UC-009 Ressourcen auflisten](../002%20Use%20Cases/UC-009-list-resources.md)
**User Story:** [US-014 Tool: list_resources](../001%20User%20Stories/US-014-list-resources.md)
**Requirement:** [REQ-019 Tool: list_resources](../003%20Requirements/REQ-019-list-resources.md)

## Testszenarien

### TS-009.01: Ressourcen werden korrekt als DTOs gemappt

| Feld | Wert |
|------|------|
| **Schicht** | Application |
| **Typ** | Unit-Test |
| **Status** | Implementiert |
| **Testklasse** | `McpBooking.Application.Tests.UseCases.ListResourcesUseCaseTests` |
| **Testmethode** | `ExecuteAsync_ReturnsResources_MappedToDtos` |

**Vorbedingung:** Repository-Mock liefert 2 Ressourcen (Gemeindesaal, Vereinsraum).
**Aktion:** `ListResourcesUseCase.ExecuteAsync()` aufrufen.
**Erwartetes Ergebnis:** 2 ResourceDto-Objekte mit korrekten Werten (Id, Title, Cost, Visitors).

---

### TS-009.02: Leere Ressourcenliste wird korrekt behandelt

| Feld | Wert |
|------|------|
| **Schicht** | Application |
| **Typ** | Unit-Test |
| **Status** | Implementiert |
| **Testklasse** | `McpBooking.Application.Tests.UseCases.ListResourcesUseCaseTests` |
| **Testmethode** | `ExecuteAsync_EmptyList_ReturnsEmptyCollection` |

**Vorbedingung:** Repository-Mock liefert leere Liste.
**Aktion:** `ListResourcesUseCase.ExecuteAsync()` aufrufen.
**Erwartetes Ergebnis:** Leere Collection.

---

### TS-009.03: Paginierungsparameter werden korrekt weitergegeben

| Feld | Wert |
|------|------|
| **Schicht** | Application |
| **Typ** | Unit-Test |
| **Status** | Implementiert |
| **Testklasse** | `McpBooking.Application.Tests.UseCases.ListResourcesUseCaseTests` |
| **Testmethode** | `ExecuteAsync_PassesPaginationParameters` |

**Vorbedingung:** Repository-Mock erwartet page=3, perPage=50.
**Aktion:** `ListResourcesUseCase.ExecuteAsync(page: 3, perPage: 50)` aufrufen.
**Erwartetes Ergebnis:** Repository wird genau einmal mit page=3, perPage=50 aufgerufen.

---

### TS-009.04: API-Antwort wird korrekt deserialisiert

| Feld | Wert |
|------|------|
| **Schicht** | Infrastructure |
| **Typ** | Unit-Test |
| **Status** | Implementiert |
| **Testklasse** | `McpBooking.Infrastructure.Tests.Repositories.ResourceRepositoryTests` |
| **Testmethode** | `ListAsync_ReturnsDeserializedResources` |

**Vorbedingung:** HTTP-Mock liefert JSON-Array mit 2 Ressourcen.
**Aktion:** `ResourceRepository.ListAsync()` aufrufen.
**Erwartetes Ergebnis:** 2 Resource-Entities mit korrekt deserialisierten Werten.

---

### TS-009.05: Leeres API-Array wird als leere Liste behandelt

| Feld | Wert |
|------|------|
| **Schicht** | Infrastructure |
| **Typ** | Unit-Test |
| **Status** | Implementiert |
| **Testklasse** | `McpBooking.Infrastructure.Tests.Repositories.ResourceRepositoryTests` |
| **Testmethode** | `ListAsync_EmptyArray_ReturnsEmptyList` |

**Vorbedingung:** HTTP-Mock liefert `[]`.
**Aktion:** `ResourceRepository.ListAsync()` aufrufen.
**Erwartetes Ergebnis:** Leere Liste.

---

### TS-009.06: Korrekte Query-Parameter im HTTP-Request

| Feld | Wert |
|------|------|
| **Schicht** | Infrastructure |
| **Typ** | Unit-Test |
| **Status** | Implementiert |
| **Testklasse** | `McpBooking.Infrastructure.Tests.Repositories.ResourceRepositoryTests` |
| **Testmethode** | `ListAsync_SendsCorrectQueryParameters` |

**Vorbedingung:** HTTP-Mock akzeptiert alle Requests.
**Aktion:** `ResourceRepository.ListAsync(page: 3, perPage: 50)` aufrufen.
**Erwartetes Ergebnis:** HTTP-Request enthält `page=3` und `per_page=50` als Query-Parameter.

---

### TS-009.07: MCP-Tool liefert gültiges JSON

| Feld | Wert |
|------|------|
| **Schicht** | Server |
| **Typ** | Unit-Test |
| **Status** | Implementiert |
| **Testklasse** | `McpBooking.Server.Tests.Tools.ListResourcesToolTests` |
| **Testmethode** | `ExecuteAsync_ValidParams_ReturnsJson` |

**Vorbedingung:** UseCase-Mock liefert 1 Ressource (Gemeindesaal).
**Aktion:** `ListResourcesTool.ExecuteAsync()` aufrufen.
**Erwartetes Ergebnis:** Gültiges JSON-Array mit id=1, title="Gemeindesaal".

---

### TS-009.08: Ungültige Seite wird abgelehnt

| Feld | Wert |
|------|------|
| **Schicht** | Server |
| **Typ** | Unit-Test |
| **Status** | Implementiert |
| **Testklasse** | `McpBooking.Server.Tests.Tools.ListResourcesToolTests` |
| **Testmethode** | `ExecuteAsync_InvalidPage_ReturnsError` |

**Vorbedingung:** —
**Aktion:** `ListResourcesTool.ExecuteAsync(page: 0)` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung enthält "page muss >= 1 sein".

---

### TS-009.09: Ungültige Seitengröße wird abgelehnt

| Feld | Wert |
|------|------|
| **Schicht** | Server |
| **Typ** | Unit-Test |
| **Status** | Implementiert |
| **Testklasse** | `McpBooking.Server.Tests.Tools.ListResourcesToolTests` |
| **Testmethode** | `ExecuteAsync_InvalidPerPage_ReturnsError` |

**Vorbedingung:** —
**Aktion:** `ListResourcesTool.ExecuteAsync(perPage: 101)` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung enthält "per_page muss zwischen 1 und 100 liegen".

---

### TS-009.10: Authentifizierungsfehler wird verständlich gemeldet

| Feld | Wert |
|------|------|
| **Schicht** | Server |
| **Typ** | Unit-Test |
| **Status** | Implementiert |
| **Testklasse** | `McpBooking.Server.Tests.Tools.ListResourcesToolTests` |
| **Testmethode** | `ExecuteAsync_AuthError_ReturnsReadableMessage` |

**Vorbedingung:** UseCase-Mock wirft `HttpRequestException` mit StatusCode 401.
**Aktion:** `ListResourcesTool.ExecuteAsync()` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung enthält "Authentifizierung fehlgeschlagen".

## Zusammenfassung

| Schicht | Anzahl | Status |
|---------|--------|--------|
| Application | 3 | Implementiert |
| Infrastructure | 3 | Implementiert |
| Server | 4 | Implementiert |
| **Gesamt** | **10** | **Alle bestanden** |
