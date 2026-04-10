---
id: "024"
title: Tool get_form
tags:
  - UserStory
status: open
---

# US-024: Tool get_form

**Issue:** [#24 — US-024 Tool: get_form](https://github.com/RalfGuder/MCP-Booking/issues/24)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`GET /forms/{id}`

## Story

**Als** Nutzer eines KI-Assistenten,
**moechte ich** die Details eines einzelnen Formulars abrufen koennen,
**damit** ich Struktur und Einstellungen eines bestimmten Formulars einsehen kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| id | integer | ja | Eindeutige Formular-ID |

## Akzeptanzkriterien

- [ ] MCP-Tool `get_form` ist registriert und aufrufbar
- [ ] Formulardetails werden vollstaendig zurueckgegeben (inkl. structure_json, settings_json)
- [ ] 404-Fehler bei nicht existierendem Formular wird verstaendlich gemeldet
- [ ] API-Fehler werden als verstaendliche Fehlermeldung zurueckgegeben
- [ ] Unit-Test: Erfolgreicher Abruf
- [ ] Unit-Test: Formular nicht gefunden (404)
- [ ] Unit-Test: API-Fehlerbehandlung
