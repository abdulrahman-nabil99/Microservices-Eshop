
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
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage("Product Id is required");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Product Name is required")
                .Length(2,30).WithMessage("Product Name Must be between 2 and 30 characters");
            RuleFor(p => p.Categories).NotEmpty().WithMessage("At least one category must be selected");
            RuleFor(p => p.Price).NotEmpty().GreaterThan(0).WithMessage("Price must be greated than 0");
            RuleFor(p => p.ImageFile).NotEmpty().WithMessage("Product image is required");
        }
    }
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        private IDocumentSession _documentSession;
        public UpdateProductCommandHandler(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
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
                throw new ProductNotFoundException(command.Id);
            }
        }
    }
}
