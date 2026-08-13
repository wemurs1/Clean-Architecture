using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infra.Options;
using Microsoft.Extensions.Options;

namespace Catalog.Infra.Repositories;

public class BrandRepository(IOptions<DatabaseSettings> opt)
    : BaseRepository<ProductBrand>(opt.Value, opt.Value.BrandCollectionName), IBrandRepository
{
}
