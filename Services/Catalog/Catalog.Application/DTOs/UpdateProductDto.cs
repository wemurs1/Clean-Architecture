using System.ComponentModel.DataAnnotations;
using Catalog.Core.Entities;

namespace Catalog.Application.DTOs;

public record class UpdateProductDto
{
    [Required]
    public required string Id { get; set; }

    [Required]
    public required string Name { get; init; }

    [Required]
    public required string Summary { get; init; }

    [Required]
    public required string Description { get; init; }

    [Required]
    public required string ImageFile { get; init; }

    [Required]
    public required string BrandId { get; init; }

    [Required]
    public required string TypeId { get; init; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; init; }
}