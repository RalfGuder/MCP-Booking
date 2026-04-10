---
id: "006"
title: Buchung genehmigen
tags:
  - UseCase
  - Bookings
status: open
---

# UC-006: Buchung genehmigen

**User Story:** [US-011 Tool: approve_booking](../001%20User%20Stories/US-011-approve-booking.md) | [Issue #11](https://github.com/RalfGuder/MCP-Booking/issues/11)

## Akteure

- **Primär:** KI-Assistent (z.B. Claude)
- **Sekundär:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und über stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.
3. Die Buchung existiert und hat den Status `pending`.

## Auslöser

Der Nutzer möchte eine ausstehende Buchung genehmigen (z.B. "Genehmige die Buchung 42").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `approve_booking` mit der Buchungs-ID auf.
2. Der MCP-Server validiert den Parameter `id`.
3. Der MCP-Server sendet einen `POST /bookings/{id}/approve`-Request an die API.
4. Die API setzt den Status der Buchung auf `approved`.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent bestätigt dem Nutzer die Genehmigung.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| id | integer | ja | > 0 |

## Ergebnis

Strukturiertes Objekt mit:
- Bestätigung der Genehmigung
- Aktualisierter Buchungsstatus (`approved`)

## Fehlerabläufe

### E1: Buchung nicht gefunden (404)
3a. Die API liefert 404 Not Found.
4a. Der MCP-Server liefert eine Fehlermeldung: "Buchung mit ID [id] nicht gefunden."

### E2: Buchung bereits genehmigt
3a. Die Buchung hat bereits den Status `approved`.
4a. Die API liefert eine entsprechende Antwort.
5a. Der MCP-Server informiert: "Die Buchung ist bereits genehmigt."

### E3: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

## Nachbedingungen

- Der Status der Buchung ist `approved`.


## Test Case

- [TC-006](../004%20Test%20Cases/TC-006-approve-booking.md)
