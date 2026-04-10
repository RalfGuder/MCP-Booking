---
id: "006"
title: Parametervalidierung
tags:
  - Requirement
  - Core
  - Funktional
priority: hoch
status: open
---

# REQ-006: Parametervalidierung

## Beschreibung

Der MCP-Server **muss** alle eingehenden Tool-Parameter validieren, bevor ein API-Request gesendet wird. Ungültige Parameter werden als verständliche Fehlermeldung zurückgegeben.

## Quelle

- Alle Use Cases (UC-001 bis UC-024) — Hauptablauf Schritt 2 „validiert die Parameter"
- UC-001, UC-003, UC-014 — Fehlerablauf E4: Ungültiger Parameter

## Akzeptanzkriterien

1. Pflichtfelder werden auf Vorhandensein geprüft.
2. Integer-Parameter werden auf gültigen Typ und Wertebereich geprüft (z.B. id > 0).
3. String-Parameter werden auf Nicht-Leerheit geprüft wo erforderlich.
4. Enum-Parameter werden gegen die erlaubten Werte geprüft (z.B. status ∈ {pending, approved, trash}).
5. Datumsparameter werden auf gültiges ISO-8601-Format geprüft.
6. date_to >= date_from wird geprüft wo beide Parameter vorhanden.
7. Validierungsfehler nennen den betroffenen Parameter und den erwarteten Wert/Typ.

## Testbarkeit

- Unit-Test je Validierungsregel: Gültiger Wert → kein Fehler, ungültiger Wert → Fehlermeldung.


## Verknüpfte Artefakte

- Design-Spec: [2026-04-10-us001-mcp-server-design.md](../005%20Super%20Powers/specs/2026-04-10-us001-mcp-server-design.md)
- Implementierungsplan: [2026-04-10-us001-mcp-server-phase1.md](../005%20Super%20Powers/plans/2026-04-10-us001-mcp-server-phase1.md)