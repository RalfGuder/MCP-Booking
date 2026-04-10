---
id: "033"
title: Harte Zeichenfolgen (Base64)
tags:
  - UserStory
  - Querschnitt
status: open
---

# US-033: Harte Zeichenfolgen (Base64)

**Issue:** [#33 — US-033 Harte Zeichenfolgen](https://github.com/RalfGuder/MCP-Booking/issues/33)

## Story

**Als** Entwickler des MCP-Booking-Projekts,
**möchte ich** dass harte Zeichenfolgen, die nicht lokalisiert werden, Base64-codiert in Properties/Strings.cs hinterlegt werden,
**damit** sie nicht im Klartext im Quellcode stehen und erst beim Abruf decodiert werden.

## Akzeptanzkriterien

- [ ] Nicht-lokalisierbare Strings (z.B. API-Pfade, Content-Types, Header-Namen) sind in `Properties/Strings.cs` ausgelagert
- [ ] Strings sind Base64-codiert gespeichert
- [ ] Decodierung erfolgt erst beim Abruf über eine Property oder Methode
- [ ] Jedes Projekt mit harten Strings hat eine eigene `Properties/Strings.cs`
- [ ] Die Klasse ist mit englischen Dokumentationskommentaren versehen (siehe US-030)

## Technische Hinweise

- Datei: `Properties/Strings.cs` je Projekt
- Pattern: `public static string ApiPath => Decode("L3Jlc291cmNlcw==");`
- Decode-Methode: `Convert.FromBase64String()` + `Encoding.UTF8.GetString()`
- Betrifft hauptsächlich Infrastructure-Projekt (API-Pfade, Content-Types)

## Verknüpfte Artefakte

- Use Case: [UC-028-harte-zeichenfolgen](../002%20Use%20Cases/UC-028-harte-zeichenfolgen.md)
- Requirement: [REQ-038-harte-zeichenfolgen](../003%20Requirements/REQ-038-harte-zeichenfolgen.md)
- Test Case: [TC-028-harte-zeichenfolgen](../004%20Test%20Cases/TC-028-harte-zeichenfolgen.md)
