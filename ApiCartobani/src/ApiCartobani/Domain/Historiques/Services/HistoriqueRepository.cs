namespace ApiCartobani.Domain.Historiques.Services;

using ApiCartobani.Domain.Historiques;
using ApiCartobani.Databases;
using ApiCartobani.Services;

public interface IHistoriqueRepository : IGenericRepository<Historique>
{
}

public sealed class HistoriqueRepository : GenericRepository<Historique>, IHistoriqueRepository
{
    private readonly CartobaniDbContext _dbContext;

    public HistoriqueRepository(CartobaniDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
