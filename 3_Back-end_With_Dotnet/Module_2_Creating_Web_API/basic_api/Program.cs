var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers(); // To podłącza wszystkie kontrolery automatycznie

app.MapGet("/downloads", () => "downloads text"); // Minimal API możesz mieszać

app.Run();
