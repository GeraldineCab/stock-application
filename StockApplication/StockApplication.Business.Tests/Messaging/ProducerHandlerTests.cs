using System.Threading;
using NUnit.Framework;
using StockApplication.Business.Messaging;
using StockApplication.Business.Messaging.Interfaces;
using Telerik.JustMock;

namespace StockApplication.Business.Tests.Messaging
{
    [TestFixture]
    [Explicit("Mocking Kafka services is necessary")]
    public class ProducerHandlerTests
    {
        private IProducerHandler _producerHandler;

        [SetUp]
        public void SetUp()
        {
            _producerHandler = new ProducerHandler();
        }

        [Test]
        public void Do_ProduceMessageAsync_Throw_NoException()
        {
            // Assert
            Assert.DoesNotThrowAsync(() => _producerHandler.ProduceMessageAsync(Arg.AnyString, Arg.IsAny<CancellationToken>()));
        }
    }
}
