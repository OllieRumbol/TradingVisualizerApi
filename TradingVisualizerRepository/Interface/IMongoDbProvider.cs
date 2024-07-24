namespace TradingVisualizerRepository.Interface;

public interface IMongoDbProvider
{
    Task<List<I>> GetCollectionFromDb<I>(string collectionName);
}
