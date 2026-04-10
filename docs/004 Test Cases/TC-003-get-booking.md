---
id: "003"
title: Buchung abrufen
tags:
  - TestCase
  - Bookings
status: ausstehend
---

# TC-003: Buchung abrufen

**Use Case:** [UC-003 Buchung abrufen](../002%20Use%20Cases/UC-003-get-booking.md)
**User Story:** [US-008 Tool: get_booking](../001%20User%20Stories/US-008-get-booking.md)
**Requirement:** [REQ-013 Tool: get_booking](../003%20Requirements/REQ-013-get-booking.md)

## Testszenarien

### TS-003.01: Buchungsdetails werden vollständig zurückgegeben

| Feld | Wert |
|------|------|
| **Schicht** | Application |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** Repository-Mock liefert Buchung mit ID 42.
**Aktion:** `GetBookingUseCase.ExecuteAsync(42)` aufrufen.
**Erwartetes Ergebnis:** BookingDto mit allen Feldern (ID, Status, Ressource, Formulardaten).

---

### TS-003.02: Nicht existierende Buchung liefert 404-Fehlermeldung

| Feld | Wert |
|------|------|
| **Schicht** | Server |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** API liefert 404.
**Aktion:** `GetBookingTool.ExecuteAsync(id: 999)` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Buchung mit ID 999 nicht gefunden."

---

### TS-003.03: Ungültige ID wird abgelehnt

| Feld | Wert |
|------|------|
| **Schicht** | Server |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** —
**Aktion:** `GetBookingTool.ExecuteAsync(id: 0)` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Ungültige Buchungs-ID."
