---
id: "028"
title: Tool update_settings
tags:
  - UserStory
status: open
---

# US-028: Tool update_settings

**Issue:** [#28 — US-028 Tool: update_settings](https://github.com/RalfGuder/MCP-Booking/issues/28)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`PUT /settings`

## Story

**Als** Nutzer eines KI-Assistenten,
**möchte ich** die Einstellungen des Buchungssystems aktualisieren können,
**damit** ich Kalender-, Buchungs-, UI- und Bestätigungseinstellungen ändern kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| calendar | object | nein | Kalender-Anzeige- und Verhaltenseinstellungen |
| booking | object | nein | Buchungs-Workflow-Einstellungen |
| ui | object | nein | Benutzeroberflächen-Einstellungen |
| confirmation | object | nein | Bestätigungs- und Benachrichtigungseinstellungen |

## Akzeptanzkriterien

- [ ] MCP-Tool `update_settings` ist registriert und aufrufbar
- [ ] Partielle Aktualisierung möglich (nur geänderte Gruppen senden)
- [ ] Erfolgreiche Aktualisierung liefert Bestätigung
- [ ] API-Fehler werden als verständliche Fehlermeldung zurückgegeben
- [ ] Unit-Test: Erfolgreiche Aktualisierung
- [ ] Unit-Test: API-Fehlerbehandlung
