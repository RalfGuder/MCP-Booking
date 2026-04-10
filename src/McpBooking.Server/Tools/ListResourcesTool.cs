using System.ComponentModel;
using System.Net;
using System.Text.Json;
using McpBooking.Application.UseCases;
using McpBooking.Server.Properties;
using ModelContextProtocol.Server;

namespace McpBooking.Server.Tools;

[McpServerToolType]
public class ListResourcesTool
{
    private readonly ListResourcesUseCase _useCase;

    public ListResourcesTool(ListResourcesUseCase useCase)
    {
        _useCase = useCase;
    }

    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

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
