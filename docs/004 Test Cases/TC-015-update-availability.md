---
id: "015"
title: Verfügbarkeit aktualisieren
tags:
  - TestCase
  - Availability
status: ausstehend
---

# TC-015: Verfügbarkeit aktualisieren

**Use Case:** [UC-015 Verfügbarkeit aktualisieren](../002%20Use%20Cases/UC-015-update-availability.md)
**User Story:** [US-020 Tool: update_availability](../001%20User%20Stories/US-020-update-availability.md)
**Requirement:** [REQ-025 Tool: update_availability](../003%20Requirements/REQ-025-update-availability.md)

## Testszenarien

### TS-015.01: Verfügbarkeit wird erfolgreich aktualisiert

**Schicht:** Application | **Status:** Ausstehend
**Vorbedingung:** Repository-Mock akzeptiert Aktualisierung.
**Aktion:** `UpdateAvailabilityUseCase.ExecuteAsync(1)` aufrufen.
**Erwartetes Ergebnis:** Bestätigung der Aktualisierung.

### TS-015.02: Nicht existierende Ressource liefert 404-Fehlermeldung

**Schicht:** Server | **Status:** Ausstehend
**Vorbedingung:** API liefert 404.
**Aktion:** `UpdateAvailabilityTool.ExecuteAsync(resourceId: 999)` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Ressource mit ID 999 nicht gefunden."
