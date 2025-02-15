namespace Catalog.API.Models.Products.UpdateProducts
{
    public record UpdateProductCommand(Guid Id,string Name, List<string> Category, string Description, string ImageFile,decimal Price):
        ICommand<UpdateProductResult>;

        public record UpdateProductResult(bool IsSuccess);
    /// <summary>
    /// Esta clase se encargara de establecer el orden de actualizacion del producto.
    /// </summary>
    public class UpdateProductHandler
    {
    }
}
