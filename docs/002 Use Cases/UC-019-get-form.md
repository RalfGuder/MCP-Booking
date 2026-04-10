---
id: "019"
title: Formular abrufen
tags:
  - UseCase
  - Forms
status: open
---

# UC-019: Formular abrufen

**User Story:** [US-024 Tool: get_form](../001%20User%20Stories/US-024-get-form.md) | [Issue #24](https://github.com/RalfGuder/MCP-Booking/issues/24)

## Akteure

- **Primaer:** KI-Assistent (z.B. Claude)
- **Sekundaer:** WP Booking Calendar REST API

## Vorbedingungen

1. Der MCP-Server ist gestartet und ueber stdio erreichbar.
2. Die API-Zugangsdaten sind korrekt konfiguriert.
3. Die Formular-ID ist bekannt.

## Ausloeser

Der Nutzer fragt nach Details eines Formulars (z.B. "Zeige mir die Struktur des Formulars 2").

## Hauptablauf

1. Der KI-Assistent ruft das MCP-Tool `get_form` mit der Formular-ID auf.
2. Der MCP-Server validiert den Parameter `id`.
3. Der MCP-Server sendet einen `GET /forms/{id}`-Request an die API.
4. Die API liefert die vollstaendigen Formulardetails.
5. Der MCP-Server transformiert die API-Antwort in ein strukturiertes Tool-Ergebnis.
6. Der KI-Assistent praesentiert die Formulardetails dem Nutzer.

## Parameter

| Name | Typ | Pflicht | Validierung |
|------|-----|---------|-------------|
| id | integer | ja | > 0 |

## Ergebnis

Strukturiertes Objekt mit:
- Formular-ID, Titel, Slug, Status
- Formularstruktur (structure_json)
- Formulareinstellungen (settings_json)

## Fehlerablaeufe

### E1: Formular nicht gefunden (404)
3a. Die API liefert 404 Not Found.
4a. Der MCP-Server liefert eine Fehlermeldung: "Formular mit ID [id] nicht gefunden."

### E2: Authentifizierungsfehler (401/403)
3a. Die API liefert 401 oder 403.
4a. Der MCP-Server liefert eine entsprechende Fehlermeldung.

## Nachbedingungen

- Keine Zustandsaenderung im System (lesender Zugriff).
