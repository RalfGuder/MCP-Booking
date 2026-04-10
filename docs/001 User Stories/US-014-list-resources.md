# User Story: Tool list_resources

**Issue:** [#14 — US-014 Tool: list_resources](https://github.com/RalfGuder/MCP-Booking/issues/14)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`GET /resources`

## Story

**Als** Nutzer eines KI-Assistenten,
**moechte ich** alle verfuegbaren Ressourcen (Buchungstypen) auflisten koennen,
**damit** ich einen Ueberblick ueber die buchbaren Ressourcen erhalte.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| page | integer | nein | Aktuelle Seite (Standard: 1) |
| per_page | integer | nein | Eintraege pro Seite (Standard: 20, Max: 100) |

## Akzeptanzkriterien

- [ ] MCP-Tool `list_resources` ist registriert und aufrufbar
- [ ] Paginierung funktioniert (page, per_page)
- [ ] Leere Ergebnisliste wird korrekt behandelt
- [ ] API-Fehler werden als verstaendliche Fehlermeldung zurueckgegeben
- [ ] Unit-Test: Happy Path mit Ergebnissen
- [ ] Unit-Test: Leere Ergebnisliste
- [ ] Unit-Test: API-Fehlerbehandlung
