---
id: "005"
title: Buchung löschen
tags:
  - TestCase
  - Bookings
status: ausstehend
---

# TC-005: Buchung löschen

**Use Case:** [UC-005 Buchung löschen](../002%20Use%20Cases/UC-005-delete-booking.md)
**User Story:** [US-010 Tool: delete_booking](../001%20User%20Stories/US-010-delete-booking.md)
**Requirement:** [REQ-015 Tool: delete_booking](../003%20Requirements/REQ-015-delete-booking.md)

## Testszenarien

### TS-005.01: Buchung wird erfolgreich gelöscht

| Feld | Wert |
|------|------|
| **Schicht** | Application |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** Repository-Mock akzeptiert Löschung.
**Aktion:** `DeleteBookingUseCase.ExecuteAsync(42)` aufrufen.
**Erwartetes Ergebnis:** Bestätigung der Löschung.

---

### TS-005.02: Nicht existierende Buchung liefert 404-Fehlermeldung

| Feld | Wert |
|------|------|
| **Schicht** | Server |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** API liefert 404.
**Aktion:** `DeleteBookingTool.ExecuteAsync(id: 999)` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Buchung mit ID 999 nicht gefunden."
