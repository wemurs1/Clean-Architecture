using  Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CatalogController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IList<ProductResponse>>> GetAllProducts([FromQuery] CatalogSpecParams catalogSpecParams)
    {
        var result = await mediator.Send(new GetAllProductsQuery(catalogSpecParams));
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponse>> GetProductById(string id)
    {
        var query = new GetProductByIdQuery(id);
        var result = await mediator.Send(query);
        return Ok(result);
    }
}
