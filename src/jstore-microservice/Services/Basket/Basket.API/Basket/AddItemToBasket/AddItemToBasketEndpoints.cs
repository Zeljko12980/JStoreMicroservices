using System.Security.Claims;


namespace Basket.API.Basket.AddItemToBasket
{

    public class AddItemToBasketRequest
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Color { get; set; }
    }

    public class AddItemToBasketResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ShoppingCart Cart { get; set; } // Dodato za vraćanje korpe

    }

    public class AddItemToBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // POST endpoint za dodavanje artikla u korpu
            app.MapPost("/basket/items", async (
                HttpContext context, // Korišćenje HttpContext za pristup JWT korisniku
                AddItemToBasketRequest request,
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
                var command = new AddItemToCartCommand(
                    userName, // Dodaj korisničko ime
                    request.ProductId,
                    request.ProductName,
                    request.Price,
                    request.Quantity,
                    request.Color
                );

                // Poslati komandu putem MediatR-a
                var basket= await mediator.Send(command, cancellationToken);

                // Pripremiti odgovor sa informacijama o uspešnom dodavanju artikla u korpu
                var response = new AddItemToBasketResponse
                {
                    Success = true,
                    Message = "Item added to basket.",
                    Cart = basket.Cart// Vraćanje ažurirane korpe
                };

                return Results.Ok(response); // Vratiti odgovor sa statusom 200 OK
            })
            .WithName("AddItemToBasket")
            .WithSummary("Adds an item to the shopping cart")
            .WithDescription("Creates or updates an item in the user's basket")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
        }
    }
}
