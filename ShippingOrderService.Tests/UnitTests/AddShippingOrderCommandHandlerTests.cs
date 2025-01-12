
using FluentAssertions;
using Moq;
using ShippingOrderService.Application.Commands.CreateShippingOrder;
using ShippingOrderService.Domain.Entities;
using ShippingOrderService.Domain.Interfaces;

namespace ShippingOrderService.Tests.UnitTests
{
    public class AddShippingOrderCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Add_ShippingOrder_Successfully()
        {
            // Arrange
            var mockRepository = new Mock<IShippingOrderRepository>();
            var handler = new CreateShippingOrderHandler(mockRepository.Object);
            var command = new CreateShippingOrderCommand("SO12345", DateTime.Now,1 );

            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<ShippingOrder>())).Returns(Task.CompletedTask);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(1);
            mockRepository.Verify(repo => repo.AddAsync(It.IsAny<ShippingOrder>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Throw_Exception_When_TrackingNumber_Is_Empty()
        {
            // Arrange
            var mockRepository = new Mock<IShippingOrderRepository>();
            var handler = new CreateShippingOrderHandler(mockRepository.Object);
            var command = new CreateShippingOrderCommand("", DateTime.Now, 1 );

            // Act
            Func<Task> action = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await action.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Tracking number cannot be empty.");
        }

    }
}
