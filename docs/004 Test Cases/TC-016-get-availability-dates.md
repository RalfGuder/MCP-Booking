---
id: "016"
title: Verfügbare Daten abrufen
tags:
  - TestCase
  - Availability
status: ausstehend
---

# TC-016: Verfügbare Daten abrufen

**Use Case:** [UC-016 Verfügbare Daten abrufen](../002%20Use%20Cases/UC-016-get-availability-dates.md)
**User Story:** [US-021 Tool: get_availability_dates](../001%20User%20Stories/US-021-get-availability-dates.md)
**Requirement:** [REQ-026 Tool: get_availability_dates](../003%20Requirements/REQ-026-get-availability-dates.md)

## Testszenarien

### TS-016.01: Verfügbare Daten werden korrekt zurückgegeben

**Schicht:** Application | **Status:** Ausstehend
**Vorbedingung:** Repository-Mock liefert verfügbare Daten.
**Aktion:** `GetAvailabilityDatesUseCase.ExecuteAsync(1, "2026-07-01", "2026-07-31")` aufrufen.
**Erwartetes Ergebnis:** Liste verfügbarer Daten.

### TS-016.02: Keine verfügbaren Daten liefert leere Liste

**Schicht:** Application | **Status:** Ausstehend
**Vorbedingung:** Repository-Mock liefert leere Liste.
**Aktion:** `GetAvailabilityDatesUseCase.ExecuteAsync(1, "2026-12-24", "2026-12-26")` aufrufen.
**Erwartetes Ergebnis:** Leere Collection.

### TS-016.03: Ungültiger Datumsbereich wird abgelehnt

**Schicht:** Server | **Status:** Ausstehend
**Vorbedingung:** —
**Aktion:** Tool mit date_to < date_from aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Enddatum muss nach dem Startdatum liegen."
