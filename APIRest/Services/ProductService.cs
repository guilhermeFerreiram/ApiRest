using APIRest.DTOs;
using APIRest.Interfaces;

namespace APIRest.Services;

public class ProductService : IProductService
{
    public async Task<ProductDto> Create(string name, double value)
    {
        return await Task.FromResult(new ProductDto { Id = 1, Name = "Computador", Value = 2000.00 });
    }

    public async Task Delete(int id)
    {
        var product = await Get(id);

        Console.WriteLine($"Produto deletado: {product.Name}");
    }

    public async Task<ProductDto> Get(int id)
    {
        return await Task.FromResult(new ProductDto { Id = id, Name = "Computador", Value = 2000.00 });
    }

    public async Task<List<ProductDto>> GetByFilters(List<int> ids, List<string> names)
    {
        var products = new List<ProductDto>() { 
            new() { Id = 1, Name = "Computador", Value = 2000.00 },
            new() { Id = 2, Name = "Cadeira", Value = 300.00 },
            new() { Id = 2, Name = "Mesa", Value = 500.00 },
        };
        return await Task.FromResult(products);
    }

    public async Task Update(int id, string? name, double? value)
    {
        var product = await Get(id);

        product.Name = string.IsNullOrEmpty(name) ? product.Name : name;
        product.Value = value ?? product.Value;

        Console.WriteLine($"Produto atualizado: {product.Name}");
    }
}
