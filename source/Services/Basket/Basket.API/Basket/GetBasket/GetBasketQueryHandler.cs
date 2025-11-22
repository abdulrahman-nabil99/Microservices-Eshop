namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketQueryResult>;
    public record GetBasketQueryResult(ShoppingCart Cart);
    public class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketQueryResult>
    {
        public async Task<GetBasketQueryResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var result = await repository.GetBasketAsync(query.UserName, cancellationToken);
            return new GetBasketQueryResult(result);
        }
    }
}
