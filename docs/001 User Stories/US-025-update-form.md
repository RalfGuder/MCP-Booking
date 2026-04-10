---
id: "025"
title: Tool update_form
tags:
  - UserStory
status: open
---

# US-025: Tool update_form

**Issue:** [#25 — US-025 Tool: update_form](https://github.com/RalfGuder/MCP-Booking/issues/25)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`PUT /forms/{id}`

## Story

**Als** Nutzer eines KI-Assistenten,
**möchte ich** ein bestehendes Formular aktualisieren können,
**damit** ich Titel, Struktur, Einstellungen oder Status eines Formulars ändern kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| id | integer | ja | Eindeutige Formular-ID |
| title | string | nein | Neuer Anzeigename |
| form_slug | string | nein | Neuer URL-Slug |
| structure_json | string | nein | Neue Formularstruktur (JSON) |
| settings_json | string | nein | Neue Einstellungen (JSON) |
| status | string | nein | Neuer Status: `published`, `draft` |

## Akzeptanzkriterien

- [ ] MCP-Tool `update_form` ist registriert und aufrufbar
- [ ] Partielle Aktualisierung möglich (nur geänderte Felder senden)
- [ ] Erfolgreiche Aktualisierung liefert Bestätigung
- [ ] 404-Fehler bei nicht existierendem Formular wird verständlich gemeldet
- [ ] API-Fehler werden als verständliche Fehlermeldung zurückgegeben
- [ ] Unit-Test: Erfolgreiche Aktualisierung
- [ ] Unit-Test: Formular nicht gefunden (404)
- [ ] Unit-Test: API-Fehlerbehandlung
