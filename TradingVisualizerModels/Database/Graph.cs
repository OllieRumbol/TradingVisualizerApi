using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TradingVisualizerModels.Database;

public class Graph
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonElement("key")]
    public string Key { get; set; } = string.Empty;

    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("type")]
    public string Type { get; set; } = string.Empty;

    [BsonElement("xAxisLabel")]
    public string XAxislabel { get; set; } = string.Empty;

    [BsonElement("yAxisLabel")]
    public string YAxislabel { get; set;} = string.Empty;

    [BsonElement("graphKey")]
    public string[] GraphKeys { get; set; }
}
