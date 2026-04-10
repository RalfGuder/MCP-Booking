---
id: "020"
title: Tool update_availability
tags:
  - UserStory
status: open
---

# US-020: Tool update_availability

**Issue:** [#20 — US-020 Tool: update_availability](https://github.com/RalfGuder/MCP-Booking/issues/20)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`PUT /availability/{resource_id}`

## Story

**Als** Nutzer eines KI-Assistenten,
**möchte ich** die Verfügbarkeit einer Ressource aktualisieren können,
**damit** ich Zeiträume als verfügbar oder nicht verfügbar markieren kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| resource_id | integer | ja | Ressource-ID |

## Akzeptanzkriterien

- [ ] MCP-Tool `update_availability` ist registriert und aufrufbar
- [ ] Pflichtfeld `resource_id` wird validiert
- [ ] Erfolgreiche Aktualisierung liefert Bestätigung
- [ ] 404-Fehler bei nicht existierender Ressource wird verständlich gemeldet
- [ ] API-Fehler werden als verständliche Fehlermeldung zurückgegeben
- [ ] Unit-Test: Erfolgreiche Aktualisierung
- [ ] Unit-Test: Ressource nicht gefunden (404)
- [ ] Unit-Test: API-Fehlerbehandlung
