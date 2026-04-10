---
id: "034"
title: "Tool: get_settings_group"
tags:
  - Requirement
  - Funktional
  - Settings
priority: mittel
status: open
---

# REQ-034: Tool get_settings_group

## Beschreibung

Der MCP-Server **muss** ein Tool `get_settings_group` bereitstellen, das über `GET /settings/{group}` die Einstellungen einer bestimmten Gruppe abruft.

## Quelle

- [US-029](../001%20User%20Stories/US-029-get-settings-group.md) | [UC-024](../002%20Use%20Cases/UC-024-get-settings-group.md)

## Akzeptanzkriterien

1. Tool `get_settings_group` ist im MCP-Server registriert.
2. Pflichtparameter: group (string, Pattern: `^[a-z_]+$`).
3. Ergebnis enthält Einstellungen der angegebenen Gruppe.
4. 404 bei nicht existierender Gruppe gemäß REQ-005.
5. Ungültiger Gruppenname wird mit Validierungsfehler abgelehnt.

## Abhängigkeiten

REQ-001, REQ-005, REQ-006, REQ-008
