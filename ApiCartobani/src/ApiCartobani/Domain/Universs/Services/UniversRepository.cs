namespace ApiCartobani.Domain.Universs.Services;

using ApiCartobani.Domain.Universs;
using ApiCartobani.Databases;
using ApiCartobani.Services;

public interface IUniversRepository : IGenericRepository<Univers>
{
}

public sealed class UniversRepository : GenericRepository<Univers>, IUniversRepository
{
    private readonly CartobaniDbContext _dbContext;

    public UniversRepository(CartobaniDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
