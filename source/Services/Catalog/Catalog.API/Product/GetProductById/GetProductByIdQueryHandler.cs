namespace Catalog.API.Product.GetProductById
{
    public record GetProductByIdResult(Models.Product Product);
    public record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductByIdResult>;

    internal class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        private IDocumentSession _documentSession;
        public GetProductByIdQueryHandler(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await _documentSession.LoadAsync<Models.Product>(query.ProductId, cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException(query.ProductId);
            }
            return new GetProductByIdResult(product);
        }
    }
}
