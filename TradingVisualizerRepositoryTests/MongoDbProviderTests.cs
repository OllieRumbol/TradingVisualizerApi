using MongoDB.Driver;
using TradingVisualizerModels.Database;
using TradingVisualizerRepository.Instance;

namespace TradingVisualizerRepositoryTests
{
    [TestClass]
    public class MongoDbProviderTests
    {
        private MongoClient _mongoClient;
        private MongoDbProvider _mongoProvider;

        [TestInitialize]
        public void Setup()
        {
            _mongoClient = new MongoClient("mongodb+srv://obourne:5W*TUVnc4zLYmCnm@trading-visualizer-db.mongocluster.cosmos.azure.com/?tls=true&authMechanism=SCRAM-SHA-256&retrywrites=false&maxIdleTimeMS=120000");
            _mongoProvider = new MongoDbProvider(_mongoClient);
        }

        [TestMethod]
        public async Task GetCollectionFromDb_ReturnsTradeCollection()
        {
            // Arrange

            // Act
            var result = await _mongoProvider.GetCollectionFromDb<Trade>("Trades");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(8, result.Count());
        }
    }
}
