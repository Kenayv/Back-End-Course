var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton(DbControllerMock.getInstance());
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});
builder.Services.AddAuthorization();

var app = builder.Build();

app.Use(
    async (context, next) =>
    {
        try
        {
            await next();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unhandled exception: {ex.Message}");

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var error = new { error = "Internal server error." };
            await context.Response.WriteAsJsonAsync(error);
        }
    }
);

app.Use(
    async (context, next) =>
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault();

        if (token != "secret-token" && context.Request.Path != "/")
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsJsonAsync(new { error = "Unauthorized." });
            return;
        }

        await next();
    }
);

app.Use(
    async (context, next) =>
    {
        Console.WriteLine($"Method: {context.Request.Method} Path: {context.Request.Path}");
        await next();
        Console.WriteLine($"Status: {context.Response.StatusCode}");
    }
);

app.UseHttpLogging();
app.UseAuthorization();
app.MapControllers();
app.Map("/", () => "Simple API");

app.Run();
