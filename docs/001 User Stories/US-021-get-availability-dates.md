---
id: "021"
title: Tool get_availability_dates
tags:
  - UserStory
status: open
---

# US-021: Tool get_availability_dates

**Issue:** [#21 — US-021 Tool: get_availability_dates](https://github.com/RalfGuder/MCP-Booking/issues/21)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`GET /availability/{resource_id}/dates`

## Story

**Als** Nutzer eines KI-Assistenten,
**möchte ich** die verfügbaren Daten einer Ressource abrufen können,
**damit** ich konkrete buchbare Termine für einen Zeitraum erhalte.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| resource_id | integer | ja | Ressource-ID |
| date_from | date | ja | Startdatum (ISO 8601) |
| date_to | date | ja | Enddatum (ISO 8601) |
| prop_name | string | nein | Filter nach Eigenschaft |

## Akzeptanzkriterien

- [ ] MCP-Tool `get_availability_dates` ist registriert und aufrufbar
- [ ] Pflichtfelder `resource_id`, `date_from`, `date_to` werden validiert
- [ ] Liste verfügbarer Daten wird korrekt zurückgegeben
- [ ] Leere Datenliste wird korrekt behandelt
- [ ] API-Fehler werden als verständliche Fehlermeldung zurückgegeben
- [ ] Unit-Test: Erfolgreicher Abruf mit Ergebnissen
- [ ] Unit-Test: Keine verfügbaren Daten
- [ ] Unit-Test: API-Fehlerbehandlung
