---
id: "028"
title: Harte Zeichenfolgen (Base64)
tags:
  - TestCase
  - Querschnitt
status: ausstehend
---

# TC-028: Harte Zeichenfolgen (Base64)

**Use Case:** [UC-028 Harte Zeichenfolgen Base64-codieren](../002%20Use%20Cases/UC-028-harte-zeichenfolgen.md)
**User Story:** [US-033 Harte Zeichenfolgen](../001%20User%20Stories/US-033-harte-zeichenfolgen.md)
**Requirement:** [REQ-038 Harte Zeichenfolgen](../003%20Requirements/REQ-038-harte-zeichenfolgen.md)

## Testszenarien

### TS-028.01: Base64-codierter String wird korrekt decodiert

**Schicht:** Unit-Test | **Status:** Ausstehend
**Vorbedingung:** `Properties/Strings.cs` mit Base64-codierten Strings existiert.
**Aktion:** `Strings.ApiResourcesPath` abrufen.
**Erwartetes Ergebnis:** `/resources` (decodierter Wert).

### TS-028.02: Round-Trip-Test Base64-Encoding

**Schicht:** Unit-Test | **Status:** Ausstehend
**Vorbedingung:** —
**Aktion:** String codieren und decodieren.
**Erwartetes Ergebnis:** Original-String und decodierter String sind identisch.

### TS-028.03: Keine Klartext-API-Pfade im Code

**Schicht:** Code-Analyse | **Status:** Ausstehend
**Vorbedingung:** Alle Source-Projekte kompiliert.
**Aktion:** Grep nach `/resources`, `/bookings`, `/forms`, `/settings`, `/availability` in `.cs`-Dateien (außer Strings.cs und Tests).
**Erwartetes Ergebnis:** 0 Treffer.
