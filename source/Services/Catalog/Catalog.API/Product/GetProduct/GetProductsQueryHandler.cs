namespace Catalog.API.Product.GetProduct
{
    public record GetProductsResult(IEnumerable<Models.Product> Products);
    public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;

    internal class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        private IDocumentSession _documentSession;
        public GetProductsQueryHandler(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await _documentSession.Query<Models.Product>().ToPagedListAsync(query.PageNumber ?? 1,query.PageSize ?? 10, cancellationToken);
            return new(products);
        }

    }
}
