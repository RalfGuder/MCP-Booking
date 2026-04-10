---
id: "023"
title: "Tool: delete_resource"
tags:
  - Requirement
  - Funktional
  - Resources
priority: mittel
status: open
---

# REQ-023: Tool delete_resource

## Beschreibung

Der MCP-Server **muss** ein Tool `delete_resource` bereitstellen, das über `DELETE /resources/{id}` eine Ressource löscht.

## Quelle

- [US-018](../001%20User%20Stories/US-018-delete-resource.md) | [UC-013](../002%20Use%20Cases/UC-013-delete-resource.md)

## Akzeptanzkriterien

1. Tool `delete_resource` ist im MCP-Server registriert.
2. Pflichtparameter: id (integer, > 0).
3. Erfolgreiche Löschung liefert Bestätigung.
4. Fehlerbehandlung gemäß REQ-005.

## Abhängigkeiten

REQ-001, REQ-005, REQ-006, REQ-008
