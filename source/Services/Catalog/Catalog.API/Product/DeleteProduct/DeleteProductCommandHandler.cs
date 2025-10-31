
namespace Catalog.API.Product.DeleteProduct
{
    public record DeleteProductCommand(Guid ProductId) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess) : ICommand;
    internal class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        private ILogger<DeleteProductCommandHandler> _logger;
        private IDocumentSession _documentSession;
        public DeleteProductCommandHandler(ILogger<DeleteProductCommandHandler> logger, IDocumentSession documentSession)
        {
            _logger = logger;
            _documentSession = documentSession;
        }
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("DeleteProductCommandHandler.Handle Was Called By {@Command}", command);
            _documentSession.Delete<Models.Product>(command.ProductId);
            await _documentSession.SaveChangesAsync(cancellationToken);
            return new(true);
        }
    }
}
