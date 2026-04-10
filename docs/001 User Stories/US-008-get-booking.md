# User Story: Tool get_booking

**Issue:** [#8 — US-008 Tool: get_booking](https://github.com/RalfGuder/MCP-Booking/issues/8)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`GET /bookings/{id}`

## Story

**Als** Nutzer eines KI-Assistenten,
**moechte ich** die Details einer einzelnen Buchung abrufen koennen,
**damit** ich alle Informationen zu einer bestimmten Buchung einsehen kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| id | integer | ja | Eindeutige Buchungs-ID |

## Akzeptanzkriterien

- [ ] MCP-Tool `get_booking` ist registriert und aufrufbar
- [ ] Buchungsdetails werden vollstaendig zurueckgegeben
- [ ] 404-Fehler bei nicht existierender Buchung wird verstaendlich gemeldet
- [ ] API-Fehler werden als verstaendliche Fehlermeldung zurueckgegeben
- [ ] Unit-Test: Erfolgreicher Abruf
- [ ] Unit-Test: Buchung nicht gefunden (404)
- [ ] Unit-Test: API-Fehlerbehandlung
