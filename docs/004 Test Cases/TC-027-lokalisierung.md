---
id: "027"
title: Lokalisierung
tags:
  - TestCase
  - Querschnitt
status: ausstehend
---

# TC-027: Lokalisierung

**Use Case:** [UC-027 Nutzermeldungen lokalisieren](../002%20Use%20Cases/UC-027-lokalisierung.md)
**User Story:** [US-032 Lokalisierung](../001%20User%20Stories/US-032-lokalisierung.md)
**Requirement:** [REQ-037 Lokalisierung](../003%20Requirements/REQ-037-lokalisierung.md)

## Testszenarien

### TS-027.01: Deutsche Fehlermeldung wird korrekt geladen

**Schicht:** Application | **Status:** Ausstehend
**Vorbedingung:** `CultureInfo.CurrentUICulture = new CultureInfo("de")`.
**Aktion:** `Messages.AuthenticationFailed` abrufen.
**Erwartetes Ergebnis:** "Authentifizierung fehlgeschlagen. Bitte API-Zugangsdaten prüfen."

### TS-027.02: Englische Fehlermeldung wird korrekt geladen

**Schicht:** Application | **Status:** Ausstehend
**Vorbedingung:** `CultureInfo.CurrentUICulture = new CultureInfo("en")`.
**Aktion:** `Messages.AuthenticationFailed` abrufen.
**Erwartetes Ergebnis:** "Authentication failed. Please check API credentials."

### TS-027.03: Französische Fehlermeldung wird korrekt geladen

**Schicht:** Application | **Status:** Ausstehend
**Vorbedingung:** `CultureInfo.CurrentUICulture = new CultureInfo("fr")`.
**Aktion:** `Messages.AuthenticationFailed` abrufen.
**Erwartetes Ergebnis:** Französische Übersetzung vorhanden.

### TS-027.04: Fallback auf Deutsch bei fehlender Sprache

**Schicht:** Application | **Status:** Ausstehend
**Vorbedingung:** `CultureInfo.CurrentUICulture = new CultureInfo("ja")` (nicht unterstützt).
**Aktion:** `Messages.AuthenticationFailed` abrufen.
**Erwartetes Ergebnis:** Deutsche Meldung als Fallback.

### TS-027.05: Keine hardcodierten Strings in Fehlermeldungen

**Schicht:** Code-Analyse | **Status:** Ausstehend
**Vorbedingung:** Alle Source-Projekte kompiliert.
**Aktion:** Grep nach `"Fehler:` in `.cs`-Dateien (außer .resx und Strings.cs).
**Erwartetes Ergebnis:** 0 Treffer.
