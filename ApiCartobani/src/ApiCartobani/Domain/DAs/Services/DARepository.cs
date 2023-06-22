namespace ApiCartobani.Domain.DAs.Services;

using ApiCartobani.Domain.DAs;
using ApiCartobani.Databases;
using ApiCartobani.Services;

public interface IDARepository : IGenericRepository<DA>
{
}

public sealed class DARepository : GenericRepository<DA>, IDARepository
{
    private readonly CartobaniDbContext _dbContext;

    public DARepository(CartobaniDbContext dbContext) : base(dbContext, "DAs")
    {
        _dbContext = dbContext;
    }
}
