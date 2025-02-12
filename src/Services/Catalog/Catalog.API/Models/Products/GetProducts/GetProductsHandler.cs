namespace Catalog.API.Models.Products.GetProducts
{
    public record GetProductsQuery() : IQuery<GetProductsResult>;

    /// <summary>
    /// Devolvemos  una lista o ienumerable de productos
    /// este record representa  la solicitud de obtencion de productos y la estructura de productos
    /// que esperamos.
    /// </summary>
    /// <param name="Products"></param>
    public record GetProductsResult(IEnumerable<Product> Products);
    public class GetProductsHandler
    {
    }
}
