
using MediatR;
using ShippingOrderService.Domain.Entities;
using ShippingOrderService.Domain.Interfaces;

namespace ShippingOrderService.Application.Commands.CreateShippingOrder;

public class CreateShippingOrderHandler : IRequestHandler<CreateShippingOrderCommand, int>
{
    private readonly IShippingOrderRepository _repository;

    public CreateShippingOrderHandler(IShippingOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateShippingOrderCommand request, CancellationToken cancellationToken)
    {
        var shippingOrder = new ShippingOrder(request.TrackingNumber, request.ShippingDate, request.poId);
        await _repository.AddAsync(shippingOrder);
        await _repository.SaveChangesAsync();
        return shippingOrder.Id;
    }
}
