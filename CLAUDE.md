# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

MCP-Booking is an MCP server that exposes the WP Booking Calendar REST API as tools for AI assistants. Licensed under MIT (Copyright 2026 RalfGuder).

## Architecture

- **Clean Architecture** with 4 layers: Domain, Application, Infrastructure, Server
- **Solution:** `MCP-Booking.slnx` (.NET 10, SDK-Style)
- **Central config:** `Directory.Build.props` (net10.0, Nullable, ImplicitUsings, GenerateDocumentationFile)
- **Test framework:** MSTest.Sdk, Moq, Shouldly
- **Localization:** `.resx` files (DE, EN, FR, ES) in Server project
- **Hard-coded strings:** Base64-encoded in `Properties/Strings.cs` (Infrastructure + Server)
- **XML documentation:** Required on all public members (CS1591 as error)

## Current State

Phase 1 is complete: MCP core (stdio transport, auth, config, error handling) + `list_resources` tool. Cross-cutting concerns (US-030–033) are implemented. Phase 2 (23 remaining endpoint tools) is pending.

## Build & Test

```bash
dotnet build
dotnet test
```

## Environment Variables

- `WPBC_API_URL` — optional, defaults to https://kv-milowerland.de/wp-json/wpbc/v1
- `WPBC_USERNAME` — required
- `WPBC_PASSWORD` — required

## Development Environment

- **Platform:** Windows (Visual Studio / .NET ecosystem)
- **Repository:** https://github.com/RalfGuder/MCP-Booking
- **Documentation:** Obsidian vault under `docs/` with numbered folders (User Stories, Use Cases, Requirements, Test Cases, Super Powers)
