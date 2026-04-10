---
id: "015"
title: Verfügbarkeit aktualisieren
tags:
  - UseCase
  - Availability
status: open
---

# UC-015: Verfügbarkeit aktualisieren

**User Story:** [US-020 Tool: update_availability](../001%20User%20Stories/US-020-update-availability.md) | [Issue #20](https://github.com/RalfGuder/MCP-Booking/issues/20)

## Akteure

- **Primär:** KI-Assistent (z.B. Claude)
- **Sekundär:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und über stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.
3. Die Ressource existiert im System.

## Auslöser

Der Nutzer möchte die Verfügbarkeit einer Ressource ändern (z.B. "Sperre den Gemeindesaal am 20. Juni").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `update_availability` mit der Ressource-ID und den Änderungsdaten auf.
2. Der MCP-Server validiert den Pflichtparameter `resource_id`.
3. Der MCP-Server sendet einen `PUT /availability/{resource_id}`-Request an die API.
4. Die API aktualisiert die Verfügbarkeit und liefert eine Bestätigung.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent bestätigt dem Nutzer die Änderung.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| resource_id | integer | ja | > 0 |

## Ergebnis

Strukturiertes Objekt mit:
- Bestätigung der Aktualisierung

## Fehlerabläufe

### E1: Ressource nicht gefunden (404)
3a. Die API liefert 404 Not Found.
4a. Der MCP-Server liefert eine Fehlermeldung: "Ressource mit ID [id] nicht gefunden."

### E2: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

## Nachbedingungen

- Die Verfügbarkeit der Ressource ist gemäß den Änderungen aktualisiert.
