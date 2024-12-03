using TradingVisualizerModels.Response;

namespace TradingVisualizerService.Interface;

public interface IGraphService
{
    Task<List<AllGraphsResponse>> GetAllGraphs();
}
