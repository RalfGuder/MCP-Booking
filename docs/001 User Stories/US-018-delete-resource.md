---
id: "018"
title: Tool delete_resource
tags:
  - UserStory
status: open
---

# US-018: Tool delete_resource

**Issue:** [#18 — US-018 Tool: delete_resource](https://github.com/RalfGuder/MCP-Booking/issues/18)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`DELETE /resources/{id}`

## Story

**Als** Nutzer eines KI-Assistenten,
**möchte ich** eine Ressource löschen können,
**damit** ich nicht mehr benötigte Buchungstypen entfernen kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| id | integer | ja | Eindeutige Ressource-ID |

## Akzeptanzkriterien

- [ ] MCP-Tool `delete_resource` ist registriert und aufrufbar
- [ ] Erfolgreiche Löschung liefert Bestätigung
- [ ] 404-Fehler bei nicht existierender Ressource wird verständlich gemeldet
- [ ] API-Fehler werden als verständliche Fehlermeldung zurückgegeben
- [ ] Unit-Test: Erfolgreiche Löschung
- [ ] Unit-Test: Ressource nicht gefunden (404)
- [ ] Unit-Test: API-Fehlerbehandlung
