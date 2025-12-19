using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration
{
    public class BasketCheckoutEventHandler (ISender sender, ILogger<BasketCheckoutEventHandler> logger) 
        : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            logger.LogInformation($"Integration Event Handler Handled : {context.Message.GetType().Name}");

            var command = MapToCreateOrderCommand(context.Message);

            var result = await sender.Send(command);

            throw new NotImplementedException();
        }

        private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent basket)
        {
            var addressDto = new AddressDto(basket.FirstName, basket.LastName, basket.EmailAddress, basket.AddressLine, basket.Country, basket.State, basket.ZipCode);
            var paymentDto = new PaymentDto(basket.CardName, basket.CardNumber, basket.Expiration, basket.CVV, basket.PaymentMethod);
            var orderId = Guid.NewGuid();
            OrderDto orderDto = new(
                orderId,
                basket.CustomerId,
                basket.UserName,
                addressDto,
                addressDto,
                paymentDto,
                OrderStatus.Pending,
                [
                    new (orderId, new Guid("C498398B-88B4-4BC3-A418-E0C9D98C6ED5"), 1, 999.90m),
                    new (orderId, new Guid("43BEEA6C-6F0B-4C31-98AE-3344ACC2E8A2"), 1, 1500.90m)
                ]);
            return new(orderDto);
        }
    }
}
