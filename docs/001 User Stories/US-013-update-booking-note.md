---
id: "013"
title: Tool update_booking_note
tags:
  - UserStory
status: open
---

# US-013: Tool update_booking_note

**Issue:** [#13 — US-013 Tool: update_booking_note](https://github.com/RalfGuder/MCP-Booking/issues/13)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`PUT /bookings/{id}/note`

## Story

**Als** Nutzer eines KI-Assistenten,
**möchte ich** einer Buchung eine Notiz hinzufügen können,
**damit** ich zusätzliche Informationen zu einer Buchung dokumentieren kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| id | integer | ja | Eindeutige Buchungs-ID |
| note | string | ja | Notiztext |

## Akzeptanzkriterien

- [ ] MCP-Tool `update_booking_note` ist registriert und aufrufbar
- [ ] Pflichtfeld `note` wird validiert
- [ ] Erfolgreiche Aktualisierung liefert Bestätigung
- [ ] 404-Fehler bei nicht existierender Buchung wird verständlich gemeldet
- [ ] API-Fehler werden als verständliche Fehlermeldung zurückgegeben
- [ ] Unit-Test: Erfolgreiche Notizerstellung
- [ ] Unit-Test: Buchung nicht gefunden (404)
- [ ] Unit-Test: API-Fehlerbehandlung
