---
id: "010"
title: Tool delete_booking
tags:
  - UserStory
status: open
---

# US-010: Tool delete_booking

**Issue:** [#10 — US-010 Tool: delete_booking](https://github.com/RalfGuder/MCP-Booking/issues/10)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`DELETE /bookings/{id}`

## Story

**Als** Nutzer eines KI-Assistenten,
**möchte ich** eine Buchung löschen können,
**damit** ich nicht mehr benötigte Buchungen entfernen kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| id | integer | ja | Eindeutige Buchungs-ID |

## Akzeptanzkriterien

- [ ] MCP-Tool `delete_booking` ist registriert und aufrufbar
- [ ] Erfolgreiche Löschung liefert Bestätigung
- [ ] 404-Fehler bei nicht existierender Buchung wird verständlich gemeldet
- [ ] API-Fehler werden als verständliche Fehlermeldung zurückgegeben
- [ ] Unit-Test: Erfolgreiche Löschung
- [ ] Unit-Test: Buchung nicht gefunden (404)
- [ ] Unit-Test: API-Fehlerbehandlung
