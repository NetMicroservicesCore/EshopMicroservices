using Carter;
using Mapster;
using MediatR;

namespace Catalog.API.Models.Products.CreateProduct
{
    
    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    
    public record CreateProductResponse(Guid id);
    public class CreateProductEndPoint : ICarterModule
    {
        /// <summary>
        /// definimos el metodo que se va a encargar de  crear las rutas de nuestro minimal api
        /// </summary>
        /// <param name="app"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                //utilizamos mapster para  convertir la peticion en un objeto del tipo  command
                var command = request.Adapt<CreateProductCommand>();
                //enviamos la peticion del comando transformado al MediaTr
                //este comando sera el que inicia el mediador y activa la clase que lo va a manejar
                var result = await sender.Send(command);
                ///almacenamos la respuesta de crear producto  y asi obtener los resultados obtenidos
                var response = result.Adapt<CreateProductResponse>();
                //creamos  el metodo que se encarga de devolver una respuesta
                return Results.Created($"/products/{response.id}", response);
            })
                .WithName("CreateProduct")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Creacion de Productos")
                .WithDescription("Crear Producto");
            
        }
    }
}
