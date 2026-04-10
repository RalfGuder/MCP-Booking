# User Story: Tool approve_booking

**Issue:** [#11 — US-011 Tool: approve_booking](https://github.com/RalfGuder/MCP-Booking/issues/11)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`POST /bookings/{id}/approve`

## Story

**Als** Nutzer eines KI-Assistenten,
**moechte ich** eine ausstehende Buchung genehmigen koennen,
**damit** ich den Buchungsstatus von "pending" auf "approved" setzen kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| id | integer | ja | Eindeutige Buchungs-ID |

## Akzeptanzkriterien

- [ ] MCP-Tool `approve_booking` ist registriert und aufrufbar
- [ ] Erfolgreiche Genehmigung liefert Bestaetigung
- [ ] 404-Fehler bei nicht existierender Buchung wird verstaendlich gemeldet
- [ ] API-Fehler werden als verstaendliche Fehlermeldung zurueckgegeben
- [ ] Unit-Test: Erfolgreiche Genehmigung
- [ ] Unit-Test: Buchung nicht gefunden (404)
- [ ] Unit-Test: API-Fehlerbehandlung
