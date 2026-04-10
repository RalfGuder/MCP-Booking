---
id: "017"
title: Tool update_resource
tags:
  - UserStory
status: open
---

# US-017: Tool update_resource

**Issue:** [#17 — US-017 Tool: update_resource](https://github.com/RalfGuder/MCP-Booking/issues/17)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`PUT /resources/{id}`

## Story

**Als** Nutzer eines KI-Assistenten,
**möchte ich** eine bestehende Ressource aktualisieren können,
**damit** ich Eigenschaften wie Name, Kosten oder Besucheranzahl ändern kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| id | integer | ja | Eindeutige Ressource-ID |
| title | string | nein | Neuer Anzeigename |
| cost | string | nein | Neue Kosten |
| visitors | integer | nein | Neue Besucheranzahl |

## Akzeptanzkriterien

- [ ] MCP-Tool `update_resource` ist registriert und aufrufbar
- [ ] Partielle Aktualisierung möglich (nur geänderte Felder senden)
- [ ] Erfolgreiche Aktualisierung liefert Bestätigung
- [ ] 404-Fehler bei nicht existierender Ressource wird verständlich gemeldet
- [ ] API-Fehler werden als verständliche Fehlermeldung zurückgegeben
- [ ] Unit-Test: Erfolgreiche Aktualisierung
- [ ] Unit-Test: Ressource nicht gefunden (404)
- [ ] Unit-Test: API-Fehlerbehandlung
