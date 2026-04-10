---
id: "008"
title: Notiz an Buchung anfuegen
tags:
  - UseCase
  - Bookings
status: open
---

# UC-008: Notiz an Buchung anfuegen

**User Story:** [US-013 Tool: update_booking_note](../001%20User%20Stories/US-013-update-booking-note.md) | [Issue #13](https://github.com/RalfGuder/MCP-Booking/issues/13)

## Akteure

- **Primaer:** KI-Assistent (z.B. Claude)
- **Sekundaer:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und ueber stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.
3. Die Buchung existiert im System.

## Ausloeser

Der Nutzer moechte einer Buchung eine Notiz hinzufuegen (z.B. "Fuege zur Buchung 42 die Notiz 'Rollstuhlgerecht benoetigt' hinzu").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `update_booking_note` mit ID und Notiztext auf.
2. Der MCP-Server validiert die Parameter `id` und `note`.
3. Der MCP-Server sendet einen `PUT /bookings/{id}/note`-Request an die API mit dem JSON-Body.
4. Die API speichert die Notiz und liefert eine Bestaetigung.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent bestaetigt dem Nutzer das Anfuegen der Notiz.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| id | integer | ja | > 0 |
| note | string | ja | Nicht-leerer String |

## Ergebnis

Strukturiertes Objekt mit:
- Bestaetigung der Notizerstellung
- Buchungs-ID

## Fehlerablaeufe

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
