---
id: "007"
title: Tool create_booking
tags:
  - UserStory
status: open
---

# US-007: Tool create_booking

**Issue:** [#7 — US-007 Tool: create_booking](https://github.com/RalfGuder/MCP-Booking/issues/7)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`POST /bookings`

## Story

**Als** Nutzer eines KI-Assistenten,
**möchte ich** eine neue Buchung anlegen können,
**damit** ich Buchungen direkt über den KI-Assistenten erstellen kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| booking_type | integer | ja | Ressource-/Buchungstyp-ID |
| form_data | object | ja | Ausgefüllte Formularfelder |
| dates | array[date-time] | ja | Array der gewünschten Buchungsdaten |

## Akzeptanzkriterien

- [ ] MCP-Tool `create_booking` ist registriert und aufrufbar
- [ ] Alle Pflichtfelder werden validiert
- [ ] Erfolgreiche Erstellung liefert Bestätigung mit Buchungs-ID
- [ ] Validierungsfehler (fehlende Pflichtfelder) werden verständlich weitergegeben
- [ ] API-Fehler werden als verständliche Fehlermeldung zurückgegeben
- [ ] Unit-Test: Erfolgreiche Buchungserstellung
- [ ] Unit-Test: Fehlende Pflichtfelder
- [ ] Unit-Test: API-Fehlerbehandlung
