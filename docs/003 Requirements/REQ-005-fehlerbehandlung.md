---
id: "005"
title: Fehlerbehandlung
tags:
  - Requirement
  - Core
  - Funktional
priority: hoch
status: open
---

# REQ-005: Fehlerbehandlung

## Beschreibung

Der MCP-Server **muss** API-Fehler (HTTP 4xx, 5xx) und Netzwerkfehler abfangen und als verständliche Tool-Fehlermeldungen an den KI-Assistenten weitergeben.

## Quelle

- [US-001 MCP-Server](../001%20User%20Stories/US-001-mcp-server.md) — Phase 1, Punkt 4
- Alle Use Cases (UC-001 bis UC-024) — Fehlerabläufe E1–E4

## Akzeptanzkriterien

1. HTTP 401 → Fehlermeldung „Authentifizierung fehlgeschlagen."
2. HTTP 403 → Fehlermeldung „Keine Berechtigung für diese Aktion."
3. HTTP 404 → Fehlermeldung „[Entität] mit ID [id] nicht gefunden."
4. HTTP 5xx → Fehlermeldung „Serverfehler bei der Booking API."
5. Netzwerkfehler (Timeout, DNS) → Fehlermeldung „Die Booking API ist nicht erreichbar."
6. Fehlermeldungen sind in deutscher Sprache verfasst.
7. Fehler werden als MCP-Tool-Error (isError: true) zurückgegeben, nicht als Exceptions.

## Testbarkeit

- Unit-Test: Jeder HTTP-Statuscode erzeugt die erwartete Fehlermeldung.
- Unit-Test: Netzwerkfehler erzeugen die erwartete Fehlermeldung.
- Unit-Test: Fehler werden als MCP-isError-Antwort formatiert.


## Verknüpfte Artefakte

- Design-Spec: [2026-04-10-us001-mcp-server-design.md](../005%20Super%20Powers/specs/2026-04-10-us001-mcp-server-design.md)
- Implementierungsplan: [2026-04-10-us001-mcp-server-phase1.md](../005%20Super%20Powers/plans/2026-04-10-us001-mcp-server-phase1.md)