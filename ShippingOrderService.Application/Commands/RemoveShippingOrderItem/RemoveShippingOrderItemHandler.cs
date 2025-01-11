

using MediatR;
using ShippingOrderService.Domain.Interfaces;
using ShippingOrderService.Domain.Entities;

namespace ShippingOrderService.Application.Commands.RemoveShippingOrderItem;

public class RemoveShippingOrderItemHandler : IRequestHandler<RemoveShippingOrderItemCommand,bool>
{
    private readonly IShippingOrderRepository _repository;

    public RemoveShippingOrderItemHandler(IShippingOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(RemoveShippingOrderItemCommand request, CancellationToken cancellationToken)
    {
        var shippingOrder = await _repository.GetByIdAsync(request.ShippingOrderId);

        if (shippingOrder == null)
            throw new InvalidOperationException($"Shipping order with ID {request.ShippingOrderId} not found.");

        shippingOrder.RemoveItem(request.ItemId);

       var result= await _repository.SaveChangesAsync();

        return result;
    }
}
