
using System.Security.Claims;

namespace Basket.API.Basket.RemoveItemFromBasket
{
    public record RemoveItemFromBasketRequest(Guid ProductId, int Quantity, string Color);

    public record RemoveItemFromBasketResponse(bool Success, string Message, ShoppingCart Cart);
    public class RemoveItemFromBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/items/remove", async (
                 HttpContext context, // Korišćenje HttpContext za pristup JWT korisniku
                 RemoveItemFromBasketRequest request,
                 ISender mediator,
                 CancellationToken cancellationToken) =>
            {
                // Dobijanje korisničkog imena iz JWT tokena
                var userName = context.User?.FindFirst(ClaimTypes.Name)?.Value;

                if (userName == null)
                {
                    return Results.Unauthorized(); // Ako nema korisničkog imena u tokenu, vrati Unauthorized
                }

                // Kreiraj novu komandu sa ažuriranim korisničkim imenom
                var command = new RemoveItemFromBasketCommand(userName, request.ProductId, request.Quantity, request.Color);

                // Poslati komandu putem MediatR-a
                var basket = await mediator.Send(command, cancellationToken);

                // Pripremiti odgovor sa informacijama o uspešnom uklanjanju artikla
                var response = new RemoveItemFromBasketResponse(
                    Success: true,
                    Message: "Item removed or updated in basket.",
                    Cart: basket.Cart
                );

                return Results.Ok(response); // Vratiti odgovor sa statusom 200 OK
            })
             .RequireAuthorization()
             .WithName("RemoveItemFromBasket")
             .WithSummary("Removes or updates an item in the user's shopping cart")
             .WithDescription("Removes a specified quantity of an item from the basket or removes the item if quantity becomes zero.")
             .Produces<RemoveItemFromBasketResponse>(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status400BadRequest)
             .Produces(StatusCodes.Status404NotFound);
        }
    }
}
