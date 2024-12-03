using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using Scalar.AspNetCore;
using TradingVisualizerApi.Handlers;
using TradingVisualizerRepository.Instance;
using TradingVisualizerRepository.Interface;
using TradingVisualizerService.Instance;
using TradingVisualizerService.Interface;
using TradingVisualizerService.Mapper;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.local.json", optional: true);
builder.Services.AddOpenApi();
builder.Services.AddExceptionHandler<ProblemExceptionHandler>();
builder.Services.AddControllers();
builder.Services.AddSingleton<IMongoClient>((m) =>
{
    var connectionString = builder.Configuration.GetConnectionString("MongoDb");
    return new MongoClient(connectionString);
});
builder.Services.AddSingleton<IMongoDbProvider, MongoDbProvider>();
builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddSingleton<IGraphRepository, GraphRepository>();
builder.Services.AddSingleton<ITradeRepository, TradeRepository>();
builder.Services.AddSingleton<IShareRespository, ShareRepository>();
builder.Services.AddSingleton<TradeMapper>();
builder.Services.AddSingleton<ShareMapper>();
builder.Services.AddSingleton<IGraphService, GraphService>();
builder.Services.AddSingleton<ITradeService, TradeService>();
builder.Services.AddSingleton<IShareService, ShareService>();
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

        var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
        context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
    };
});

var app = builder.Build();
app.MapOpenApi();
app.MapScalarApiReference();
app.MapControllers();
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.Run();