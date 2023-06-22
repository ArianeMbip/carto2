namespace ApiCartobani.Domain.TypeElements.Services;

using ApiCartobani.Domain.TypeElements;
using ApiCartobani.Databases;
using ApiCartobani.Services;
using MongoDB.Driver;

public interface ITypeElementRepository : IGenericRepository<TypeElement>
{
    //public Task<List<TypeElement>> SearchByProperty(string propertyName, string searchValue);
}

public sealed class TypeElementRepository : GenericRepository<TypeElement>, ITypeElementRepository
{
    private readonly CartobaniDbContext _dbContext;

    public TypeElementRepository(CartobaniDbContext dbContext) : base(dbContext, "TypeElements")
    {
        _dbContext = dbContext;
    }

   
}
