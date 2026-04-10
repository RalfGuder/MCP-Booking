---
id: "013"
title: "Tool: get_booking"
tags:
  - Requirement
  - Funktional
  - Bookings
priority: mittel
status: open
---

# REQ-013: Tool get_booking

## Beschreibung

Der MCP-Server **muss** ein Tool `get_booking` bereitstellen, das über `GET /bookings/{id}` die vollständigen Details einer einzelnen Buchung abruft.

## Quelle

- [US-008](../001%20User%20Stories/US-008-get-booking.md) | [UC-003](../002%20Use%20Cases/UC-003-get-booking.md)

## Akzeptanzkriterien

1. Tool `get_booking` ist im MCP-Server registriert.
2. Pflichtparameter: id (integer, > 0).
3. Buchungsdetails werden vollständig zurückgegeben (ID, Status, Ressource, Formulardaten, Daten, Notizen).
4. 404 bei nicht existierender Buchung gemäß REQ-005.

## Abhängigkeiten

REQ-001, REQ-005, REQ-006, REQ-008
