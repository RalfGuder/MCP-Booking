---
id: "006"
title: Tool list_bookings
tags:
  - UserStory
status: open
---

# US-006: Tool list_bookings

**Issue:** [#6 — US-006 Tool: list_bookings](https://github.com/RalfGuder/MCP-Booking/issues/6)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`GET /bookings`

## Story

**Als** Nutzer eines KI-Assistenten,
**moechte ich** Buchungen auflisten und nach verschiedenen Kriterien filtern koennen,
**damit** ich einen Ueberblick ueber vorhandene Buchungen erhalte.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| page | integer | nein | Aktuelle Seite (Standard: 1) |
| per_page | integer | nein | Eintraege pro Seite (Standard: 20, Max: 100) |
| resource_id | integer | nein | Filter nach Ressource/Buchungstyp |
| status | string | nein | Filter nach Status: `pending`, `approved`, `trash` |
| date_from | date | nein | Buchungen ab diesem Datum (ISO 8601) |
| date_to | date | nein | Buchungen bis zu diesem Datum (ISO 8601) |
| is_new | boolean | nein | Filter nach neu/ungelesen |
| search | string | nein | Volltextsuche |
| orderby | string | nein | Sortierung: `booking_id`, `sort_date`, `modification_date` |
| order | string | nein | Sortierrichtung: `ASC`, `DESC` |

## Akzeptanzkriterien

- [ ] MCP-Tool `list_bookings` ist registriert und aufrufbar
- [ ] Alle Filter-Parameter werden korrekt an die API weitergegeben
- [ ] Paginierung funktioniert (page, per_page)
- [ ] Leere Ergebnisliste wird korrekt behandelt
- [ ] API-Fehler werden als verstaendliche Fehlermeldung zurueckgegeben
- [ ] Unit-Test: Happy Path mit Ergebnissen
- [ ] Unit-Test: Leere Ergebnisliste
- [ ] Unit-Test: Filterung nach Status
- [ ] Unit-Test: API-Fehlerbehandlung
