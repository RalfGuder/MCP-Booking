---
id: "020"
title: Formular aktualisieren
tags:
  - TestCase
  - Forms
status: ausstehend
---

# TC-020: Formular aktualisieren

**Use Case:** [UC-020 Formular aktualisieren](../002%20Use%20Cases/UC-020-update-form.md)
**User Story:** [US-025 Tool: update_form](../001%20User%20Stories/US-025-update-form.md)
**Requirement:** [REQ-030 Tool: update_form](../003%20Requirements/REQ-030-update-form.md)

## Testszenarien

### TS-020.01: Formular wird erfolgreich aktualisiert

**Schicht:** Application | **Status:** Ausstehend
**Vorbedingung:** Repository-Mock akzeptiert Aktualisierung.
**Aktion:** `UpdateFormUseCase.ExecuteAsync(2, title: "Neuer Name")` aufrufen.
**Erwartetes Ergebnis:** Bestätigung der Aktualisierung.

### TS-020.02: Ungültiges JSON wird abgelehnt

**Schicht:** Server | **Status:** Ausstehend
**Vorbedingung:** —
**Aktion:** `UpdateFormTool.ExecuteAsync(id: 2, structureJson: "invalid")` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Ungültiges JSON-Format in structure_json."

### TS-020.03: Nicht existierendes Formular liefert 404-Fehlermeldung

**Schicht:** Server | **Status:** Ausstehend
**Vorbedingung:** API liefert 404.
**Aktion:** `UpdateFormTool.ExecuteAsync(id: 999)` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Formular mit ID 999 nicht gefunden."
