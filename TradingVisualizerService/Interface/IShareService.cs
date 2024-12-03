using TradingVisualizerModels.Response;

namespace TradingVisualizerService.Interface;

public interface IShareService
{
    Task<List<ShareResponse>> GetAllShares();
}
