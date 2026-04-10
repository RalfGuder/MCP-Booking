---
id: "019"
title: "Tool: list_resources"
tags:
  - Requirement
  - Funktional
  - Resources
priority: hoch
status: open
---

# REQ-019: Tool list_resources

## Beschreibung

Der MCP-Server **muss** ein Tool `list_resources` bereitstellen, das über `GET /resources` alle verfügbaren Ressourcen (Buchungstypen) paginiert auflistet.

## Quelle

- [US-014](../001%20User%20Stories/US-014-list-resources.md) | [UC-009](../002%20Use%20Cases/UC-009-list-resources.md)

## Hinweis

Dieses Tool wird in **Phase 1** als erstes Endpunkt-Tool implementiert (siehe US-001).

## Akzeptanzkriterien

1. Tool `list_resources` ist im MCP-Server registriert.
2. Unterstützte Parameter: page, per_page.
3. Ergebnis enthält Liste der Ressourcen (ID, Titel, Kosten, Besucheranzahl).
4. Paginierung gemäß REQ-007.
5. Fehlerbehandlung gemäß REQ-005.

## Abhängigkeiten

REQ-001, REQ-005, REQ-006, REQ-007, REQ-008
