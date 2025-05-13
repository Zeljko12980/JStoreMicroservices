using System.Security.Claims;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketRequest(
    decimal TotalPrice,
    string FirstName,
    string LastName,
    string EmailAddress,
    string AddressLine,
    string Country,
    string State,
    string ZipCode,
    string CardName,
    string CardNumber,
    string Expiration,
    string CVV,
    int PaymentMethod
    );
public record CheckoutBasketResponse(bool IsSuccess);

public class CheckoutBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/checkout", async (HttpContext context,CheckoutBasketRequest request, ISender sender) =>
        {
            var userName = context.User?.FindFirst(ClaimTypes.Name)?.Value;
            var userId = context.User?.FindFirst("UserId")?.Value;


            var command = new BasketCheckoutDto()
            {
                 UserName=userName,
                CustomerId= Guid.Parse(userId),
                TotalPrice= request.TotalPrice,
                FirstName=request.FirstName,
                LastName= request.LastName,
                EmailAddress= request.EmailAddress,
                AddressLine= request.AddressLine,
                Country=request.Country,
                State= request.State,
                ZipCode= request.ZipCode,
                CardName= request.CardName,
                CardNumber= request.CardNumber,
                Expiration= request.Expiration,
                CVV= request.CVV,
                PaymentMethod= request.PaymentMethod

            };
            var result = await sender.Send(new CheckoutBasketCommand(command));

            var response = result.Adapt<CheckoutBasketResponse>();

            return Results.Ok(response);
        })
        .WithName("CheckoutBasket")
        .Produces<CheckoutBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Checkout Basket")
        .WithDescription("Checkout Basket");
    }
}
