using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Commands;

public record UpdateProductCommand(UpdateProductDto Dto) : IRequest<bool> { }
