---
id: "011"
title: Ressource abrufen
tags:
  - UseCase
  - Resources
status: open
---

# UC-011: Ressource abrufen

**User Story:** [US-016 Tool: get_resource](../001%20User%20Stories/US-016-get-resource.md) | [Issue #16](https://github.com/RalfGuder/MCP-Booking/issues/16)

## Akteure

- **Primär:** KI-Assistent (z.B. Claude)
- **Sekundär:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und über stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.
3. Die Ressource-ID ist bekannt.

## Auslöser

Der Nutzer fragt nach Details einer bestimmten Ressource (z.B. "Zeige mir die Details der Ressource 3").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `get_resource` mit der Ressource-ID auf.
2. Der MCP-Server validiert den Parameter `id`.
3. Der MCP-Server sendet einen `GET /resources/{id}`-Request an die API.
4. Die API liefert die vollständigen Ressourcendetails.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent präsentiert die Ressourcendetails dem Nutzer.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| id | integer | ja | > 0 |

## Ergebnis

Strukturiertes Objekt mit:
- Ressource-ID, Titel, Kosten, Besucheranzahl

## Fehlerabläufe

### E1: Ressource nicht gefunden (404)
3a. Die API liefert 404 Not Found.
4a. Der MCP-Server liefert eine Fehlermeldung: "Ressource mit ID [id] nicht gefunden."

### E2: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

## Nachbedingungen

- Keine Zustandsänderung im System (lesender Zugriff).


## Test Case

- [TC-011](../004%20Test%20Cases/TC-011-get-resource.md)
