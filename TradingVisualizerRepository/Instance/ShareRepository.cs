using Microsoft.Extensions.Caching.Memory;
using TradingVisualizerModels.Database;
using TradingVisualizerRepository.Interface;

namespace TradingVisualizerRepository.Instance;

public class ShareRepository : IShareRespository
{
    private readonly IMongoDbProvider _mongoDbProvider;
    private readonly IMemoryCache _memoryCache;
    private readonly TimeSpan CacheTimeout = TimeSpan.FromDays(7);
    private readonly string SharesCacheKey = "Shares-Cache-Key";
    private readonly string SharesCollectionName = "Shares";

    public ShareRepository(IMongoDbProvider mongoDbProvider, IMemoryCache memoryCache)
    {
        _mongoDbProvider = mongoDbProvider;
        _memoryCache = memoryCache;
    }

    public async Task<List<Share>> GetAllShares()
    {
        if (_memoryCache.TryGetValue(SharesCacheKey, out List<Share> cachedShares))
        {
            return cachedShares;
        }

        var shares = await _mongoDbProvider.GetCollectionFromDb<Share>(SharesCollectionName);

        if (shares != null)
        {
            _memoryCache.Set(SharesCacheKey, shares, CacheTimeout);
        }

        return shares;
    }
}
