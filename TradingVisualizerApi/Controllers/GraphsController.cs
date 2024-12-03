using Microsoft.AspNetCore.Mvc;
using TradingVisualizerModels.Response;
using TradingVisualizerService.Interface;

namespace TradingVisualizerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GraphsController : ControllerBase
{
    private readonly IGraphService _graphService;

    public GraphsController(IGraphService graphService)
    {
        _graphService = graphService;
    }

    [HttpGet]
    public async Task<ActionResult<List<AllGraphsResponse>>> GetAllTrades()
    {
        var graphs = _graphService.GetAllGraphs();

        return Ok(graphs);
    }
}
