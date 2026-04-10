// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.ComponentModel;
using System.Net;
using System.Text.Json;
using McpBooking.Application.UseCases;
using McpBooking.Server.Properties;
using ModelContextProtocol.Server;

namespace McpBooking.Server.Tools;

/// <summary>
/// MCP tool that exposes the list_resources endpoint to MCP clients.
/// </summary>
[McpServerToolType]
public class ListResourcesTool
{
    private readonly ListResourcesUseCase _useCase;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListResourcesTool"/> class.
    /// </summary>
    /// <param name="useCase">The use case that retrieves the resource list.</param>
    public ListResourcesTool(ListResourcesUseCase useCase)
    {
        _useCase = useCase;
    }

    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    /// <summary>
    /// Lists all bookable resources from the WP Booking Calendar API, serialized as a JSON string.
    /// </summary>
    /// <param name="page">The page number (1-based, default: 1).</param>
    /// <param name="perPage">The number of items per page (1-100, default: 20).</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>
    /// A JSON-formatted string containing the list of resources,
    /// or a localized error message if the request fails.
    /// </returns>
    [McpServerTool(Name = "list_resources"), Description("Listet alle buchbaren Ressourcen auf.")]
    public async Task<string> ExecuteAsync(
        [Description("Seite (Standard: 1)")] int page = 1,
        [Description("Einträge pro Seite (Standard: 20, Max: 100)")] int perPage = 20,
        CancellationToken ct = default)
    {
        if (page < 1) return Messages.ErrorPageInvalid;
        if (perPage < 1 || perPage > 100) return Messages.ErrorPerPageInvalid;

        try
        {
            var resources = await _useCase.ExecuteAsync(page, perPage, ct);
            return JsonSerializer.Serialize(resources, s_jsonOptions);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            return Messages.ErrorAuthenticationFailed;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Forbidden)
        {
            return Messages.ErrorForbidden;
        }
        catch (HttpRequestException ex) when (ex.StatusCode >= HttpStatusCode.InternalServerError)
        {
            return Messages.ErrorServerError;
        }
        catch (HttpRequestException)
        {
            return Messages.ErrorApiUnreachable;
        }
    }
}
