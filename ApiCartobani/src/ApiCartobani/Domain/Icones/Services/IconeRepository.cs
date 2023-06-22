namespace ApiCartobani.Domain.Icones.Services;

using ApiCartobani.Domain.Icones;
using ApiCartobani.Databases;
using ApiCartobani.Services;

public interface IIconeRepository : IGenericRepository<Icone>
{
}

public sealed class IconeRepository : GenericRepository<Icone>, IIconeRepository
{
    private readonly CartobaniDbContext _dbContext;

    public IconeRepository(CartobaniDbContext dbContext) : base(dbContext, "Icones")
    {
        _dbContext = dbContext;
    }
}
