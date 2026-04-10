---
id: "022"
title: Einstellungen auflisten
tags:
  - UseCase
  - Settings
status: open
---

# UC-022: Einstellungen auflisten

**User Story:** [US-027 Tool: list_settings](../001%20User%20Stories/US-027-get-settings.md) | [Issue #27](https://github.com/RalfGuder/MCP-Booking/issues/27)

## Akteure

- **Primär:** KI-Assistent (z.B. Claude)
- **Sekundär:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und über stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.

## Auslöser

Der Nutzer fragt nach den aktuellen Einstellungen (z.B. "Zeige mir die Buchungseinstellungen").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `list_settings` auf.
2. Der MCP-Server sendet einen `GET /settings`-Request an die API.
3. Die API liefert alle Einstellungsgruppen (calendar, booking, ui, confirmation).
4. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
5. Der KI-Assistent präsentiert die Einstellungen dem Nutzer.

## Parameter

Keine Parameter erforderlich.

## Ergebnis

Strukturiertes Objekt mit:
- Kalender-Einstellungen (Anzeige und Verhalten)
- Buchungs-Einstellungen (Workflow)
- UI-Einstellungen (Benutzeroberfläche)
- Bestätigungs-Einstellungen (Benachrichtigungen)

## Fehlerabläufe

### E1: Authentifizierungsfehler (401/403)
2a. Die API liefert 401 oder 403.
3a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

### E2: API nicht erreichbar
2a. Der HTTP-Request schlägt fehl.
3a. Der MCP-Server liefert eine Fehlermeldung: "Die Booking API ist nicht erreichbar."

## Nachbedingungen

- Keine Zustandsänderung im System (lesender Zugriff).
