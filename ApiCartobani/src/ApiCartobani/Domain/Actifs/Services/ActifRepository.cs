namespace ApiCartobani.Domain.Actifs.Services;

using ApiCartobani.Domain.Actifs;
using ApiCartobani.Databases;
using ApiCartobani.Services;

public interface IActifRepository : IGenericRepository<Actif>
{
}

public sealed class ActifRepository : GenericRepository<Actif>, IActifRepository
{
    private readonly CartobaniDbContext _dbContext;

    public ActifRepository(CartobaniDbContext dbContext) : base(dbContext, "Actifs")
    {
        _dbContext = dbContext;
    }
}
