using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using StockApplication.Business.Messaging;
using StockApplication.Business.Messaging.Interfaces;
using StockApplication.Business.Services.Interfaces;
using StockApplication.Business.ValidationServices.Interfaces;
using StockApplication.Dto;
using Telerik.JustMock;

namespace StockApplication.Business.Tests.Messaging
{
    [TestFixture]
    [Explicit("Mocking Kafka services is necessary")]
    public class ConsumerHandlerTests
    {
        private IStockService _stockService;
        private IUserService _userService;
        private IMessageValidationService _messageValidationService;
        private IProducerHandler _producerHandler;
        private IConsumerHandler _consumerHandler;

        [SetUp]
        public void SetUp()
        {
            _stockService = Mock.Create<IStockService>(Behavior.Strict);
            _userService = Mock.Create<IUserService>(Behavior.Strict);
            _messageValidationService = Mock.Create<IMessageValidationService>(Behavior.Strict);
            _producerHandler = new ProducerHandler();
            _consumerHandler = new ConsumerHandler(_stockService, _messageValidationService, _userService);

            _producerHandler.ProduceMessageAsync("AAPL.US", new CancellationToken());
        }

        [Test]
        public async Task Do_ConsumeMessageAsync_When_IsDecoupledCall_Returns_Message()
        {
            // Arrange
            var message = "Stock is valid";
            Mock.Arrange(() => _userService.GetUsername()).Returns("Username");
            Mock.Arrange(() => _messageValidationService.GetStockCommand(Arg.AnyString)).Returns(Arg.AnyString);
            Mock.Arrange(() => _stockService.GetStockClosePriceAsync(Arg.AnyString, Arg.IsAny<CancellationToken>()))
                .TaskResult(message);

            await _producerHandler.ProduceMessageAsync("AAPL.US", new CancellationToken());

            // Act
            var result = await _consumerHandler.ConsumeMessageAsync(Arg.IsAny<CancellationToken>(), true);

            // Assert
            Assert.That(result, Is.InstanceOf<MessageDto>());
            Assert.AreEqual(message, result.Text);
        }

        [Test]
        public async Task Do_ConsumeMessageAsync_When_IsNotDecoupledCallAndIsStockCommand_Returns_MessageFromBot()
        {
            // Arrange
            var message = "Stock is valid";
            Mock.Arrange(() => _messageValidationService.GetStockCommand(Arg.AnyString)).Returns(Arg.AnyString);
            Mock.Arrange(() => _stockService.GetStockClosePriceAsync(Arg.AnyString, Arg.IsAny<CancellationToken>()))
                .TaskResult(message);
            Mock.Arrange(() => _userService.GetUsername()).OccursNever();

            await _producerHandler.ProduceMessageAsync("/stock=AAPL.US", new CancellationToken());

            // Act
            var result = await _consumerHandler.ConsumeMessageAsync(Arg.IsAny<CancellationToken>());

            // Assert
            Assert.That(result, Is.InstanceOf<MessageDto>());
            Assert.AreEqual("Bot", result.Username);
        }

        [Test]
        public async Task Do_ConsumeMessageAsync_When_IsNotDecoupledCallAndIsNotStockCommand_Returns_Message()
        {
            // Arrange
            var message = "Stock is valid";
            Mock.Arrange(() => _userService.GetUsername()).Returns("Username");
            Mock.Arrange(() => _messageValidationService.GetStockCommand(Arg.AnyString)).Returns(string.Empty);
            Mock.Arrange(() => _stockService.GetStockClosePriceAsync(Arg.AnyString, Arg.IsAny<CancellationToken>()))
                .TaskResult(message);

            await _producerHandler.ProduceMessageAsync("Hello", new CancellationToken());

            // Act
            var result = await _consumerHandler.ConsumeMessageAsync(Arg.IsAny<CancellationToken>());

            // Assert
            Assert.That(result, Is.InstanceOf<MessageDto>());
            Assert.AreEqual("Username", result.Username);
        }
    }
}
