---
id: "023"
title: Einstellungen aktualisieren
tags:
  - TestCase
  - Settings
status: ausstehend
---

# TC-023: Einstellungen aktualisieren

**Use Case:** [UC-023 Einstellungen aktualisieren](../002%20Use%20Cases/UC-023-update-settings.md)
**User Story:** [US-028 Tool: update_settings](../001%20User%20Stories/US-028-update-settings.md)
**Requirement:** [REQ-033 Tool: update_settings](../003%20Requirements/REQ-033-update-settings.md)

## Testszenarien

### TS-023.01: Einstellungen werden erfolgreich aktualisiert

**Schicht:** Application | **Status:** Ausstehend
**Vorbedingung:** Repository-Mock akzeptiert Aktualisierung.
**Aktion:** `UpdateSettingsUseCase.ExecuteAsync(calendar: {...})` aufrufen.
**Erwartetes Ergebnis:** Bestätigung der Aktualisierung.

### TS-023.02: Keine Einstellungsgruppe angegeben liefert Fehlermeldung

**Schicht:** Server | **Status:** Ausstehend
**Vorbedingung:** —
**Aktion:** `UpdateSettingsTool.ExecuteAsync()` ohne Gruppen aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Mindestens eine Einstellungsgruppe muss angegeben werden."

### TS-023.03: Partielle Aktualisierung nur einer Gruppe

**Schicht:** Server | **Status:** Ausstehend
**Vorbedingung:** UseCase-Mock akzeptiert Aktualisierung.
**Aktion:** `UpdateSettingsTool.ExecuteAsync(calendar: {...})` nur mit calendar aufrufen.
**Erwartetes Ergebnis:** Nur calendar wird aktualisiert, andere Gruppen bleiben unverändert.
