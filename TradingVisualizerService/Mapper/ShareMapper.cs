using Riok.Mapperly.Abstractions;
using TradingVisualizerModels.Database;
using TradingVisualizerModels.Response;

namespace TradingVisualizerService.Mapper;

[Mapper]
public partial class ShareMapper
{
    public partial ShareResponse ShareToShareResponse(Share share);
    public partial List<ShareResponse> ShareToShareResponse(List<Share> shares);
}
