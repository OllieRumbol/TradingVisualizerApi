using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TradingVisualizerModels;

public class Trade
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonElement("month")]
    public string Month { get; set; } = string.Empty;

    [BsonElement("year")]
    public int Year { get; set; }

    [BsonElement("amountInvested")]
    public int AmountInvestedThisMonth { get; set; }

    [BsonElement("valueOfShares")]
    public decimal TotlalValueOfShares { get; set; }

    [BsonElement("numberOfSharesOwned")]
    public decimal TotalNumberOfSharesOwned { get; set; }
}
