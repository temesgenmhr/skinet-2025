using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts
    (string? brand, string? type,  string? sort)
    {
        var products = await repository.GetProductsAsync(brand, type, sort);
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repository.GetProductAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repository.AddProduct(product);
        if (await repository.SaveChangesAsync())
        {
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        return BadRequest("Problem while creating product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if (id != product.Id || !ProductExists(id))
        {
            return BadRequest();
        }
        repository.UpdateProduct(id, product);

       if(!await repository.SaveChangesAsync())
        {
            return BadRequest("Problem while updating product");
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repository.GetProductAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        repository.DeleteProduct(product);
        if (!await repository.SaveChangesAsync())
        {
            return BadRequest("Problem while deleting product");
        }
        return NoContent();
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var brands = await repository.GetBrandsAsync();
        return Ok(brands);
    }
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var types = await repository.GetTypesAsync();
        return Ok(types);
    }
    private bool ProductExists(int id)
    {
        return repository.ProductExists(id);
    }
}