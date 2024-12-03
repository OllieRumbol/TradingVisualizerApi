using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingVisualizerModels.Database;
using TradingVisualizerRepository.Instance;
using TradingVisualizerRepository.Interface;

namespace TradingVisualizerRepositoryTests;

[TestClass]
public class ShareRepositoryTests
{
    private Mock<IMongoDbProvider> _mongoDbProvider;
    private Mock<IMemoryCache> _memoryCache;
    private ShareRepository _shareRepository;

    [TestInitialize]
    public void Setup()
    {
        _mongoDbProvider = new Mock<IMongoDbProvider>();
        _memoryCache = new Mock<IMemoryCache>();
        _shareRepository = new ShareRepository(_mongoDbProvider.Object, _memoryCache.Object);
    }

    [TestMethod]
    public async Task GetAllShares_ReturnsShareList_NoCache()
    {
        // Arrange
        object noCacheShares = null;
        var shares = new List<Share>
        {
            new Share
            {
                Id = "abc123"
            }
        };
        _memoryCache.Setup(m => m.TryGetValue(It.IsAny<string>, out noCacheShares)).Returns(false);
        _memoryCache.Setup(m => m.CreateEntry(It.IsAny<object>())).Returns(Mock.Of<ICacheEntry>);
        _mongoDbProvider.Setup(m => m.GetCollectionFromDb<Share>(It.IsAny<string>())).ReturnsAsync(shares);

        // Act 
        var result = await _shareRepository.GetAllShares();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("abc123", result.First().Id);
    }

    [TestMethod]
    public async Task GetAllShares_ReturnsShareList_WithCache()
    {
        // Arrange
        object shares = new List<Share>
        {
            new Share
            {
                Id = "abc123",
                Label = "label1",
                Value = 1
            }
        };

        _memoryCache.Setup(m => m.TryGetValue(It.IsAny<string>(), out shares)).Returns(true);

        // Act 
        var result = await _shareRepository.GetAllShares();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("abc123", result.First().Id);
    }
}
