---
id: "008"
title: Notiz an Buchung anfügen
tags:
  - UseCase
  - Bookings
status: open
---

# UC-008: Notiz an Buchung anfügen

**User Story:** [US-013 Tool: update_booking_note](../001%20User%20Stories/US-013-update-booking-note.md) | [Issue #13](https://github.com/RalfGuder/MCP-Booking/issues/13)

## Akteure

- **Primär:** KI-Assistent (z.B. Claude)
- **Sekundär:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und über stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.
3. Die Buchung existiert im System.

## Auslöser

Der Nutzer möchte einer Buchung eine Notiz hinzufügen (z.B. "Füge zur Buchung 42 die Notiz 'Rollstuhlgerecht benötigt' hinzu").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `update_booking_note` mit ID und Notiztext auf.
2. Der MCP-Server validiert die Parameter `id` und `note`.
3. Der MCP-Server sendet einen `PUT /bookings/{id}/note`-Request an die API mit dem JSON-Body.
4. Die API speichert die Notiz und liefert eine Bestätigung.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent bestätigt dem Nutzer das Anfügen der Notiz.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| id | integer | ja | > 0 |
| note | string | ja | Nicht-leerer String |

## Ergebnis

Strukturiertes Objekt mit:
- Bestätigung der Notizerstellung
- Buchungs-ID

## Fehlerabläufe

### E1: Buchung nicht gefunden (404)
3a. Die API liefert 404 Not Found.
4a. Der MCP-Server liefert eine Fehlermeldung: "Buchung mit ID [id] nicht gefunden."

### E2: Leere Notiz
2a. Der Notiztext ist leer.
3a. Der MCP-Server liefert eine Fehlermeldung: "Der Notiztext darf nicht leer sein."

### E3: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

## Nachbedingungen

- Die Notiz ist der Buchung zugeordnet.


## Test Case

- [TC-008](../004%20Test%20Cases/TC-008-update-booking-note.md)
