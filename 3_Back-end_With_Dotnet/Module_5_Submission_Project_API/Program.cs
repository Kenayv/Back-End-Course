var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton(DbControllerMock.getInstance());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/home/error");
}
else
{
    app.UseDeveloperExceptionPage();
}

app.MapControllers();
app.Map("/", () => "Simple API");
app.Map("/home/error", () => "An unexpected error has occurred. Try again later");

app.Run();
