using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetAllCategories
{
    public record GetCategoriesRequest(int? PageNumber = 1, int? PageSize = 10);
    public record GetCategoriesResponse(IEnumerable<string> Categories);

    public class GetCategoriesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/categories", async (ISender sender) =>
            {
                var result = await sender.Send(new GetUniqueCategoriesQuery());

                return Results.Ok(new GetCategoriesResponse(result));
            })
            .WithName("GetCategories")
            .Produces<GetCategoriesResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Categories")
            .WithDescription("Get all unique product categories");
        }
    }
}
