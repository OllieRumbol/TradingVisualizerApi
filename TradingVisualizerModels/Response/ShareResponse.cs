using MongoDB.Bson;

namespace TradingVisualizerModels.Response;

public class ShareResponse
{
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    public string Label { get; set; } = string.Empty;

    public decimal Value { get; set; }
}
