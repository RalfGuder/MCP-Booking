---
id: "002"
title: Buchung anlegen
tags:
  - TestCase
  - Bookings
status: ausstehend
---

# TC-002: Buchung anlegen

**Use Case:** [UC-002 Buchung anlegen](../002%20Use%20Cases/UC-002-create-booking.md)
**User Story:** [US-007 Tool: create_booking](../001%20User%20Stories/US-007-create-booking.md)
**Requirement:** [REQ-012 Tool: create_booking](../003%20Requirements/REQ-012-create-booking.md)

## Testszenarien

### TS-002.01: Buchung wird erfolgreich erstellt

| Feld | Wert |
|------|------|
| **Schicht** | Application |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** Repository-Mock akzeptiert Erstellung.
**Aktion:** `CreateBookingUseCase.ExecuteAsync(bookingType, formData, dates)` aufrufen.
**Erwartetes Ergebnis:** Buchungs-ID und Status `pending` werden zurückgegeben.

---

### TS-002.02: Fehlende Pflichtfelder werden abgelehnt

| Feld | Wert |
|------|------|
| **Schicht** | Server |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** —
**Aktion:** `CreateBookingTool.ExecuteAsync()` ohne booking_type aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Pflichtfeld booking_type fehlt."

---

### TS-002.03: Ungültige Ressource wird gemeldet

| Feld | Wert |
|------|------|
| **Schicht** | Server |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** API liefert 404 für die Ressource-ID.
**Aktion:** `CreateBookingTool.ExecuteAsync(bookingType: 999)` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Ressource mit ID 999 nicht gefunden."
