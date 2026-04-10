---
id: "012"
title: Tool set_booking_pending
tags:
  - UserStory
status: open
---

# US-012: Tool set_booking_pending

**Issue:** [#12 — US-012 Tool: set_booking_pending](https://github.com/RalfGuder/MCP-Booking/issues/12)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`POST /bookings/{id}/pending`

## Story

**Als** Nutzer eines KI-Assistenten,
**möchte ich** eine genehmigte Buchung zurück auf "ausstehend" setzen können,
**damit** ich eine bereits genehmigte Buchung erneut prüfen kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| id | integer | ja | Eindeutige Buchungs-ID |

## Akzeptanzkriterien

- [ ] MCP-Tool `set_booking_pending` ist registriert und aufrufbar
- [ ] Erfolgreiche Statusänderung liefert Bestätigung
- [ ] 404-Fehler bei nicht existierender Buchung wird verständlich gemeldet
- [ ] API-Fehler werden als verständliche Fehlermeldung zurückgegeben
- [ ] Unit-Test: Erfolgreiche Statusänderung
- [ ] Unit-Test: Buchung nicht gefunden (404)
- [ ] Unit-Test: API-Fehlerbehandlung
