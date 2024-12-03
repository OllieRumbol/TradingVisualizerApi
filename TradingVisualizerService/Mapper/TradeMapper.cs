using Riok.Mapperly.Abstractions;
using TradingVisualizerModels.Database;
using TradingVisualizerModels.Response;

namespace TradingVisualizerService.Mapper;

[Mapper]
public partial class TradeMapper
{
    public partial TradeResponse TradeToTradeResponse(Trade trade);
}
