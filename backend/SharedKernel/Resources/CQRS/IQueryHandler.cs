using MediatR;
using SharedKernel.Resources.API_Helper;

namespace SharedKernel.Resources.CQRS;

public interface IQueryHandler<TQuery, TResponse>: IRequestHandler<TQuery, ApiResponse<TResponse>> where TQuery : IQuery<TResponse>
{
    
}