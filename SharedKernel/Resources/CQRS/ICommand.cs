using MediatR;
using SharedKernel.Resources.API_Helper;

namespace SharedKernel.Resources.CQRS;

public interface ICommand: IRequest<ApiResponse>
{
}

public interface ICommand<TResponse> : IRequest<ApiResponse<TResponse>>
{
}