---
id: "024"
title: Einstellungsgruppe abrufen
tags:
  - TestCase
  - Settings
status: ausstehend
---

# TC-024: Einstellungsgruppe abrufen

**Use Case:** [UC-024 Einstellungsgruppe abrufen](../002%20Use%20Cases/UC-024-get-settings-group.md)
**User Story:** [US-029 Tool: get_settings_group](../001%20User%20Stories/US-029-get-settings-group.md)
**Requirement:** [REQ-034 Tool: get_settings_group](../003%20Requirements/REQ-034-get-settings-group.md)

## Testszenarien

### TS-024.01: Einstellungen einer Gruppe werden korrekt zurückgegeben

**Schicht:** Application | **Status:** Ausstehend
**Vorbedingung:** Repository-Mock liefert calendar-Einstellungen.
**Aktion:** `GetSettingsGroupUseCase.ExecuteAsync("calendar")` aufrufen.
**Erwartetes Ergebnis:** SettingsGroupDto mit calendar-Einstellungen.

### TS-024.02: Nicht existierende Gruppe liefert 404-Fehlermeldung

**Schicht:** Server | **Status:** Ausstehend
**Vorbedingung:** API liefert 404.
**Aktion:** `GetSettingsGroupTool.ExecuteAsync(group: "nonexistent")` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Einstellungsgruppe 'nonexistent' nicht gefunden."

### TS-024.03: Ungültiger Gruppenname wird abgelehnt

**Schicht:** Server | **Status:** Ausstehend
**Vorbedingung:** —
**Aktion:** `GetSettingsGroupTool.ExecuteAsync(group: "INVALID!")` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Ungültiger Gruppenname."
