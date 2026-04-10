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
**moechte ich** die Einstellungen des Buchungssystems aktualisieren koennen,
**damit** ich Kalender-, Buchungs-, UI- und Bestaetigungseinstellungen aendern kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| calendar | object | nein | Kalender-Anzeige- und Verhaltenseinstellungen |
| booking | object | nein | Buchungs-Workflow-Einstellungen |
| ui | object | nein | Benutzeroberflaechen-Einstellungen |
| confirmation | object | nein | Bestaetigungs- und Benachrichtigungseinstellungen |

## Akzeptanzkriterien

- [ ] MCP-Tool `update_settings` ist registriert und aufrufbar
- [ ] Partielle Aktualisierung moeglich (nur geaenderte Gruppen senden)
- [ ] Erfolgreiche Aktualisierung liefert Bestaetigung
- [ ] API-Fehler werden als verstaendliche Fehlermeldung zurueckgegeben
- [ ] Unit-Test: Erfolgreiche Aktualisierung
- [ ] Unit-Test: API-Fehlerbehandlung
