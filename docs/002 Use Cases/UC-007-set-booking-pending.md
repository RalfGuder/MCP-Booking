---
id: "007"
title: Buchung auf ausstehend setzen
tags:
  - UseCase
  - Bookings
status: open
---

# UC-007: Buchung auf ausstehend setzen

**User Story:** [US-012 Tool: set_booking_pending](../001%20User%20Stories/US-012-set-booking-pending.md) | [Issue #12](https://github.com/RalfGuder/MCP-Booking/issues/12)

## Akteure

- **Primär:** KI-Assistent (z.B. Claude)
- **Sekundär:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und über stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.
3. Die Buchung existiert und hat den Status `approved`.

## Auslöser

Der Nutzer möchte eine genehmigte Buchung zur erneuten Prüfung zurücksetzen (z.B. "Setze Buchung 42 zurück auf ausstehend").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `set_booking_pending` mit der Buchungs-ID auf.
2. Der MCP-Server validiert den Parameter `id`.
3. Der MCP-Server sendet einen `POST /bookings/{id}/pending`-Request an die API.
4. Die API setzt den Status der Buchung auf `pending`.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent bestätigt dem Nutzer die Statusänderung.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| id | integer | ja | > 0 |

## Ergebnis

Strukturiertes Objekt mit:
- Bestätigung der Statusänderung
- Aktualisierter Buchungsstatus (`pending`)

## Fehlerabläufe

### E1: Buchung nicht gefunden (404)
3a. Die API liefert 404 Not Found.
4a. Der MCP-Server liefert eine Fehlermeldung: "Buchung mit ID [id] nicht gefunden."

### E2: Buchung bereits ausstehend
3a. Die Buchung hat bereits den Status `pending`.
4a. Der MCP-Server informiert: "Die Buchung ist bereits ausstehend."

### E3: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

## Nachbedingungen

- Der Status der Buchung ist `pending`.
