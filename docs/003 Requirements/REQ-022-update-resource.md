---
id: "022"
title: "Tool: update_resource"
tags:
  - Requirement
  - Funktional
  - Resources
priority: mittel
status: open
---

# REQ-022: Tool update_resource

## Beschreibung

Der MCP-Server **muss** ein Tool `update_resource` bereitstellen, das über `PUT /resources/{id}` eine bestehende Ressource partiell aktualisiert (title, cost, visitors).

## Quelle

- [US-017](../001%20User%20Stories/US-017-update-resource.md) | [UC-012](../002%20Use%20Cases/UC-012-update-resource.md)

## Akzeptanzkriterien

1. Tool `update_resource` ist im MCP-Server registriert.
2. Pflichtparameter: id (integer, > 0).
3. Optionale Parameter: title, cost, visitors.
4. Partielle Aktualisierung: Nur übergebene Felder werden geändert.
5. Fehlerbehandlung gemäß REQ-005.

## Abhängigkeiten

REQ-001, REQ-005, REQ-006, REQ-008
