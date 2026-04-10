---
id: "004"
title: Buchung aktualisieren
tags:
  - TestCase
  - Bookings
status: ausstehend
---

# TC-004: Buchung aktualisieren

**Use Case:** [UC-004 Buchung aktualisieren](../002%20Use%20Cases/UC-004-update-booking.md)
**User Story:** [US-009 Tool: update_booking](../001%20User%20Stories/US-009-update-booking.md)
**Requirement:** [REQ-014 Tool: update_booking](../003%20Requirements/REQ-014-update-booking.md)

## Testszenarien

### TS-004.01: Buchung wird erfolgreich aktualisiert

| Feld | Wert |
|------|------|
| **Schicht** | Application |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** Repository-Mock akzeptiert Aktualisierung.
**Aktion:** `UpdateBookingUseCase.ExecuteAsync(42, formData)` aufrufen.
**Erwartetes Ergebnis:** Bestätigung der Aktualisierung.

---

### TS-004.02: Nicht existierende Buchung liefert 404-Fehlermeldung

| Feld | Wert |
|------|------|
| **Schicht** | Server |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** API liefert 404.
**Aktion:** `UpdateBookingTool.ExecuteAsync(id: 999)` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Buchung mit ID 999 nicht gefunden."

---

### TS-004.03: Keine Felder zum Aktualisieren liefert Fehlermeldung

| Feld | Wert |
|------|------|
| **Schicht** | Server |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** —
**Aktion:** `UpdateBookingTool.ExecuteAsync(id: 42)` ohne weitere Felder aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Mindestens ein Feld muss aktualisiert werden."
