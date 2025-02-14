
namespace Catalog.API.Models.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id):IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product Product);

    internal class GetProductByIdHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public Task<GetProductByIdResult> Handle(GetProductByIdQuery queryt, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
