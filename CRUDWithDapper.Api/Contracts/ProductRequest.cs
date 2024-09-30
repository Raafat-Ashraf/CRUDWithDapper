namespace CRUDWithDapper.Api.Contracts;

public class ProductRequest
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
}
