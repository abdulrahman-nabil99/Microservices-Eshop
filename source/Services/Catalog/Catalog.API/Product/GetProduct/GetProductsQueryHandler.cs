namespace Catalog.API.Product.GetProduct
{
    public record GetProductsResult(IEnumerable<Models.Product> Products);
    public record GetProductsQuery() : IQuery<GetProductsResult>;

    internal class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        private IDocumentSession _documentSession;
        private ILogger<GetProductsQueryHandler> _logger;
        public GetProductsQueryHandler(IDocumentSession documentSession, ILogger<GetProductsQueryHandler> logger)
        {
            _documentSession = documentSession;
            _logger = logger;
        }
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetProductsQueryHandler.Handle Called with {@Query}", query);
            var products = await _documentSession.Query<Models.Product>().ToListAsync();
            return new(products);
        }

    }
}
