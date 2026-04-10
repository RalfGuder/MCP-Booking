---
id: "005"
title: Buchung loeschen
tags:
  - UseCase
  - Bookings
status: open
---

# UC-005: Buchung loeschen

**User Story:** [US-010 Tool: delete_booking](../001%20User%20Stories/US-010-delete-booking.md) | [Issue #10](https://github.com/RalfGuder/MCP-Booking/issues/10)

## Akteure

- **Primaer:** KI-Assistent (z.B. Claude)
- **Sekundaer:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und ueber stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.
3. Die zu loeschende Buchung existiert im System.

## Ausloeser

Der Nutzer moechte eine Buchung entfernen (z.B. "Loesche die Buchung 42").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `delete_booking` mit der Buchungs-ID auf.
2. Der MCP-Server validiert den Parameter `id`.
3. Der MCP-Server sendet einen `DELETE /bookings/{id}`-Request an die API.
4. Die API loescht die Buchung und liefert eine Bestaetigung.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent bestaetigt dem Nutzer die erfolgreiche Loeschung.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| id | integer | ja | > 0 |

## Ergebnis

Strukturiertes Objekt mit:
- Bestaetigung der Loeschung
- ID der geloeschten Buchung

## Fehlerablaeufe

### E1: Buchung nicht gefunden (404)
3a. Die API liefert 404 Not Found.
4a. Der MCP-Server liefert eine Fehlermeldung: "Buchung mit ID [id] nicht gefunden."

### E2: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

## Nachbedingungen

- Die Buchung existiert nicht mehr im System.
- Die Verfuegbarkeit der zugehoerigen Ressource fuer die freigegebenen Daten ist wiederhergestellt.
