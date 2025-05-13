using GrpcContracts;
using Grpc.Core;
using Marten;
using Catalog.API.Models;


// Ensure that the GrpcContracts namespace contains the ProductAvailabilityRequest and ProductAvailabilityResponse types.
// If these types are defined in a different namespace or assembly, update the using directive accordingly.

public class ProductServiceGrpc : ProductService.ProductServiceBase
{
    private readonly IDocumentSession _session;

    public ProductServiceGrpc(IDocumentSession session)
    {
        _session = session;
    }

    public override async Task<ProductAvailabilityResponse> CheckProductAvailability(ProductAvailabilityRequest request, ServerCallContext context)
    {

        // Pronađi proizvod po ID-u
        if (!Guid.TryParse(request.ProductId, out var productId))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid ProductId format."));
        }

        // Učitavanje proizvoda
        var product = await _session.LoadAsync<Product>(productId);

        if (product is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Product with ID {productId} not found."));
        }


        // Provjeri da li je dostupna tražena količina
        bool isAvailable = product.StockQuantity >= request.RequestedQuantity;

        if (isAvailable)
        {
            // Smanji dostupnu količinu i sačuvaj u bazu
            product.StockQuantity -= request.RequestedQuantity;

            _session.Store(product); // dodaj u session
            await _session.SaveChangesAsync(); // sačuvaj promjene

        }


        return new ProductAvailabilityResponse
        {
            IsAvailable = isAvailable,
            AvailableQuantity = product.StockQuantity
        };

    }

}
