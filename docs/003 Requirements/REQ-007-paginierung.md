---
id: "007"
title: Paginierung
tags:
  - Requirement
  - Core
  - Funktional
priority: mittel
status: open
---

# REQ-007: Paginierung

## Beschreibung

Alle Listen-Endpunkte **müssen** Paginierung unterstützen. Die Parameter `page` und `per_page` werden an die API weitergegeben, und die Paginierungsinformationen werden im Ergebnis zurückgeliefert.

## Quelle

- [UC-001 Buchungen auflisten](../002%20Use%20Cases/UC-001-list-bookings.md) — Parameter page, per_page
- [UC-009 Ressourcen auflisten](../002%20Use%20Cases/UC-009-list-resources.md) — Parameter page, per_page
- [UC-017 Formulare auflisten](../002%20Use%20Cases/UC-017-list-forms.md) — Parameter page, per_page

## Akzeptanzkriterien

1. `page` hat den Standardwert 1 und muss >= 1 sein.
2. `per_page` hat den Standardwert 20 und muss im Bereich 1–100 liegen.
3. Die API-Antwort enthält Paginierungsinformationen (aktuelle Seite, Gesamtanzahl).
4. Leere Ergebnislisten werden korrekt als leeres Array zurückgegeben.

## Testbarkeit

- Unit-Test: Standardwerte werden korrekt angewendet.
- Unit-Test: Ungültige Werte (page=0, per_page=101) erzeugen Validierungsfehler.
