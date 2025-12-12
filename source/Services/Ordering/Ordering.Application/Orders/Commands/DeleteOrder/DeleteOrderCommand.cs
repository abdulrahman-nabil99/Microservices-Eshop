namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public record DeleteOrderCommand(Guid OrderId) : ICommand<DeleteOrderResult>;

    public record DeleteOrderResult(bool IsSuccess);

    public class DeleteOrderCommandValidators : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidators()
        {
            RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId is required");
        }
    }
}
