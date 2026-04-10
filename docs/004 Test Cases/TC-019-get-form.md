---
id: "019"
title: Formular abrufen
tags:
  - TestCase
  - Forms
status: ausstehend
---

# TC-019: Formular abrufen

**Use Case:** [UC-019 Formular abrufen](../002%20Use%20Cases/UC-019-get-form.md)
**User Story:** [US-024 Tool: get_form](../001%20User%20Stories/US-024-get-form.md)
**Requirement:** [REQ-029 Tool: get_form](../003%20Requirements/REQ-029-get-form.md)

## Testszenarien

### TS-019.01: Formulardetails werden vollständig zurückgegeben

**Schicht:** Application | **Status:** Ausstehend
**Vorbedingung:** Repository-Mock liefert Formular mit ID 2.
**Aktion:** `GetFormUseCase.ExecuteAsync(2)` aufrufen.
**Erwartetes Ergebnis:** FormDto mit ID, Titel, Slug, Status, structure_json, settings_json.

### TS-019.02: Nicht existierendes Formular liefert 404-Fehlermeldung

**Schicht:** Server | **Status:** Ausstehend
**Vorbedingung:** API liefert 404.
**Aktion:** `GetFormTool.ExecuteAsync(id: 999)` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Formular mit ID 999 nicht gefunden."
