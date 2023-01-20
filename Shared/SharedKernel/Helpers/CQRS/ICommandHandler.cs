using MediatR;
using SharedKernel.Helpers.API_Helper;

namespace SharedKernel.Helpers.CQRS;

public interface ICommandHandler<TCommand>: IRequestHandler<TCommand, ApiResponse> where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, ApiResponse<TResponse>>
    where TCommand : ICommand<TResponse>
{
}