---
id: "009"
title: Tool update_booking
tags:
  - UserStory
status: open
---

# US-009: Tool update_booking

**Issue:** [#9 — US-009 Tool: update_booking](https://github.com/RalfGuder/MCP-Booking/issues/9)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`PUT /bookings/{id}`

## Story

**Als** Nutzer eines KI-Assistenten,
**möchte ich** eine bestehende Buchung aktualisieren können,
**damit** ich Buchungsdaten wie Formulardaten, Typ oder Status ändern kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| id | integer | ja | Eindeutige Buchungs-ID |
| form_data | object | nein | Aktualisierte Formularfelder |
| booking_type | integer | nein | Neuer Buchungstyp |
| status | string | nein | Neuer Buchungsstatus |

## Akzeptanzkriterien

- [ ] MCP-Tool `update_booking` ist registriert und aufrufbar
- [ ] Partielle Aktualisierung möglich (nur geänderte Felder senden)
- [ ] Erfolgreiche Aktualisierung liefert Bestätigung
- [ ] 404-Fehler bei nicht existierender Buchung wird verständlich gemeldet
- [ ] API-Fehler werden als verständliche Fehlermeldung zurückgegeben
- [ ] Unit-Test: Erfolgreiche Aktualisierung
- [ ] Unit-Test: Buchung nicht gefunden (404)
- [ ] Unit-Test: API-Fehlerbehandlung
