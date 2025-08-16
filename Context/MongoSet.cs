using System;
using MongoDB.Driver;
using System.Linq;
using System.Linq.Expressions;
using Box.Mongo.Repository.Models;
using Newtonsoft.Json;

namespace Box.Mongo.Repository.Context;

public class MongoSet<TEntity> where TEntity : BaseEntity
{
    private readonly IMongoCollection<TEntity> _collection;

    public MongoSet(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<TEntity>(collectionName);
    }

    public IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> predicate = null)
    {
        if (predicate == null)
            return _collection.AsQueryable();

        return _collection.AsQueryable().Where(predicate);
    }

    public void Add(TEntity entity)
    {
        _collection.InsertOne(entity);
    }

    public void Update(TEntity entity)
    {
        _collection.ReplaceOne(x => x.Id == entity.Id, entity);
    }

    public void Delete(string id)
    {
        _collection.DeleteOne(x => x.Id == id);
    }

    public string ToJson(TEntity entity)
    {
        return JsonConvert.SerializeObject(entity, Formatting.Indented);
    }

    public TEntity FromJson(string json)
    {
        return JsonConvert.DeserializeObject<TEntity>(json);
    }
}
