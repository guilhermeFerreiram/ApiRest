using APIRest.Data;
using APIRest.DTOs;
using APIRest.Exceptions;
using APIRest.Interfaces;
using APIRest.Models;
using Microsoft.EntityFrameworkCore;

namespace APIRest.Services;

public class ProductService(AppDbContext context) : IProductService
{
    public async Task<ProductDto> Create(string name, double value)
    {
        if (string.IsNullOrEmpty(name) || value < 0)
            throw new BusinessRuleException("Os dados fornecidos não são válidos para criar um novo produto");

        var product = await context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name && x.DeletedAt == null);

        if (product is not null)
            throw new DuplicateRegisterException("Já existe um produto com esse nome");

        var productModel = new Product()
        {
            Name = name,
            Value = value,
            CreatedAt = DateTime.UtcNow, 
        };

        await context.Products.AddAsync(productModel);
        await context.SaveChangesAsync();

        var productDto = new ProductDto()
        {
            Id = productModel.Id,
            Name = productModel.Name,
            Value = productModel.Value,
            CreatedAt = productModel.CreatedAt
        };

        return productDto;
    }

    public async Task Delete(int id)
    {
        var product = await context.Products
            .FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null);

        if (product is null)
            throw new NotFoundException($"Produto de id {id} não encontrado");

        product.DeletedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
    }

    public async Task<ProductDto> Get(int id)
    {
        var product = await context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null);

        if (product is null)
            throw new NotFoundException($"Produto de id {id} não encontrado");

        var productDto = new ProductDto()
        {
            Id = product.Id,
            Name = product.Name,
            Value = product.Value,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };

        return productDto;
    }

    public async Task<List<ProductDto>> GetByFilters(List<int> ids, List<string> names)
    {
        var query = context.Products.AsQueryable();

        query = query.Where(x => x.DeletedAt == null);

        if (ids is not null && ids.Count != 0)
            query = query.Where(x => ids.Contains(x.Id));

        if (names is not null && names.Count != 0)
            query = query.Where(x => names.Contains(x.Name));

        var products = await query.ToListAsync();

        var productsDtos = products.Select(x => new ProductDto()
        {
            Id = x.Id,
            Name = x.Name,
            Value = x.Value,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt
        }).ToList();

        return productsDtos;
    }

    public async Task Update(int id, string? name, double? value)
    {
        var product = await context.Products
            .FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null);

        if (product is null)
            throw new NotFoundException($"Produto de id {id} não encontrado");

        product.Name = string.IsNullOrEmpty(name) ? product.Name : name;
        product.Value = value ?? product.Value;
        product.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
    }
}
