using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository(StoreContext context) : IProductRepository
{
    private readonly StoreContext context = context;

    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort)
    {
        var query = context.Products.AsQueryable();
        if (!string.IsNullOrWhiteSpace(brand))
        {
            query = query.Where(p => p.Brand == brand);
        }
        if (!string.IsNullOrWhiteSpace(type))
        {
            query = query.Where(p => p.Type == type);
        }
        query = sort switch
        {
            "priceAsc" => query.OrderBy(p => p.Price),
            "priceDesc" => query.OrderByDescending(p => p.Price),
            _ => query.OrderBy(p => p.Name)
        };

        return await query.ToListAsync();
    }

    public async Task<Product> GetProductAsync(int id)
    {
        return await context.Products.FindAsync(id);
    }

    public void AddProduct(Product product)
    {
        context.Products.Add(product);
    }

    public void UpdateProduct(int id, Product product)
    {
        context.Entry(product).State = EntityState.Modified;
    }

    public void DeleteProduct(Product product)
    {

        context.Products.Remove(product);

    }

    public bool ProductExists(int id)
    {
        return context.Products.Any(e => e.Id == id);
    }



    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;

    }

    public async Task<IReadOnlyList<string>> GetBrandsAsync()
    {
        return await context.Products
        .Select(x => x.Brand)
        .Distinct()
        .ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
        return await context.Products
        .Select(x => x.Type)
          .Distinct()
          .ToListAsync();
    }
}