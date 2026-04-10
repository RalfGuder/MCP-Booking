---
id: "012"
title: "Tool: create_booking"
tags:
  - Requirement
  - Funktional
  - Bookings
priority: mittel
status: open
---

# REQ-012: Tool create_booking

## Beschreibung

Der MCP-Server **muss** ein Tool `create_booking` bereitstellen, das über `POST /bookings` eine neue Buchung anlegt. Pflichtfelder sind booking_type, form_data und dates.

## Quelle

- [US-007](../001%20User%20Stories/US-007-create-booking.md) | [UC-002](../002%20Use%20Cases/UC-002-create-booking.md)

## Akzeptanzkriterien

1. Tool `create_booking` ist im MCP-Server registriert.
2. Pflichtfelder: booking_type (integer), form_data (object), dates (array).
3. Erfolgreiche Erstellung liefert Buchungs-ID und Status zurück.
4. Parametervalidierung gemäß REQ-006.
5. Fehlerbehandlung gemäß REQ-005.

## Abhängigkeiten

REQ-001, REQ-005, REQ-006, REQ-008
