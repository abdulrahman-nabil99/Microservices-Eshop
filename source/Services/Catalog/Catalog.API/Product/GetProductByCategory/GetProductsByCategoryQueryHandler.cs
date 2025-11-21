namespace Catalog.API.Product.GetProductByCategory
{
    public record GetProductByCategoryResult(IEnumerable<Models.Product> Products);
    public record GetProductsByCategoryQuery(string CategoryName) : IQuery<GetProductByCategoryResult>;
    internal class GetProductsByCategoryQueryHandler : IQueryHandler<GetProductsByCategoryQuery, GetProductByCategoryResult>
    {
        private IDocumentSession _documentSession;
        public GetProductsByCategoryQueryHandler(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }
        public async Task<GetProductByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            var products = await _documentSession.Query<Models.Product>()
                .Where(p => p.Categories.Contains(query.CategoryName))
                .ToListAsync(cancellationToken);
            return new GetProductByCategoryResult(products);
        }
    }
}
