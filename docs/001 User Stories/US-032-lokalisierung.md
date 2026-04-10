---
id: "032"
title: Lokalisierung
tags:
  - UserStory
  - Querschnitt
status: open
---

# US-032: Lokalisierung

**Issue:** [#32 — US-032 Lokalisierung](https://github.com/RalfGuder/MCP-Booking/issues/32)

## Story

**Als** Nutzer eines KI-Assistenten,
**möchte ich** dass alle Nutzermeldungen und Log-Einträge lokalisiert sind,
**damit** ich Fehlermeldungen und Hinweise in meiner bevorzugten Sprache erhalte.

## Akzeptanzkriterien

- [ ] Alle Nutzermeldungen (Fehlermeldungen, Bestätigungen) sind über Resource-Dateien lokalisiert
- [ ] Resource-Dateien liegen unter `Properties/Messages.resx` in jedem betroffenen Projekt
- [ ] Unterstützte Sprachen: Deutsch (de), Englisch (en), Französisch (fr), Spanisch (es)
- [ ] Standardsprache ist Deutsch
- [ ] Keine hardcodierten Strings in Fehlermeldungen oder Nutzerausgaben
- [ ] Log-Einträge sind ebenfalls lokalisiert

## Technische Hinweise

- Resource-Dateien: `Messages.resx` (Standard/de), `Messages.en.resx`, `Messages.fr.resx`, `Messages.es.resx`
- Zugriff über generierten `Messages`-Klasse (z.B. `Messages.AuthenticationFailed`)
- Betrifft hauptsächlich Infrastructure- und Server-Projekte

## Verknüpfte Artefakte

- Use Case: [UC-027-lokalisierung](../002%20Use%20Cases/UC-027-lokalisierung.md)
- Requirement: [REQ-037-lokalisierung](../003%20Requirements/REQ-037-lokalisierung.md)
- Test Case: [TC-027-lokalisierung](../004%20Test%20Cases/TC-027-lokalisierung.md)
