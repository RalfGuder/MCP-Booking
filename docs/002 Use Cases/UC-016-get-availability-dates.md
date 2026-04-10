---
id: "016"
title: Verfügbare Daten abrufen
tags:
  - UseCase
  - Availability
status: open
---

# UC-016: Verfügbare Daten abrufen

**User Story:** [US-021 Tool: get_availability_dates](../001%20User%20Stories/US-021-get-availability-dates.md) | [Issue #21](https://github.com/RalfGuder/MCP-Booking/issues/21)

## Akteure

- **Primär:** KI-Assistent (z.B. Claude)
- **Sekundär:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und über stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.
3. Die Ressource existiert im System.

## Auslöser

Der Nutzer fragt nach konkreten buchbaren Terminen (z.B. "Wann ist der Vereinsraum im Juli frei?").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `get_availability_dates` mit Ressource-ID und Datumsbereich auf.
2. Der MCP-Server validiert die Pflichtfelder `resource_id`, `date_from`, `date_to`.
3. Der MCP-Server sendet einen `GET /availability/{resource_id}/dates`-Request mit den Query-Parametern an die API.
4. Die API liefert eine Liste der verfügbaren Daten.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent präsentiert die verfügbaren Termine dem Nutzer.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| resource_id | integer | ja | > 0 |
| date_from | date | ja | ISO 8601 Format |
| date_to | date | ja | ISO 8601 Format, >= date_from |
| prop_name | string | nein | Eigenschaftsfilter |

## Ergebnis

Strukturiertes Objekt mit:
- Liste der verfügbaren Daten im angegebenen Zeitraum

## Alternative Abläufe

### A1: Keine verfügbaren Daten
4a. Die API liefert eine leere Liste.
5a. Der MCP-Server liefert ein leeres Ergebnis mit Hinweis "Keine verfügbaren Termine im angegebenen Zeitraum".

## Fehlerabläufe

### E1: Ressource nicht gefunden (404)
3a. Die API liefert 404 Not Found.
4a. Der MCP-Server liefert eine Fehlermeldung: "Ressource mit ID [id] nicht gefunden."

### E2: Ungültiger Datumsbereich
2a. `date_to` liegt vor `date_from`.
3a. Der MCP-Server liefert eine Fehlermeldung: "Enddatum muss nach dem Startdatum liegen."

### E3: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

## Nachbedingungen

- Keine Zustandsänderung im System (lesender Zugriff).
