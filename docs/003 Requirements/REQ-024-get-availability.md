---
id: "024"
title: "Tool: get_availability"
tags:
  - Requirement
  - Funktional
  - Availability
priority: mittel
status: open
---

# REQ-024: Tool get_availability

## Beschreibung

Der MCP-Server **muss** ein Tool `get_availability` bereitstellen, das über `GET /availability/{resource_id}` die Verfügbarkeit einer Ressource für einen Zeitraum abfragt.

## Quelle

- [US-019](../001%20User%20Stories/US-019-get-availability.md) | [UC-014](../002%20Use%20Cases/UC-014-get-availability.md)

## Akzeptanzkriterien

1. Tool `get_availability` ist im MCP-Server registriert.
2. Pflichtparameter: resource_id (integer), date_from (date), date_to (date).
3. Optionaler Parameter: prop_name (string).
4. date_to >= date_from wird validiert.
5. Ergebnis enthält Verfügbarkeitsdaten pro Tag.
6. Fehlerbehandlung gemäß REQ-005.

## Abhängigkeiten

REQ-001, REQ-005, REQ-006, REQ-008
