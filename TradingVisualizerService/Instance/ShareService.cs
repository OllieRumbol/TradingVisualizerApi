using TradingVisualizerModels.Response;
using TradingVisualizerRepository.Interface;
using TradingVisualizerService.Interface;
using TradingVisualizerService.Mapper;

namespace TradingVisualizerService.Instance;

public class ShareService : IShareService
{
    private readonly IShareRespository _shareRepository;
    private readonly ShareMapper _shareMapper;

    public ShareService(IShareRespository shareRepository, ShareMapper shareMapper)
    {
        _shareRepository = shareRepository;
        _shareMapper = shareMapper;
    }

    public async Task<List<ShareResponse>> GetAllShares()
    {
        var shares = await _shareRepository.GetAllShares();
        var sharesResponse = _shareMapper.ShareToShareResponse(shares);
        return sharesResponse;
    }
}
