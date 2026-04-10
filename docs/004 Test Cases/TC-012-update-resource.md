---
id: "012"
title: Ressource aktualisieren
tags:
  - TestCase
  - Resources
status: ausstehend
---

# TC-012: Ressource aktualisieren

**Use Case:** [UC-012 Ressource aktualisieren](../002%20Use%20Cases/UC-012-update-resource.md)
**User Story:** [US-017 Tool: update_resource](../001%20User%20Stories/US-017-update-resource.md)
**Requirement:** [REQ-022 Tool: update_resource](../003%20Requirements/REQ-022-update-resource.md)

## Testszenarien

### TS-012.01: Ressource wird erfolgreich aktualisiert

**Schicht:** Application | **Status:** Ausstehend
**Vorbedingung:** Repository-Mock akzeptiert Aktualisierung.
**Aktion:** `UpdateResourceUseCase.ExecuteAsync(3, title: "Neuer Name")` aufrufen.
**Erwartetes Ergebnis:** Bestätigung der Aktualisierung.

### TS-012.02: Nicht existierende Ressource liefert 404-Fehlermeldung

**Schicht:** Server | **Status:** Ausstehend
**Vorbedingung:** API liefert 404.
**Aktion:** `UpdateResourceTool.ExecuteAsync(id: 999)` aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Ressource mit ID 999 nicht gefunden."

### TS-012.03: Keine Felder zum Aktualisieren liefert Fehlermeldung

**Schicht:** Server | **Status:** Ausstehend
**Vorbedingung:** —
**Aktion:** `UpdateResourceTool.ExecuteAsync(id: 3)` ohne weitere Felder aufrufen.
**Erwartetes Ergebnis:** Fehlermeldung "Mindestens ein Feld muss aktualisiert werden."
