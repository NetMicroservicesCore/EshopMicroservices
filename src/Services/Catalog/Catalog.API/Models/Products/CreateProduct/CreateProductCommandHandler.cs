using BuildingBlocks.CQRS;
using Marten;

namespace Catalog.API.Models.Products.CreateProduct
{

    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price): ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //1.-en este apartado crearemos el producto desde el command object
            Product producto = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
            };
            //2.-guardamos en base de datos
            session.Store(producto);
            //pesistimos los datos en la base de datos de PostGreSQL
            await session.SaveChangesAsync(cancellationToken);
            //3.-regresamos  el CreateProductResult result
            return new CreateProductResult(Guid.NewGuid());
            
        }
    }
}
