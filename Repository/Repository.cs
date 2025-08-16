using System;
using System.Linq;
using System.Linq.Expressions;
using Box.Mongo.Repository.Models;
using Box.Mongo.Repository.Context;

namespace Box.Mongo.Repository.Repository;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(string id);
    string ToJson(TEntity entity);
    TEntity FromJson(string json);
}

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly MongoSet<TEntity> _set;

    public GenericRepository(MongoDbContext context)
    {
        _set = context.Set<TEntity>();
    }

    public IQueryable<TEntity> GetAll() => _set.AsQueryable();

    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        => _set.AsQueryable(predicate);

    public void Add(TEntity entity) => _set.Add(entity);
    public void Update(TEntity entity) => _set.Update(entity);
    public void Delete(string id) => _set.Delete(id);

    public string ToJson(TEntity entity) => _set.ToJson(entity);
    public TEntity FromJson(string json) => _set.FromJson(json);
}
