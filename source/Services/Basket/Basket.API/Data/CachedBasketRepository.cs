using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
    {
        public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            var result = await repository.DeleteBasketAsync(userName, cancellationToken); ;
            if (result)
            {
                await cache.RemoveAsync(userName,cancellationToken);
            }
            return result;
        }

        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            var cachedBasekt = await cache.GetStringAsync(userName, cancellationToken);
            if (!string.IsNullOrEmpty(cachedBasekt))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasekt) ?? throw new BasketNotFoundException(userName);
            }
            var basket = await repository.GetBasketAsync(userName, cancellationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart cart, CancellationToken cancellationToken = default)
        {
            var basket = await repository.StoreBasketAsync(cart, cancellationToken); ;
            await cache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }
    }
}
