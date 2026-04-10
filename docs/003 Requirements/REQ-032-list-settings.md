---
id: "032"
title: "Tool: list_settings"
tags:
  - Requirement
  - Funktional
  - Settings
priority: mittel
status: open
---

# REQ-032: Tool list_settings

## Beschreibung

Der MCP-Server **muss** ein Tool `list_settings` bereitstellen, das über `GET /settings` alle Einstellungsgruppen (calendar, booking, ui, confirmation) abruft.

## Quelle

- [US-027](../001%20User%20Stories/US-027-get-settings.md) | [UC-022](../002%20Use%20Cases/UC-022-list-settings.md)

## Akzeptanzkriterien

1. Tool `list_settings` ist im MCP-Server registriert.
2. Keine Parameter erforderlich.
3. Ergebnis enthält alle Einstellungsgruppen.
4. Fehlerbehandlung gemäß REQ-005.

## Abhängigkeiten

REQ-001, REQ-005, REQ-008
