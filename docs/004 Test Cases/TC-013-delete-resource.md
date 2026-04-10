---
id: "013"
title: Ressource löschen
tags:
  - TestCase
  - Resources
status: ausstehend
---

# TC-013: Ressource löschen

**Use Case:** [UC-013 Ressource löschen](../002%20Use%20Cases/UC-013-delete-resource.md)
**User Story:** [US-018 Tool: delete_resource](../001%20User%20Stories/US-018-delete-resource.md)
**Requirement:** [REQ-023 Tool: delete_resource](../003%20Requirements/REQ-023-delete-resource.md)

## Testszenarien

### TS-013.01: Ressource wird erfolgreich gelöscht

**Schicht:** Application | **Status:** Ausstehend
**Vorbedingung:** Repository-Mock akzeptiert Löschung.
**Aktion:** `DeleteResourceUseCase.ExecuteAsync(5)` aufrufen.
**Erwartetes Ergebnis:** Bestätigung der Löschung.

### TS-013.02: Nicht existierende Ressource liefert 404-Fehlermeldung

**Schicht:** Server | **Status:** Ausstehend
**Vorbedingung:** API liefert 404.
**Aktion:** `DeleteResourceTool.ExecuteAsync(id: 999)` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Ressource mit ID 999 nicht gefunden."
