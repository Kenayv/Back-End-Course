using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SwaggerApiClientTest;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        app.UseSwagger();
        app.MapControllers();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Api v1"));

        await Task.Run(() => app.RunAsync());

        var httpClient = new HttpClient();
        var client = new CustomApiClient("http://localhost:5000", httpClient);

        var user = await client.UserAsync(15);

        Console.WriteLine($"Fetched {user.Name}");
    }
}
