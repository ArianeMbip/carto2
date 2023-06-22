namespace ApiCartobani.Domain.Attributs.Services;

using ApiCartobani.Domain.Attributs;
using ApiCartobani.Databases;
using ApiCartobani.Services;

public interface IAttributRepository : IGenericRepository<Attribut>
{
}

public sealed class AttributRepository : GenericRepository<Attribut>, IAttributRepository
{
    private readonly CartobaniDbContext _dbContext;

    public AttributRepository(CartobaniDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
