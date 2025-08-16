using System;
using MongoDB.Driver;
using Box.Mongo.Repository.Models;

namespace Box.Mongo.Repository.Context;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public MongoSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
    {
        var collectionName = typeof(TEntity).Name.ToLower();
        return new MongoSet<TEntity>(_database, collectionName);
    }
}
