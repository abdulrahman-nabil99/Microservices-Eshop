using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints
{
    //public record GetOrdersRequest(PaginationRequest Request);
    public record GetOrdersResponse(PaginatedResult<OrderDto> Result);

    public class GetOrders : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var query = new GetOrdersQuery(request);
                var result = await sender.Send(query);
                var response = result.Adapt<GetOrdersByNameResponse>();

                return Results.Ok(response);
            })
            .WithName("GetOrders")
            .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Orders")
            .WithDescription("Get Orders");
        }
    }
}
