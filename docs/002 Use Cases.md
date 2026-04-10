# 002 Use Cases

Übersicht aller Use Cases des MCP-Booking-Projekts. Jeder Use Case beschreibt einen konkreten Ablauf für ein MCP-Tool und ist mit der zugehörigen User Story verknüpft.

## Use Cases

### Bookings

| ID | Titel | User Story | Datei | Endpoint |
|----|-------|------------|-------|----------|
| UC-001 | Buchungen auflisten | [US-006](001%20User%20Stories/US-006-list-bookings.md) | [UC-001](002%20Use%20Cases/UC-001-list-bookings.md) | `GET /bookings` |
| UC-002 | Buchung anlegen | [US-007](001%20User%20Stories/US-007-create-booking.md) | [UC-002](002%20Use%20Cases/UC-002-create-booking.md) | `POST /bookings` |
| UC-003 | Buchung abrufen | [US-008](001%20User%20Stories/US-008-get-booking.md) | [UC-003](002%20Use%20Cases/UC-003-get-booking.md) | `GET /bookings/{id}` |
| UC-004 | Buchung aktualisieren | [US-009](001%20User%20Stories/US-009-update-booking.md) | [UC-004](002%20Use%20Cases/UC-004-update-booking.md) | `PUT /bookings/{id}` |
| UC-005 | Buchung löschen | [US-010](001%20User%20Stories/US-010-delete-booking.md) | [UC-005](002%20Use%20Cases/UC-005-delete-booking.md) | `DELETE /bookings/{id}` |
| UC-006 | Buchung genehmigen | [US-011](001%20User%20Stories/US-011-approve-booking.md) | [UC-006](002%20Use%20Cases/UC-006-approve-booking.md) | `POST /bookings/{id}/approve` |
| UC-007 | Buchung auf ausstehend setzen | [US-012](001%20User%20Stories/US-012-set-booking-pending.md) | [UC-007](002%20Use%20Cases/UC-007-set-booking-pending.md) | `POST /bookings/{id}/pending` |
| UC-008 | Notiz an Buchung anfügen | [US-013](001%20User%20Stories/US-013-update-booking-note.md) | [UC-008](002%20Use%20Cases/UC-008-update-booking-note.md) | `PUT /bookings/{id}/note` |

### Resources

| ID | Titel | User Story | Datei | Endpoint |
|----|-------|------------|-------|----------|
| UC-009 | Ressourcen auflisten | [US-014](001%20User%20Stories/US-014-list-resources.md) | [UC-009](002%20Use%20Cases/UC-009-list-resources.md) | `GET /resources` |
| UC-010 | Ressource anlegen | [US-015](001%20User%20Stories/US-015-create-resource.md) | [UC-010](002%20Use%20Cases/UC-010-create-resource.md) | `POST /resources` |
| UC-011 | Ressource abrufen | [US-016](001%20User%20Stories/US-016-get-resource.md) | [UC-011](002%20Use%20Cases/UC-011-get-resource.md) | `GET /resources/{id}` |
| UC-012 | Ressource aktualisieren | [US-017](001%20User%20Stories/US-017-update-resource.md) | [UC-012](002%20Use%20Cases/UC-012-update-resource.md) | `PUT /resources/{id}` |
| UC-013 | Ressource löschen | [US-018](001%20User%20Stories/US-018-delete-resource.md) | [UC-013](002%20Use%20Cases/UC-013-delete-resource.md) | `DELETE /resources/{id}` |

### Availability

| ID | Titel | User Story | Datei | Endpoint |
|----|-------|------------|-------|----------|
| UC-014 | Verfügbarkeit abrufen | [US-019](001%20User%20Stories/US-019-get-availability.md) | [UC-014](002%20Use%20Cases/UC-014-get-availability.md) | `GET /availability/{resource_id}` |
| UC-015 | Verfügbarkeit aktualisieren | [US-020](001%20User%20Stories/US-020-update-availability.md) | [UC-015](002%20Use%20Cases/UC-015-update-availability.md) | `PUT /availability/{resource_id}` |
| UC-016 | Verfügbare Daten abrufen | [US-021](001%20User%20Stories/US-021-get-availability-dates.md) | [UC-016](002%20Use%20Cases/UC-016-get-availability-dates.md) | `GET /availability/{resource_id}/dates` |

### Forms

