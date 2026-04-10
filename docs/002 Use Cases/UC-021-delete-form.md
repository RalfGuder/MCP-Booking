---
id: "021"
title: Formular löschen
tags:
  - UseCase
  - Forms
status: open
---

# UC-021: Formular löschen

**User Story:** [US-026 Tool: delete_form](../001%20User%20Stories/US-026-delete-form.md) | [Issue #26](https://github.com/RalfGuder/MCP-Booking/issues/26)

## Akteure

- **Primär:** KI-Assistent (z.B. Claude)
- **Sekundär:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und über stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.
3. Das zu löschende Formular existiert im System.

## Auslöser

Der Nutzer möchte ein Formular entfernen (z.B. "Lösche das Formular 4").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `delete_form` mit der Formular-ID auf.
2. Der MCP-Server validiert den Parameter `id`.
3. Der MCP-Server sendet einen `DELETE /forms/{id}`-Request an die API.
4. Die API löscht das Formular und liefert eine Bestätigung.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent bestätigt dem Nutzer die Löschung.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| id | integer | ja | > 0 |

## Ergebnis

Strukturiertes Objekt mit:
- Bestätigung der Löschung
- ID des gelöschten Formulars

## Fehlerabläufe

### E1: Formular nicht gefunden (404)
3a. Die API liefert 404 Not Found.
4a. Der MCP-Server liefert eine Fehlermeldung: "Formular mit ID [id] nicht gefunden."

### E2: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

## Nachbedingungen

- Das Formular existiert nicht mehr im System.
