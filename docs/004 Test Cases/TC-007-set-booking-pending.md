---
id: "007"
title: Buchung auf ausstehend setzen
tags:
  - TestCase
  - Bookings
status: ausstehend
---

# TC-007: Buchung auf ausstehend setzen

**Use Case:** [UC-007 Buchung auf ausstehend setzen](../002%20Use%20Cases/UC-007-set-booking-pending.md)
**User Story:** [US-012 Tool: set_booking_pending](../001%20User%20Stories/US-012-set-booking-pending.md)
**Requirement:** [REQ-017 Tool: set_booking_pending](../003%20Requirements/REQ-017-set-booking-pending.md)

## Testszenarien

### TS-007.01: Buchung wird erfolgreich auf ausstehend gesetzt

| Feld | Wert |
|------|------|
| **Schicht** | Application |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** Repository-Mock akzeptiert Statusänderung.
**Aktion:** `SetBookingPendingUseCase.ExecuteAsync(42)` aufrufen.
**Erwartetes Ergebnis:** Bestätigung mit Status `pending`.

---

### TS-007.02: Nicht existierende Buchung liefert 404-Fehlermeldung

| Feld | Wert |
|------|------|
| **Schicht** | Server |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** API liefert 404.
**Aktion:** `SetBookingPendingTool.ExecuteAsync(id: 999)` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Buchung mit ID 999 nicht gefunden."
