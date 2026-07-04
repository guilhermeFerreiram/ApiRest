using APIRest.Data;
using APIRest.DTOs;
using APIRest.Exceptions;
using APIRest.Interfaces;
using APIRest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace APIRest.Services;

public class ProductService(
    AppDbContext context,
    IMemoryCache memoryCache
) : IProductService
{
    private const string productCacheKey = "product-";

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

        var x = await context.Products.AddAsync(productModel);
        await context.SaveChangesAsync();

        var productDto = new ProductDto()
        {
            Id = x.Entity.Id,
            Name = x.Entity.Name,
            Value = x.Entity.Value,
            CreatedAt = x.Entity.CreatedAt
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

        memoryCache.Remove($"{productCacheKey}{id}");
    }

    public async Task<ProductDto> Get(int id)
    {
        var cacheKey = $"{productCacheKey}{id}";

        var foundInCache = memoryCache.TryGetValue(cacheKey, out Product? product);

        if (!foundInCache || product is null)
        {
            product = await context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null);

            if (product is null)
                throw new NotFoundException($"Produto de id {id} não encontrado");

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                .SetSlidingExpiration(TimeSpan.FromMinutes(2));

            memoryCache.Set(cacheKey, product, cacheOptions);
        }

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

    public async Task<List<ProductDto>> GetAll(List<int> ids, List<string> names)
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

    public async Task Update(int id, string name, double value)
    {
        var product = await context.Products
            .FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null);

        if (product is null)
            throw new NotFoundException($"Produto de id {id} não encontrado");

        product.Name = name;
        product.Value = value;
        product.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();

        memoryCache.Remove($"{productCacheKey}{id}");
    }

    public async Task Patch(int id, string? name, double? value)
    {
        var product = await context.Products
            .FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null);

        if (product is null)
            throw new NotFoundException($"Produto de id {id} não encontrado");

        product.Name = string.IsNullOrEmpty(name) ? product.Name : name;
        product.Value = value ?? product.Value;
        product.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();

        memoryCache.Remove($"{productCacheKey}{id}");
    }
}
