namespace ApiCartobani.Domain.GestionnaireActifs.Services;

using ApiCartobani.Domain.GestionnaireActifs;
using ApiCartobani.Databases;
using ApiCartobani.Services;

public interface IGestionnaireActifRepository : IGenericRepository<GestionnaireActif>
{
}

//public sealed class GestionnaireActifRepository : GenericRepository<GestionnaireActif>, IGestionnaireActifRepository
//{
//    private readonly CartobaniDbContext _dbContext;

//    public GestionnaireActifRepository(CartobaniDbContext dbContext) : base(dbContext)
//    {
//        _dbContext = dbContext;
//    }
//}

public sealed class GestionnaireActifRepository : GenericRepository<GestionnaireActif>, IGestionnaireActifRepository
{
    private readonly CartobaniDbContext _dbContext;

    public GestionnaireActifRepository(CartobaniDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}