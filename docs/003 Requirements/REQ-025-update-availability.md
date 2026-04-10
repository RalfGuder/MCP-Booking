---
id: "025"
title: "Tool: update_availability"
tags:
  - Requirement
  - Funktional
  - Availability
priority: mittel
status: open
---

# REQ-025: Tool update_availability

## Beschreibung

Der MCP-Server **muss** ein Tool `update_availability` bereitstellen, das über `PUT /availability/{resource_id}` die Verfügbarkeit einer Ressource aktualisiert.

## Quelle

- [US-020](../001%20User%20Stories/US-020-update-availability.md) | [UC-015](../002%20Use%20Cases/UC-015-update-availability.md)

## Akzeptanzkriterien

1. Tool `update_availability` ist im MCP-Server registriert.
2. Pflichtparameter: resource_id (integer, > 0).
3. Erfolgreiche Aktualisierung liefert Bestätigung.
4. Fehlerbehandlung gemäß REQ-005.

## Abhängigkeiten

REQ-001, REQ-005, REQ-006, REQ-008
