# User Story: Tool create_form

**Issue:** [#23 — US-023 Tool: create_form](https://github.com/RalfGuder/MCP-Booking/issues/23)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`POST /forms`

## Story

**Als** Nutzer eines KI-Assistenten,
**moechte ich** ein neues Buchungsformular anlegen koennen,
**damit** ich Formulare fuer verschiedene Buchungstypen erstellen kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| title | string | ja | Anzeigename des Formulars |
| structure_json | string | ja | JSON-String mit Formularstruktur |
| form_slug | string | nein | URL-freundlicher Slug |
| settings_json | string | nein | JSON-String mit Formulareinstellungen |
| status | string | nein | Veroeffentlichungsstatus: `published`, `draft` |

## Akzeptanzkriterien

- [ ] MCP-Tool `create_form` ist registriert und aufrufbar
- [ ] Pflichtfelder `title` und `structure_json` werden validiert
- [ ] Erfolgreiche Erstellung liefert Bestaetigung mit Formular-ID
- [ ] Validierungsfehler werden verstaendlich weitergegeben
- [ ] API-Fehler werden als verstaendliche Fehlermeldung zurueckgegeben
- [ ] Unit-Test: Erfolgreiche Erstellung
- [ ] Unit-Test: Fehlende Pflichtfelder
- [ ] Unit-Test: API-Fehlerbehandlung
