---
id: "004"
title: Buchung aktualisieren
tags:
  - UseCase
  - Bookings
status: open
---

# UC-004: Buchung aktualisieren

**User Story:** [US-009 Tool: update_booking](../001%20User%20Stories/US-009-update-booking.md) | [Issue #9](https://github.com/RalfGuder/MCP-Booking/issues/9)

## Akteure

- **Primaer:** KI-Assistent (z.B. Claude)
- **Sekundaer:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und ueber stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.
3. Die zu aktualisierende Buchung existiert im System.

## Ausloeser

Der Nutzer moechte eine bestehende Buchung aendern (z.B. "Aendere die E-Mail-Adresse in der Buchung 42").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `update_booking` mit ID und den zu aendernden Feldern auf.
2. Der MCP-Server validiert die Parameter.
3. Der MCP-Server sendet einen `PUT /bookings/{id}`-Request an die API mit dem JSON-Body.
4. Die API aktualisiert die Buchung und liefert eine Bestaetigung.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent bestaetigt dem Nutzer die erfolgreiche Aktualisierung.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| id | integer | ja | > 0 |
| form_data | object | nein | Objekt mit aktualisierten Formularfeldern |
| booking_type | integer | nein | > 0, existierende Ressource-ID |
| status | string | nein | Einer von: `pending`, `approved`, `trash` |

## Ergebnis

Strukturiertes Objekt mit:
- Bestaetigung der Aktualisierung
- Aktualisierte Buchungsdetails

## Alternative Ablaeufe

### A1: Nur Status aendern
1a. Nur das Feld `status` wird uebergeben.
3a. Der Request enthaelt nur das Status-Feld im Body.

## Fehlerablaeufe

### E1: Buchung nicht gefunden (404)
3a. Die API liefert 404 Not Found.
4a. Der MCP-Server liefert eine Fehlermeldung: "Buchung mit ID [id] nicht gefunden."

### E2: Keine Felder zum Aktualisieren
2a. Ausser `id` wurden keine Felder uebergeben.
3a. Der MCP-Server liefert eine Fehlermeldung: "Mindestens ein Feld muss aktualisiert werden."

### E3: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

## Nachbedingungen

- Die Buchung im System wurde mit den neuen Werten aktualisiert.
