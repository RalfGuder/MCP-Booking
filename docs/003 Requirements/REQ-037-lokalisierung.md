---
id: "037"
title: Lokalisierung
tags:
  - Requirement
  - Querschnitt
  - Funktional
priority: mittel
status: open
---

# REQ-037: Lokalisierung

## Beschreibung

Alle Nutzermeldungen und Log-Einträge **müssen** über Resource-Dateien (`Properties/Messages.resx`) lokalisiert werden. Unterstützte Sprachen: Deutsch, Englisch, Französisch, Spanisch.

## Quelle

- [US-032](../001%20User%20Stories/US-032-lokalisierung.md) | [UC-027](../002%20Use%20Cases/UC-027-lokalisierung.md)

## Akzeptanzkriterien

1. `Properties/Messages.resx` (Standard/Deutsch) existiert in jedem betroffenen Projekt.
2. `Properties/Messages.en.resx`, `Messages.fr.resx`, `Messages.es.resx` existieren.
3. Alle Fehlermeldungen aus REQ-005 sind über Resource-Keys abrufbar.
4. Keine hardcodierten Strings in Fehlermeldungen oder Nutzerausgaben.
5. Standardsprache ist Deutsch (Fallback).

## Testbarkeit

- Unit-Test: Resource-Key liefert korrekte Meldung in jeder Sprache.
- Unit-Test: Fallback auf Deutsch bei fehlender Übersetzung.
