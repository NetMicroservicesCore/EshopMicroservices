using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.CQRS
{
    /// <summary>
    /// Esta interfaz gestiona las solicitudes de consulta y se encarga de que cada consulta corresponda a el tipo de
    /// respuesta solicitado.
    /// </summary>
    /// <typeparam name="TQuery">la consulta generica</typeparam>
    /// <typeparam name="TResponse">la respuesta generica</typeparam>
    public interface IQueryHandler<in TQuery, TResponse>: IRequestHandler<TQuery,TResponse> where TQuery: IQuery<TResponse> where TResponse: notnull
    {

    }
}
