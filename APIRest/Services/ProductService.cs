using APIRest.DTOs;
using APIRest.Interfaces;

namespace APIRest.Services;

public class ProductService : IProductService
{
    public async Task<ProductDto> GetProduct(int id)
    {
        return await Task.FromResult(new ProductDto { Id = id });
    }
}
