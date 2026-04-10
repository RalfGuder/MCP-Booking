---
id: "001"
title: Buchungen auflisten
tags:
  - UseCase
  - Bookings
status: open
---

# UC-001: Buchungen auflisten

**User Story:** [US-006](../001%20User%20Stories/US-006-list-bookings.md) | [Issue #6](https://github.com/RalfGuder/MCP-Booking/issues/6)

## Akteure

- **Primär:** KI-Assistent (z.B. Claude)
- **Sekundär:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und über stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.
3. Die WP Booking Calendar API ist erreichbar.

## Auslöser

Der Nutzer fragt den KI-Assistenten nach einer Übersicht von Buchungen (z.B. "Zeige mir alle genehmigten Buchungen für nächste Woche").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `list_bookings` auf.
2. Der MCP-Server validiert die übergebenen Parameter.
3. Der MCP-Server sendet einen `GET /bookings`-Request an die API mit den Query-Parametern.
4. Die API liefert eine paginierte Liste von Buchungen zurück.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent präsentiert die Buchungsliste dem Nutzer.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| page | integer | nein | >= 1, Standard: 1 |
| per_page | integer | nein | 1–100, Standard: 20 |
| resource_id | integer | nein | > 0 |
| status | string | nein | Einer von: `pending`, `approved`, `trash` |
| date_from | date | nein | ISO 8601 Format |
| date_to | date | nein | ISO 8601 Format, >= date_from |
| is_new | boolean | nein | true/false |
| search | string | nein | Nicht-leerer String |
| orderby | string | nein | Einer von: `booking_id`, `sort_date`, `modification_date` |
| order | string | nein | Einer von: `ASC`, `DESC` |

## Ergebnis

Strukturiertes Objekt mit:
- Liste der Buchungen (ID, Datum, Status, Ressource, Formulardaten)
- Paginierungsinformationen (aktuelle Seite, Gesamtanzahl)

## Alternative Abläufe

### A1: Keine Buchungen gefunden
4a. Die API liefert eine leere Liste.
5a. Der MCP-Server liefert ein leeres Ergebnis mit Hinweis "Keine Buchungen gefunden".

### A2: Filterung ohne Ergebnisse
4a. Die Filterkriterien ergeben keine Treffer.
5a. Der MCP-Server liefert ein leeres Ergebnis mit Hinweis auf die aktiven Filter.

## Fehlerabläufe

### E1: Authentifizierungsfehler (401)
3a. Die API liefert 401 Unauthorized.
4a. Der MCP-Server liefert eine Fehlermeldung: "Authentifizierung fehlgeschlagen. Bitte API-Zugangsdaten prüfen."

### E2: Berechtigungsfehler (403)
3a. Die API liefert 403 Forbidden.
4a. Der MCP-Server liefert eine Fehlermeldung: "Keine Berechtigung für diese Aktion."

### E3: API nicht erreichbar
3a. Der HTTP-Request schlägt fehl (Timeout, DNS-Fehler).
4a. Der MCP-Server liefert eine Fehlermeldung: "Die Booking API ist nicht erreichbar."

### E4: Ungültiger Parameter
2a. Ein Parameter hat einen ungültigen Wert (z.B. status = "invalid").
3a. Der MCP-Server liefert eine Fehlermeldung mit Hinweis auf den fehlerhaften Parameter.

## Nachbedingungen

- Keine Zustandsänderung im System (lesender Zugriff).


## Test Case

- [TC-001](../004%20Test%20Cases/TC-001-list-bookings.md)
