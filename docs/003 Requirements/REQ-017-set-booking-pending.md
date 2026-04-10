---
id: "017"
title: "Tool: set_booking_pending"
tags:
  - Requirement
  - Funktional
  - Bookings
priority: mittel
status: open
---

# REQ-017: Tool set_booking_pending

## Beschreibung

Der MCP-Server **muss** ein Tool `set_booking_pending` bereitstellen, das über `POST /bookings/{id}/pending` den Status einer Buchung auf `pending` zurücksetzt.

## Quelle

- [US-012](../001%20User%20Stories/US-012-set-booking-pending.md) | [UC-007](../002%20Use%20Cases/UC-007-set-booking-pending.md)

## Akzeptanzkriterien

1. Tool `set_booking_pending` ist im MCP-Server registriert.
2. Pflichtparameter: id (integer, > 0).
3. Erfolgreiche Statusänderung liefert aktualisierten Status.
4. Fehlerbehandlung gemäß REQ-005.

## Abhängigkeiten

REQ-001, REQ-005, REQ-006, REQ-008
