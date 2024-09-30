using CRUDWithDapper.Api.Contracts;
using CRUDWithDapper.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CRUDWithDapper.Api.Models;


namespace CRUDWithDapper.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;


    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        var products = await _productService.GetProductsAsync();

        return Ok(products);
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);

        return product == null ? NotFound() : Ok(product);
    }


    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(ProductRequest request)
    {
        var createdProduct = await _productService.AddProductAsync(request);

        return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductRequest request)
    {
        await _productService.UpdateProductAsync(id, request);
        return NoContent();
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteProductAsync(id);

        return NoContent();
    }

}
