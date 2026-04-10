---
id: "003"
title: Buchung abrufen
tags:
  - UseCase
  - Bookings
status: open
---

# UC-003: Buchung abrufen

**User Story:** [US-008 Tool: get_booking](../001%20User%20Stories/US-008-get-booking.md) | [Issue #8](https://github.com/RalfGuder/MCP-Booking/issues/8)

## Akteure

- **Primaer:** KI-Assistent (z.B. Claude)
- **Sekundaer:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und ueber stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.
3. Die Buchungs-ID ist bekannt.

## Ausloeser

Der Nutzer fragt nach den Details einer bestimmten Buchung (z.B. "Zeige mir die Details der Buchung 42").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `get_booking` mit der Buchungs-ID auf.
2. Der MCP-Server validiert den Parameter `id`.
3. Der MCP-Server sendet einen `GET /bookings/{id}`-Request an die API.
4. Die API liefert die vollstaendigen Buchungsdetails.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent praesentiert die Buchungsdetails dem Nutzer.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| id | integer | ja | > 0 |

## Ergebnis

Strukturiertes Objekt mit allen Buchungsdetails:
- Buchungs-ID, Status, Erstellungsdatum
- Ressource/Buchungstyp
- Formulardaten (Name, E-Mail, etc.)
- Gebuchte Daten
- Notizen

## Fehlerablaeufe

### E1: Buchung nicht gefunden (404)
3a. Die API liefert 404 Not Found.
4a. Der MCP-Server liefert eine Fehlermeldung: "Buchung mit ID [id] nicht gefunden."

### E2: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

### E3: Ungueltiger Parameter
2a. Die ID ist keine gueltige Zahl.
3a. Der MCP-Server liefert eine Fehlermeldung: "Ungueltige Buchungs-ID."

## Nachbedingungen

- Keine Zustandsaenderung im System (lesender Zugriff).
