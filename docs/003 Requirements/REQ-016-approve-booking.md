---
id: "016"
title: "Tool: approve_booking"
tags:
  - Requirement
  - Funktional
  - Bookings
priority: mittel
status: open
---

# REQ-016: Tool approve_booking

## Beschreibung

Der MCP-Server **muss** ein Tool `approve_booking` bereitstellen, das über `POST /bookings/{id}/approve` den Status einer Buchung auf `approved` setzt.

## Quelle

- [US-011](../001%20User%20Stories/US-011-approve-booking.md) | [UC-006](../002%20Use%20Cases/UC-006-approve-booking.md)

## Akzeptanzkriterien

1. Tool `approve_booking` ist im MCP-Server registriert.
2. Pflichtparameter: id (integer, > 0).
3. Erfolgreiche Genehmigung liefert aktualisierten Status.
4. Fehlerbehandlung gemäß REQ-005.

## Abhängigkeiten

REQ-001, REQ-005, REQ-006, REQ-008
