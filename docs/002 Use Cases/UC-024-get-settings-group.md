---
id: "024"
title: Einstellungsgruppe abrufen
tags:
  - UseCase
  - Settings
status: open
---

# UC-024: Einstellungsgruppe abrufen

**User Story:** [US-029 Tool: get_settings_group](../001%20User%20Stories/US-029-get-settings-group.md) | [Issue #29](https://github.com/RalfGuder/MCP-Booking/issues/29)

## Akteure

- **Primaer:** KI-Assistent (z.B. Claude)
- **Sekundaer:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und ueber stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.

## Ausloeser

Der Nutzer fragt nach Einstellungen einer bestimmten Gruppe (z.B. "Zeige mir nur die Kalender-Einstellungen").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `get_settings_group` mit dem Gruppennamen auf.
2. Der MCP-Server validiert den Parameter `group`.
3. Der MCP-Server sendet einen `GET /settings/{group}`-Request an die API.
4. Die API liefert die Einstellungen der angegebenen Gruppe.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent praesentiert die Gruppeneinstellungen dem Nutzer.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| group | string | ja | Nicht-leer, Pattern: `^[a-z_]+$` (nur Kleinbuchstaben und Unterstriche) |

## Ergebnis

Strukturiertes Objekt mit:
- Einstellungen der angegebenen Gruppe

## Fehlerablaeufe

### E1: Gruppe nicht gefunden (404)
3a. Die API liefert 404 Not Found.
4a. Der MCP-Server liefert eine Fehlermeldung: "Einstellungsgruppe '[group]' nicht gefunden."

### E2: Ungueltiger Gruppenname
2a. Der Gruppenname enthaelt ungueltige Zeichen.
3a. Der MCP-Server liefert eine Fehlermeldung: "Ungueltiger Gruppenname. Erlaubt sind nur Kleinbuchstaben und Unterstriche."

### E3: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

## Nachbedingungen

- Keine Zustandsaenderung im System (lesender Zugriff).
