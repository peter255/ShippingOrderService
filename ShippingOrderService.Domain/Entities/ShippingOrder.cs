using PurchaseOrderService.Domain.Entities;
using ShippingOrderService.Domain.Enums;

namespace ShippingOrderService.Domain.Entities
{
    public class ShippingOrder : BaseEntity
    {

        public int POId { get; private set; }
        public string TrackingNumber { get; private set; }
        public DateTime ShippingDate { get; private set; }
        public ShippingOrderState State { get; private set; } = ShippingOrderState.Created;

        private readonly List<ShippingOrderItem> _items = new();
        public IReadOnlyCollection<ShippingOrderItem> Items => _items.AsReadOnly();

        private ShippingOrder() { } // Required for EF Core

        public ShippingOrder(string trackingNumber, DateTime shippingDate, int poId)
        {
            if (string.IsNullOrWhiteSpace(trackingNumber))
                throw new ArgumentException("Tracking number cannot be empty.", nameof(trackingNumber));

            TrackingNumber = trackingNumber;
            ShippingDate = shippingDate;
            POId = poId;
        }

        public void AddItem(ShippingOrderItem item)
        {
            if (_items.Any(i => i.Description == item.Description))
                throw new InvalidOperationException($"Item with description '{item.Description}' already exists.");

            _items.Add(item);
        }

        public void RemoveItem(int itemId)
        {
            var item = _items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
                throw new InvalidOperationException($"Item with ID {itemId} not found.");

            _items.Remove(item);
        }

        public void ChangeState(ShippingOrderState newState)
        {
            if (newState < State)
                throw new InvalidOperationException("Cannot revert to a previous state.");

            State = newState;
        }
    }

}

