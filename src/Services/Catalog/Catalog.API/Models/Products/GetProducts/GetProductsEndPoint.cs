
namespace Catalog.API.Models.Products.GetProducts
{
    public record GetProductsResponse(IEnumerable<Product> Products);
    public class GetProductsEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) => {

                var result = await sender.Send(new GetProductsQuery());
            })
        }
    }
}
