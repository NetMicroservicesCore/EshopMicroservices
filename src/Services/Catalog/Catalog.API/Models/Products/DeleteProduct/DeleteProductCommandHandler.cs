
namespace Catalog.API.Models.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id):ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool IsSuccess);
    internal class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
