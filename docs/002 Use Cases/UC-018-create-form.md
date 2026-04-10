---
id: "018"
title: Formular anlegen
tags:
  - UseCase
  - Forms
status: open
---

# UC-018: Formular anlegen

**User Story:** [US-023 Tool: create_form](../001%20User%20Stories/US-023-create-form.md) | [Issue #23](https://github.com/RalfGuder/MCP-Booking/issues/23)

## Akteure

- **Primär:** KI-Assistent (z.B. Claude)
- **Sekundär:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und über stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.

## Auslöser

Der Nutzer möchte ein neues Buchungsformular erstellen (z.B. "Erstelle ein Formular 'Raumreservierung' mit Feldern für Name und E-Mail").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `create_form` mit den Formulardaten auf.
2. Der MCP-Server validiert die Pflichtfelder `title` und `structure_json`.
3. Der MCP-Server sendet einen `POST /forms`-Request an die API.
4. Die API erstellt das Formular und liefert eine Bestätigung mit der neuen Formular-ID.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent bestätigt dem Nutzer die Erstellung.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| title | string | ja | Nicht-leerer String |
| structure_json | string | ja | Gültiger JSON-String |
| form_slug | string | nein | URL-freundlicher Slug |
| settings_json | string | nein | Gültiger JSON-String |
| status | string | nein | Einer von: `published`, `draft` |

## Ergebnis

Strukturiertes Objekt mit:
- Formular-ID des neu erstellten Formulars
- Bestätigung mit Titel und Status

## Fehlerabläufe

### E1: Fehlende Pflichtfelder
2a. `title` oder `structure_json` fehlen.
3a. Der MCP-Server liefert eine Fehlermeldung: "Pflichtfeld [Feldname] fehlt."

### E2: Ungültiges JSON
2a. `structure_json` oder `settings_json` sind kein gültiges JSON.
3a. Der MCP-Server liefert eine Fehlermeldung: "Ungültiges JSON-Format in [Feldname]."

### E3: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

## Nachbedingungen

- Ein neues Formular existiert im System (Status: `published` oder `draft`).


## Test Case

- [TC-018](../004%20Test%20Cases/TC-018-create-form.md)
