namespace ApiCartobani.Domain.Fonctionnalites.Services;

using ApiCartobani.Domain.Fonctionnalites;
using ApiCartobani.Databases;
using ApiCartobani.Services;

public interface IFonctionnaliteRepository : IGenericRepository<Fonctionnalite>
{
}

public sealed class FonctionnaliteRepository : GenericRepository<Fonctionnalite>, IFonctionnaliteRepository
{
    private readonly CartobaniDbContext _dbContext;

    public FonctionnaliteRepository(CartobaniDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
