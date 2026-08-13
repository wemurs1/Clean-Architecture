namespace Catalog.Infra.Options;

public sealed class DatabaseSettings
{
    public required string ConnectionString { get; set; }
    public required string DatabaseName { get; set; }
    public required string ProductCollectionName { get; set; }
    public required string BrandCollectionName { get; set; }
    public required string TypeCollectionName { get; set; }
}
