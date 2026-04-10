---
id: "014"
title: Verfügbarkeit abrufen
tags:
  - UseCase
  - Availability
status: open
---

# UC-014: Verfügbarkeit abrufen

**User Story:** [US-019 Tool: get_availability](../001%20User%20Stories/US-019-get-availability.md) | [Issue #19](https://github.com/RalfGuder/MCP-Booking/issues/19)

## Akteure

- **Primär:** KI-Assistent (z.B. Claude)
- **Sekundär:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und über stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.
3. Die Ressource existiert im System.

## Auslöser

Der Nutzer fragt nach der Verfügbarkeit einer Ressource (z.B. "Ist der Gemeindesaal im Juni verfügbar?").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `get_availability` mit Ressource-ID und Datumsbereich auf.
2. Der MCP-Server validiert die Pflichtfelder `resource_id`, `date_from`, `date_to`.
3. Der MCP-Server sendet einen `GET /availability/{resource_id}`-Request mit den Query-Parametern an die API.
4. Die API liefert die Verfügbarkeitsdaten für den Zeitraum.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent präsentiert die Verfügbarkeit dem Nutzer.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| resource_id | integer | ja | > 0 |
| date_from | date | ja | ISO 8601 Format |
| date_to | date | ja | ISO 8601 Format, >= date_from |
| prop_name | string | nein | Eigenschaftsfilter |

## Ergebnis

Strukturiertes Objekt mit:
- Verfügbarkeitsdaten pro Tag im angegebenen Zeitraum
- Status je Datum (verfügbar/belegt/teilweise belegt)

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
