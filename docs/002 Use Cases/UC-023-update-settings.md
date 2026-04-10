---
id: "023"
title: Einstellungen aktualisieren
tags:
  - UseCase
  - Settings
status: open
---

# UC-023: Einstellungen aktualisieren

**User Story:** [US-028 Tool: update_settings](../001%20User%20Stories/US-028-update-settings.md) | [Issue #28](https://github.com/RalfGuder/MCP-Booking/issues/28)

## Akteure

- **Primaer:** KI-Assistent (z.B. Claude)
- **Sekundaer:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und ueber stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.

## Ausloeser

Der Nutzer moechte Einstellungen aendern (z.B. "Setze die Standard-Buchungsdauer auf 2 Stunden").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `update_settings` mit den zu aendernden Einstellungsgruppen auf.
2. Der MCP-Server validiert die Parameter.
3. Der MCP-Server sendet einen `PUT /settings`-Request an die API mit dem JSON-Body.
4. Die API aktualisiert die Einstellungen und liefert eine Bestaetigung.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent bestaetigt dem Nutzer die Aenderung.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| calendar | object | nein | Kalender-Einstellungen |
| booking | object | nein | Buchungs-Workflow-Einstellungen |
| ui | object | nein | Benutzeroberflaechen-Einstellungen |
| confirmation | object | nein | Bestaetigungs-/Benachrichtigungseinstellungen |

## Ergebnis

Strukturiertes Objekt mit:
- Bestaetigung der Aktualisierung
- Aktualisierte Einstellungen

## Alternative Ablaeufe

### A1: Nur eine Gruppe aendern
1a. Nur eine Einstellungsgruppe wird uebergeben (z.B. nur `calendar`).
3a. Der Request enthaelt nur die geaenderte Gruppe.

## Fehlerablaeufe

### E1: Keine Einstellungen uebergeben
2a. Keine der vier Gruppen wurde uebergeben.
3a. Der MCP-Server liefert eine Fehlermeldung: "Mindestens eine Einstellungsgruppe muss angegeben werden."

### E2: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

## Nachbedingungen

- Die Einstellungen im System sind gemaess den Aenderungen aktualisiert.
