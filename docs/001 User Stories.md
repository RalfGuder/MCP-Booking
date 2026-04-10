# 001 User Stories

Übersicht aller User Stories des MCP-Booking-Projekts. Jede User Story ist mit einem GitHub Issue verknüpft und beschreibt eine Anforderung an den MCP-Server für die WP Booking Calendar API des Kulturvereins Milower Land e.V.

## User Stories

| ID | Titel | Issue | Datei | Beschreibung |
|----|-------|-------|-------|--------------|
| US-001 | MCP-Server | [#1](https://github.com/RalfGuder/MCP-Booking/issues/1) | [US-001-mcp-server.md](001%20User%20Stories/US-001-mcp-server.md) | MCP-Server, der die Booking API als Tools für KI-Assistenten bereitstellt |
| US-002 | Architektur | [#2](https://github.com/RalfGuder/MCP-Booking/issues/2) | [US-002-architektur.md](001%20User%20Stories/US-002-architektur.md) | Clean Architecture mit 4 Schichten (Domain, Application, Infrastructure, Presentation) |
| US-003 | Neue Projektmappe | [#3](https://github.com/RalfGuder/MCP-Booking/issues/3) | [US-003-neue-projektmappe.md](001%20User%20Stories/US-003-neue-projektmappe.md) | Visual Studio Solution mit src/ und tests/ Projekten |
| US-004 | Programmiersprache | [#4](https://github.com/RalfGuder/MCP-Booking/issues/4) | [US-004-programmiersprache.md](001%20User%20Stories/US-004-programmiersprache.md) | C# mit SDK-Style-Projektdateien und modernen .NET-Konventionen |
| US-005 | Softwaredesign | [#5](https://github.com/RalfGuder/MCP-Booking/issues/5) | [US-005-softwaredesign.md](001%20User%20Stories/US-005-softwaredesign.md) | Test-Driven Development (TDD) mit xUnit |

## Endpunkt-Tools (US-001 Sub-Stories)

### Bookings

| ID | Tool | Issue | Datei | Endpoint |
|----|------|-------|-------|----------|
| US-006 | `list_bookings` | [#6](https://github.com/RalfGuder/MCP-Booking/issues/6) | [US-006-list-bookings.md](001%20User%20Stories/US-006-list-bookings.md) | `GET /bookings` |
| US-007 | `create_booking` | [#7](https://github.com/RalfGuder/MCP-Booking/issues/7) | [US-007-create-booking.md](001%20User%20Stories/US-007-create-booking.md) | `POST /bookings` |
| US-008 | `get_booking` | [#8](https://github.com/RalfGuder/MCP-Booking/issues/8) | [US-008-get-booking.md](001%20User%20Stories/US-008-get-booking.md) | `GET /bookings/{id}` |
| US-009 | `update_booking` | [#9](https://github.com/RalfGuder/MCP-Booking/issues/9) | [US-009-update-booking.md](001%20User%20Stories/US-009-update-booking.md) | `PUT /bookings/{id}` |
| US-010 | `delete_booking` | [#10](https://github.com/RalfGuder/MCP-Booking/issues/10) | [US-010-delete-booking.md](001%20User%20Stories/US-010-delete-booking.md) | `DELETE /bookings/{id}` |
| US-011 | `approve_booking` | [#11](https://github.com/RalfGuder/MCP-Booking/issues/11) | [US-011-approve-booking.md](001%20User%20Stories/US-011-approve-booking.md) | `POST /bookings/{id}/approve` |
| US-012 | `set_booking_pending` | [#12](https://github.com/RalfGuder/MCP-Booking/issues/12) | [US-012-set-booking-pending.md](001%20User%20Stories/US-012-set-booking-pending.md) | `POST /bookings/{id}/pending` |
| US-013 | `update_booking_note` | [#13](https://github.com/RalfGuder/MCP-Booking/issues/13) | [US-013-update-booking-note.md](001%20User%20Stories/US-013-update-booking-note.md) | `PUT /bookings/{id}/note` |

### Resources

| ID | Tool | Issue | Datei | Endpoint |
|----|------|-------|-------|----------|
| US-014 | `list_resources` | [#14](https://github.com/RalfGuder/MCP-Booking/issues/14) | [US-014-list-resources.md](001%20User%20Stories/US-014-list-resources.md) | `GET /resources` |
| US-015 | `create_resource` | [#15](https://github.com/RalfGuder/MCP-Booking/issues/15) | [US-015-create-resource.md](001%20User%20Stories/US-015-create-resource.md) | `POST /resources` |
| US-016 | `get_resource` | [#16](https://github.com/RalfGuder/MCP-Booking/issues/16) | [US-016-get-resource.md](001%20User%20Stories/US-016-get-resource.md) | `GET /resources/{id}` |
| US-017 | `update_resource` | [#17](https://github.com/RalfGuder/MCP-Booking/issues/17) | [US-017-update-resource.md](001%20User%20Stories/US-017-update-resource.md) | `PUT /resources/{id}` |
| US-018 | `delete_resource` | [#18](https://github.com/RalfGuder/MCP-Booking/issues/18) | [US-018-delete-resource.md](001%20User%20Stories/US-018-delete-resource.md) | `DELETE /resources/{id}` |

### Availability

| ID | Tool | Issue | Datei | Endpoint |
|----|------|-------|-------|----------|
| US-019 | `get_availability` | [#19](https://github.com/RalfGuder/MCP-Booking/issues/19) | [US-019-get-availability.md](001%20User%20Stories/US-019-get-availability.md) | `GET /availability/{resource_id}` |
| US-020 | `update_availability` | [#20](https://github.com/RalfGuder/MCP-Booking/issues/20) | [US-020-update-availability.md](001%20User%20Stories/US-020-update-availability.md) | `PUT /availability/{resource_id}` |
| US-021 | `get_availability_dates` | [#21](https://github.com/RalfGuder/MCP-Booking/issues/21) | [US-021-get-availability-dates.md](001%20User%20Stories/US-021-get-availability-dates.md) | `GET /availability/{resource_id}/dates` |

### Forms

| ID | Tool | Issue | Datei | Endpoint |
|----|------|-------|-------|----------|
| US-022 | `list_forms` | [#22](https://github.com/RalfGuder/MCP-Booking/issues/22) | [US-022-list-forms.md](001%20User%20Stories/US-022-list-forms.md) | `GET /forms` |
| US-023 | `create_form` | [#23](https://github.com/RalfGuder/MCP-Booking/issues/23) | [US-023-create-form.md](001%20User%20Stories/US-023-create-form.md) | `POST /forms` |
| US-024 | `get_form` | [#24](https://github.com/RalfGuder/MCP-Booking/issues/24) | [US-024-get-form.md](001%20User%20Stories/US-024-get-form.md) | `GET /forms/{id}` |
| US-025 | `update_form` | [#25](https://github.com/RalfGuder/MCP-Booking/issues/25) | [US-025-update-form.md](001%20User%20Stories/US-025-update-form.md) | `PUT /forms/{id}` |
| US-026 | `delete_form` | [#26](https://github.com/RalfGuder/MCP-Booking/issues/26) | [US-026-delete-form.md](001%20User%20Stories/US-026-delete-form.md) | `DELETE /forms/{id}` |

### Settings

| ID | Tool | Issue | Datei | Endpoint |
|----|------|-------|-------|----------|
| US-027 | `get_settings` | [#27](https://github.com/RalfGuder/MCP-Booking/issues/27) | [US-027-get-settings.md](001%20User%20Stories/US-027-get-settings.md) | `GET /settings` |
| US-028 | `update_settings` | [#28](https://github.com/RalfGuder/MCP-Booking/issues/28) | [US-028-update-settings.md](001%20User%20Stories/US-028-update-settings.md) | `PUT /settings` |
| US-029 | `get_settings_group` | [#29](https://github.com/RalfGuder/MCP-Booking/issues/29) | [US-029-get-settings-group.md](001%20User%20Stories/US-029-get-settings-group.md) | `GET /settings/{group}` |

## Abhängigkeiten

```
US-001 MCP-Server
  ├── US-006–US-013 (Booking-Tools)
  ├── US-014–US-018 (Resource-Tools)
  ├── US-019–US-021 (Availability-Tools)
  ├── US-022–US-026 (Form-Tools)
  ├── US-027–US-029 (Settings-Tools)
  ├── US-002 Architektur
  │     ├── US-003 Neue Projektmappe
  │     └── US-004 Programmiersprache
  ├── US-003 Neue Projektmappe
  │     ├── US-002 Architektur
  │     └── US-004 Programmiersprache
  ├── US-004 Programmiersprache
  │     └── US-003 Neue Projektmappe
  └── US-005 Softwaredesign
        ├── US-003 Neue Projektmappe
        └── US-004 Programmiersprache
```

## Umsetzungsreihenfolge (empfohlen)

1. **US-004** Programmiersprache — Technologieentscheidung als Grundlage
2. **US-002** Architektur — Schichtenstruktur definieren
3. **US-003** Neue Projektmappe — Solution und Projekte anlegen
4. **US-005** Softwaredesign — TDD-Workflow etablieren
5. **US-001** MCP-Server — Kernfunktionalität implementieren
   - 5a. Booking-Tools (US-006–US-013)
   - 5b. Resource-Tools (US-014–US-018)
   - 5c. Availability-Tools (US-019–US-021)
   - 5d. Form-Tools (US-022–US-026)
   - 5e. Settings-Tools (US-027–US-029)

## Zusammenfassung

Das Projekt hat das Ziel, einen **MCP-Server** (Model Context Protocol) in **C#** zu entwickeln, der die REST API des WordPress-Plugins *WP Booking Calendar* kapselt. Dadurch können KI-Assistenten wie Claude Buchungen, Ressourcen, Verfügbarkeiten, Formulare und Einstellungen des Kulturvereins Milower Land e.V. verwalten.

Die Architektur folgt **Clean Architecture** mit vier Schichten, die als separate Projekte in einer Visual Studio Solution organisiert werden. Die Entwicklung erfolgt nach dem **TDD**-Ansatz (Red-Green-Refactor) mit xUnit als Test-Framework.

Insgesamt umfasst der MCP-Server **24 Tools** (8 Booking, 5 Resource, 3 Availability, 5 Form, 3 Settings), die jeweils einen API-Endpunkt kapseln.
