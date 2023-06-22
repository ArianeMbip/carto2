namespace ApiCartobani.Domain.ValeurAttributs.Services;

using ApiCartobani.Domain.ValeurAttributs;
using ApiCartobani.Databases;
using ApiCartobani.Services;

public interface IValeurAttributRepository : IGenericRepository<ValeurAttribut>
{
}

public sealed class ValeurAttributRepository : GenericRepository<ValeurAttribut>, IValeurAttributRepository
{
    private readonly CartobaniDbContext _dbContext;

    public ValeurAttributRepository(CartobaniDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