| ID | Titel | User Story | Datei | Endpoint |
|----|-------|------------|-------|----------|
| UC-017 | Formulare auflisten | [US-022](001%20User%20Stories/US-022-list-forms.md) | [UC-017](002%20Use%20Cases/UC-017-list-forms.md) | `GET /forms` |
| UC-018 | Formular anlegen | [US-023](001%20User%20Stories/US-023-create-form.md) | [UC-018](002%20Use%20Cases/UC-018-create-form.md) | `POST /forms` |
| UC-019 | Formular abrufen | [US-024](001%20User%20Stories/US-024-get-form.md) | [UC-019](002%20Use%20Cases/UC-019-get-form.md) | `GET /forms/{id}` |
| UC-020 | Formular aktualisieren | [US-025](001%20User%20Stories/US-025-update-form.md) | [UC-020](002%20Use%20Cases/UC-020-update-form.md) | `PUT /forms/{id}` |
| UC-021 | Formular löschen | [US-026](001%20User%20Stories/US-026-delete-form.md) | [UC-021](002%20Use%20Cases/UC-021-delete-form.md) | `DELETE /forms/{id}` |

### Settings

| ID | Titel | User Story | Datei | Endpoint |
|----|-------|------------|-------|----------|
| UC-022 | Einstellungen auflisten | [US-027](001%20User%20Stories/US-027-get-settings.md) | [UC-022](002%20Use%20Cases/UC-022-list-settings.md) | `GET /settings` |
| UC-023 | Einstellungen aktualisieren | [US-028](001%20User%20Stories/US-028-update-settings.md) | [UC-023](002%20Use%20Cases/UC-023-update-settings.md) | `PUT /settings` |
| UC-024 | Einstellungsgruppe abrufen | [US-029](001%20User%20Stories/US-029-get-settings-group.md) | [UC-024](002%20Use%20Cases/UC-024-get-settings-group.md) | `GET /settings/{group}` |

## Zuordnung US → UC

| User Story | Use Case |
|------------|----------|
| US-006 | UC-001 |
| US-007 | UC-002 |
| US-008 | UC-003 |
| US-009 | UC-004 |
| US-010 | UC-005 |
| US-011 | UC-006 |
| US-012 | UC-007 |
| US-013 | UC-008 |
| US-014 | UC-009 |
| US-015 | UC-010 |
| US-016 | UC-011 |
| US-017 | UC-012 |
| US-018 | UC-013 |
| US-019 | UC-014 |
| US-020 | UC-015 |
| US-021 | UC-016 |
| US-022 | UC-017 |
| US-023 | UC-018 |
| US-024 | UC-019 |
| US-025 | UC-020 |
| US-026 | UC-021 |
| US-027 | UC-022 |
| US-028 | UC-023 |
| US-029 | UC-024 |

## Querschnittsanforderungen

| ID | Titel | User Story | Datei |
|----|-------|------------|-------|
| UC-025 | Dokumentationskommentare pflegen | [US-030](001%20User%20Stories/US-030-dokumentationskommentare.md) | [UC-025](002%20Use%20Cases/UC-025-dokumentationskommentare.md) |
| UC-026 | MSTest.Sdk-Style für Test-Projekte | [US-031](001%20User%20Stories/US-031-teststyle.md) | [UC-026](002%20Use%20Cases/UC-026-teststyle.md) |
| UC-027 | Nutzermeldungen lokalisieren | [US-032](001%20User%20Stories/US-032-lokalisierung.md) | [UC-027](002%20Use%20Cases/UC-027-lokalisierung.md) |
| UC-028 | Harte Zeichenfolgen Base64-codieren | [US-033](001%20User%20Stories/US-033-harte-zeichenfolgen.md) | [UC-028](002%20Use%20Cases/UC-028-harte-zeichenfolgen.md) |

## Use Case Struktur

Jeder Use Case folgt dieser Struktur:
- **Akteure** — Wer ist beteiligt (KI-Assistent, API)
- **Vorbedingungen** — Was muss vor der Ausführung gelten
- **Auslöser** — Beispielhafte Nutzeranfrage
- **Hauptablauf** — Nummerierte Schritte des Normalfalls
- **Parameter** — Eingabeparameter mit Typ und Validierung
- **Ergebnis** — Was wird zurückgeliefert
- **Alternative Abläufe** — Varianten des Hauptablaufs
- **Fehlerabläufe** — Fehlerszenarien (401, 403, 404, Validierung)
- **Nachbedingungen** — Systemzustand nach der Ausführung
