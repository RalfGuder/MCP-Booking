---
id: "025"
title: Dokumentationskommentare pflegen
tags:
  - UseCase
  - Querschnitt
status: open
---

# UC-025: Dokumentationskommentare pflegen

**User Story:** [US-030 Dokumentationskommentare](../001%20User%20Stories/US-030-dokumentationskommentare.md) | [Issue #30](https://github.com/RalfGuder/MCP-Booking/issues/30)

## Akteure

- **Primär:** Entwickler

## Vorbedingungen

1. Das Projekt ist mit `dotnet build` kompilierbar.
2. Die Klassen und Member sind implementiert.

## Auslöser

Ein neuer oder geänderter öffentlicher Member wird zum Repository hinzugefügt.

## Hauptablauf

1. Der Entwickler erstellt oder ändert eine öffentliche Klasse, Methode oder Property.
2. Der Entwickler fügt einen XML-Dokumentationskommentar in Englisch hinzu (`///`).
3. Für Methoden werden `<summary>`, `<param>` und `<returns>` angegeben.
4. Für Klassen wird ein `<summary>` und ggf. `<remarks>` angegeben.
5. Der Dateikopf enthält Copyright- und Lizenzinformation.
6. `dotnet build` wird ausgeführt — keine CS1591-Warnungen.

## Nachbedingungen

- Alle öffentlichen Member haben englische XML-Dokumentationskommentare.
- `dotnet build` erzeugt keine Warnungen zu fehlenden Kommentaren.

## Test Case

- [TC-025](../004%20Test%20Cases/TC-025-dokumentationskommentare.md)
