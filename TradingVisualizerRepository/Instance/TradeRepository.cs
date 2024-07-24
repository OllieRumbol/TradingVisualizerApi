using Microsoft.Extensions.Caching.Memory;
using TradingVisualizerModels;
using TradingVisualizerRepository.Interface;

namespace TradingVisualizerRepository.Instance;

public class TradeRepository : ITradeRepository
{
    private readonly IMongoDbProvider _mongoDbProvider;
    private readonly IMemoryCache _memoryCache;
    private readonly TimeSpan CacheTimeout = TimeSpan.FromDays(7);
    private readonly string TradeCacheKey = "Trades-Cache-Key";
    private readonly string TradeCollectionName = "Trades";

    public TradeRepository(IMongoDbProvider mongoDbProvider, IMemoryCache memoryCache)
    {
        _mongoDbProvider = mongoDbProvider;
        _memoryCache = memoryCache;
    }

    public async Task<List<Trade>> GetAllTrades()
    {
        if (_memoryCache.TryGetValue(TradeCacheKey, out List<Trade> cachedTrades))
        {
            return cachedTrades;
        }

        var trades = await _mongoDbProvider.GetCollectionFromDb<Trade>(TradeCollectionName);

        if (trades != null)
        {
            _memoryCache.Set(TradeCacheKey, trades);
        }

        return trades;
    }
}
