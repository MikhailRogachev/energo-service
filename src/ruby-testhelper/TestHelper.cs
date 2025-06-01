using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace ruby_testhelper;

public static class TestHelper
{
    public static IOptions<TOption> GetOptionsFromAppSettings<TOption>() where TOption : class
    {
        FileInfo fi = new FileInfo("appsettings.json");
        var jsonOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        using StreamReader reader = new(fi.FullName);
        string body = reader.ReadToEnd();
        var jsonBody = JsonObject.Parse(body);
        var jsonBodyParameter = jsonBody![typeof(TOption).Name];

        var obj = JsonSerializer.Deserialize<TOption>(jsonBodyParameter!, jsonOptions)!;
        return Options.Create(obj!);
    }
}
