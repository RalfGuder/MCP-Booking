---
id: "004"
title: Programmiersprache C# mit SDK-Style
tags:
  - UserStory
status: open
---

# US-004: Programmiersprache C# mit SDK-Style

**Issue:** [#4 — Programmiersprache](https://github.com/RalfGuder/MCP-Booking/issues/4)

## Story

**Als** Entwickler des MCP-Booking-Projekts,
**möchte ich** C# als Programmiersprache mit SDK-Style-Projektdateien verwenden,
**damit** das Projekt moderne .NET-Tooling-Konventionen nutzt, schlank konfiguriert ist und mit `dotnet`-CLI sowie Visual Studio gleichermassen bearbeitet werden kann.

## Hintergrund

Das Repository ist bereits mit einer Visual-Studio-/.NET-`.gitignore` aufgesetzt. Die Entscheidung für C# mit SDK-Style-Projektdateien (im Gegensatz zum alten "verbose" `.csproj`-Format) stellt sicher, dass:

- Projektdateien minimal und lesbar sind
- NuGet-Paketverweise direkt in der `.csproj` stehen (`PackageReference`)
- Plattformübergreifendes Bauen mit `dotnet build` möglich ist
- Moderne C#-Features (Nullable Reference Types, Implicit Usings, File-Scoped Namespaces) genutzt werden

## Akzeptanzkriterien

1. **Sprache & Runtime:**
   - Programmiersprache: C# (aktuelle stabile Sprachversion)
   - Target-Framework: aktuelles .NET LTS (z.B. `net8.0` oder `net9.0`)
   - Alle Projekte verwenden dasselbe Target-Framework

2. **SDK-Style-Projektdateien:**
   - Alle `.csproj`-Dateien nutzen das SDK-Style-Format (`<Project Sdk="Microsoft.NET.Sdk">`)
   - Keine legacy-Elemente (`<Import>`, `<ItemGroup>` mit expliziten Compile-Einträgen)
   - Class Libraries: `Sdk="Microsoft.NET.Sdk"`
   - Konsolenanwendung (Server): `Sdk="Microsoft.NET.Sdk"` mit `<OutputType>Exe</OutputType>`
   - Test-Projekte: `Sdk="Microsoft.NET.Sdk"`

3. **Moderne C#-Features:**
   - `<Nullable>enable</Nullable>` in allen Projekten
   - `<ImplicitUsings>enable</ImplicitUsings>` in allen Projekten
   - File-Scoped Namespaces bevorzugt (`namespace McpBooking.Domain;`)

4. **Zentrale Konfiguration (optional, empfohlen):**
   - `Directory.Build.props` im Root für gemeinsame Properties (TargetFramework, Nullable, ImplicitUsings)
   - Vermeidet Duplizierung in einzelnen `.csproj`-Dateien

5. **NuGet-Pakete:**
   - Alle Paketverweise als `<PackageReference>` in den `.csproj`-Dateien
   - Keine `packages.config`-Dateien

6. **Build & Tooling:**
   - `dotnet build` funktioniert fehlerfrei
   - `dotnet test` funktioniert fehlerfrei
   - `dotnet run --project src/McpBooking.Server` startet den Server

## Technische Hinweise

- Architektur: Clean Architecture (siehe [Issue #2](https://github.com/RalfGuder/MCP-Booking/issues/2))
- Projektmappe: Visual Studio Solution (siehe [Issue #3](https://github.com/RalfGuder/MCP-Booking/issues/3))
- Test-Framework: xUnit mit `Microsoft.NET.Test.Sdk` (siehe [Issue #5](https://github.com/RalfGuder/MCP-Booking/issues/5))

## Abhängigkeiten

- Issue #3 (Neue Projektmappe) — Die Projektmappe nutzt die hier definierten Konventionen
