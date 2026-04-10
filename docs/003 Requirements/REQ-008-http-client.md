---
id: "008"
title: HTTP-Client für WordPress-API
tags:
  - Requirement
  - Core
  - Technisch
priority: hoch
status: open
---

# REQ-008: HTTP-Client für WordPress-API

## Beschreibung

Der MCP-Server **muss** einen HTTP-Client bereitstellen, der Anfragen an die WP Booking Calendar REST API sendet und Antworten verarbeitet. Der Client ist über ein Interface abstrahiert und austauschbar.

## Quelle

- [US-002 Architektur](../001%20User%20Stories/US-002-architektur.md) — Infrastructure-Schicht
- Alle Use Cases (UC-001 bis UC-024) — Hauptablauf Schritt 3 „sendet Request an die API"

## Akzeptanzkriterien

1. Der HTTP-Client sendet GET, POST, PUT und DELETE Requests.
2. JSON-Serialisierung und -Deserialisierung der Request-/Response-Bodies.
3. Authentifizierungsheader werden automatisch angefügt.
4. Timeout-Handling für langsame oder nicht erreichbare API.
5. Der Client ist über ein Interface (`IBookingApiClient` o.ä.) abstrahiert.
6. In Tests kann der HTTP-Client durch einen Mock ersetzt werden.

## Testbarkeit

- Unit-Test: Request-Aufbau (URL, Header, Body) prüfen.
- Unit-Test: Response-Deserialisierung prüfen.
- Integrationstest: Realer API-Aufruf gegen die WordPress-Instanz.


## Verknüpfte Artefakte

- Design-Spec: [2026-04-10-us001-mcp-server-design.md](../005%20Super%20Powers/specs/2026-04-10-us001-mcp-server-design.md)
- Implementierungsplan: [2026-04-10-us001-mcp-server-phase1.md](../005%20Super%20Powers/plans/2026-04-10-us001-mcp-server-phase1.md)