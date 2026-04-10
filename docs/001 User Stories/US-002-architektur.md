---
id: "002"
title: Clean Architecture
tags:
  - UserStory
status: open
---

# US-002: Clean Architecture

**Issue:** [#2 — Architektur](https://github.com/RalfGuder/MCP-Booking/issues/2)

## Story

**Als** Entwickler des MCP-Booking-Projekts,
**moechte ich** eine Clean-Architecture-Struktur mit klar getrennten Schichten,
**damit** die Geschaeftslogik unabhaengig von externen Frameworks, der API und der Infrastruktur bleibt und das Projekt langfristig wartbar und testbar ist.

## Hintergrund

Das MCP-Booking-Projekt soll einen MCP-Server bereitstellen, der die WP Booking Calendar REST API kapselt (siehe [Issue #1](https://github.com/RalfGuder/MCP-Booking/issues/1)). Clean Architecture stellt sicher, dass:

- Die Domaenenlogik (Buchungen, Ressourcen, Formulare) keine Abhaengigkeiten zu externen Bibliotheken hat.
- Der HTTP-Client fuer die WordPress-API austauschbar ist (z.B. fuer Tests mit Mocks).
- Der MCP-Transport (stdio, SSE, etc.) unabhaengig von der Geschaeftslogik ist.
- Aenderungen an der externen API nur die aeussere Schicht betreffen.

## Akzeptanzkriterien

1. **Domain-Schicht (Core):**
   - Enthaelt Entities: `Booking`, `Resource`, `Availability`, `Form`, `Settings`
   - Enthaelt Interfaces (Ports): `IBookingRepository`, `IResourceRepository`, `IAvailabilityRepository`, `IFormRepository`, `ISettingsRepository`
   - Hat keine Abhaengigkeiten zu externen NuGet-Paketen oder Framework-Bibliotheken
   - Definiert Value Objects und Enums (z.B. `BookingStatus`: Pending, Approved, Trash)

2. **Application-Schicht (Use Cases):**
   - Enthaelt Use-Case-Klassen/Services, die die Domain-Interfaces nutzen
   - Orchestriert die Geschaeftslogik (z.B. Buchung erstellen, genehmigen, filtern)
   - Abhaengig nur von der Domain-Schicht (nach innen gerichtet)
   - Definiert DTOs fuer die Kommunikation mit der aeusseren Schicht

3. **Infrastructure-Schicht:**
   - Implementiert die Repository-Interfaces mit HTTP-Aufrufen an die WP Booking Calendar API
   - Enthaelt den HTTP-Client und die Serialisierung/Deserialisierung
   - Konfigurationsmanagement (API-URL, Credentials)

4. **Presentation-Schicht (MCP-Server):**
   - Registriert MCP-Tools und mappt sie auf Use Cases
   - Handhabt den MCP-Transport (stdio)
   - Kein Geschaeftslogik in dieser Schicht

5. **Dependency Rule:**
   - Abhaengigkeiten zeigen immer nach innen (Presentation -> Application -> Domain)
   - Infrastructure implementiert Domain-Interfaces (Dependency Inversion)
   - Dependency Injection verbindet die Schichten zur Laufzeit

## Schichtendiagramm

```
+----------------------------------------------+
|          Presentation (MCP-Server)           |
|   MCP-Tools, Transport, Tool-Registration    |
+----------------------------------------------+
|          Infrastructure (API-Client)         |
|   HTTP-Client, Repositories, Konfiguration   |
+----------------------------------------------+
|          Application (Use Cases)             |
|   Services, DTOs, Orchestrierung             |
+----------------------------------------------+
|          Domain (Core)                       |
|   Entities, Interfaces, Value Objects        |
+----------------------------------------------+
```

## Technische Hinweise

- Programmiersprache: C#, SDK-Style (siehe [Issue #4](https://github.com/RalfGuder/MCP-Booking/issues/4))
- Projektmappe: Visual Studio Solution (siehe [Issue #3](https://github.com/RalfGuder/MCP-Booking/issues/3))
- TDD: Tests gegen Domain- und Application-Schicht mit gemockten Repositories (siehe [Issue #5](https://github.com/RalfGuder/MCP-Booking/issues/5))

## Abhaengigkeiten

- Issue #3 (Neue Projektmappe) — Die Projektstruktur muss die Schichten widerspiegeln
- Issue #4 (Programmiersprache) — C# / .NET bestimmt die Projekttypen
