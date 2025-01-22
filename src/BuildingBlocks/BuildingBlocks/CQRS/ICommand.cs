using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.CQRS
{

    /// <summary>
    /// esta interfaz no es generica y no devuelve ningun tipo de valor
    /// </summary>
    public interface ICommand : ICommand<Unit>
    {
        
    }
    /// <summary>
    /// Esta interfaz es generica y devuelve una respuesta generica
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface ICommand<out TResponse>: IRequest<TResponse>
    {
    }
}
