using TradingVisualizerModels;

namespace TradingVisualizerRepository.Interface;

public interface IShareRespository
{
    Task<List<Share>> GetAllShares();
}
