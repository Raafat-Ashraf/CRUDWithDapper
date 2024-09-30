using CRUDWithDapper.Api.Contracts;
using CRUDWithDapper.Api.Interfaces;
using CRUDWithDapper.Api.Models;
using Dapper;
using System.Data;

namespace CRUDWithDapper.Api.Services;

public class ProductService(IDbConnection dbConnection) : IProductService
{
    private readonly IDbConnection _dbConnection = dbConnection;


    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        const string query = "select * from Products";
        return await _dbConnection.QueryAsync<Product>(query);
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        const string query = "select * from Products where Id = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<Product>(query, new { Id = id });
    }

    public async Task<Product> AddProductAsync(ProductRequest product)
    {
        const string query = "Insert into Products (Name, Price) values (@Name, @Price)";
        await _dbConnection.ExecuteAsync(query, product);

        var id = await GetLastProductIdAsync();
        return new Product() { Id = id, Name = product.Name, Price = product.Price };
    }

    public async Task UpdateProductAsync(int id, ProductRequest request)
    {
        const string query = "Update Products set Name = @Name, Price = @Price where Id = @Id";

        await _dbConnection.ExecuteAsync(query, new { Id = id, Name = request.Name, Price = request.Price });
    }

    public async Task DeleteProductAsync(int id)
    {
        const string query = "Delete from Products where Id = @Id";
        await _dbConnection.ExecuteAsync(query, new { Id = id });
    }


    public async Task<int> GetLastProductIdAsync()
    {
        const string query = "SELECT ISNULL(MAX(Id), 0) FROM Products";
        return await _dbConnection.ExecuteScalarAsync<int>(query);
    }
}
