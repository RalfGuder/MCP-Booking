---
id: "027"
title: Nutzermeldungen lokalisieren
tags:
  - UseCase
  - Querschnitt
status: open
---

# UC-027: Nutzermeldungen lokalisieren

**User Story:** [US-032 Lokalisierung](../001%20User%20Stories/US-032-lokalisierung.md) | [Issue #32](https://github.com/RalfGuder/MCP-Booking/issues/32)

## Akteure

- **Primär:** Nutzer eines KI-Assistenten
- **Sekundär:** MCP-Server

## Vorbedingungen

1. Der MCP-Server ist gestartet.
2. Resource-Dateien (Messages.resx) sind vorhanden.

## Auslöser

Der MCP-Server muss eine Fehlermeldung oder Bestätigung an den Nutzer ausgeben.

## Hauptablauf

1. Ein Tool-Aufruf erzeugt eine Nutzer- oder Fehlermeldung.
2. Der Server liest die Meldung aus der Resource-Datei (`Messages.resx`).
3. Die Meldung wird in der Sprache des aktuellen Thread-Contexts zurückgegeben.
4. Falls keine lokalisierte Version vorhanden ist, wird die Standardsprache (Deutsch) verwendet.

## Alternative Abläufe

### A1: Englische Meldung
3a. Der KI-Assistent hat `en` als Sprache konfiguriert.
4a. Die Meldung wird aus `Messages.en.resx` gelesen.

## Nachbedingungen

- Die Meldung ist in der konfigurierten Sprache ausgegeben.

## Test Case

- [TC-027](../004%20Test%20Cases/TC-027-lokalisierung.md)
