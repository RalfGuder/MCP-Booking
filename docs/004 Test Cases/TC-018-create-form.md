---
id: "018"
title: Formular anlegen
tags:
  - TestCase
  - Forms
status: ausstehend
---

# TC-018: Formular anlegen

**Use Case:** [UC-018 Formular anlegen](../002%20Use%20Cases/UC-018-create-form.md)
**User Story:** [US-023 Tool: create_form](../001%20User%20Stories/US-023-create-form.md)
**Requirement:** [REQ-028 Tool: create_form](../003%20Requirements/REQ-028-create-form.md)

## Testszenarien

### TS-018.01: Formular wird erfolgreich erstellt

**Schicht:** Application | **Status:** Ausstehend
**Vorbedingung:** Repository-Mock akzeptiert Erstellung.
**Aktion:** `CreateFormUseCase.ExecuteAsync("Raumreservierung", structureJson)` aufrufen.
**Erwartetes Ergebnis:** Formular-ID wird zurückgegeben.

### TS-018.02: Fehlende Pflichtfelder werden abgelehnt

**Schicht:** Server | **Status:** Ausstehend
**Vorbedingung:** —
**Aktion:** `CreateFormTool.ExecuteAsync(title: "")` ohne structure_json aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Pflichtfeld structure_json fehlt."

### TS-018.03: Ungültiges JSON wird abgelehnt

**Schicht:** Server | **Status:** Ausstehend
**Vorbedingung:** —
**Aktion:** `CreateFormTool.ExecuteAsync(title: "Test", structureJson: "invalid")` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Ungültiges JSON-Format in structure_json."
