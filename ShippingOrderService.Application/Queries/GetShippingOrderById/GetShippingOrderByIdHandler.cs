
using MediatR;
using ShippingOrderService.Application.Queries.GetShippingOrderById;
using ShippingOrderService.Application.Responses;
using ShippingOrderService.Domain.Interfaces;

namespace SHOService.Application.Handlers;

public class GetShippingOrderByIdHandler : IRequestHandler<GetShippingOrderByIdQuery, ShippingOrderResponse>
{
    private readonly IShippingOrderRepository _repository;

    public GetShippingOrderByIdHandler(IShippingOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<ShippingOrderResponse> Handle(GetShippingOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var shippingOrder = await _repository.GetByIdAsync(request.ShippingOrderId);

        if (shippingOrder == null)
            throw new KeyNotFoundException($"Shipping order with ID {request.ShippingOrderId} not found.");

        return new ShippingOrderResponse
        {
            Id = shippingOrder.Id,
            TrackingNumber = shippingOrder.TrackingNumber,
            ShippingDate = shippingOrder.ShippingDate,
            State = shippingOrder.State,
            Items = shippingOrder.Items.Select(item => new ShippingOrderItemResponse
            {
                Id = item.Id,
                Description = item.Description,
                Weight = item.Weight
            }).ToList()
        };
    }
}
