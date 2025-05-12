
namespace Basket.API.Data;

public class BasketRepository(IDocumentSession session)
    : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        var basket = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);

        return basket is null ? null : basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        session.Store(basket);
        await session.SaveChangesAsync(cancellationToken);
        return basket;
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        session.Delete<ShoppingCart>(userName);
        await session.SaveChangesAsync(cancellationToken);
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
        // Pokušaj da dobiješ postojeću korpu
        var basket = await GetBasket(userName, cancellationToken);

        // Ako ne postoji, kreiraj novu
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
            existingItem.Price = price; // možeš i izostaviti ako ne želiš da ažuriraš cijenu
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

        // Sačuvaj promjene
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
