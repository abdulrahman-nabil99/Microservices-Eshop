namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var count = await context.Orders.LongCountAsync(cancellationToken);
            var orders = await context.Orders
                .AsNoTracking()
                .Include(o => o.OrderItems)
                .OrderBy(o => o.Id)
                .Skip(request.Request.PageIndex * request.Request.PageSize)
                .Take(request.Request.PageSize)
                .ToListAsync(cancellationToken);
            return new(new(request.Request.PageIndex, request.Request.PageSize, count, orders.ToOrderDtoList()));
        }
    }
}
