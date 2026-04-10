---
id: "017"
title: Formulare auflisten
tags:
  - UseCase
  - Forms
status: open
---

# UC-017: Formulare auflisten

**User Story:** [US-022 Tool: list_forms](../001%20User%20Stories/US-022-list-forms.md) | [Issue #22](https://github.com/RalfGuder/MCP-Booking/issues/22)

## Akteure

- **Primaer:** KI-Assistent (z.B. Claude)
- **Sekundaer:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und ueber stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.

## Ausloeser

Der Nutzer fragt nach den vorhandenen Buchungsformularen (z.B. "Welche Formulare gibt es?").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `list_forms` auf.
2. Der MCP-Server validiert die optionalen Parameter.
3. Der MCP-Server sendet einen `GET /forms`-Request an die API.
4. Die API liefert eine paginierte Liste von Formularen.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent praesentiert die Formularliste dem Nutzer.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| page | integer | nein | >= 1, Standard: 1 |
| per_page | integer | nein | 1–100, Standard: 20 |

## Ergebnis

Strukturiertes Objekt mit:
- Liste der Formulare (ID, Titel, Slug, Status)
- Paginierungsinformationen

## Alternative Ablaeufe

### A1: Keine Formulare vorhanden
4a. Die API liefert eine leere Liste.
5a. Der MCP-Server liefert ein leeres Ergebnis mit Hinweis "Keine Formulare vorhanden".

## Fehlerablaeufe

### E1: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

## Nachbedingungen

- Keine Zustandsaenderung im System (lesender Zugriff).
