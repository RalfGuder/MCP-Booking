---
id: "030"
title: "Tool: update_form"
tags:
  - Requirement
  - Funktional
  - Forms
priority: mittel
status: open
---

# REQ-030: Tool update_form

## Beschreibung

Der MCP-Server **muss** ein Tool `update_form` bereitstellen, das über `PUT /forms/{id}` ein bestehendes Formular partiell aktualisiert.

## Quelle

- [US-025](../001%20User%20Stories/US-025-update-form.md) | [UC-020](../002%20Use%20Cases/UC-020-update-form.md)

## Akzeptanzkriterien

1. Tool `update_form` ist im MCP-Server registriert.
2. Pflichtparameter: id (integer, > 0).
3. Optionale Parameter: title, form_slug, structure_json, settings_json, status.
4. Partielle Aktualisierung: Nur übergebene Felder werden geändert.
5. JSON-Parameter werden auf Gültigkeit geprüft.
6. Fehlerbehandlung gemäß REQ-005.

## Abhängigkeiten

REQ-001, REQ-005, REQ-006, REQ-008
