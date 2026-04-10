---
id: "012"
title: Ressource aktualisieren
tags:
  - UseCase
  - Resources
status: open
---

# UC-012: Ressource aktualisieren

**User Story:** [US-017 Tool: update_resource](../001%20User%20Stories/US-017-update-resource.md) | [Issue #17](https://github.com/RalfGuder/MCP-Booking/issues/17)

## Akteure

- **Primär:** KI-Assistent (z.B. Claude)
- **Sekundär:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und über stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.
3. Die zu aktualisierende Ressource existiert im System.

## Auslöser

Der Nutzer möchte eine Ressource ändern (z.B. "Aendere die Kosten der Ressource 3 auf 75 EUR").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `update_resource` mit ID und den zu ändernden Feldern auf.
2. Der MCP-Server validiert die Parameter.
3. Der MCP-Server sendet einen `PUT /resources/{id}`-Request an die API.
4. Die API aktualisiert die Ressource und liefert eine Bestätigung.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent bestätigt dem Nutzer die Aktualisierung.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| id | integer | ja | > 0 |
| title | string | nein | Nicht-leerer String |
| cost | string | nein | Kostenangabe |
| visitors | integer | nein | > 0 |

## Ergebnis

Strukturiertes Objekt mit:
- Bestätigung der Aktualisierung
- Aktualisierte Ressourcendetails

## Fehlerabläufe

### E1: Ressource nicht gefunden (404)
3a. Die API liefert 404 Not Found.
4a. Der MCP-Server liefert eine Fehlermeldung: "Ressource mit ID [id] nicht gefunden."

### E2: Keine Felder zum Aktualisieren
2a. Ausser `id` wurden keine Felder übergeben.
3a. Der MCP-Server liefert eine Fehlermeldung: "Mindestens ein Feld muss aktualisiert werden."

### E3: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

## Nachbedingungen

- Die Ressource im System wurde mit den neuen Werten aktualisiert.


## Test Case

- [TC-012](../004%20Test%20Cases/TC-012-update-resource.md)
