

using MediatR;
using ShippingOrderService.Domain.Interfaces;
using ShippingOrderService.Domain.Entities;

namespace ShippingOrderService.Application.Commands.AddShippingOrderItem;

public class AddShippingOrderItemHandler : IRequestHandler<AddShippingOrderItemCommand,bool>
{
    private readonly IShippingOrderRepository _repository;

    public AddShippingOrderItemHandler(IShippingOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(AddShippingOrderItemCommand request, CancellationToken cancellationToken)
    {
        var shippingOrder = await _repository.GetByIdAsync(request.ShippingOrderId);

        if (shippingOrder == null)
            throw new InvalidOperationException($"Shipping order with ID {request.ShippingOrderId} not found.");

        var item = new ShippingOrderItem(request.Description, request.Weight);
        shippingOrder.AddItem(item);

      var result=  await _repository.SaveChangesAsync();

        return result;
    }
}
