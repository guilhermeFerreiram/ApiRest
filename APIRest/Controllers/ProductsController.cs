using System.Net;
using APIRest.DTOs;
using APIRest.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIRest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> Get([FromRoute] int id)
        {
            var product = await productService.Get(id);

            return StatusCode((int)HttpStatusCode.OK, new BaseResponseDto<ProductDto>(false, [], product));
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAll([FromQuery] List<int> ids, [FromQuery] List<string> names)
        {
            var products = await productService.GetAll(ids, names);

            return StatusCode((int)HttpStatusCode.OK, new BaseResponseDto<List<ProductDto>>(false, [], products));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromQuery] string name, [FromQuery] double value)
        {
            var product = await productService.Create(name, value);

            return StatusCode((int)HttpStatusCode.Created, new BaseResponseDto<ProductDto>(false, [], product));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromQuery] string name, [FromQuery] double value)
        {
            await productService.Update(id, name, value);

            return StatusCode((int)HttpStatusCode.NoContent);
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch([FromRoute] int id, [FromQuery] string? name, [FromQuery] double? value)
        {
            await productService.Patch(id, name, value);

            return StatusCode((int)HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await productService.Delete(id);

            return StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}
