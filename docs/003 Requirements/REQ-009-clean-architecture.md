---
id: "009"
title: Clean Architecture
tags:
  - Requirement
  - Architektur
  - Nicht-Funktional
priority: hoch
status: open
---

# REQ-009: Clean Architecture

## Beschreibung

Das Projekt **muss** nach Clean-Architecture-Prinzipien strukturiert sein. Die Abhängigkeiten zeigen immer nach innen (Dependency Rule), und die Geschäftslogik ist unabhängig von externen Frameworks.

## Quelle

- [US-002 Architektur](../001%20User%20Stories/US-002-architektur.md) — Alle Akzeptanzkriterien

## Akzeptanzkriterien

1. **Domain-Schicht** enthält Entities, Interfaces und Value Objects — keine externen NuGet-Abhängigkeiten.
2. **Application-Schicht** enthält Use Cases und DTOs — abhängig nur von Domain.
3. **Infrastructure-Schicht** implementiert Domain-Interfaces mit HTTP-Aufrufen.
4. **Presentation-Schicht** (MCP-Server) registriert Tools und handhabt Transport.
5. Dependency Inversion: Infrastructure implementiert Domain-Interfaces.
6. Dependency Injection verbindet die Schichten zur Laufzeit.

## Testbarkeit

- Architekturtest: Domain-Projekt hat keine externen NuGet-Referenzen.
- Architekturtest: Application-Projekt referenziert nur Domain.
- Unit-Test: Domain- und Application-Schicht sind ohne Infrastructure testbar (mit Mocks).


## Verknüpfte Artefakte

- Design-Spec: [2026-04-10-us001-mcp-server-design.md](../005%20Super%20Powers/specs/2026-04-10-us001-mcp-server-design.md)
- Implementierungsplan: [2026-04-10-us001-mcp-server-phase1.md](../005%20Super%20Powers/plans/2026-04-10-us001-mcp-server-phase1.md)