using NSwag;
using NSwag.CodeGeneration.CSharp;

public class ClientGenerator
{
    public async Task GenerateClient()
    {
        using var httpClient = new HttpClient();
        var swaggerJson = await httpClient.GetStringAsync(
            "http://localhost:5000/swagger/v1/swagger.json"
        );
        var document = await OpenApiDocument.FromJsonAsync(swaggerJson);

        var settings = new CSharpClientGeneratorSettings
        {
            ClassName = "CustomApiClient",
            CSharpGeneratorSettings = { Namespace = "SwaggerApiClientTest" },
        };

        var generator = new CSharpClientGenerator(document, settings);
        var code = generator.GenerateFile();

        await File.WriteAllTextAsync("GeneratedApiClient.cs", code);
    }
}
