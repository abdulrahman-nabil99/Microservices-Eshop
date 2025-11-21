namespace Catalog.API.Product.DeleteProduct
{
    public record DeleteProductCommand(Guid ProductId) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess) : ICommand;
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(p => p.ProductId).NotEmpty().WithMessage("Product Id is required");
        }
    }
    internal class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        private IDocumentSession _documentSession;
        public DeleteProductCommandHandler( IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            _documentSession.Delete<Models.Product>(command.ProductId);
            await _documentSession.SaveChangesAsync(cancellationToken);
            return new(true);
        }
    }
}
