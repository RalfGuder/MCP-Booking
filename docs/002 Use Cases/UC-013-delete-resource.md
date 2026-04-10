---
id: "013"
title: Ressource löschen
tags:
  - UseCase
  - Resources
status: open
---

# UC-013: Ressource löschen

**User Story:** [US-018 Tool: delete_resource](../001%20User%20Stories/US-018-delete-resource.md) | [Issue #18](https://github.com/RalfGuder/MCP-Booking/issues/18)

## Akteure

- **Primär:** KI-Assistent (z.B. Claude)
- **Sekundär:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und über stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.
3. Die zu löschende Ressource existiert im System.

## Auslöser

Der Nutzer möchte eine Ressource entfernen (z.B. "Lösche die Ressource 5").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `delete_resource` mit der Ressource-ID auf.
2. Der MCP-Server validiert den Parameter `id`.
3. Der MCP-Server sendet einen `DELETE /resources/{id}`-Request an die API.
4. Die API löscht die Ressource und liefert eine Bestätigung.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent bestätigt dem Nutzer die Löschung.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| id | integer | ja | > 0 |

## Ergebnis

Strukturiertes Objekt mit:
- Bestätigung der Löschung
- ID der gelöschten Ressource

## Fehlerabläufe

### E1: Ressource nicht gefunden (404)
3a. Die API liefert 404 Not Found.
4a. Der MCP-Server liefert eine Fehlermeldung: "Ressource mit ID [id] nicht gefunden."

### E2: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

## Nachbedingungen

- Die Ressource existiert nicht mehr im System.
- Bestehende Buchungen für diese Ressource sind davon potenziell betroffen.
