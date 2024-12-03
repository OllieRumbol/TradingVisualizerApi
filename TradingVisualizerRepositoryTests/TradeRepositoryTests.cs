using Microsoft.Extensions.Caching.Memory;
using Moq;
using TradingVisualizerModels.Database;
using TradingVisualizerRepository.Instance;
using TradingVisualizerRepository.Interface;

namespace TradingVisualizerRepositoryTests;

[TestClass]
public class TradeRepositoryTests
{
    private Mock<IMongoDbProvider> _mongoDbProvider;
    private Mock<IMemoryCache> _memoryCache;
    private TradeRepository _tradeRepository;

    [TestInitialize]
    public void Setup()
    {
        _mongoDbProvider = new Mock<IMongoDbProvider>();
        _memoryCache = new Mock<IMemoryCache>();
        _tradeRepository = new TradeRepository(_mongoDbProvider.Object, _memoryCache.Object);
    }

    [TestMethod]
    public async Task GetAllTrades_ReturnsTradeList_NoCache()
    {
        // Arrange
        object noCacheTrades = null;
        List<Trade> trades = new List<Trade>
        {
            new Trade
            {
                Id = "abc123"
            }
        };
        _memoryCache.Setup(m => m.TryGetValue(It.IsAny<string>, out noCacheTrades)).Returns(false);
        _memoryCache.Setup(m => m.CreateEntry(It.IsAny<object>())).Returns(Mock.Of<ICacheEntry>);
        _mongoDbProvider.Setup(m => m.GetCollectionFromDb<Trade>(It.IsAny<string>())).ReturnsAsync(trades);

        // Act 
        var result = await _tradeRepository.GetAllTrades();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("abc123", result.First().Id);
    }

    [TestMethod]
    public async Task GetAllTrades_ReturnsTradeList_WithCache()
    {
        // Arrange
        object trades = new List<Trade>
        {
            new Trade
            {
                Id = "abc123",
                Month = "January",
                Year = 2024,
                AmountInvestedThisMonth = 100,
                TotlalValueOfShares = 150,
                TotalNumberOfSharesOwned = 10
            }
        };

        _memoryCache.Setup(m => m.TryGetValue(It.IsAny<string>(), out trades)).Returns(true);

        // Act 
        var result = await _tradeRepository.GetAllTrades();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("abc123", result.First().Id);
    }
}
