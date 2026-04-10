---
id: "023"
title: Tool create_form
tags:
  - UserStory
status: open
---

# US-023: Tool create_form

**Issue:** [#23 — US-023 Tool: create_form](https://github.com/RalfGuder/MCP-Booking/issues/23)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`POST /forms`

## Story

**Als** Nutzer eines KI-Assistenten,
**möchte ich** ein neues Buchungsformular anlegen können,
**damit** ich Formulare für verschiedene Buchungstypen erstellen kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| title | string | ja | Anzeigename des Formulars |
| structure_json | string | ja | JSON-String mit Formularstruktur |
| form_slug | string | nein | URL-freundlicher Slug |
| settings_json | string | nein | JSON-String mit Formulareinstellungen |
| status | string | nein | Veröffentlichungsstatus: `published`, `draft` |

## Akzeptanzkriterien

- [ ] MCP-Tool `create_form` ist registriert und aufrufbar
- [ ] Pflichtfelder `title` und `structure_json` werden validiert
- [ ] Erfolgreiche Erstellung liefert Bestätigung mit Formular-ID
- [ ] Validierungsfehler werden verständlich weitergegeben
- [ ] API-Fehler werden als verständliche Fehlermeldung zurückgegeben
- [ ] Unit-Test: Erfolgreiche Erstellung
- [ ] Unit-Test: Fehlende Pflichtfelder
- [ ] Unit-Test: API-Fehlerbehandlung
