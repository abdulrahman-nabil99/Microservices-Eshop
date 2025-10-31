namespace Catalog.API.Product.CreateProduct
{
    public class CreateProductCommand : ICommand<CreateProductResult>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageFile { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public List<string> Categories { get; set; } = new List<string>();
    }
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand,CreateProductResult>
    {
        private IDocumentSession _documentSession;
        public CreateProductCommandHandler(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Create Product Form Command
            var product = new Models.Product()
            {
                Name = command.Name,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
                Categories = command.Categories
            };
            // Add To Database And Save
            _documentSession.Store(product);
            // Return Command Result
            await _documentSession.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(product.Id);
        }
    }
}
