using System.ComponentModel;
using System.Net;
using System.Text.Json;
using McpBooking.Application.UseCases;
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

    [McpServerTool(Name = "list_resources"), Description("Listet alle buchbaren Ressourcen auf.")]
    public async Task<string> ExecuteAsync(
        [Description("Seite (Standard: 1)")] int page = 1,
        [Description("Einträge pro Seite (Standard: 20, Max: 100)")] int perPage = 20,
        CancellationToken ct = default)
    {
        if (page < 1) return "Fehler: page muss >= 1 sein.";
        if (perPage < 1 || perPage > 100) return "Fehler: per_page muss zwischen 1 und 100 liegen.";

        try
        {
            var resources = await _useCase.ExecuteAsync(page, perPage, ct);
            return JsonSerializer.Serialize(resources, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            });
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            return "Fehler: Authentifizierung fehlgeschlagen. Bitte API-Zugangsdaten prüfen.";
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Forbidden)
        {
            return "Fehler: Keine Berechtigung für diese Aktion.";
        }
        catch (HttpRequestException)
        {
            return "Fehler: Die Booking API ist nicht erreichbar.";
        }
    }
}
