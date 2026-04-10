---
id: "029"
title: "Tool: get_form"
tags:
  - Requirement
  - Funktional
  - Forms
priority: mittel
status: open
---

# REQ-029: Tool get_form

## Beschreibung

Der MCP-Server **muss** ein Tool `get_form` bereitstellen, das über `GET /forms/{id}` die vollständigen Details eines Formulars abruft (inkl. Struktur und Einstellungen).

## Quelle

- [US-024](../001%20User%20Stories/US-024-get-form.md) | [UC-019](../002%20Use%20Cases/UC-019-get-form.md)

## Akzeptanzkriterien

1. Tool `get_form` ist im MCP-Server registriert.
2. Pflichtparameter: id (integer, > 0).
3. Formulardetails werden vollständig zurückgegeben (ID, Titel, Slug, Status, structure_json, settings_json).
4. Fehlerbehandlung gemäß REQ-005.

## Abhängigkeiten

REQ-001, REQ-005, REQ-006, REQ-008
