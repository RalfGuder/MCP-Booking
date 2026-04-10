# User Story: Tool set_booking_pending

**Issue:** [#12 — US-012 Tool: set_booking_pending](https://github.com/RalfGuder/MCP-Booking/issues/12)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`POST /bookings/{id}/pending`

## Story

**Als** Nutzer eines KI-Assistenten,
**moechte ich** eine genehmigte Buchung zurueck auf "ausstehend" setzen koennen,
**damit** ich eine bereits genehmigte Buchung erneut pruefen kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| id | integer | ja | Eindeutige Buchungs-ID |

## Akzeptanzkriterien

- [ ] MCP-Tool `set_booking_pending` ist registriert und aufrufbar
- [ ] Erfolgreiche Statusaenderung liefert Bestaetigung
- [ ] 404-Fehler bei nicht existierender Buchung wird verstaendlich gemeldet
- [ ] API-Fehler werden als verstaendliche Fehlermeldung zurueckgegeben
- [ ] Unit-Test: Erfolgreiche Statusaenderung
- [ ] Unit-Test: Buchung nicht gefunden (404)
- [ ] Unit-Test: API-Fehlerbehandlung
