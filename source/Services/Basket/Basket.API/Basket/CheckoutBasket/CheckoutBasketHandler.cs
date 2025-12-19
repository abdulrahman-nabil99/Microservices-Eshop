using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDto Basket): ICommand<CheckoutBasketCommandResult>;
    public record CheckoutBasketCommandResult(bool IsSuccess);

    public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.Basket).NotNull().WithMessage("Basket To Checkout can't be null");
            RuleFor(x => x.Basket.UserName).NotEmpty().WithMessage("CustomerId is required");
        }
    }
    public class CheckoutBasketCommandHandler(IBasketRepository basketRepository, IPublishEndpoint publishEndpoint) 
        : ICommandHandler<CheckoutBasketCommand, CheckoutBasketCommandResult>
    {
        public async Task<CheckoutBasketCommandResult> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetBasketAsync(request.Basket.UserName, cancellationToken);

            if(basket is null)
            {
                return new (false);
            }

            var eventMessage = request.Basket.Adapt<BasketCheckoutEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;

            await publishEndpoint.Publish(eventMessage, cancellationToken);

            await basketRepository.DeleteBasketAsync(request.Basket.UserName, cancellationToken);

            return new (true);
        }
    }
}
