namespace Catalog.API.Product.GetProductByCategory
{
    public record GetProductByCategoryResult(IEnumerable<Models.Product> Products);
    public record GetProductsByCategoryQuery(string CategoryName) : IQuery<GetProductByCategoryResult>;
    internal class GetProductsByCategoryQueryHandler : IQueryHandler<GetProductsByCategoryQuery, GetProductByCategoryResult>
    {
        private IDocumentSession _documentSession;
        private ILogger<GetProductsByCategoryQueryHandler> _logger;
        public GetProductsByCategoryQueryHandler(IDocumentSession documentSession, ILogger<GetProductsByCategoryQueryHandler> logger)
        {
            _documentSession = documentSession;
            _logger = logger;
        }
        public async Task<GetProductByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetProductsByCategoryResult.Handle was Called By {@Query}", query);
            var products = await _documentSession.Query<Models.Product>()
                .Where(p => p.Categories.Contains(query.CategoryName))
                .ToListAsync(cancellationToken);
            return new GetProductByCategoryResult(products);
        }
    }
}
