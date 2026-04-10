# User Story: Tool get_resource

**Issue:** [#16 — US-016 Tool: get_resource](https://github.com/RalfGuder/MCP-Booking/issues/16)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`GET /resources/{id}`

## Story

**Als** Nutzer eines KI-Assistenten,
**moechte ich** die Details einer einzelnen Ressource abrufen koennen,
**damit** ich alle Informationen zu einem bestimmten Buchungstyp einsehen kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| id | integer | ja | Eindeutige Ressource-ID |

## Akzeptanzkriterien

- [ ] MCP-Tool `get_resource` ist registriert und aufrufbar
- [ ] Ressourcendetails werden vollstaendig zurueckgegeben
- [ ] 404-Fehler bei nicht existierender Ressource wird verstaendlich gemeldet
- [ ] API-Fehler werden als verstaendliche Fehlermeldung zurueckgegeben
- [ ] Unit-Test: Erfolgreicher Abruf
- [ ] Unit-Test: Ressource nicht gefunden (404)
- [ ] Unit-Test: API-Fehlerbehandlung
