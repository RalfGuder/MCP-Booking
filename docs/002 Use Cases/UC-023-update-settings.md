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

- **Primär:** KI-Assistent (z.B. Claude)
- **Sekundär:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und über stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.

## Auslöser

Der Nutzer möchte Einstellungen ändern (z.B. "Setze die Standard-Buchungsdauer auf 2 Stunden").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `update_settings` mit den zu ändernden Einstellungsgruppen auf.
2. Der MCP-Server validiert die Parameter.
3. Der MCP-Server sendet einen `PUT /settings`-Request an die API mit dem JSON-Body.
4. Die API aktualisiert die Einstellungen und liefert eine Bestätigung.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent bestätigt dem Nutzer die Änderung.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| calendar | object | nein | Kalender-Einstellungen |
| booking | object | nein | Buchungs-Workflow-Einstellungen |
| ui | object | nein | Benutzeroberflächen-Einstellungen |
| confirmation | object | nein | Bestätigungs-/Benachrichtigungseinstellungen |

## Ergebnis

Strukturiertes Objekt mit:
- Bestätigung der Aktualisierung
- Aktualisierte Einstellungen

## Alternative Abläufe

### A1: Nur eine Gruppe ändern
1a. Nur eine Einstellungsgruppe wird übergeben (z.B. nur `calendar`).
3a. Der Request enthält nur die geänderte Gruppe.

## Fehlerabläufe

### E1: Keine Einstellungen übergeben
2a. Keine der vier Gruppen wurde übergeben.
3a. Der MCP-Server liefert eine Fehlermeldung: "Mindestens eine Einstellungsgruppe muss angegeben werden."

### E2: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

## Nachbedingungen

- Die Einstellungen im System sind gemäß den Änderungen aktualisiert.


## Test Case

- [TC-023](../004%20Test%20Cases/TC-023-update-settings.md)
