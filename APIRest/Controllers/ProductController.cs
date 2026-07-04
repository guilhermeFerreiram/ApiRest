using APIRest.DTOs;
using APIRest.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIRest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet]
        public async Task<ProductDto> Get(int id)
        {
            var product = await productService.GetProduct(id);

            return product;
        }
    }
}
