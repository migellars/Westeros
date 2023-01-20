using Application.Dto;
using SharedKernel.Helpers.CQRS;

namespace Application.Features.Auth.Queries.User.GetById;

public class GetUserByIdQuery: IQuery<UserDto>
{
    public GetUserByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}