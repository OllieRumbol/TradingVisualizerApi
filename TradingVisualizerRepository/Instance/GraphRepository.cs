using Microsoft.Extensions.Caching.Memory;
using TradingVisualizerModels.Database;
using TradingVisualizerRepository.Interface;

namespace TradingVisualizerRepository.Instance;

public class GraphRepository : IGraphRepository
{
    private readonly IMongoDbProvider _mongoDbProvider;
    private readonly IMemoryCache _memoryCache;
    private readonly TimeSpan CacheTimeout = TimeSpan.FromDays(7);
    private readonly string GraphCacheKey = "Graph-Cache-Key";
    private readonly string GraphCollectionName = "Graphs";

    public GraphRepository(IMongoDbProvider mongoDbProvider, IMemoryCache memoryCache)
    {
        _mongoDbProvider = mongoDbProvider;
        _memoryCache = memoryCache;
    }

    public async Task<List<Graph>> GetAllGraphs()
    {
        if (_memoryCache.TryGetValue(GraphCacheKey, out List<Graph> cachedGraphs))
        {
            return cachedGraphs;
        }

        var graphs = await _mongoDbProvider.GetCollectionFromDb<Graph>(GraphCollectionName);

        if (graphs == null)
        {
            throw new InvalidOperationException("No graph data in db");
        }
        else
        {
            _memoryCache.Set(GraphCacheKey, graphs, CacheTimeout);
        }

        return graphs;
    }

    public async Task<Graph> GetGraph(string id)
    {
        var graphs = await GetAllGraphs();

        var graph = graphs.FirstOrDefault(graph => graph.Id == id);
        if (graph == null)
        {
            throw new InvalidOperationException("No graph with matching id");
        }

        return graph;
    }
}
