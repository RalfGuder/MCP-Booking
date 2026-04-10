# User Story: Tool list_forms

**Issue:** [#22 — US-022 Tool: list_forms](https://github.com/RalfGuder/MCP-Booking/issues/22)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`GET /forms`

## Story

**Als** Nutzer eines KI-Assistenten,
**moechte ich** alle verfuegbaren Buchungsformulare auflisten koennen,
**damit** ich einen Ueberblick ueber die vorhandenen Formulare erhalte.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| page | integer | nein | Aktuelle Seite (Standard: 1) |
| per_page | integer | nein | Eintraege pro Seite (Standard: 20, Max: 100) |

## Akzeptanzkriterien

- [ ] MCP-Tool `list_forms` ist registriert und aufrufbar
- [ ] Paginierung funktioniert (page, per_page)
- [ ] Leere Ergebnisliste wird korrekt behandelt
- [ ] API-Fehler werden als verstaendliche Fehlermeldung zurueckgegeben
- [ ] Unit-Test: Happy Path mit Ergebnissen
- [ ] Unit-Test: Leere Ergebnisliste
- [ ] Unit-Test: API-Fehlerbehandlung
