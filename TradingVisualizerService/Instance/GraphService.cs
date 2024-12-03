using TradingVisualizerModels.Enums;
using TradingVisualizerModels.Response;
using TradingVisualizerRepository.Interface;
using TradingVisualizerService.Interface;
using TradingVisualizerService.Mapper;

namespace TradingVisualizerService.Instance;

public class GraphService : IGraphService
{
    private readonly IGraphRepository _graphRepository;

    public GraphService(IGraphRepository graphRepository)
    {
        _graphRepository = graphRepository;
    }

    public async Task<List<AllGraphsResponse>> GetAllGraphs()
    {
        var graphs = await _graphRepository.GetAllGraphs();
        var allGraphResponse = graphs.Select(g =>
        {
            return new AllGraphsResponse
            {
                Id = g.Id,
                GraphTypes = g.Type switch
                {
                    "BAR" => GraphTypes.Bar,
                    _ => GraphTypes.Error
                }
            };
        }).ToList();

        return allGraphResponse;
    }
}
