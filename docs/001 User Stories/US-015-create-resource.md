---
id: "015"
title: Tool create_resource
tags:
  - UserStory
status: open
---

# US-015: Tool create_resource

**Issue:** [#15 — US-015 Tool: create_resource](https://github.com/RalfGuder/MCP-Booking/issues/15)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`POST /resources`

## Story

**Als** Nutzer eines KI-Assistenten,
**möchte ich** eine neue Ressource (Buchungstyp) anlegen können,
**damit** ich neue buchbare Einheiten im System erstellen kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| title | string | ja | Anzeigename der Ressource |
| cost | string | nein | Kosten der Ressource |
| visitors | integer | nein | Erlaubte Besucheranzahl |

## Akzeptanzkriterien

- [ ] MCP-Tool `create_resource` ist registriert und aufrufbar
- [ ] Pflichtfeld `title` wird validiert
- [ ] Erfolgreiche Erstellung liefert Bestätigung mit Ressource-ID
- [ ] Validierungsfehler werden verständlich weitergegeben
- [ ] API-Fehler werden als verständliche Fehlermeldung zurückgegeben
- [ ] Unit-Test: Erfolgreiche Erstellung
- [ ] Unit-Test: Fehlender Titel
- [ ] Unit-Test: API-Fehlerbehandlung
