namespace ApiCartobani.Domain.Environnements.Services;

using ApiCartobani.Domain.Environnements;
using ApiCartobani.Databases;
using ApiCartobani.Services;

public interface IEnvironnementRepository : IGenericRepository<Environnement>
{
}

public sealed class EnvironnementRepository : GenericRepository<Environnement>, IEnvironnementRepository
{
    private readonly CartobaniDbContext _dbContext;

    public EnvironnementRepository(CartobaniDbContext dbContext) : base(dbContext, "Environnements")
    {
        _dbContext = dbContext;
    }
}
