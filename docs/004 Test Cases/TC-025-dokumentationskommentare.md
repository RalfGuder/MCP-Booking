---
id: "025"
title: Dokumentationskommentare
tags:
  - TestCase
  - Querschnitt
status: ausstehend
---

# TC-025: Dokumentationskommentare

**Use Case:** [UC-025 Dokumentationskommentare pflegen](../002%20Use%20Cases/UC-025-dokumentationskommentare.md)
**User Story:** [US-030 Dokumentationskommentare](../001%20User%20Stories/US-030-dokumentationskommentare.md)
**Requirement:** [REQ-035 Dokumentationskommentare](../003%20Requirements/REQ-035-dokumentationskommentare.md)

## Testszenarien

### TS-025.01: Build ohne CS1591-Warnungen

**Schicht:** Build | **Status:** Ausstehend
**Vorbedingung:** `<GenerateDocumentationFile>true</GenerateDocumentationFile>` in Directory.Build.props.
**Aktion:** `dotnet build -warnaserror` ausführen.
**Erwartetes Ergebnis:** 0 Warnungen, 0 Fehler.

### TS-025.02: Alle öffentlichen Klassen haben Summary-Kommentare

**Schicht:** Code-Analyse | **Status:** Ausstehend
**Vorbedingung:** Alle Source-Projekte kompiliert.
**Aktion:** Grep nach `public class` ohne vorangehenden `///`-Kommentar.
**Erwartetes Ergebnis:** 0 Treffer.

### TS-025.03: Methoden haben param- und returns-Kommentare

**Schicht:** Code-Analyse | **Status:** Ausstehend
**Vorbedingung:** Alle Source-Projekte kompiliert.
**Aktion:** Prüfe öffentliche Methoden auf `<param>` und `<returns>` Tags.
**Erwartetes Ergebnis:** Alle öffentlichen Methoden mit Parametern haben `<param>` Tags.
