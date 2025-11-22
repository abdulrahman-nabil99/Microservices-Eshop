namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketCommandResult>; 
    public record StoreBasketCommandResult(string UserName);
    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(c => c.Cart).NotNull().WithMessage("Cart Can not be null");
            RuleFor(c => c.Cart.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }
    public class StoreBasketCommandHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketCommandResult>
    {
        public async Task<StoreBasketCommandResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart cart = command.Cart;
            var result = await repository.StoreBasketAsync(cart, cancellationToken);
            return new(result.UserName);
        }
    }
}
