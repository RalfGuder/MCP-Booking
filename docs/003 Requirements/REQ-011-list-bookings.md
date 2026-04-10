---
id: "011"
title: "Tool: list_bookings"
tags:
  - Requirement
  - Funktional
  - Bookings
priority: mittel
status: open
---

# REQ-011: Tool list_bookings

## Beschreibung

Der MCP-Server **muss** ein Tool `list_bookings` bereitstellen, das Buchungen über `GET /bookings` auflistet und nach Status, Ressource, Datumsbereich, Suchbegriff und Sortierung filtern kann.

## Quelle

- [US-006](../001%20User%20Stories/US-006-list-bookings.md) | [UC-001](../002%20Use%20Cases/UC-001-list-bookings.md)

## Akzeptanzkriterien

1. Tool `list_bookings` ist im MCP-Server registriert.
2. Unterstützte Filter: page, per_page, resource_id, status, date_from, date_to, is_new, search, orderby, order.
3. Paginierung gemäß REQ-007.
4. Parametervalidierung gemäß REQ-006.
5. Fehlerbehandlung gemäß REQ-005.

## Abhängigkeiten

REQ-001, REQ-005, REQ-006, REQ-007, REQ-008
