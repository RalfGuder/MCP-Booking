---
id: "028"
title: "Tool: create_form"
tags:
  - Requirement
  - Funktional
  - Forms
priority: mittel
status: open
---

# REQ-028: Tool create_form

## Beschreibung

Der MCP-Server **muss** ein Tool `create_form` bereitstellen, das über `POST /forms` ein neues Buchungsformular mit Titel und Struktur anlegt.

## Quelle

- [US-023](../001%20User%20Stories/US-023-create-form.md) | [UC-018](../002%20Use%20Cases/UC-018-create-form.md)

## Akzeptanzkriterien

1. Tool `create_form` ist im MCP-Server registriert.
2. Pflichtparameter: title (string), structure_json (gültiger JSON-String).
3. Optionale Parameter: form_slug, settings_json, status (published/draft).
4. Erfolgreiche Erstellung liefert Formular-ID.
5. Ungültiges JSON wird mit Fehlermeldung abgelehnt.
6. Fehlerbehandlung gemäß REQ-005.

## Abhängigkeiten

REQ-001, REQ-005, REQ-006, REQ-008
