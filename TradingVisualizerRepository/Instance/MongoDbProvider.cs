using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using MongoDB.Driver;
using TradingVisualizerModels;
using TradingVisualizerRepository.Interface;

namespace TradingVisualizerRepository.Instance;

public class MongoDbProvider : IMongoDbProvider
{
    private readonly IMongoClient _mongoClient;
    private readonly string DatabaseName = "Trading-Visualizer";
    
    public MongoDbProvider(IMongoClient mongoClient) 
    {
        _mongoClient = mongoClient;
    }

    public async Task<List<I>> GetCollectionFromDb<I>(string collectionName)
    {
        var collection = await _mongoClient
            .GetDatabase(DatabaseName)
            .GetCollection<I>(collectionName)
            .AsQueryable()
            .ToListAsync();

        return collection;
    }
}
