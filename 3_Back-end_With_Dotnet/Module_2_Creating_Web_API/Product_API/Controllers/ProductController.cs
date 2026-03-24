using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("Api/Products")]
public class ProductController : ControllerBase
{
    private static List<Product> products = new List<Product>();

    [HttpGet]
    public ActionResult<List<Product>> GetAll() => products;

    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        return product != null ? Ok(product) : NotFound();
    }

    [HttpPost]
    public ActionResult<Product> Create(Product newProduct)
    {
        newProduct.Id = products.Count + 1;
        products.Add(newProduct);
        return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
    }

    [HttpPut("{id}")]
    public ActionResult<Product> Update(int id, Product newProduct)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();

        product.Name = newProduct.Name;
        product.Description = newProduct.Description;
        product.Price = newProduct.Price;

        return Ok(product);
    }

    [HttpDelete("{id}")]
    public ActionResult<Product> Delete(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();

        products.Remove(product);
        return NoContent();
    }
}
