using PurchaseOrderService.Domain.Entities;

namespace ShippingOrderService.Domain.Entities
{
    public class ShippingOrderItem : BaseEntity
    {
        public string Description { get; private set; }
        public decimal Weight { get; private set; }

        private ShippingOrderItem() { } // Required for EF Core

        public ShippingOrderItem(string description, decimal weight)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty.", nameof(description));

            if (weight <= 0)
                throw new ArgumentException("Weight must be greater than zero.", nameof(weight));

            Description = description;
            Weight = weight;
        }
    }
}

