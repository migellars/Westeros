using MediatR;
using SharedKernel.Helpers.API_Helper;

namespace SharedKernel.Helpers.CQRS;

public interface ICommand: IRequest<ApiResponse>
{
}

public interface ICommand<TResponse> : IRequest<ApiResponse<TResponse>>
{
}