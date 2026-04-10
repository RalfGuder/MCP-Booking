---
id: "002"
title: Buchung anlegen
tags:
  - UseCase
  - Bookings
status: open
---

# UC-002: Buchung anlegen

**User Story:** [US-007 Tool: create_booking](../001%20User%20Stories/US-007-create-booking.md) | [Issue #7](https://github.com/RalfGuder/MCP-Booking/issues/7)

## Akteure

- **Primaer:** KI-Assistent (z.B. Claude)
- **Sekundaer:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und ueber stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.
3. Die gewuenschte Ressource (booking_type) existiert im System.

## Ausloeser

Der Nutzer bittet den KI-Assistenten, eine neue Buchung anzulegen (z.B. "Buche den Gemeindesaal fuer den 15. Mai").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `create_booking` auf.
2. Der MCP-Server validiert die Pflichtfelder (booking_type, form_data, dates).
3. Der MCP-Server sendet einen `POST /bookings`-Request an die API mit dem JSON-Body.
4. Die API erstellt die Buchung und liefert eine Bestaetigung mit der neuen Buchungs-ID.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent bestaetigt dem Nutzer die erfolgreiche Buchung.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| booking_type | integer | ja | > 0, muss existierende Ressource-ID sein |
| form_data | object | ja | Nicht-leeres Objekt mit Formularfeldern |
| dates | array[date-time] | ja | Mindestens ein Datum, ISO 8601 Format |

## Ergebnis

Strukturiertes Objekt mit:
- Buchungs-ID der neu erstellten Buchung
- Status der Buchung (typischerweise `pending`)
- Bestaetigung der gebuchten Daten

## Alternative Ablaeufe

### A1: Buchung mit Sofort-Genehmigung
4a. Die API-Konfiguration sieht automatische Genehmigung vor.
5a. Die Buchung wird mit Status `approved` zurueckgeliefert.

## Fehlerablaeufe

### E1: Fehlende Pflichtfelder
2a. Ein oder mehrere Pflichtfelder fehlen.
3a. Der MCP-Server liefert eine Fehlermeldung: "Pflichtfeld [Feldname] fehlt."

### E2: Ungueltige Ressource
3a. Die API liefert einen Fehler, weil die Ressource-ID nicht existiert.
4a. Der MCP-Server liefert eine Fehlermeldung: "Ressource mit ID [id] nicht gefunden."

### E3: Datum nicht verfuegbar
3a. Die API liefert einen Fehler, weil das gewuenschte Datum bereits belegt ist.
4a. Der MCP-Server liefert eine Fehlermeldung: "Das gewuenschte Datum ist nicht verfuegbar."

### E4: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

## Nachbedingungen

- Eine neue Buchung existiert im System (Status: `pending` oder `approved`).
- Die Verfuegbarkeit der Ressource fuer die gebuchten Daten ist aktualisiert.
