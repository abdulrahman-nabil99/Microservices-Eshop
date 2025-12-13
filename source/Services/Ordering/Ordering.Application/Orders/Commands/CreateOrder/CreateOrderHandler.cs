namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler(IApplicationDbContext context) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // Create Order Entity From Command Object
            var order = CreateNewOrder(request.Order);
            // Save order in the database
            context.Orders.Add(order);
            await context.SaveChangesAsync(cancellationToken);
            // Return result
            return new(order.Id.Value);
        }

        private Order CreateNewOrder(OrderDto order)
        {
            var shippingAddress = CreateAddress(order.ShippingAddress);
            var billingAddress = CreateAddress(order.BillingAddress);
            var payment = CreatePayment(order.Payment);
            var newOrder = Order.Create(
                OrderId.Of(new Guid()),
                CustomerId.Of(order.CustomerId),
                OrderName.Of(order.OrderName),
                shippingAddress,
                billingAddress,
                payment);

            foreach (var item in order.OrderItems)
            {
                newOrder.Add(ProductId.Of(item.ProductId), item.Quantity, item.Price);
            }
            return newOrder;
        }

        private Address CreateAddress(AddressDto address)
        {
            return Address.Of(address.FirstName, address.LastName, address.EmailAddress, address.AddressLine, address.Country, address.State, address.ZipCode);
        }

        private Payment CreatePayment(PaymentDto payment)
        {
            return Payment.Of(payment.CardName, payment.CardNumber, payment.Expiration, payment.CVV, payment.PaymentMethod);
        }
    }
}
