
namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderHandler(IApplicationDbContext context) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            // Check Order Exist
            var orderId = OrderId.Of(request.OrderId);
            var order = await context.Orders.FindAsync([orderId], cancellationToken: cancellationToken)
                ?? throw new OrderNotFoundException(request.OrderId);

            // Delete Order From database and save
            context.Orders.Remove(order);
            var result = await context.SaveChangesAsync(cancellationToken);

            return new(result > 0);
        }
    }
}
