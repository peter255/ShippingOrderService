using Microsoft.EntityFrameworkCore;
using ShippingOrderService.Domain.Entities;
using ShippingOrderService.Domain.Interfaces;
using ShippingOrderService.Infrastructure.Persistence;

namespace ShippingOrderService.Infrastructure.Repositories
{

    public class ShippingOrderRepository : IShippingOrderRepository
    {
        private readonly ShippingOrderDbContext _context;

        public ShippingOrderRepository(ShippingOrderDbContext context)
        {
            _context = context;
        }

        public async Task<ShippingOrder> GetByIdAsync(int id)
        {
            return await _context.ShippingOrders
                                 .Include(so => so.Items)
                                 .FirstOrDefaultAsync(so => so.Id == id);
        }

        public async Task AddAsync(ShippingOrder shippingOrder)
        {
            await _context.ShippingOrders.AddAsync(shippingOrder);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
