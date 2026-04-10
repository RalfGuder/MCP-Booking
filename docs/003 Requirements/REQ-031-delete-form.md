---
id: "031"
title: "Tool: delete_form"
tags:
  - Requirement
  - Funktional
  - Forms
priority: mittel
status: open
---

# REQ-031: Tool delete_form

## Beschreibung

Der MCP-Server **muss** ein Tool `delete_form` bereitstellen, das über `DELETE /forms/{id}` ein Formular löscht.

## Quelle

- [US-026](../001%20User%20Stories/US-026-delete-form.md) | [UC-021](../002%20Use%20Cases/UC-021-delete-form.md)

## Akzeptanzkriterien

1. Tool `delete_form` ist im MCP-Server registriert.
2. Pflichtparameter: id (integer, > 0).
3. Erfolgreiche Löschung liefert Bestätigung.
4. Fehlerbehandlung gemäß REQ-005.

## Abhängigkeiten

REQ-001, REQ-005, REQ-006, REQ-008
