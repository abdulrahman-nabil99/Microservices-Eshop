namespace Basket.API.Basket.CheckoutBasket
{
    public record CheckoutBasketRequest(BasketCheckoutDto Basket);
    public record CheckoutBasketResponse(bool IsSuccess);
    public class CheckoutBasketEndpointcs : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/checkout", async (ISender sender, CheckoutBasketRequest request) =>
            {
                var command = request.Adapt<CheckoutBasketCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CheckoutBasketResponse>();

                return Results.Ok(response);

            })
            .WithName("Basket Checkout")
            .Produces<CheckoutBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Basket Checkout")
            .WithDescription("Basket Checkout");
        }
    }
}
