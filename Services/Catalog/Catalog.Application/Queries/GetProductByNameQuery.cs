using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public record GetProductByNameQuery(string ProductName) : IRequest<IList<ProductResponse>> { }
