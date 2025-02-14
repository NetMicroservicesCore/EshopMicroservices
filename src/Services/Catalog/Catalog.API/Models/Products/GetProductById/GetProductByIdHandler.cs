namespace Catalog.API.Models.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id):IQuery<GetProductByIdResult>;
    public class GetProductByIdHandler
    {
    }
}
