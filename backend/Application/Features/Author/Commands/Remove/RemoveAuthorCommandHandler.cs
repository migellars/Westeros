using Application.Contracts;
using Application.Helper.Constants;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Resources.API_Helper;
using SharedKernel.Resources.CQRS;

namespace Application.Features.Author.Commands.Remove;

internal class RemoveAuthorCommandHandler : ICommandHandler<RemoveAuthorCommand>
{
    private readonly IWesterosContext _context;

    public RemoveAuthorCommandHandler(IWesterosContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse> Handle(RemoveAuthorCommand request, CancellationToken cancellationToken)
    {
        var validate = new RemoveAuthorCommandValidator();
        await validate.ValidateAndThrowAsync(request, cancellationToken);
        // check if record is present
        var data = await _context.Author.FirstOrDefaultAsync(x => x.Id == request.Id && x.IsDeleted == false,
            cancellationToken);
        switch (data)
        {
            case null:
                return ApiResponse.GetFailed();
            default:
                _context.Author.Remove(data);
                await _context.SaveChangesAsync();
                break;
        }
        return ApiResponse.GetSuccess(null, null, BaseConstant.Deleted);

    }
}