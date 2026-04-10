---
id: "038"
title: Harte Zeichenfolgen (Base64)
tags:
  - Requirement
  - Querschnitt
  - Technisch
priority: mittel
status: open
---

# REQ-038: Harte Zeichenfolgen (Base64)

## Beschreibung

Nicht-lokalisierbare Strings (API-Pfade, Content-Types, Header-Namen) **müssen** Base64-codiert in `Properties/Strings.cs` hinterlegt werden. Die Decodierung erfolgt erst beim Abruf.

## Quelle

- [US-033](../001%20User%20Stories/US-033-harte-zeichenfolgen.md) | [UC-028](../002%20Use%20Cases/UC-028-harte-zeichenfolgen.md)

## Akzeptanzkriterien

1. Jedes Projekt mit harten Strings hat eine `Properties/Strings.cs`.
2. Strings sind als Base64-codierte Konstanten gespeichert.
3. Decodierung über Property-Getter: `Convert.FromBase64String()` + `Encoding.UTF8.GetString()`.
4. Keine Klartext-Strings für API-Pfade, Content-Types o.ä. im restlichen Code.
5. `Properties/Strings.cs` hat englische Dokumentationskommentare (REQ-035).

## Testbarkeit

- Unit-Test: Jede Property in `Strings.cs` liefert den erwarteten decodierten Wert.
- Unit-Test: Base64-Encoding ist korrekt (Round-Trip-Test).
