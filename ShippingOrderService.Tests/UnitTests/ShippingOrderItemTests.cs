using ShippingOrderService.Domain.Entities;

namespace ShippingOrderService.Tests.UnitTests
{
    public class ShippingOrderItemTests
    {
        [Fact]
        public void CreateShippingOrderItem_Should_Succeed()
        {
            // Arrange & Act
            var item = new ShippingOrderItem("Laptop", 5.5m);

            // Assert
            Assert.Equal("Laptop", item.Description);
            Assert.Equal(5.5m, item.Weight);
        }

        [Fact]
        public void CreateShippingOrderItem_Should_Throw_Exception_When_Weight_Is_Negative()
        {
            // Arrange
            Action action = () => new ShippingOrderItem("Laptop", -1.0m);

            // Act & Assert
            Assert.Throws<ArgumentException>(action);
        }
    }
}
