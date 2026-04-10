---
id: "018"
title: "Tool: update_booking_note"
tags:
  - Requirement
  - Funktional
  - Bookings
priority: mittel
status: open
---

# REQ-018: Tool update_booking_note

## Beschreibung

Der MCP-Server **muss** ein Tool `update_booking_note` bereitstellen, das über `PUT /bookings/{id}/note` einer Buchung eine Notiz hinzufügt.

## Quelle

- [US-013](../001%20User%20Stories/US-013-update-booking-note.md) | [UC-008](../002%20Use%20Cases/UC-008-update-booking-note.md)

## Akzeptanzkriterien

1. Tool `update_booking_note` ist im MCP-Server registriert.
2. Pflichtparameter: id (integer, > 0), note (string, nicht leer).
3. Erfolgreiche Aktualisierung liefert Bestätigung.
4. Fehlerbehandlung gemäß REQ-005.

## Abhängigkeiten

REQ-001, REQ-005, REQ-006, REQ-008
