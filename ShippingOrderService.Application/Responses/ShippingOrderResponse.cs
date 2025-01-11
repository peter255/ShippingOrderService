
using ShippingOrderService.Domain.Enums;

namespace ShippingOrderService.Application.Responses;

public class ShippingOrderResponse
{
    public int Id { get; set; }
    public string TrackingNumber { get; set; }
    public DateTime ShippingDate { get; set; }
    public ShippingOrderState State { get; set; }
    public List<ShippingOrderItemResponse> Items { get; set; }
}

public class ShippingOrderItemResponse
{
    public int Id { get; set; }
    public string Description { get; set; }
    public decimal Weight { get; set; }
}
