---
id: "021"
title: Formular löschen
tags:
  - TestCase
  - Forms
status: ausstehend
---

# TC-021: Formular löschen

**Use Case:** [UC-021 Formular löschen](../002%20Use%20Cases/UC-021-delete-form.md)
**User Story:** [US-026 Tool: delete_form](../001%20User%20Stories/US-026-delete-form.md)
**Requirement:** [REQ-031 Tool: delete_form](../003%20Requirements/REQ-031-delete-form.md)

## Testszenarien

### TS-021.01: Formular wird erfolgreich gelöscht

**Schicht:** Application | **Status:** Ausstehend
**Vorbedingung:** Repository-Mock akzeptiert Löschung.
**Aktion:** `DeleteFormUseCase.ExecuteAsync(4)` aufrufen.
**Erwartetes Ergebnis:** Bestätigung der Löschung.

### TS-021.02: Nicht existierendes Formular liefert 404-Fehlermeldung

**Schicht:** Server | **Status:** Ausstehend
**Vorbedingung:** API liefert 404.
**Aktion:** `DeleteFormTool.ExecuteAsync(id: 999)` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Formular mit ID 999 nicht gefunden."
