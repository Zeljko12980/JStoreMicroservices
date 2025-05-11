namespace Basket.API.Models;

public class ShoppingCart
{
    public string UserName { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }

    //Required for Mapping
    public ShoppingCart()
    {
    }

    public void AddItem(Guid productId, string productName, decimal price, int quantity, string color)
    {
        var existingItem = Items.FirstOrDefault(i => i.ProductId == productId && i.Color == color);

        if (existingItem != null)
        {
            // Ako postoji isti proizvod (po ID-u i boji), povećaj količinu
            existingItem.Quantity += quantity;
        }
        else
        {
            // Ako je novi proizvod, dodaj ga
            var newItem = new ShoppingCartItem
            {
                ProductId = productId,
                ProductName = productName,
                Price = price,
                Quantity = quantity,
                Color = color
            };

            Items.Add(newItem);
        }
    }

    public void RemoveOneItem(Guid productId, string color)
    {
        var item = Items.FirstOrDefault(i => i.ProductId == productId && i.Color == color);

        if (item != null)
        {
            item.Quantity--;

            if (item.Quantity <= 0)
            {
                Items.Remove(item);
            }
        }
    }

    public void RemoveItemCompletely(Guid productId, string color)
    {
        var item = Items.FirstOrDefault(i => i.ProductId == productId && i.Color == color);

        if (item != null)
        {
            Items.Remove(item);
        }
    }



}
