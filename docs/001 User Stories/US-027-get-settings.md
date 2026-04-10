---
id: "027"
title: Tool list_settings
tags:
  - UserStory
status: open
---

# US-027: Tool list_settings

**Issue:** [#27 — US-027 Tool: list_settings](https://github.com/RalfGuder/MCP-Booking/issues/27)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`GET /settings`

## Story

**Als** Nutzer eines KI-Assistenten,
**moechte ich** alle Einstellungen des Buchungssystems abrufen koennen,
**damit** ich die aktuelle Konfiguration (Kalender, Buchung, UI, Bestaetigung) einsehen kann.

## Parameter

Keine Parameter erforderlich.

## Akzeptanzkriterien

- [ ] MCP-Tool `list_settings` ist registriert und aufrufbar
- [ ] Alle Einstellungsgruppen werden zurueckgegeben
- [ ] API-Fehler werden als verstaendliche Fehlermeldung zurueckgegeben
- [ ] Unit-Test: Erfolgreicher Abruf
- [ ] Unit-Test: API-Fehlerbehandlung
