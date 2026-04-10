---
id: "008"
title: Notiz an Buchung anfügen
tags:
  - TestCase
  - Bookings
status: ausstehend
---

# TC-008: Notiz an Buchung anfügen

**Use Case:** [UC-008 Notiz an Buchung anfügen](../002%20Use%20Cases/UC-008-update-booking-note.md)
**User Story:** [US-013 Tool: update_booking_note](../001%20User%20Stories/US-013-update-booking-note.md)
**Requirement:** [REQ-018 Tool: update_booking_note](../003%20Requirements/REQ-018-update-booking-note.md)

## Testszenarien

### TS-008.01: Notiz wird erfolgreich angefügt

| Feld | Wert |
|------|------|
| **Schicht** | Application |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** Repository-Mock akzeptiert Notizerstellung.
**Aktion:** `UpdateBookingNoteUseCase.ExecuteAsync(42, "Rollstuhlgerecht benötigt")` aufrufen.
**Erwartetes Ergebnis:** Bestätigung der Notizerstellung.

---

### TS-008.02: Leere Notiz wird abgelehnt

| Feld | Wert |
|------|------|
| **Schicht** | Server |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** —
**Aktion:** `UpdateBookingNoteTool.ExecuteAsync(id: 42, note: "")` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Der Notiztext darf nicht leer sein."

---

### TS-008.03: Nicht existierende Buchung liefert 404-Fehlermeldung

| Feld | Wert |
|------|------|
| **Schicht** | Server |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** API liefert 404.
**Aktion:** `UpdateBookingNoteTool.ExecuteAsync(id: 999, note: "Test")` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Buchung mit ID 999 nicht gefunden."
