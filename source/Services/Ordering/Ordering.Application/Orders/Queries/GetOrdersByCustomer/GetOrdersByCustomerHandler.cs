namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public class GetOrdersByCustomerHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
    {
        public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
        {
            var customerId = CustomerId.Of(request.CustomerId);
            var orders = await context.Orders
                .AsNoTracking()
                .Include(o => o.OrderItems)
                .Where(o => o.CustomerId == customerId)
                .OrderBy(o => o.OrderName.Value)
                .ToListAsync(cancellationToken);
            return new(orders.ToOrderDtoList());
        }
    }

}
