namespace Basket.API.Data;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default);
    Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default);
    Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default);
    Task<ShoppingCart> AddItemToCart(string userName, Guid productId, string productName, decimal price, int quantity, string color, CancellationToken cancellationToken = default);
    Task<ShoppingCart> RemoveItemFromCart(string userName, Guid productId, string color, int quantity, CancellationToken cancellationToken = default);
}
     

