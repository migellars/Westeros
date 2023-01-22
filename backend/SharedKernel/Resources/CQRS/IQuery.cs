using MediatR;
using SharedKernel.Resources.API_Helper;

namespace SharedKernel.Resources.CQRS;

public interface IQuery<TResponse> : IRequest<ApiResponse<TResponse>> 
{
    
}