---
id: "011"
title: Ressource abrufen
tags:
  - TestCase
  - Resources
status: ausstehend
---

# TC-011: Ressource abrufen

**Use Case:** [UC-011 Ressource abrufen](../002%20Use%20Cases/UC-011-get-resource.md)
**User Story:** [US-016 Tool: get_resource](../001%20User%20Stories/US-016-get-resource.md)
**Requirement:** [REQ-021 Tool: get_resource](../003%20Requirements/REQ-021-get-resource.md)

## Testszenarien

### TS-011.01: Ressourcendetails werden vollständig zurückgegeben

**Schicht:** Application | **Status:** Ausstehend
**Vorbedingung:** Repository-Mock liefert Ressource mit ID 3.
**Aktion:** `GetResourceUseCase.ExecuteAsync(3)` aufrufen.
**Erwartetes Ergebnis:** ResourceDto mit allen Feldern.

### TS-011.02: Nicht existierende Ressource liefert 404-Fehlermeldung

**Schicht:** Server | **Status:** Ausstehend
**Vorbedingung:** API liefert 404.
**Aktion:** `GetResourceTool.ExecuteAsync(id: 999)` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Ressource mit ID 999 nicht gefunden."
