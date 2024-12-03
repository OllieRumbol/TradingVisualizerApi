using MongoDB.Bson;

namespace TradingVisualizerModels.Response;

public class TradeResponse
{
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    public string Month { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public int Year { get; set; }

    public int AmountInvestedThisMonth { get; set; }

    public int TotalAmountInvested { get; set; }

    public decimal TotlalValueOfShares { get; set; }

    public decimal TotalNumberOfSharesOwned { get; set; }

    public decimal TotalProfits {  get; set; }
}