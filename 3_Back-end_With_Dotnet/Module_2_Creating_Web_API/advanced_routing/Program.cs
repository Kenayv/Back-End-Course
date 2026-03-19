var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string getProductName(int id) =>
    id switch
    {
        0 => "cow",
        1 => "beef",
        2 => "milk",
        3 => "cheese",
        _ => throw new ArgumentException(),
    };

app.MapGet(
    "/users/{userId}/posts/{slug}",
    (int userId, string slug) =>
    {
        return $"user ID: {userId}, Post ID: {slug}";
    }
);

app.MapGet(
    "/Products/{id:int:min(0):max(3)}",
    (int id) =>
    {
        return getProductName(id);
    }
);

app.MapGet(
    "/report/{year?}",
    (int? year = 2026) =>
    {
        return $"Report for year {year}";
    }
);

app.MapGet(
    "/files/{*filePath}",
    (string filePath) =>
    {
        return filePath;
    }
);

app.MapGet(
    "/search",
    (string? q, int page = 1) =>
    {
        return $"Searching for {q} on page {page}";
    }
);

app.MapGet(
    "/store/{category}/{productId:int?}/{extraPath}",
    (string category, int? productId, string? extraPath, bool inStock = true) => { }
);
app.Run();
