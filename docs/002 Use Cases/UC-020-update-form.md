---
id: "020"
title: Formular aktualisieren
tags:
  - UseCase
  - Forms
status: open
---

# UC-020: Formular aktualisieren

**User Story:** [US-025 Tool: update_form](../001%20User%20Stories/US-025-update-form.md) | [Issue #25](https://github.com/RalfGuder/MCP-Booking/issues/25)

## Akteure

- **Primaer:** KI-Assistent (z.B. Claude)
- **Sekundaer:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und ueber stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.
3. Das zu aktualisierende Formular existiert im System.

## Ausloeser

Der Nutzer moechte ein Formular aendern (z.B. "Fuege dem Formular 2 ein Telefon-Feld hinzu").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `update_form` mit ID und den zu aendernden Feldern auf.
2. Der MCP-Server validiert die Parameter.
3. Der MCP-Server sendet einen `PUT /forms/{id}`-Request an die API.
4. Die API aktualisiert das Formular und liefert eine Bestaetigung.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent bestaetigt dem Nutzer die Aktualisierung.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| id | integer | ja | > 0 |
| title | string | nein | Nicht-leerer String |
| form_slug | string | nein | URL-freundlicher Slug |
| structure_json | string | nein | Gueltiger JSON-String |
| settings_json | string | nein | Gueltiger JSON-String |
| status | string | nein | Einer von: `published`, `draft` |

## Ergebnis

Strukturiertes Objekt mit:
- Bestaetigung der Aktualisierung
- Aktualisierte Formulardetails

## Fehlerablaeufe

### E1: Formular nicht gefunden (404)
3a. Die API liefert 404 Not Found.
4a. Der MCP-Server liefert eine Fehlermeldung: "Formular mit ID [id] nicht gefunden."

### E2: Ungueltiges JSON
2a. `structure_json` oder `settings_json` sind kein gueltiges JSON.
3a. Der MCP-Server liefert eine Fehlermeldung: "Ungueltiges JSON-Format in [Feldname]."

### E3: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

## Nachbedingungen

- Das Formular im System wurde mit den neuen Werten aktualisiert.
