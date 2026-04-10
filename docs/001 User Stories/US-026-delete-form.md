---
id: "026"
title: Tool delete_form
tags:
  - UserStory
status: open
---

# US-026: Tool delete_form

**Issue:** [#26 — US-026 Tool: delete_form](https://github.com/RalfGuder/MCP-Booking/issues/26)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`DELETE /forms/{id}`

## Story

**Als** Nutzer eines KI-Assistenten,
**möchte ich** ein Formular löschen können,
**damit** ich nicht mehr benötigte Buchungsformulare entfernen kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| id | integer | ja | Eindeutige Formular-ID |

## Akzeptanzkriterien

- [ ] MCP-Tool `delete_form` ist registriert und aufrufbar
- [ ] Erfolgreiche Löschung liefert Bestätigung
- [ ] 404-Fehler bei nicht existierendem Formular wird verständlich gemeldet
- [ ] API-Fehler werden als verständliche Fehlermeldung zurückgegeben
- [ ] Unit-Test: Erfolgreiche Löschung
- [ ] Unit-Test: Formular nicht gefunden (404)
- [ ] Unit-Test: API-Fehlerbehandlung
