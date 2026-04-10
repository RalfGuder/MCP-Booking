---
id: "026"
title: "Tool: get_availability_dates"
tags:
  - Requirement
  - Funktional
  - Availability
priority: mittel
status: open
---

# REQ-026: Tool get_availability_dates

## Beschreibung

Der MCP-Server **muss** ein Tool `get_availability_dates` bereitstellen, das über `GET /availability/{resource_id}/dates` die konkreten verfügbaren Daten einer Ressource für einen Zeitraum abruft.

## Quelle

- [US-021](../001%20User%20Stories/US-021-get-availability-dates.md) | [UC-016](../002%20Use%20Cases/UC-016-get-availability-dates.md)

## Akzeptanzkriterien

1. Tool `get_availability_dates` ist im MCP-Server registriert.
2. Pflichtparameter: resource_id (integer), date_from (date), date_to (date).
3. Optionaler Parameter: prop_name (string).
4. date_to >= date_from wird validiert.
5. Ergebnis enthält Liste verfügbarer Daten.
6. Fehlerbehandlung gemäß REQ-005.

## Abhängigkeiten

REQ-001, REQ-005, REQ-006, REQ-008
