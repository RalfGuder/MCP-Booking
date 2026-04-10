---
id: "028"
title: Harte Zeichenfolgen Base64-codieren
tags:
  - UseCase
  - Querschnitt
status: open
---

# UC-028: Harte Zeichenfolgen Base64-codieren

**User Story:** [US-033 Harte Zeichenfolgen](../001%20User%20Stories/US-033-harte-zeichenfolgen.md) | [Issue #33](https://github.com/RalfGuder/MCP-Booking/issues/33)

## Akteure

- **Primär:** Entwickler

## Vorbedingungen

1. Das Projekt enthält nicht-lokalisierbare Strings (z.B. API-Pfade, Content-Types).

## Auslöser

Ein neuer nicht-lokalisierbarer String wird im Code benötigt.

## Hauptablauf

1. Der Entwickler identifiziert einen nicht-lokalisierbaren String (z.B. `/resources`).
2. Der String wird Base64-codiert (z.B. `L3Jlc291cmNlcw==`).
3. In `Properties/Strings.cs` des betroffenen Projekts wird eine Property angelegt.
4. Die Property decodiert den Base64-String beim Abruf.
5. Im Code wird anstelle des Klartext-Strings `Strings.ApiResourcesPath` verwendet.

## Nachbedingungen

- Der String ist nicht im Klartext im Quellcode sichtbar.
- Der decodierte Wert ist identisch mit dem ursprünglichen String.

## Test Case

- [TC-028](../004%20Test%20Cases/TC-028-harte-zeichenfolgen.md)
