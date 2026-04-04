var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/home/error");
}
else
{
    app.UseDeveloperExceptionPage();
}
app.UseAuthentication();
app.UseHttpLogging();
app.UseAuthorization();

app.Use(
    async (context, next) =>
    {
        Console.WriteLine($"request path: {context.Request.Path}");
        await next.Invoke();
        Console.WriteLine($"status: {context.Response.StatusCode}");
    }
);

app.Use(
    async (context, next) =>
    {
        var startTime = DateTime.UtcNow;
        await next.Invoke();
        var duration = DateTime.UtcNow - startTime;

        Console.WriteLine($"request time: {duration.TotalMilliseconds} ms");
    }
);

app.MapGet("/", () => "Hello World!");
app.MapGet("/skibidi_toilet", () => "pop yes yes yes");
app.MapGet(
    "/exception_test",
    () =>
    {
        throw new Exception("Error occured 123");
    }
);
app.Run();
