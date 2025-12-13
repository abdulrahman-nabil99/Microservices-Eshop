namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler(IApplicationDbContext context) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            // Check Order Exist
            var orderId = OrderId.Of(request.Order.Id);
            var order = await context.Orders.FindAsync([orderId], cancellationToken: cancellationToken) 
                ?? throw new OrderNotFoundException(request.Order.Id);

            // Update Order Entity From Command Object
            UpdateOrderWithNewValues(order, request.Order);

            // Save order in the database
            context.Orders.Update(order);
            int result = await context.SaveChangesAsync(cancellationToken);

            // Return result
            return new(result > 0);
        }

        private void UpdateOrderWithNewValues(Order order, OrderDto newValues)
        {
            var shipplingAddress = CreateAddress(newValues.ShippingAddress);
            var billingAddress = CreateAddress(newValues.BillingAddress);
            var payment = CreatePayment(newValues.Payment);

            order.Update(
                OrderName.Of(newValues.OrderName),
                shipplingAddress,
                billingAddress,
                payment,
                newValues.Status
                );
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
