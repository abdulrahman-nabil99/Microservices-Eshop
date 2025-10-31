
namespace Catalog.API.Product.UpdateProduct
{
    public class UpdateProductCommand : ICommand<UpdateProductResult>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageFile { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public List<string> Categories { get; set; } = new List<string>();
    }
    public record UpdateProductResult(bool IsSuccess);
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        private ILogger<UpdateProductCommandHandler> _logger;
        private IDocumentSession _documentSession;
        public UpdateProductCommandHandler(ILogger<UpdateProductCommandHandler> logger, IDocumentSession documentSession)
        {
            _logger = logger;
            _documentSession = documentSession;
        }
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("UpdateProductCommandHandler.Handle Was Called By {@command}", command);
                var product = new Models.Product()
                {
                    Id = command.Id,
                    Name = command.Name,
                    Description = command.Description,
                    Price = command.Price,
                    Categories = command.Categories,
                    ImageFile = command.ImageFile
                };
                _documentSession.Update(product);
                await _documentSession.SaveChangesAsync(cancellationToken);
                return new UpdateProductResult(true);
            }
            catch
            {
                throw new ProductNotFoundException();
            }
        }
    }
}
