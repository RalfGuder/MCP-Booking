---
id: "009"
title: Ressourcen auflisten
tags:
  - UseCase
  - Resources
status: open
---

# UC-009: Ressourcen auflisten

**User Story:** [US-014 Tool: list_resources](../001%20User%20Stories/US-014-list-resources.md) | [Issue #14](https://github.com/RalfGuder/MCP-Booking/issues/14)

## Akteure

- **Primär:** KI-Assistent (z.B. Claude)
- **Sekundär:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und über stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.

## Auslöser

Der Nutzer fragt nach den verfügbaren Ressourcen (z.B. "Welche Räume kann ich buchen?").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `list_resources` auf.
2. Der MCP-Server validiert die optionalen Parameter.
3. Der MCP-Server sendet einen `GET /resources`-Request an die API.
4. Die API liefert eine paginierte Liste von Ressourcen.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent präsentiert die Ressourcenliste dem Nutzer.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| page | integer | nein | >= 1, Standard: 1 |
| per_page | integer | nein | 1–100, Standard: 20 |

## Ergebnis

Strukturiertes Objekt mit:
- Liste der Ressourcen (ID, Titel, Kosten, Besucheranzahl)
- Paginierungsinformationen

## Alternative Abläufe

### A1: Keine Ressourcen vorhanden
4a. Die API liefert eine leere Liste.
5a. Der MCP-Server liefert ein leeres Ergebnis mit Hinweis "Keine Ressourcen vorhanden".

## Fehlerabläufe

### E1: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

### E2: API nicht erreichbar
3a. Der HTTP-Request schlägt fehl.
4a. Der MCP-Server liefert eine Fehlermeldung: "Die Booking API ist nicht erreichbar."

## Nachbedingungen

- Keine Zustandsänderung im System (lesender Zugriff).

## Verknüpfte Artefakte

- User Story: [US-014 Tool: list_resources](../001%20User%20Stories/US-014-list-resources.md)
- Requirement: [REQ-019 Tool: list_resources](../003%20Requirements/REQ-019-list-resources.md)
- Design-Spec: [2026-04-10-us001-mcp-server-design.md](../005%20Super%20Powers/specs/2026-04-10-us001-mcp-server-design.md)
- Implementierungsplan: [2026-04-10-us001-mcp-server-phase1.md](../005%20Super%20Powers/plans/2026-04-10-us001-mcp-server-phase1.md)


## Test Case

- [TC-009](../004%20Test%20Cases/TC-009-list-resources.md)
