# User Story: Tool update_availability

**Issue:** [#20 — US-020 Tool: update_availability](https://github.com/RalfGuder/MCP-Booking/issues/20)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`PUT /availability/{resource_id}`

## Story

**Als** Nutzer eines KI-Assistenten,
**moechte ich** die Verfuegbarkeit einer Ressource aktualisieren koennen,
**damit** ich Zeitraeume als verfuegbar oder nicht verfuegbar markieren kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| resource_id | integer | ja | Ressource-ID |

## Akzeptanzkriterien

- [ ] MCP-Tool `update_availability` ist registriert und aufrufbar
- [ ] Pflichtfeld `resource_id` wird validiert
- [ ] Erfolgreiche Aktualisierung liefert Bestaetigung
- [ ] 404-Fehler bei nicht existierender Ressource wird verstaendlich gemeldet
- [ ] API-Fehler werden als verstaendliche Fehlermeldung zurueckgegeben
- [ ] Unit-Test: Erfolgreiche Aktualisierung
- [ ] Unit-Test: Ressource nicht gefunden (404)
- [ ] Unit-Test: API-Fehlerbehandlung
