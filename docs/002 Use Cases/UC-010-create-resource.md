---
id: "010"
title: Ressource anlegen
tags:
  - UseCase
  - Resources
status: open
---

# UC-010: Ressource anlegen

**User Story:** [US-015 Tool: create_resource](../001%20User%20Stories/US-015-create-resource.md) | [Issue #15](https://github.com/RalfGuder/MCP-Booking/issues/15)

## Akteure

- **Primär:** KI-Assistent (z.B. Claude)
- **Sekundär:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und über stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.

## Auslöser

Der Nutzer möchte eine neue buchbare Ressource anlegen (z.B. "Erstelle eine Ressource 'Vereinsraum' mit Kosten 50 EUR für 30 Besucher").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `create_resource` mit den Ressourcendaten auf.
2. Der MCP-Server validiert das Pflichtfeld `title`.
3. Der MCP-Server sendet einen `POST /resources`-Request an die API.
4. Die API erstellt die Ressource und liefert eine Bestätigung mit der neuen Ressource-ID.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent bestätigt dem Nutzer die Erstellung.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| title | string | ja | Nicht-leerer String |
| cost | string | nein | Kostenangabe |
| visitors | integer | nein | > 0 |

## Ergebnis

Strukturiertes Objekt mit:
- Ressource-ID der neu erstellten Ressource
- Bestätigung mit Titel und weiteren Eigenschaften

## Fehlerabläufe

### E1: Fehlender Titel
2a. Das Pflichtfeld `title` fehlt oder ist leer.
3a. Der MCP-Server liefert eine Fehlermeldung: "Der Ressourcentitel darf nicht leer sein."

### E2: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

## Nachbedingungen

- Eine neue Ressource existiert im System.
