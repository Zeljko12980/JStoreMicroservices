namespace Basket.API.Basket.AddItemToBasket
{
    public class AddItemToCartCommand : ICommand<AddItemToCartResult>
    {
        public string UserName { get; init; }
        public Guid ProductId { get; init; }
        public string ProductName { get; init; }
        public decimal Price { get; init; }
        public int Quantity { get; init; }
        public string Color { get; init; }

        public AddItemToCartCommand(
            string userName,
            Guid productId,
            string productName,
            decimal price,
            int quantity,
            string color)
        {
            UserName = userName;
            ProductId = productId;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
            Color = color;
        }
    }

    public record AddItemToCartResult(ShoppingCart Cart);
    public class AddItemToCartCommandHandler : ICommandHandler<AddItemToCartCommand,AddItemToCartResult>
    {
        private readonly IBasketRepository _basketRepository;

        public AddItemToCartCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<AddItemToCartResult> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
        {
          var basket=  await _basketRepository.AddItemToCart(
                request.UserName,
                request.ProductId,
                request.ProductName,
                request.Price,
                request.Quantity,
                request.Color,
                cancellationToken
            );

            return new AddItemToCartResult(basket);
        }
    }

}
