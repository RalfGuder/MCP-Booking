---
id: "014"
title: Verfügbarkeit abrufen
tags:
  - TestCase
  - Availability
status: ausstehend
---

# TC-014: Verfügbarkeit abrufen

**Use Case:** [UC-014 Verfügbarkeit abrufen](../002%20Use%20Cases/UC-014-get-availability.md)
**User Story:** [US-019 Tool: get_availability](../001%20User%20Stories/US-019-get-availability.md)
**Requirement:** [REQ-024 Tool: get_availability](../003%20Requirements/REQ-024-get-availability.md)

## Testszenarien

### TS-014.01: Verfügbarkeitsdaten werden korrekt zurückgegeben

**Schicht:** Application | **Status:** Ausstehend
**Vorbedingung:** Repository-Mock liefert Verfügbarkeitsdaten.
**Aktion:** `GetAvailabilityUseCase.ExecuteAsync(1, "2026-06-01", "2026-06-30")` aufrufen.
**Erwartetes Ergebnis:** Verfügbarkeitsdaten pro Tag im Zeitraum.

### TS-014.02: Ungültiger Datumsbereich wird abgelehnt

**Schicht:** Server | **Status:** Ausstehend
**Vorbedingung:** —
**Aktion:** `GetAvailabilityTool.ExecuteAsync(resourceId: 1, dateFrom: "2026-06-30", dateTo: "2026-06-01")` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Enddatum muss nach dem Startdatum liegen."

### TS-014.03: Nicht existierende Ressource liefert 404-Fehlermeldung

**Schicht:** Server | **Status:** Ausstehend
**Vorbedingung:** API liefert 404.
**Aktion:** `GetAvailabilityTool.ExecuteAsync(resourceId: 999)` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Ressource mit ID 999 nicht gefunden."
