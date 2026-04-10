---
id: "002"
title: stdio-Transport
tags:
  - Requirement
  - Core
  - Funktional
priority: hoch
status: open
---

# REQ-002: stdio-Transport

## Beschreibung

Der MCP-Server **muss** über den stdio-Transport kommunizieren. Eingehende JSON-RPC-Nachrichten werden über `stdin` empfangen, ausgehende Nachrichten über `stdout` gesendet.

## Quelle

- [US-001 MCP-Server](../001%20User%20Stories/US-001-mcp-server.md) — Phase 1, Punkt 1
- Alle Use Cases (UC-001 bis UC-024) — Vorbedingung „über stdio erreichbar"

## Akzeptanzkriterien

1. Der Server liest JSON-RPC-Nachrichten zeilenweise von `stdin`.
2. Der Server schreibt JSON-RPC-Antworten zeilenweise auf `stdout`.
3. Diagnose-/Log-Ausgaben werden **nicht** auf `stdout` geschrieben (nur `stderr` oder Logging-Framework).
4. Der Server kann als Kindprozess eines MCP-Hosts gestartet werden.

## Testbarkeit

- Unit-Test: JSON-RPC-Nachricht über simulierten stdin senden und Antwort auf stdout prüfen.
- Integrationstest: Server als Prozess starten und über Pipes kommunizieren.
