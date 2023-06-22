namespace ApiCartobani.Domain.Flux.Services;

using ApiCartobani.Domain.Flux;
using ApiCartobani.Databases;
using ApiCartobani.Services;


public interface IFluxRepository : IGenericRepository<Flux>
{
   // Task<User> CreateAsync(User student);
}


public sealed class FluxRepository : GenericRepository<Flux>, IFluxRepository
{
    private readonly CartobaniDbContext _dbContext;

    public FluxRepository(CartobaniDbContext dbContext) : base(dbContext, "Flux")
    {
        _dbContext = dbContext;
    }
}

//public sealed class FluxRepository : IFluxRepository
//{
//    private readonly CartobaniDbContext _dbContext;

//    public FluxRepository(CartobaniDbContext dbContext)
//    {
//        _dbContext = dbContext;
//    }

//    public async Task<User> CreateAsync(User student)
//    {
//        await _dbContext.Users.InsertOneAsync(student);
//        return student;
//    }
//}
