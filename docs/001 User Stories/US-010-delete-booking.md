# User Story: Tool delete_booking

**Issue:** [#10 — US-010 Tool: delete_booking](https://github.com/RalfGuder/MCP-Booking/issues/10)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`DELETE /bookings/{id}`

## Story

**Als** Nutzer eines KI-Assistenten,
**moechte ich** eine Buchung loeschen koennen,
**damit** ich nicht mehr benoetigte Buchungen entfernen kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| id | integer | ja | Eindeutige Buchungs-ID |

## Akzeptanzkriterien

- [ ] MCP-Tool `delete_booking` ist registriert und aufrufbar
- [ ] Erfolgreiche Loeschung liefert Bestaetigung
- [ ] 404-Fehler bei nicht existierender Buchung wird verstaendlich gemeldet
- [ ] API-Fehler werden als verstaendliche Fehlermeldung zurueckgegeben
- [ ] Unit-Test: Erfolgreiche Loeschung
- [ ] Unit-Test: Buchung nicht gefunden (404)
- [ ] Unit-Test: API-Fehlerbehandlung
