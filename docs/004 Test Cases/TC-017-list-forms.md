---
id: "017"
title: Formulare auflisten
tags:
  - TestCase
  - Forms
status: ausstehend
---

# TC-017: Formulare auflisten

**Use Case:** [UC-017 Formulare auflisten](../002%20Use%20Cases/UC-017-list-forms.md)
**User Story:** [US-022 Tool: list_forms](../001%20User%20Stories/US-022-list-forms.md)
**Requirement:** [REQ-027 Tool: list_forms](../003%20Requirements/REQ-027-list-forms.md)

## Testszenarien

### TS-017.01: Formulare werden korrekt aufgelistet

**Schicht:** Application | **Status:** Ausstehend
**Vorbedingung:** Repository-Mock liefert 2 Formulare.
**Aktion:** `ListFormsUseCase.ExecuteAsync()` aufrufen.
**Erwartetes Ergebnis:** 2 FormDto-Objekte mit ID, Titel, Slug, Status.

### TS-017.02: Leere Formularliste wird korrekt behandelt

**Schicht:** Application | **Status:** Ausstehend
**Vorbedingung:** Repository-Mock liefert leere Liste.
**Aktion:** `ListFormsUseCase.ExecuteAsync()` aufrufen.
**Erwartetes Ergebnis:** Leere Collection.

### TS-017.03: Paginierung wird korrekt weitergegeben

**Schicht:** Infrastructure | **Status:** Ausstehend
**Vorbedingung:** HTTP-Mock akzeptiert alle Requests.
**Aktion:** `FormRepository.ListAsync(page: 2, perPage: 10)` aufrufen.
**Erwartetes Ergebnis:** HTTP-Request enthält `page=2` und `per_page=10`.
