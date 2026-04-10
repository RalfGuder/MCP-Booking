---
id: "003"
title: Neue Projektmappe
tags:
  - UserStory
status: open
---

# US-003: Neue Projektmappe

**Issue:** [#3 - Neue Projektmappe](https://github.com/RalfGuder/MCP-Booking/issues/3)

## Story

**Als** Entwickler des MCP-Booking-Projekts,
**möchte ich** eine neue Visual Studio Solution mit korrekt strukturierten Projekten anlegen,
**damit** die Entwicklung des MCP-Servers auf einer sauberen, den Architekturvorgaben entsprechenden Projektstruktur aufbauen kann.

## Hintergrund

Das Repository enthält bisher nur die `.gitignore`, die Lizenz, die API-Spezifikation (`docs/booking-api-v1.yaml`) und Konfigurationsdateien. Es existiert noch kein Source-Code oder Build-System. Die Projektmappe muss die Clean-Architecture-Schichten (siehe [Issue #2](https://github.com/RalfGuder/MCP-Booking/issues/2)) als separate Projekte abbilden.

## Akzeptanzkriterien

1. **Solution-Datei:**
   - Eine `MCP-Booking.sln` im Repository-Root
   - Alle Projekte sind in der Solution referenziert
   - Solution Folders gruppieren die Projekte logisch (z.B. `src`, `tests`)

2. **Source-Projekte (unter `src/`):**
   - `McpBooking.Domain` — Class Library für Entities, Interfaces, Value Objects (keine externen Abhängigkeiten)
   - `McpBooking.Application` — Class Library für Use Cases, Services, DTOs (referenziert Domain)
   - `McpBooking.Infrastructure` — Class Library für HTTP-Client, Repository-Implementierungen (referenziert Domain und Application)
   - `McpBooking.Server` — Konsolenanwendung (Executable) für den MCP-Server (referenziert alle anderen Projekte)

3. **Test-Projekte (unter `tests/`):**
   - `McpBooking.Domain.Tests` — Unit-Tests für die Domain-Schicht
   - `McpBooking.Application.Tests` — Unit-Tests für die Application-Schicht
   - `McpBooking.Infrastructure.Tests` — Integrationstests für die Infrastructure-Schicht
   - `McpBooking.Server.Tests` — Tests für den MCP-Server

4. **SDK-Style Projektdateien:**
   - Alle `.csproj`-Dateien verwenden das SDK-Style-Format (siehe [Issue #4](https://github.com/RalfGuder/MCP-Booking/issues/4))
   - Target-Framework: aktuelles .NET LTS (z.B. `net8.0` oder `net9.0`)
   - Nullable Reference Types aktiviert
   - Implicit Usings aktiviert

5. **Build-Fähigkeit:**
   - `dotnet build` läuft fehlerfrei durch
   - `dotnet test` läuft fehlerfrei durch (auch wenn noch keine Tests vorhanden)

## Verzeichnisstruktur

```
MCP-Booking/
├── MCP-Booking.sln
├── src/
│   ├── McpBooking.Domain/
│   │   └── McpBooking.Domain.csproj
│   ├── McpBooking.Application/
│   │   └── McpBooking.Application.csproj
│   ├── McpBooking.Infrastructure/
│   │   └── McpBooking.Infrastructure.csproj
│   └── McpBooking.Server/
│       └── McpBooking.Server.csproj
├── tests/
│   ├── McpBooking.Domain.Tests/
│   │   └── McpBooking.Domain.Tests.csproj
│   ├── McpBooking.Application.Tests/
│   │   └── McpBooking.Application.Tests.csproj
│   ├── McpBooking.Infrastructure.Tests/
│   │   └── McpBooking.Infrastructure.Tests.csproj
│   └── McpBooking.Server.Tests/
│       └── McpBooking.Server.Tests.csproj
├── docs/
│   ├── booking-api-v1.yaml
│   └── user-stories/
├── .gitignore
├── LICENSE
└── CLAUDE.md
```

## Technische Hinweise

- Programmiersprache: C# (siehe [Issue #4](https://github.com/RalfGuder/MCP-Booking/issues/4))
- Architektur: Clean Architecture (siehe [Issue #2](https://github.com/RalfGuder/MCP-Booking/issues/2))
- Test-Framework: xUnit (empfohlen für .NET TDD, siehe [Issue #5](https://github.com/RalfGuder/MCP-Booking/issues/5))

## Abhängigkeiten

- Issue #2 (Architektur) — bestimmt die Schichten und damit die Projektstruktur
- Issue #4 (Programmiersprache) — bestimmt SDK-Style und Target-Framework
