using Microsoft.Extensions.Caching.Memory;
using Moq;
using TradingVisualizerModels.Database;
using TradingVisualizerRepository.Instance;
using TradingVisualizerRepository.Interface;

namespace TradingVisualizerRepositoryTests
{
    [TestClass]
    public class GraphRepositoryTests
    {
        private Mock<IMongoDbProvider> _mongoDbProvider;
        private Mock<IMemoryCache> _memoryCache;
        private GraphRepository _graphRepository;

        [TestInitialize]
        public void Setup()
        {
            _mongoDbProvider = new Mock<IMongoDbProvider>();
            _memoryCache = new Mock<IMemoryCache>();
            _graphRepository = new GraphRepository(_mongoDbProvider.Object, _memoryCache.Object);
        }

        [TestMethod]
        public async Task GetAllGraphs_NoCache_SuccessfullyRetrunsGraphs()
        {
            // Arrange
            object noCacheShares = null;
            var graphs = new List<Graph>
            {
                new Graph
                {
                    Id = Guid.NewGuid().ToString()
                }
            };

            _memoryCache.Setup(m => m.TryGetValue(It.IsAny<string>, out noCacheShares)).Returns(false);
            _memoryCache.Setup(m => m.CreateEntry(It.IsAny<object>())).Returns(Mock.Of<ICacheEntry>);
            _mongoDbProvider.Setup(m => m.GetCollectionFromDb<Graph>(It.IsAny<string>())).ReturnsAsync(graphs);

            // Act
            var result = await _graphRepository.GetAllGraphs();

            // Assert
            Assert.AreEqual(1, result.Count);

        }
    }
}
