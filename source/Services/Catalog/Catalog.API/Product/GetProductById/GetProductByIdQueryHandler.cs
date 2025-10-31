namespace Catalog.API.Product.GetProductById
{
    public record GetProductByIdResult(Models.Product Product);
    public record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductByIdResult>;

    internal class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        private IDocumentSession _documentSession;
        private ILogger<GetProductByIdQueryHandler> _logger;
        public GetProductByIdQueryHandler(IDocumentSession documentSession, ILogger<GetProductByIdQueryHandler> logger)
        {
            _documentSession = documentSession;
            _logger = logger;
        }
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetProductByIdQuery.Handle was Called By {@Query}", query);
            var product = await _documentSession.LoadAsync<Models.Product>(query.ProductId, cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException();
            }
            return new GetProductByIdResult(product);
        }
    }
}
