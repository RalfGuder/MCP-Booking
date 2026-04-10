---
id: "035"
title: Dokumentationskommentare
tags:
  - Requirement
  - Querschnitt
  - Nicht-Funktional
priority: mittel
status: open
---

# REQ-035: Dokumentationskommentare

## Beschreibung

Alle öffentlichen Member (Klassen, Methoden, Properties) **müssen** detaillierte XML-Dokumentationskommentare in englischer Sprache enthalten. Dateiköpfe **müssen** Copyright- und Lizenzinformation enthalten.

## Quelle

- [US-030](../001%20User%20Stories/US-030-dokumentationskommentare.md) | [UC-025](../002%20Use%20Cases/UC-025-dokumentationskommentare.md)

## Akzeptanzkriterien

1. Alle öffentlichen Klassen haben `<summary>`-Kommentare.
2. Alle öffentlichen Methoden haben `<summary>`, `<param>`, `<returns>`-Kommentare.
3. Alle öffentlichen Properties haben `<summary>`-Kommentare.
4. Sprache: Englisch.
5. `dotnet build` mit `<GenerateDocumentationFile>true</GenerateDocumentationFile>` erzeugt keine CS1591-Warnungen.

## Testbarkeit

- Build-Test: `dotnet build` mit DocumentationFile-Generierung und TreatWarningsAsErrors.
- Code-Review: Stichprobenartige Prüfung der Kommentarqualität.
