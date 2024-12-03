using TradingVisualizerModels.Database;

namespace TradingVisualizerRepository.Interface;

public interface ITradeRepository
{
    Task<List<Trade>> GetAllTrades();
}
