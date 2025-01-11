using Microsoft.EntityFrameworkCore;
using ShippingOrderService.Domain.Entities;

namespace ShippingOrderService.Infrastructure.Persistence
{
    public class ShippingOrderDbContext : DbContext 
    {
        public ShippingOrderDbContext(DbContextOptions<ShippingOrderDbContext> options) : base(options) { }

        public DbSet<ShippingOrder> ShippingOrders { get; set; }
        public DbSet<ShippingOrderItem> ShippingOrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure ShippingOrder
            modelBuilder.Entity<ShippingOrder>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.TrackingNumber).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ShippingDate).IsRequired();

                entity.HasMany(e => e.Items)
                      .WithOne()
                      .HasForeignKey("ShippingOrderId")
                      .OnDelete(DeleteBehavior.Cascade); // Delete items if order is deleted
            });

            // Configure ShippingOrderItem
            modelBuilder.Entity<ShippingOrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Description).IsRequired().HasMaxLength(500); ;
                entity.Property(e => e.Weight).IsRequired();
            });
        }
    }
}
