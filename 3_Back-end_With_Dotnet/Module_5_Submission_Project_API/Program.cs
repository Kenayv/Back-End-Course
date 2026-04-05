var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton(DbControllerMock.getInstance());

var app = builder.Build();
app.MapControllers();

app.Map("/", () => "Simple API");
app.Run();
