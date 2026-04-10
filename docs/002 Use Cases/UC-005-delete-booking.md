---
id: "005"
title: Buchung löschen
tags:
  - UseCase
  - Bookings
status: open
---

# UC-005: Buchung löschen

**User Story:** [US-010 Tool: delete_booking](../001%20User%20Stories/US-010-delete-booking.md) | [Issue #10](https://github.com/RalfGuder/MCP-Booking/issues/10)

## Akteure

- **Primär:** KI-Assistent (z.B. Claude)
- **Sekundär:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und über stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.
3. Die zu löschende Buchung existiert im System.

## Auslöser

Der Nutzer möchte eine Buchung entfernen (z.B. "Lösche die Buchung 42").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `delete_booking` mit der Buchungs-ID auf.
2. Der MCP-Server validiert den Parameter `id`.
3. Der MCP-Server sendet einen `DELETE /bookings/{id}`-Request an die API.
4. Die API löscht die Buchung und liefert eine Bestätigung.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent bestätigt dem Nutzer die erfolgreiche Löschung.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| id | integer | ja | > 0 |

## Ergebnis

Strukturiertes Objekt mit:
- Bestätigung der Löschung
- ID der gelöschten Buchung

## Fehlerabläufe

### E1: Buchung nicht gefunden (404)
3a. Die API liefert 404 Not Found.
4a. Der MCP-Server liefert eine Fehlermeldung: "Buchung mit ID [id] nicht gefunden."

### E2: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

## Nachbedingungen

- Die Buchung existiert nicht mehr im System.
- Die Verfügbarkeit der zugehörigen Ressource für die freigegebenen Daten ist wiederhergestellt.


## Test Case

- [TC-005](../004%20Test%20Cases/TC-005-delete-booking.md)
