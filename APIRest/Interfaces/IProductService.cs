using APIRest.DTOs;

namespace APIRest.Interfaces;

public interface IProductService
{
    Task<ProductDto> Get(int id);
    Task<List<ProductDto>> GetAll(List<int> ids, List<string> names);
    Task<ProductDto> Create(string name, double value);
    Task Update(int id, string name, double value);
    Task Patch(int id, string? name, double? value);
    Task Delete(int id);
}
