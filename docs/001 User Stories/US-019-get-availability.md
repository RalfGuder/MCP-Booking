---
id: "019"
title: Tool get_availability
tags:
  - UserStory
status: open
---

# US-019: Tool get_availability

**Issue:** [#19 — US-019 Tool: get_availability](https://github.com/RalfGuder/MCP-Booking/issues/19)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`GET /availability/{resource_id}`

## Story

**Als** Nutzer eines KI-Assistenten,
**moechte ich** die Verfuegbarkeit einer Ressource fuer einen Zeitraum abfragen koennen,
**damit** ich sehen kann, wann eine Ressource buchbar ist.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| resource_id | integer | ja | Ressource-ID |
| date_from | date | ja | Startdatum (ISO 8601) |
| date_to | date | ja | Enddatum (ISO 8601) |
| prop_name | string | nein | Filter nach Eigenschaft |

## Akzeptanzkriterien

- [ ] MCP-Tool `get_availability` ist registriert und aufrufbar
- [ ] Pflichtfelder `resource_id`, `date_from`, `date_to` werden validiert
- [ ] Verfuegbarkeitsdaten werden korrekt zurueckgegeben
- [ ] 404-Fehler bei nicht existierender Ressource wird verstaendlich gemeldet
- [ ] API-Fehler werden als verstaendliche Fehlermeldung zurueckgegeben
- [ ] Unit-Test: Erfolgreicher Abruf
- [ ] Unit-Test: Ressource nicht gefunden (404)
- [ ] Unit-Test: API-Fehlerbehandlung
