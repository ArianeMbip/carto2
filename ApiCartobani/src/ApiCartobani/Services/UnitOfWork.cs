namespace ApiCartobani.Services;

using ApiCartobani.Databases;

public interface IUnitOfWork : IApiCartobaniService
{
    Task<int> CommitChanges(CancellationToken cancellationToken = default);
}

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly CartobaniDbContext _dbContext;

    public UnitOfWork(CartobaniDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> CommitChanges(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
