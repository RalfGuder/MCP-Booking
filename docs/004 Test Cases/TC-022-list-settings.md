---
id: "022"
title: Einstellungen auflisten
tags:
  - TestCase
  - Settings
status: ausstehend
---

# TC-022: Einstellungen auflisten

**Use Case:** [UC-022 Einstellungen auflisten](../002%20Use%20Cases/UC-022-list-settings.md)
**User Story:** [US-027 Tool: list_settings](../001%20User%20Stories/US-027-get-settings.md)
**Requirement:** [REQ-032 Tool: list_settings](../003%20Requirements/REQ-032-list-settings.md)

## Testszenarien

### TS-022.01: Alle Einstellungsgruppen werden zurückgegeben

**Schicht:** Application | **Status:** Ausstehend
**Vorbedingung:** Repository-Mock liefert alle 4 Gruppen (calendar, booking, ui, confirmation).
**Aktion:** `ListSettingsUseCase.ExecuteAsync()` aufrufen.
**Erwartetes Ergebnis:** SettingsDto mit allen 4 Gruppen.

### TS-022.02: Authentifizierungsfehler wird verständlich gemeldet

**Schicht:** Server | **Status:** Ausstehend
**Vorbedingung:** API liefert 401.
**Aktion:** `ListSettingsTool.ExecuteAsync()` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Authentifizierung fehlgeschlagen."
