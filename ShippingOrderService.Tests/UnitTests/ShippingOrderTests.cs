using ShippingOrderService.Domain.Entities;

namespace ShippingOrderService.Tests.UnitTests
{
    public class ShippingOrderTests
    {
        [Fact]
        public void CreateShippingOrder_Should_Succeed()
        {
            // Arrange
            var items = new List<ShippingOrderItem>
            {
                new ShippingOrderItem("Laptop", 3.5m),
                new ShippingOrderItem("Phone", 1.2m)
            };

            // Act
            var shippingOrder = new ShippingOrder("SO12345", DateTime.Now,1);

            // Assert
            Assert.Equal("SO12345", shippingOrder.TrackingNumber);
            Assert.Equal(2, shippingOrder.Items.Count);
        }

        [Fact]
        public void CreateShippingOrder_Should_Throw_Exception_When_TrackingNumber_Is_Empty()
        {
            // Arrange
            Action action = () => new ShippingOrder("", DateTime.Now,1);

            // Act & Assert
            Assert.Throws<ArgumentException>(action);
        }
    }
}
