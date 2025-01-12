
namespace PurchaseOrderService.Domain.Interfaces
{
    public interface IRabbitMqConsumer
    {
        Task ReciveMessageAsync();
    }
}
