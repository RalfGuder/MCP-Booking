# User Story: MCP-Server fuer die Booking API

**Issue:** [#1 — MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Story

**Als** Nutzer eines KI-Assistenten (z.B. Claude),
**moechte ich** einen MCP-Server (Model Context Protocol), der die WP Booking Calendar REST API des Kulturvereins Milower Land e.V. kapselt,
**damit** ich Buchungen, Ressourcen, Verfuegbarkeiten, Formulare und Einstellungen ueber natuerlichsprachliche Anfragen verwalten kann.

## Hintergrund

Der Kulturverein Milower Land e.V. betreibt unter `https://kv-milowerland.de` eine WordPress-Website mit dem Plugin *WP Booking Calendar*. Dieses Plugin stellt eine REST API unter `/wp-json/wpbc/v1` bereit. Die vollstaendige API-Spezifikation liegt im Repository unter [`docs/booking-api-v1.yaml`](../booking-api-v1.yaml) (OpenAPI 3.0.3).

Ein MCP-Server macht diese API als *Tools* fuer KI-Assistenten verfuegbar, sodass Buchungsvorgaenge direkt aus einem Chat-Interface heraus gesteuert werden koennen.

## Akzeptanzkriterien

1. **MCP-Protokoll:** Der Server implementiert das [Model Context Protocol](https://modelcontextprotocol.io/) und ist ueber `stdio`-Transport nutzbar.
2. **Booking-Tools:**
   - `list_bookings` — Buchungen auflisten (mit Filter: Status, Ressource, Datumsbereich, Suche, Paginierung)
   - `get_booking` — Einzelne Buchung abrufen
   - `create_booking` — Neue Buchung anlegen (booking_type, form_data, dates)
   - `update_booking` — Buchung aktualisieren
   - `delete_booking` — Buchung loeschen
   - `approve_booking` — Buchung genehmigen
   - `set_booking_pending` — Buchung auf "ausstehend" setzen
   - `update_booking_note` — Notiz an Buchung anfuegen
3. **Resource-Tools:**
   - `list_resources` — Ressourcen auflisten (Paginierung)
   - `get_resource` — Einzelne Ressource abrufen
   - `create_resource` — Ressource anlegen (title, cost, visitors)
   - `update_resource` — Ressource aktualisieren
   - `delete_resource` — Ressource loeschen
4. **Availability-Tools:**
   - `get_availability` — Verfuegbarkeit fuer eine Ressource abrufen (Datumsbereich, prop_name)
   - `update_availability` — Verfuegbarkeit aktualisieren
   - `get_availability_dates` — Verfuegbare Daten abrufen
5. **Form-Tools:**
   - `list_forms` — Formulare auflisten (Paginierung)
   - `get_form` — Einzelnes Formular abrufen
   - `create_form` — Formular anlegen (title, structure_json, etc.)
   - `update_form` — Formular aktualisieren
   - `delete_form` — Formular loeschen
6. **Settings-Tools:**
   - `get_settings` — Alle Einstellungen abrufen
   - `update_settings` — Einstellungen aktualisieren (calendar, booking, ui, confirmation)
   - `get_settings_group` — Einstellungen einer Gruppe abrufen
7. **Authentifizierung:** Der Server unterstuetzt die Authentifizierung gegenueber der WordPress-API (z.B. Application Passwords oder API-Key per Konfiguration).
8. **Konfiguration:** API-Basis-URL und Zugangsdaten sind ueber Umgebungsvariablen oder eine Konfigurationsdatei einstellbar.
9. **Fehlerbehandlung:** API-Fehler (4xx, 5xx) werden als verstaendliche Tool-Fehlermeldungen an den KI-Assistenten weitergegeben.

## Technische Hinweise

- Die API-Basis-URL ist: `https://kv-milowerland.de/wp-json/wpbc/v1`
- Die vollstaendige OpenAPI-Spezifikation: [`docs/booking-api-v1.yaml`](../booking-api-v1.yaml)
- Programmiersprache: C# (siehe [Issue #4](https://github.com/RalfGuder/MCP-Booking/issues/4))
- Architektur: Clean Architecture (siehe [Issue #2](https://github.com/RalfGuder/MCP-Booking/issues/2))
- Entwicklungsmethodik: TDD (siehe [Issue #5](https://github.com/RalfGuder/MCP-Booking/issues/5))
- Projektstruktur: Visual Studio Solution, SDK-Style (siehe [Issue #3](https://github.com/RalfGuder/MCP-Booking/issues/3))

## Abhaengigkeiten

- Issue #2 (Architektur) — definiert die Schichtenstruktur
- Issue #3 (Neue Projektmappe) — Projektmappe muss zuerst angelegt werden
- Issue #4 (Programmiersprache) — Technologieentscheidung C# / SDK-Style
- Issue #5 (Softwaredesign) — TDD-Ansatz fuer die Implementierung
