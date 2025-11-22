namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        private IDocumentSession _session = session;

        public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            _session.Delete<ShoppingCart>(userName);
            await _session.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            var basket = await _session.LoadAsync<ShoppingCart>(userName, cancellationToken);
            return basket ?? throw new BasketNotFoundException(userName);
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart cart, CancellationToken cancellationToken = default)
        {
            _session.Store(cart);
            await _session.SaveChangesAsync(cancellationToken);
            return cart;
        }
    }
}
