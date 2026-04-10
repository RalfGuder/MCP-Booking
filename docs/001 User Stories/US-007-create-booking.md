# User Story: Tool create_booking

**Issue:** [#7 — US-007 Tool: create_booking](https://github.com/RalfGuder/MCP-Booking/issues/7)
**Parent:** [#1 — US-001 MCP-Server](https://github.com/RalfGuder/MCP-Booking/issues/1)

## Endpoint

`POST /bookings`

## Story

**Als** Nutzer eines KI-Assistenten,
**moechte ich** eine neue Buchung anlegen koennen,
**damit** ich Buchungen direkt ueber den KI-Assistenten erstellen kann.

## Parameter

| Name | Typ | Pflicht | Beschreibung |
|------|-----|---------|--------------|
| booking_type | integer | ja | Ressource-/Buchungstyp-ID |
| form_data | object | ja | Ausgefuellte Formularfelder |
| dates | array[date-time] | ja | Array der gewuenschten Buchungsdaten |

## Akzeptanzkriterien

- [ ] MCP-Tool `create_booking` ist registriert und aufrufbar
- [ ] Alle Pflichtfelder werden validiert
- [ ] Erfolgreiche Erstellung liefert Bestaetigung mit Buchungs-ID
- [ ] Validierungsfehler (fehlende Pflichtfelder) werden verstaendlich weitergegeben
- [ ] API-Fehler werden als verstaendliche Fehlermeldung zurueckgegeben
- [ ] Unit-Test: Erfolgreiche Buchungserstellung
- [ ] Unit-Test: Fehlende Pflichtfelder
- [ ] Unit-Test: API-Fehlerbehandlung
