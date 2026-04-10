---
id: "014"
title: "Tool: update_booking"
tags:
  - Requirement
  - Funktional
  - Bookings
priority: mittel
status: open
---

# REQ-014: Tool update_booking

## Beschreibung

Der MCP-Server **muss** ein Tool `update_booking` bereitstellen, das über `PUT /bookings/{id}` eine bestehende Buchung partiell aktualisiert (form_data, booking_type, status).

## Quelle

- [US-009](../001%20User%20Stories/US-009-update-booking.md) | [UC-004](../002%20Use%20Cases/UC-004-update-booking.md)

## Akzeptanzkriterien

1. Tool `update_booking` ist im MCP-Server registriert.
2. Pflichtparameter: id (integer, > 0).
3. Optionale Parameter: form_data, booking_type, status.
4. Partielle Aktualisierung: Nur übergebene Felder werden geändert.
5. Mindestens ein optionales Feld muss angegeben werden.
6. Fehlerbehandlung gemäß REQ-005.

## Abhängigkeiten

REQ-001, REQ-005, REQ-006, REQ-008
