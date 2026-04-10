---
id: "020"
title: "Tool: create_resource"
tags:
  - Requirement
  - Funktional
  - Resources
priority: mittel
status: open
---

# REQ-020: Tool create_resource

## Beschreibung

Der MCP-Server **muss** ein Tool `create_resource` bereitstellen, das über `POST /resources` eine neue Ressource mit Titel, optionalen Kosten und Besucheranzahl anlegt.

## Quelle

- [US-015](../001%20User%20Stories/US-015-create-resource.md) | [UC-010](../002%20Use%20Cases/UC-010-create-resource.md)

## Akzeptanzkriterien

1. Tool `create_resource` ist im MCP-Server registriert.
2. Pflichtparameter: title (string, nicht leer).
3. Optionale Parameter: cost (string), visitors (integer, > 0).
4. Erfolgreiche Erstellung liefert Ressource-ID.
5. Fehlerbehandlung gemäß REQ-005.

## Abhängigkeiten

REQ-001, REQ-005, REQ-006, REQ-008
