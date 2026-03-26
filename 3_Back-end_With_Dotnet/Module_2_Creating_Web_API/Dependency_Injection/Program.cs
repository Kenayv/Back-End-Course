var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMyService, MyService>();

var app = builder.Build();

app.Use(
    async (context, next) =>
    {
        var myService = context.RequestServices.GetRequiredService<IMyService>();
        myService.logCreation("First middleware");
        await next.Invoke();
    }
);

app.Use(
    async (context, next) =>
    {
        var myService = context.RequestServices.GetRequiredService<IMyService>();
        myService.logCreation("Second middleware");
        await next.Invoke();
    }
);

app.MapGet(
    "/",
    (IMyService myService) =>
    {
        myService.logCreation("root");

        return Results.Ok("check console");
    }
);

app.Run();

public interface IMyService
{
    void logCreation(string msg);
}

public class MyService : IMyService
{
    private readonly int _serviceId;

    public MyService()
    {
        _serviceId = new Random().Next(100000, 999999);
    }

    public void logCreation(string message)
    {
        Console.WriteLine($"{message} - {_serviceId}");
    }
}
