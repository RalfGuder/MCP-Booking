---
id: "027"
title: "Tool: list_forms"
tags:
  - Requirement
  - Funktional
  - Forms
priority: mittel
status: open
---

# REQ-027: Tool list_forms

## Beschreibung

Der MCP-Server **muss** ein Tool `list_forms` bereitstellen, das über `GET /forms` alle Buchungsformulare paginiert auflistet.

## Quelle

- [US-022](../001%20User%20Stories/US-022-list-forms.md) | [UC-017](../002%20Use%20Cases/UC-017-list-forms.md)

## Akzeptanzkriterien

1. Tool `list_forms` ist im MCP-Server registriert.
2. Unterstützte Parameter: page, per_page.
3. Ergebnis enthält Liste der Formulare (ID, Titel, Slug, Status).
4. Paginierung gemäß REQ-007.
5. Fehlerbehandlung gemäß REQ-005.

## Abhängigkeiten

REQ-001, REQ-005, REQ-006, REQ-007, REQ-008
