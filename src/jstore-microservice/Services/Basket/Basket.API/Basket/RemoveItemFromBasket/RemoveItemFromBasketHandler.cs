
namespace Basket.API.Basket.RemoveItemFromBasket
{
    public class RemoveItemFromBasketCommand : ICommand<RemoveItemFromBasketResult>
    {
        public string UserName { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string Color { get; set; }

        public RemoveItemFromBasketCommand(string userName, Guid productId, int quantity,string color)
        {
            UserName = userName;
            ProductId = productId;
            Quantity = quantity;
            Color = color;
        }
    }

    public record RemoveItemFromBasketResult(ShoppingCart Cart);
    public class RemoveItemFromBasketHandler : ICommandHandler<RemoveItemFromBasketCommand, RemoveItemFromBasketResult>
    {
        private readonly IBasketRepository _basketRepository;

        public RemoveItemFromBasketHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<RemoveItemFromBasketResult> Handle(RemoveItemFromBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.RemoveItemFromCart(
                request.UserName,
                request.ProductId,
                request.Color,
                request.Quantity,
                cancellationToken);

            return new RemoveItemFromBasketResult(basket);
        }
    }
}
