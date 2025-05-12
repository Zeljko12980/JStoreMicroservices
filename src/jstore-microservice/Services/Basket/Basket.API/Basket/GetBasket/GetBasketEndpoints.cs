using System.Security.Claims;
using Basket.API.Basket.StoreBasket;

namespace Basket.API.Basket.GetBasket;

//public record GetBasketRequest(string UserName); 
public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket", async (HttpContext httpContext, ISender sender) =>
        {
            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Results.Unauthorized();

            var result = await sender.Send(new GetBasketQuery(userId));

            var response = result.Adapt<GetBasketResponse>();

            return Results.Ok(response);
        })
          .RequireAuthorization()
          .WithName("GetBasketByUser")
          .Produces<GetBasketResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithSummary("Get Basket by Token")
          .WithDescription("Gets the basket for the currently authenticated user based on JWT token.");



    }
}
