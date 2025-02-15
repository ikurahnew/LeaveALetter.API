using LeaveALetter.API.Core.Letters.Responses;
using LeaveALetter.API.Core.Users.Services;
using LeaveALetter.API.Data.Letters.Repositories;
using MapsterMapper;
using MediatR;

namespace LeaveALetter.API.Core.Letters.Queries;

/// <summary>
/// Request to retrieve letters by the author.
/// </summary>
/// <param name="UserId">The ID of the user for authentication.</param>
/// <param name="Password">The Password of the user for authentication.</param>
/// <param name="IncludeDeprecated">Whether to include the deprecated letters or not.</param>
public record GetLettersByAuthor(string UserId, string Password, bool IncludeDeprecated = false) : IRequest<ICollection<LetterResponse>>;

/// <summary>
/// The handler for the <see cref="GetLettersByAuthor"/> request.
/// </summary>
/// <param name="mapper">The mapper to map between entity and response.</param>
/// <param name="userService">The serivce responsible for retrieving user.</param>
/// <param name="letterRepository">The repository responsible for retrieving all author letters.</param>
public class GetLettersByAuthorHandler(IMapper mapper, IUserService userService, ILetterRepository letterRepository) : IRequestHandler<GetLettersByAuthor, ICollection<LetterResponse>>
{
    public async Task<ICollection<LetterResponse>> Handle(GetLettersByAuthor request, CancellationToken cancellationToken)
    {
        var existingUser = userService.GetAndValidateByCredentials(request.UserId, request.Password);

        var letters = letterRepository.GetByAuthor(existingUser, request.IncludeDeprecated);

        return mapper.Map<ICollection<LetterResponse>>(letters);
    }
}
