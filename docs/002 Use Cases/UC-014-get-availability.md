---
id: "014"
title: Verfuegbarkeit abrufen
tags:
  - UseCase
  - Availability
status: open
---

# UC-014: Verfuegbarkeit abrufen

**User Story:** [US-019 Tool: get_availability](../001%20User%20Stories/US-019-get-availability.md) | [Issue #19](https://github.com/RalfGuder/MCP-Booking/issues/19)

## Akteure

- **Primaer:** KI-Assistent (z.B. Claude)
- **Sekundaer:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und ueber stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.
3. Die Ressource existiert im System.

## Ausloeser

Der Nutzer fragt nach der Verfuegbarkeit einer Ressource (z.B. "Ist der Gemeindesaal im Juni verfuegbar?").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `get_availability` mit Ressource-ID und Datumsbereich auf.
2. Der MCP-Server validiert die Pflichtfelder `resource_id`, `date_from`, `date_to`.
3. Der MCP-Server sendet einen `GET /availability/{resource_id}`-Request mit den Query-Parametern an die API.
4. Die API liefert die Verfuegbarkeitsdaten fuer den Zeitraum.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent praesentiert die Verfuegbarkeit dem Nutzer.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| resource_id | integer | ja | > 0 |
| date_from | date | ja | ISO 8601 Format |
| date_to | date | ja | ISO 8601 Format, >= date_from |
| prop_name | string | nein | Eigenschaftsfilter |

## Ergebnis

Strukturiertes Objekt mit:
- Verfuegbarkeitsdaten pro Tag im angegebenen Zeitraum
- Status je Datum (verfuegbar/belegt/teilweise belegt)

## Fehlerablaeufe

### E1: Ressource nicht gefunden (404)
3a. Die API liefert 404 Not Found.
4a. Der MCP-Server liefert eine Fehlermeldung: "Ressource mit ID [id] nicht gefunden."

### E2: Ungueltiger Datumsbereich
2a. `date_to` liegt vor `date_from`.
3a. Der MCP-Server liefert eine Fehlermeldung: "Enddatum muss nach dem Startdatum liegen."

### E3: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

## Nachbedingungen

- Keine Zustandsaenderung im System (lesender Zugriff).
