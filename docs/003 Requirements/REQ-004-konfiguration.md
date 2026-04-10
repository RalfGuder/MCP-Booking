---
id: "004"
title: Konfigurationsmanagement
tags:
  - Requirement
  - Core
  - Funktional
priority: hoch
status: open
---

# REQ-004: Konfigurationsmanagement

## Beschreibung

Der MCP-Server **muss** über Umgebungsvariablen oder eine Konfigurationsdatei konfigurierbar sein. Mindestens die API-Basis-URL und die Zugangsdaten müssen extern konfigurierbar sein.

## Quelle

- [US-001 MCP-Server](../001%20User%20Stories/US-001-mcp-server.md) — Phase 1, Punkt 3

## Akzeptanzkriterien

1. Die API-Basis-URL ist über eine Umgebungsvariable konfigurierbar (z.B. `WPBC_API_URL`).
2. Die Zugangsdaten sind über Umgebungsvariablen konfigurierbar (z.B. `WPBC_USERNAME`, `WPBC_PASSWORD`).
3. Standard-API-URL ist `https://kv-milowerland.de/wp-json/wpbc/v1`.
4. Fehlende Pflicht-Konfiguration führt zu einer verständlichen Fehlermeldung beim Start.
5. Sensible Konfigurationswerte werden nicht in Logs ausgegeben.

## Testbarkeit

- Unit-Test: Konfiguration aus Umgebungsvariablen lesen.
- Unit-Test: Standardwerte werden korrekt angewendet.
- Unit-Test: Fehlende Pflichtfelder erzeugen Fehlermeldung.
