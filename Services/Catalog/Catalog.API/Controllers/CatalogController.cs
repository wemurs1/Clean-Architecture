using Catalog.Application.Commands;
using Catalog.Application.DTOs;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Core.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CatalogController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts([FromQuery] CatalogSpecParams catalogSpecParams)
    {
        var result = await mediator.Send(new GetAllProductsQuery(catalogSpecParams));
        var dtoList = result.ToPaginatedDto();
        return Ok(dtoList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductById(string id)
    {
        var query = new GetProductByIdQuery(id);
        var result = await mediator.Send(query);
        if (result == null) return NotFound();
        return Ok(result.ToDto());
    }

    [HttpGet("productName/{productName}")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductByProductName(string productName)
    {
        var query = new GetProductByNameQuery(productName);
        var result = await mediator.Send(query);
        if (result == null || !result.Any()) return NotFound();
        var dtoList = result.Select(p => p.ToDto()).ToList();
        return Ok(dtoList);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto dto)
    {
        var command = new CreateProductCommand(dto);
        var result = await mediator.Send(command);
        return Ok(result.ToDto());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteProduct(string id)
    {
        var result = await mediator.Send(new DeleteProductByIdCommand(id));
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateProduct(string id, UpdateProductDto dto)
    {
        var command = dto.ToCommand(id);
        var result = await mediator.Send(command);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpGet("GetAllBrands")]
    public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
    {
        var query = new GetAllBrandsQuery();
        var result = await mediator.Send(query);
        var brandDtoList = result.Select(b => b.ToDto()).ToList();
        return Ok(brandDtoList);
    }

    [HttpGet("GetAllTypes")]
    public async Task<ActionResult<IList<TypeDto>>> GetAllTypes()
    {
        var query = new GetAllTypesQuery();
        var result = await mediator.Send(query);
        var typeDtoList = result.Select(b => b.ToDto()).ToList();
        return Ok(typeDtoList);
    }

    [HttpGet("brands/{brand}", Name = "GetProductsByBrandName")]
    public async Task<ActionResult<IList<ProductDto>>> GetProductsByBrand(string brand)
    {
        var query = new GetProductByBrandQuery(brand);
        var result = await mediator.Send(query);
        var dtoList = result.Select(p => p.ToDto()).ToList();
        return Ok(dtoList);
    }
}
