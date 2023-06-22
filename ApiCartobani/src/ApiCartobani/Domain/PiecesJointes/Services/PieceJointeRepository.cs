namespace ApiCartobani.Domain.PiecesJointes.Services;

using ApiCartobani.Domain.PiecesJointes;
using ApiCartobani.Databases;
using ApiCartobani.Services;

public interface IPieceJointeRepository : IGenericRepository<PieceJointe>
{
}

public sealed class PieceJointeRepository : GenericRepository<PieceJointe>, IPieceJointeRepository
{
    private readonly CartobaniDbContext _dbContext;

    public PieceJointeRepository(CartobaniDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
