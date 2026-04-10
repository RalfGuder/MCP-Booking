---
id: "010"
title: Ressource anlegen
tags:
  - TestCase
  - Resources
status: ausstehend
---

# TC-010: Ressource anlegen

**Use Case:** [UC-010 Ressource anlegen](../002%20Use%20Cases/UC-010-create-resource.md)
**User Story:** [US-015 Tool: create_resource](../001%20User%20Stories/US-015-create-resource.md)
**Requirement:** [REQ-020 Tool: create_resource](../003%20Requirements/REQ-020-create-resource.md)

## Testszenarien

### TS-010.01: Ressource wird erfolgreich erstellt

**Schicht:** Application | **Status:** Ausstehend
**Vorbedingung:** Repository-Mock akzeptiert Erstellung.
**Aktion:** `CreateResourceUseCase.ExecuteAsync("Vereinsraum", "50", 30)` aufrufen.
**Erwartetes Ergebnis:** Ressource-ID wird zurückgegeben.

### TS-010.02: Fehlender Titel wird abgelehnt

**Schicht:** Server | **Status:** Ausstehend
**Vorbedingung:** —
**Aktion:** `CreateResourceTool.ExecuteAsync(title: "")` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Der Ressourcentitel darf nicht leer sein."

### TS-010.03: Authentifizierungsfehler wird verständlich gemeldet

**Schicht:** Server | **Status:** Ausstehend
**Vorbedingung:** API liefert 401.
**Aktion:** `CreateResourceTool.ExecuteAsync(title: "Test")` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Authentifizierung fehlgeschlagen."
