namespace Core.Interfaces
{
    using Core.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort);
        Task<Product> GetProductAsync(int id);
        Task<IReadOnlyList<string>> GetBrandsAsync();
        Task<IReadOnlyList<string>> GetTypesAsync();
       void AddProduct(Product product);
        void UpdateProduct(int id, Product product);
        void DeleteProduct(Product product);
        bool ProductExists(int id);
        Task<bool> SaveChangesAsync();
    }
}