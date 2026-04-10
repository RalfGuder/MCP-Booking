---
id: "001"
title: Buchungen auflisten
tags:
  - TestCase
  - Bookings
status: ausstehend
---

# TC-001: Buchungen auflisten

**Use Case:** [UC-001 Buchungen auflisten](../002%20Use%20Cases/UC-001-list-bookings.md)
**User Story:** [US-006 Tool: list_bookings](../001%20User%20Stories/US-006-list-bookings.md)
**Requirement:** [REQ-011 Tool: list_bookings](../003%20Requirements/REQ-011-list-bookings.md)

## Testszenarien

### TS-001.01: Buchungen werden korrekt als DTOs gemappt

| Feld | Wert |
|------|------|
| **Schicht** | Application |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** Repository-Mock liefert 2 Buchungen mit unterschiedlichem Status.
**Aktion:** `ListBookingsUseCase.ExecuteAsync()` aufrufen.
**Erwartetes Ergebnis:** 2 BookingDto-Objekte mit korrekten Werten.

---

### TS-001.02: Leere Buchungsliste wird korrekt behandelt

| Feld | Wert |
|------|------|
| **Schicht** | Application |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** Repository-Mock liefert leere Liste.
**Aktion:** `ListBookingsUseCase.ExecuteAsync()` aufrufen.
**Erwartetes Ergebnis:** Leere Collection.

---

### TS-001.03: Filterung nach Status wird korrekt weitergegeben

| Feld | Wert |
|------|------|
| **Schicht** | Application |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** Repository-Mock erwartet status=approved.
**Aktion:** `ListBookingsUseCase.ExecuteAsync(status: "approved")` aufrufen.
**Erwartetes Ergebnis:** Repository wird mit korrektem Statusfilter aufgerufen.

---

### TS-001.04: API-Antwort wird korrekt deserialisiert

| Feld | Wert |
|------|------|
| **Schicht** | Infrastructure |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** HTTP-Mock liefert JSON-Array mit Buchungen.
**Aktion:** `BookingRepository.ListAsync()` aufrufen.
**Erwartetes Ergebnis:** Korrekt deserialisierte Booking-Entities.

---

### TS-001.05: MCP-Tool liefert gültiges JSON

| Feld | Wert |
|------|------|
| **Schicht** | Server |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** UseCase-Mock liefert Buchungen.
**Aktion:** `ListBookingsTool.ExecuteAsync()` aufrufen.
**Erwartetes Ergebnis:** Gültiges JSON-Array.

---

### TS-001.06: Authentifizierungsfehler wird verständlich gemeldet

| Feld | Wert |
|------|------|
| **Schicht** | Server |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** UseCase-Mock wirft `HttpRequestException` mit StatusCode 401.
**Aktion:** `ListBookingsTool.ExecuteAsync()` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung enthält "Authentifizierung fehlgeschlagen".
