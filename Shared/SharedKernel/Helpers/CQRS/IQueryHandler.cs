using MediatR;
using SharedKernel.Helpers.API_Helper;

namespace SharedKernel.Helpers.CQRS;

public interface IQueryHandler<TQuery, TResponse>: IRequestHandler<TQuery, ApiResponse<TResponse>> where TQuery : IQuery<TResponse>
{
    
}