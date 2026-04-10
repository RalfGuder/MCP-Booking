---
id: "010"
title: Test-Driven Development
tags:
  - Requirement
  - Qualität
  - Nicht-Funktional
priority: hoch
status: open
---

# REQ-010: Test-Driven Development

## Beschreibung

Die Entwicklung **muss** nach dem TDD-Ansatz (Red-Green-Refactor) erfolgen. Alle öffentlichen Methoden und Use Cases sind durch automatisierte Tests abgedeckt.

## Quelle

- [US-005 Softwaredesign](../001%20User%20Stories/US-005-softwaredesign.md) — Alle Akzeptanzkriterien

## Akzeptanzkriterien

1. Test-Framework: xUnit.
2. Mocking-Framework: Moq oder NSubstitute.
3. Jede Architekturschicht hat ein eigenes Test-Projekt.
4. Tests folgen der Namenskonvention `MethodName_Scenario_ExpectedBehavior`.
5. Tests sind unabhängig und können parallel laufen.
6. Integrationstests sind durch `[Trait]` von Unit-Tests trennbar.
7. `dotnet test` führt alle Tests fehlerfrei aus.

## Testbarkeit

- Meta: Die Existenz und das Bestehen der Tests verifiziert dieses Requirement selbst.
