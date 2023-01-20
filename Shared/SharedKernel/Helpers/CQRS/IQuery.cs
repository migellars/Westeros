using MediatR;
using SharedKernel.Helpers.API_Helper;

namespace SharedKernel.Helpers.CQRS;

public interface IQuery<TResponse> : IRequest<ApiResponse<TResponse>> 
{
    
}