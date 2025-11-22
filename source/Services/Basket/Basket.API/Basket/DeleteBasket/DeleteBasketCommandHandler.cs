namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketCommandResult>;
    public record DeleteBasketCommandResult(bool IsSuccess);
    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(c => c.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }
    public class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketCommandResult>
    {
        public async Task<DeleteBasketCommandResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteBasketAsync(request.UserName, cancellationToken);
            return new DeleteBasketCommandResult(result);
        }
    }
}
