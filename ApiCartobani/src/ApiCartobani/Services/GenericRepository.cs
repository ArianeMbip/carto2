namespace ApiCartobani.Services;

using ApiCartobani.Domain;
using ApiCartobani.Databases;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MongoFramework;
using MongoDB.Driver;
using SharpCompress.Common;
using System.Linq.Expressions;
using ApiCartobani.Domain.TypeElements;

public interface IGenericRepository<TEntity> : IApiCartobaniService
    where TEntity : BaseEntity
{
    //IQueryable<TEntity> Query();
    //Task<TEntity> GetByIdOrDefault(Guid id, bool withTracking = true, CancellationToken cancellationToken = default);
    //Task<TEntity> GetById(Guid id, bool withTracking = true, CancellationToken cancellationToken = default);
    //Task<bool> Exists(Guid id, CancellationToken cancellationToken = default);
    //Task Add(TEntity entity, CancellationToken cancellationToken = default);    
    //Task AddRange(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default);    
    //void Update(TEntity entity);
    //void Remove(TEntity entity);
    //void RemoveRange(IEnumerable<TEntity> entity);

    IQueryable<TEntity> Query();
    Task<TEntity> GetByIdOrDefault(Guid id, CancellationToken cancellationToken = default);
    Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<bool> Exists(Guid id, CancellationToken cancellationToken = default);
    Task Add(TEntity entity, CancellationToken cancellationToken = default);
    Task AddRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    void Update(TEntity entity, CancellationToken cancellationToken = default);
    void Remove(TEntity entity, CancellationToken cancellationToken = default);
    void RemoveRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
}

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> 
    where TEntity : BaseEntity
{
    //private readonly CartobaniDbContext _dbContext;

    private readonly IMongoCollection<TEntity> _collection;
    public readonly IMongoDatabase _database;

    protected GenericRepository(CartobaniDbContext dbContext, string collectionName = "default")
    {
        _collection = dbContext._database.GetCollection<TEntity>(collectionName);
    }

    public virtual IQueryable<TEntity> Query()
    {
        //return await _collection.Find(_ => true).ToListAsync();
        return _collection.AsQueryable();
    }

    public virtual async Task<TEntity> GetByIdOrDefault(Guid id, CancellationToken cancellationToken = default)
    {

        return await _collection.Find(e => e.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdOrDefault(id, cancellationToken);

        if (entity == null)
            throw new NotFoundException($"{typeof(TEntity).Name} with an id '{id}' was not found.");

        return entity;
    }

    public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await (await _collection.FindAsync(p => true)).ToListAsync();
    }

    //public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
    //public virtual async Task<List<TEntity>> GetAllAsync(FilterDefinition<TEntity> filter, CancellationToken cancellationToken = default)
    //{
    //    var filtre = Builders<TEntity>.Filter.Eq("Nom", "John");
    //    var entities = await GetAllAsync(filter);
    //    return await (await _collection.FindAsync(filtre)).ToListAsync();
    //}
    
    //public async Task<List<TypeElement>> SearchByProperty(string propertyName, string searchValue)
    //{
    //    var collection = _database.GetCollection<TypeElement>("TypeElements");
    //    var filter = Builders<TypeElement>.Filter.Eq(propertyName, searchValue);
    //    var results = await collection.Find(filter).ToListAsync();
    //    return results;
    //}

    public virtual async Task<bool> Exists(Guid id, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(e => e.Id == id).AnyAsync(cancellationToken);
    }

    public virtual async Task Add(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
    }

    public virtual async Task AddRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _collection.InsertManyAsync(entities, cancellationToken: cancellationToken);
    }

    public virtual async void Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        var result = await _collection.ReplaceOneAsync(e => e.Id == entity.Id, entity, cancellationToken: cancellationToken);

        if (result.ModifiedCount != 1)
            throw new NotFoundException($"{typeof(TEntity).Name} with an id '{entity.Id}' was not found.");
    }

    public virtual async void Remove(TEntity entity, CancellationToken cancellationToken = default)
    {
        var id = entity.Id;
        var result = await _collection.DeleteOneAsync(e => e.Id == id, cancellationToken);

        if (result.DeletedCount != 1)
            throw new NotFoundException($"{typeof(TEntity).Name} with an id '{id}' was not found.");
    }

    public virtual async void RemoveRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        var ids = entities.Select(e => e.Id);
        var filter = Builders<TEntity>.Filter.In(e => e.Id, ids);
        await _collection.DeleteManyAsync(filter, cancellationToken);
    }



    //protected GenericRepository(CartobaniDbContext dbContext)
    //{
    //    this._dbContext = dbContext;
    //}

    //public virtual IQueryable<TEntity> Query()
    //{
    //    return _dbContext.Set<TEntity>();
    //}

    //public virtual async Task<TEntity> GetByIdOrDefault(Guid id, bool withTracking = true, CancellationToken cancellationToken = default)
    //{
    //    return withTracking
    //        ? await _dbContext.Set<TEntity>()
    //            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken)
    //        : await _dbContext.Set<TEntity>()
    //            .AsNoTracking()
    //            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    //}

    //public virtual async Task<TEntity> GetById(Guid id, bool withTracking = true, CancellationToken cancellationToken = default)
    //{
    //    var entity = await GetByIdOrDefault(id, withTracking, cancellationToken);

    //    if(entity == null)
    //        throw new NotFoundException($"{typeof(TEntity).Name} with an id '{id}' was not found.");

    //    return entity;
    //}

    //public virtual async Task<bool> Exists(Guid id, CancellationToken cancellationToken = default)
    //{
    //    return await _dbContext.Set<TEntity>()
    //        .AnyAsync(e => e.Id == id, cancellationToken);
    //}

    //public virtual async Task Add(TEntity entity, CancellationToken cancellationToken = default)
    //{
    //    await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
    //}

    //public virtual async Task AddRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    //{
    //    await _dbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
    //}

    //public virtual void Update(TEntity entity)
    //{
    //    _dbContext.Set<TEntity>().Update(entity);
    //}

    //public virtual void Remove(TEntity entity)
    //{
    //    _dbContext.Set<TEntity>().Remove(entity);
    //}

    //public virtual void RemoveRange(IEnumerable<TEntity> entities)
    //{
    //    _dbContext.Set<TEntity>().RemoveRange(entities);
    //}
}
