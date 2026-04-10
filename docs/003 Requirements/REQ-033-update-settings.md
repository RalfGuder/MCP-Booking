---
id: "033"
title: "Tool: update_settings"
tags:
  - Requirement
  - Funktional
  - Settings
priority: mittel
status: open
---

# REQ-033: Tool update_settings

## Beschreibung

Der MCP-Server **muss** ein Tool `update_settings` bereitstellen, das über `PUT /settings` Einstellungen der Gruppen calendar, booking, ui und confirmation aktualisiert.

## Quelle

- [US-028](../001%20User%20Stories/US-028-update-settings.md) | [UC-023](../002%20Use%20Cases/UC-023-update-settings.md)

## Akzeptanzkriterien

1. Tool `update_settings` ist im MCP-Server registriert.
2. Optionale Parameter: calendar (object), booking (object), ui (object), confirmation (object).
3. Mindestens eine Gruppe muss angegeben werden.
4. Partielle Aktualisierung: Nur übergebene Gruppen werden geändert.
5. Fehlerbehandlung gemäß REQ-005.

## Abhängigkeiten

REQ-001, REQ-005, REQ-006, REQ-008
