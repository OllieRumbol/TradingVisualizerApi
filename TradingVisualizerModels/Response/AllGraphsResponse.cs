using TradingVisualizerModels.Enums;

namespace TradingVisualizerModels.Response;

public class AllGraphsResponse
{
    public string Id { get; set; } = string.Empty;

    public GraphTypes GraphTypes { get; set; }
}




