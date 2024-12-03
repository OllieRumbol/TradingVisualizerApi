using TradingVisualizerModels.Database;

namespace TradingVisualizerRepository.Interface;

public interface IGraphRepository
{
    Task<List<Graph>> GetAllGraphs();

    Task<Graph> GetGraph(string id);
}
