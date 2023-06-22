namespace ApiCartobani.Domain.Composants.Services;

using ApiCartobani.Domain.Composants;
using ApiCartobani.Databases;
using ApiCartobani.Services;

public interface IComposantRepository : IGenericRepository<Composant>
{
}

public sealed class ComposantRepository : GenericRepository<Composant>, IComposantRepository
{
    private readonly CartobaniDbContext _dbContext;

    public ComposantRepository(CartobaniDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
