namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrdersByNameHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
        {
            var orderName = OrderName.Of(request.Name);
            var orders = await context.Orders
                .AsNoTracking()
                .Include(o => o.OrderItems)
                .Where(o => o.OrderName.Value.Contains(request.Name))
                .OrderBy(o =>  o.OrderName.Value)
                .ToListAsync(cancellationToken);
            return new(orders.ToOrderDtoList());
        }

    }
}
