using CRUDWithDapper.Api.Contracts;
using CRUDWithDapper.Api.Models;

namespace CRUDWithDapper.Api.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<Product> AddProductAsync(ProductRequest product);
    Task UpdateProductAsync(int id ,ProductRequest product);
    Task DeleteProductAsync(int id);
}
