using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infra.Options;
using Microsoft.Extensions.Options;

namespace Catalog.Infra.Repositories;

public class TypeRepository(IOptions<DatabaseSettings> settings)
    : BaseRepository<ProductType>(settings.Value, settings.Value.TypeCollectionName), ITypeRepository
{
}
