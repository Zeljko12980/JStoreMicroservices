using BuildingBlocks.Messaging.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration;
public class BasketCheckoutEventHandler
    (ISender sender, ILogger<BasketCheckoutEventHandler> logger, IApplicationDbContext _context)
    : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        var customer = await _context.Customers
                  .FirstOrDefaultAsync(c => c.Name == context.Message.UserName);

        // Ako korisnik ne postoji, kreiraj novog korisnika
        if (customer == null)
        {
            customer = Customer.Create(CustomerId.Of(context.Message.CustomerId), context.Message.UserName, context.Message.EmailAddress);

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync(context.CancellationToken); // Pass the CancellationToken from the context

            logger.LogInformation("New customer added: {CustomerId}", context.Message.CustomerId); // Ensure logger is used

            var command = MapToCreateOrderCommand(context.Message);
            await sender.Send(command);
        }
    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {
        // Create full order with incoming event data
        var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, message.Country, message.State, message.ZipCode);
        var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.CVV, message.PaymentMethod);
        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto(
            Id: orderId,
            CustomerId: message.CustomerId,
            OrderName: message.UserName,
            ShippingAddress: addressDto,
            BillingAddress: addressDto,
            Payment: paymentDto,
            Status: Ordering.Domain.Enums.OrderStatus.Pending,
            OrderItems:
            [
                new OrderItemDto(orderId, new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"), 2, 500),
                new OrderItemDto(orderId, new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"), 1, 400)
            ]);

        return new CreateOrderCommand(orderDto);
    }
}
