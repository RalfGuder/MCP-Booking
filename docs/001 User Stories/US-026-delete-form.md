# User Story: Tool delete_form

**Issue:** [#26 — US-026 Tool: delete_form](https://github.com/RalfGuder/MCP-Booking/issues/26)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`DELETE /forms/{id}`

## Story

**Als** Nutzer eines KI-Assistenten,
**moechte ich** ein Formular loeschen koennen,
**damit** ich nicht mehr benoetigte Buchungsformulare entfernen kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| id | integer | ja | Eindeutige Formular-ID |

## Akzeptanzkriterien

- [ ] MCP-Tool `delete_form` ist registriert und aufrufbar
- [ ] Erfolgreiche Loeschung liefert Bestaetigung
- [ ] 404-Fehler bei nicht existierendem Formular wird verstaendlich gemeldet
- [ ] API-Fehler werden als verstaendliche Fehlermeldung zurueckgegeben
- [ ] Unit-Test: Erfolgreiche Loeschung
- [ ] Unit-Test: Formular nicht gefunden (404)
- [ ] Unit-Test: API-Fehlerbehandlung
