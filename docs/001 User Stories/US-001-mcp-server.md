# User Story: MCP-Server fuer die Booking API

**Issue:** [#1 — MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Story

**Als** Nutzer eines KI-Assistenten (z.B. Claude),
**moechte ich** einen MCP-Server (Model Context Protocol), der die WP Booking Calendar REST API des Kulturvereins Milower Land e.V. kapselt,
**damit** ich Buchungen, Ressourcen, Verfuegbarkeiten, Formulare und Einstellungen ueber natuerlichsprachliche Anfragen verwalten kann.

## Hintergrund

Der Kulturverein Milower Land e.V. betreibt unter `https://kv-milowerland.de` eine WordPress-Website mit dem Plugin *WP Booking Calendar*. Dieses Plugin stellt eine REST API unter `/wp-json/wpbc/v1` bereit. Die vollstaendige API-Spezifikation liegt im Repository unter [`docs/booking-api-v1.yaml`](../booking-api-v1.yaml) (OpenAPI 3.0.3).

Ein MCP-Server macht diese API als *Tools* fuer KI-Assistenten verfuegbar, sodass Buchungsvorgaenge direkt aus einem Chat-Interface heraus gesteuert werden koennen.

## Phase 1 — Kernfunktionen + erstes Tool

Zunaechst werden nur die Kerninfrastruktur und ein erstes Endpunkt-Tool umgesetzt. Die weiteren Tools folgen in Phase 2.

1. **MCP-Protokoll:** Der Server implementiert das [Model Context Protocol](https://modelcontextprotocol.io/) und ist ueber `stdio`-Transport nutzbar.
2. **Authentifizierung:** Der Server unterstuetzt die Authentifizierung gegenueber der WordPress-API (z.B. Application Passwords oder API-Key per Konfiguration).
3. **Konfiguration:** API-Basis-URL und Zugangsdaten sind ueber Umgebungsvariablen oder eine Konfigurationsdatei einstellbar.
4. **Fehlerbehandlung:** API-Fehler (4xx, 5xx) werden als verstaendliche Tool-Fehlermeldungen an den KI-Assistenten weitergegeben.
5. **Erstes Tool:** `list_resources` — Ressourcen auflisten (Paginierung) (siehe [US-014](https://github.com/RalfGuder/MCP-Booking/issues/14))

## Phase 2 — Weitere Endpunkt-Tools (spaeter)

Die folgenden Tools werden nach erfolgreicher Umsetzung von Phase 1 ergaenzt:

- **Booking-Tools** (US-006–US-013): list, create, get, update, delete, approve, pending, note
- **Resource-Tools** (US-015–US-018): create, get, update, delete
- **Availability-Tools** (US-019–US-021): get, update, get_dates
- **Form-Tools** (US-022–US-026): list, create, get, update, delete
- **Settings-Tools** (US-027–US-029): get, update, get_group

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
