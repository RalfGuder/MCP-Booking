---
id: "015"
title: "Tool: delete_booking"
tags:
  - Requirement
  - Funktional
  - Bookings
priority: mittel
status: open
---

# REQ-015: Tool delete_booking

## Beschreibung

Der MCP-Server **muss** ein Tool `delete_booking` bereitstellen, das über `DELETE /bookings/{id}` eine Buchung löscht.

## Quelle

- [US-010](../001%20User%20Stories/US-010-delete-booking.md) | [UC-005](../002%20Use%20Cases/UC-005-delete-booking.md)

## Akzeptanzkriterien

1. Tool `delete_booking` ist im MCP-Server registriert.
2. Pflichtparameter: id (integer, > 0).
3. Erfolgreiche Löschung liefert Bestätigung.
4. Fehlerbehandlung gemäß REQ-005.

## Abhängigkeiten

REQ-001, REQ-005, REQ-006, REQ-008
