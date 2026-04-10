---
id: "006"
title: Buchung genehmigen
tags:
  - TestCase
  - Bookings
status: ausstehend
---

# TC-006: Buchung genehmigen

**Use Case:** [UC-006 Buchung genehmigen](../002%20Use%20Cases/UC-006-approve-booking.md)
**User Story:** [US-011 Tool: approve_booking](../001%20User%20Stories/US-011-approve-booking.md)
**Requirement:** [REQ-016 Tool: approve_booking](../003%20Requirements/REQ-016-approve-booking.md)

## Testszenarien

### TS-006.01: Buchung wird erfolgreich genehmigt

| Feld | Wert |
|------|------|
| **Schicht** | Application |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** Repository-Mock akzeptiert Genehmigung.
**Aktion:** `ApproveBookingUseCase.ExecuteAsync(42)` aufrufen.
**Erwartetes Ergebnis:** Bestätigung mit Status `approved`.

---

### TS-006.02: Nicht existierende Buchung liefert 404-Fehlermeldung

| Feld | Wert |
|------|------|
| **Schicht** | Server |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** API liefert 404.
**Aktion:** `ApproveBookingTool.ExecuteAsync(id: 999)` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Buchung mit ID 999 nicht gefunden."

---

### TS-006.03: Bereits genehmigte Buchung wird gemeldet

| Feld | Wert |
|------|------|
| **Schicht** | Server |
| **Typ** | Unit-Test |
| **Status** | Ausstehend |

**Vorbedingung:** Buchung hat bereits Status `approved`.
**Aktion:** `ApproveBookingTool.ExecuteAsync(id: 42)` aufrufen.
**Erwartetes Ergebnis:** Hinweis "Die Buchung ist bereits genehmigt."
