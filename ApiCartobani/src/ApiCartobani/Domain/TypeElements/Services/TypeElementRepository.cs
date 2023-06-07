namespace ApiCartobani.Domain.TypeElements.Services;

using ApiCartobani.Domain.TypeElements;
using ApiCartobani.Databases;
using ApiCartobani.Services;

public interface ITypeElementRepository : IGenericRepository<TypeElement>
{
}

public sealed class TypeElementRepository : GenericRepository<TypeElement>, ITypeElementRepository
{
    private readonly CartobaniDbContext _dbContext;

    public TypeElementRepository(CartobaniDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
