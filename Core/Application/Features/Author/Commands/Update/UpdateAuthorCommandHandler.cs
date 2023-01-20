using Application.Contracts;
using Application.Helper.Constants;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Helpers.API_Helper;
using SharedKernel.Helpers.CQRS;

namespace Application.Features.Author.Commands.Update;

internal class UpdateAuthorCommandHandler: ICommandHandler<UpdateAuthorCommand>
{
    private readonly ILannisterContext _context;

    public UpdateAuthorCommandHandler(ILannisterContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var validate = new UpdateAuthorCommandValidator();
        await validate.ValidateAndThrowAsync(request, cancellationToken: cancellationToken);

        var getData = await _context.Author.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        if (getData is null) return ApiResponse.GetFailed();

        getData.Address = request.GetAddress();
        getData.FirstName = request.FirstName;
        getData.LastName = request.LastName;

        _context.Author.Update(getData);
        await _context.SaveChangesAsync();
        return ApiResponse.GetSuccess(null, null, BaseConstant.Updated);
    }
}