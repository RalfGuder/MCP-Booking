---
id: "029"
title: Tool get_settings_group
tags:
  - UserStory
status: open
---

# US-029: Tool get_settings_group

**Issue:** [#29 — US-029 Tool: get_settings_group](https://github.com/RalfGuder/MCP-Booking/issues/29)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`GET /settings/{group}`

## Story

**Als** Nutzer eines KI-Assistenten,
**möchte ich** die Einstellungen einer bestimmten Gruppe abrufen können,
**damit** ich gezielt nur die relevanten Einstellungen (z.B. nur Kalender-Einstellungen) einsehen kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| group | string | ja | Gruppenname (Pattern: `^[a-z_]+$`) |

## Akzeptanzkriterien

- [ ] MCP-Tool `get_settings_group` ist registriert und aufrufbar
- [ ] Pflichtfeld `group` wird validiert (nur Kleinbuchstaben und Unterstriche)
- [ ] Einstellungen der Gruppe werden zurückgegeben
- [ ] 404-Fehler bei nicht existierender Gruppe wird verständlich gemeldet
- [ ] API-Fehler werden als verständliche Fehlermeldung zurückgegeben
- [ ] Unit-Test: Erfolgreicher Abruf
- [ ] Unit-Test: Gruppe nicht gefunden (404)
- [ ] Unit-Test: API-Fehlerbehandlung
