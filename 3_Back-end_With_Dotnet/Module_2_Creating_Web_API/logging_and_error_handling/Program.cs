using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

app.Use(
    async (context, next) =>
    {
        try
        {
            await next();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Global error caught {e}");
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("An unexpected error occured try again later");
        }
    }
);

app.UseRouting();
app.MapControllers();

app.Map("/", () => "hello world");
app.Run();
