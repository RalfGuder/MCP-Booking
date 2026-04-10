---
id: "003"
title: API-Authentifizierung
tags:
  - Requirement
  - Core
  - Funktional
  - Sicherheit
priority: hoch
status: open
---

# REQ-003: API-Authentifizierung

## Beschreibung

Der MCP-Server **muss** sich gegenüber der WP Booking Calendar REST API authentifizieren können. Die Authentifizierung erfolgt über WordPress Application Passwords oder einen API-Key.

## Quelle

- [US-001 MCP-Server](../001%20User%20Stories/US-001-mcp-server.md) — Phase 1, Punkt 2
- Alle Use Cases (UC-001 bis UC-024) — Fehlerablauf E: Authentifizierungsfehler (401/403)

## Akzeptanzkriterien

1. Der Server sendet Authentifizierungsdaten (z.B. Basic Auth Header) mit jedem API-Request.
2. Die Zugangsdaten werden **nicht** im Quellcode gespeichert.
3. Bei fehlenden Zugangsdaten startet der Server mit einer verständlichen Fehlermeldung.
4. Authentifizierungsfehler (401) werden als Tool-Fehlermeldung weitergegeben.
5. Berechtigungsfehler (403) werden als Tool-Fehlermeldung weitergegeben.

## Testbarkeit

- Unit-Test: Auth-Header wird korrekt an HTTP-Requests angefügt.
- Unit-Test: Fehlende Zugangsdaten erzeugen aussagekräftige Fehlermeldung.
- Integrationstest: API-Aufruf mit gültigen/ungültigen Credentials.
