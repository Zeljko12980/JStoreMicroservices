using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data;

public class CachedBasketRepository
    (IBasketRepository repository, IDistributedCache cache) 
    : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
        if (!string.IsNullOrEmpty(cachedBasket))
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;

        var basket = await repository.GetBasket(userName, cancellationToken);
        await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        await repository.StoreBasket(basket, cancellationToken);

        await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);

        return basket;
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        await repository.DeleteBasket(userName, cancellationToken);

        await cache.RemoveAsync(userName, cancellationToken);

        return true;
    }

    public async Task<ShoppingCart> AddItemToCart(
    string userName,
    Guid productId,
    string productName,
    decimal price,
    int quantity,
    string color,
    CancellationToken cancellationToken = default)
    {
        // Dohvati korpu iz keša ili baze
        var basket = await GetBasket(userName, cancellationToken);

        // Ako korpa ne postoji, kreiraj novu
        if (basket == null)
        {
            basket = new ShoppingCart
            {
                UserName = userName,
                Items = new List<ShoppingCartItem>()
            };
        }

        // Dodaj novi artikal ili ažuriraj postojeći
        var existingItem = basket.Items.FirstOrDefault(i => i.ProductId == productId && i.Color == color);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
            existingItem.Price = price; // ako želiš da cijena bude zadnja upisana
        }
        else
        {
            basket.Items.Add(new ShoppingCartItem
            {
                ProductId = productId,
                ProductName = productName,
                Price = price,
                Quantity = quantity,
                Color = color
            });
        }

        // Sačuvaj korpu u bazi i kešu
        await StoreBasket(basket, cancellationToken);

        return basket;
    }

    public async Task<ShoppingCart> RemoveItemFromCart(
         string userName,
         Guid productId,
         string color,
         int quantity,
         CancellationToken cancellationToken = default)
    {
        var basket = await GetBasket(userName, cancellationToken);

        if (basket == null)
        {
            throw new Exception("Basket not found for the user.");
        }

        var existingItem = basket.Items.FirstOrDefault(i => i.ProductId == productId && i.Color == color);
        if (existingItem == null)
        {
            throw new Exception("Item not found in the basket.");
        }

        // If the quantity to be removed is greater than the quantity in the basket, throw an error
        if (existingItem.Quantity < quantity)
        {
            throw new Exception("Quantity to remove is greater than the available quantity in the basket.");
        }

        existingItem.Quantity -= quantity;

        // If quantity is zero or less, remove the item
        if (existingItem.Quantity <= 0)
        {
            basket.Items.Remove(existingItem);
        }

        // Save updated basket in the repository and cache
        await StoreBasket(basket, cancellationToken);

        return basket;
    }
}
