using APIRest.DTOs;

namespace APIRest.Interfaces;

public interface IProductService
{
    Task<ProductDto> GetProduct(int id);
}
