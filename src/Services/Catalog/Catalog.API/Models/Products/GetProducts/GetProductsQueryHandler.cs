
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

    /// <summary>
    /// Este clase se encarga de gestionar la consulta de GetProducts
    /// y se encarga de devolver al resultado de Getproduct
    /// </summary>
    internal class GetProductsQueryHandler
        (IDocumentSession session, ILogger<GetProductsQueryHandler>logger)
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsQueryHandler.Hanlde llamado con {@Query}", query);
            var products = await session.Query<Product>().ToListAsync(cancellationToken);
            return new GetProductsResult(products);
        }
    }
}
