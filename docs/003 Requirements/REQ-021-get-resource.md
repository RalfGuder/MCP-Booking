---
id: "021"
title: "Tool: get_resource"
tags:
  - Requirement
  - Funktional
  - Resources
priority: mittel
status: open
---

# REQ-021: Tool get_resource

## Beschreibung

Der MCP-Server **muss** ein Tool `get_resource` bereitstellen, das über `GET /resources/{id}` die Details einer einzelnen Ressource abruft.

## Quelle

- [US-016](../001%20User%20Stories/US-016-get-resource.md) | [UC-011](../002%20Use%20Cases/UC-011-get-resource.md)

## Akzeptanzkriterien

1. Tool `get_resource` ist im MCP-Server registriert.
2. Pflichtparameter: id (integer, > 0).
3. Ressourcendetails werden vollständig zurückgegeben.
4. Fehlerbehandlung gemäß REQ-005.

## Abhängigkeiten

REQ-001, REQ-005, REQ-006, REQ-008
