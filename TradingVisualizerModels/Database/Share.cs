using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TradingVisualizerModels.Database;

public class Share
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonElement("label")]
    public string Label { get; set; } = string.Empty;

    [BsonElement("value")]
    public decimal Value { get; set; }
}
