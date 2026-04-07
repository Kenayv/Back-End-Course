
using System.Text.Json;

class Program
{
    ApplicationDbContext context = new ApplicationDbContext();

    static void AddProduct(ApplicationDbContext context, string name, double price)
    {
        var product = new Product() {Name = name, Price = price};
        context.Add(product);
        context.SaveChanges();
    }

    static List<Product> GetProducts(ApplicationDbContext context)
    {
        return context.Products.ToList();
    }

    static Product GetProductById(ApplicationDbContext context, int id)
    {
        return context.Products.Where(e => e.ProductId == id).FirstOrDefault() ?? throw new Exception();
    }

    static void UpdateProduct(ApplicationDbContext context, int id, string newName)
    {
        var product = GetProductById(context, id);

        product.Name = newName;

        context.SaveChanges();
    }

    static void RemoveProduct(ApplicationDbContext context, int id)
    {
        var product = context.Products.Find(id);

        if(product==null) return;

        context.Products.Remove(product);
        context.SaveChanges();
    }

    static void Main(string[] args)
    {
        var context = new ApplicationDbContext();

        // Create
        AddProduct(context, "Laptop", 1200.99);

        // Read
        var allProducts = GetProducts(context);
        allProducts.ForEach(p => Console.WriteLine($"{p.ProductId}: {p.Name} - ${p.Price}"));


        var singleProduct = GetProductById(context, 4);
        Console.WriteLine($"Product Found: {singleProduct.Name} - ${singleProduct.Price}");

        // Update
        UpdateProduct(context, singleProduct.ProductId, "Asus Laptok");

        Console.WriteLine($"Product renamed: {singleProduct.Name} - ${singleProduct.Price}");

        // Delete
        RemoveProduct(context, singleProduct.ProductId);

    }
}