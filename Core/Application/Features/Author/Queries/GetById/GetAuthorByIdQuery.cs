using Application.Dto;
using SharedKernel.Helpers.CQRS;

namespace Application.Features.Author.Queries.GetById;

public class GetAuthorByIdQuery: IQuery<AuthorDto>
{
    public GetAuthorByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}