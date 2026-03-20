using System.Collections.Concurrent;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

var blogs = new List<Blog>
{
    new Blog { Title = "My first post", Body = "this is my first post 123" },
    new Blog { Title = "My second post", Body = "this is my second post 123" },
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/blogs", () => blogs);

app.MapGet(
    "/blogs/{id}",
    (int id) =>
    {
        if (id < 0 || id >= blogs.Count())
        {
            return Results.NotFound();
        }
        return Results.Ok(blogs[id]);
    }
);

app.MapPost(
    "/blogs",
    (Blog blog) =>
    {
        blogs.Add(blog);
        return Results.Created($"/blogs/{blogs.Count() - 1}", blog);
    }
);

app.MapDelete(
    "/blogs/{id}",
    (int id) =>
    {
        if (id < 0 || id >= blogs.Count())
        {
            return Results.NotFound();
        }
        blogs.RemoveAt(id);
        return Results.NoContent();
    }
);

app.MapPut(
    "/blogs/{id}",
    (int id, Blog blog) =>
    {
        if (id < 0 || id >= blogs.Count())
        {
            return Results.NotFound();
        }
        blogs[id] = blog;
        return Results.Ok(blog);
    }
);

app.Run();

public class Blog
{
    public required string Title { get; set; }
    public required string Body { get; set; }
}
