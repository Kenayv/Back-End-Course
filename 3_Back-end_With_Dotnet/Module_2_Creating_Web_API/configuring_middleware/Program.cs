var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging((o) => { });

var app = builder.Build();

app.Use(
    async (context, next) =>
    {
        Console.WriteLine("Logic before"); //do something

        await next.Invoke(); //invoke the routing logic, app.mapGet();

        Console.WriteLine("Logic After"); //do something after
    }
);

//app.UseHttpLogging();

app.MapGet("/", () => "Hello World!");
app.MapGet("/hello", () => "this is the hello route");

app.Run();
