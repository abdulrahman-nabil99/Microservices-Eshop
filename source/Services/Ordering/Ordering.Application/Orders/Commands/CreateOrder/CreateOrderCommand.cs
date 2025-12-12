namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;
    public record CreateOrderResult(Guid Id);

    public class CreateOrderCommandValidators : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidators() 
        {
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Order name is required");
            RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
            RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("Order must have one item at least");
        }
    }
}
