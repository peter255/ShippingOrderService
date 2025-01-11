using ShippingOrderService.Domain.Entities;

namespace ShippingOrderService.Domain.Interfaces
{
    public interface IShippingOrderRepository
    {
        Task<ShippingOrder> GetByIdAsync(int id);
        Task AddAsync(ShippingOrder shippingOrder);
        Task<bool> SaveChangesAsync();
    }
}
