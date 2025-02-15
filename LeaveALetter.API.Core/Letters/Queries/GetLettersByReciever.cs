using LeaveALetter.API.Core.Letters.Responses;
using LeaveALetter.API.Core.Users.Services;
using LeaveALetter.API.Data.Letters.Repositories;
using MapsterMapper;
using MediatR;

namespace LeaveALetter.API.Core.Letters.Queries;

/// <summary>
/// Request to retrieve letters by the reciever.
/// </summary>
/// <param name="UserId">The ID of the user for authentication.</param>
/// <param name="Password">The Password of the user for authentication.</param>
/// <param name="RecieverId">The ID of the reciever.</param>
public record GetLettersByReciever(string UserId, string Password, string RecieverId, string RecieverName, bool IncludeDeprecated = false) : IRequest<ICollection<LetterResponse>>;

/// <summary>
/// The handler for the <see cref="GetLettersByReciever"/> request.
/// </summary>
/// <param name="mapper">The mapper to map between entity and response.</param>
/// <param name="userService">The serivce responsible for retrieving user.</param>
/// <param name="letterRepository">The repository responsible for retrieving all reciever letters.</param>
public class GetLettersByRecieverHandler(IMapper mapper, IUserService userService, ILetterRepository letterRepository) : IRequestHandler<GetLettersByReciever, ICollection<LetterResponse>>
{
    public async Task<ICollection<LetterResponse>> Handle(GetLettersByReciever request, CancellationToken cancellationToken)
    {
        var existingUser = userService.GetAndValidateByCredentials(request.UserId, request.Password);
        var reciever = userService.GetAndValidateReceiver(request.RecieverId, request.RecieverName);

        var letters = letterRepository.GetByReciever(existingUser, reciever, request.IncludeDeprecated);

        return mapper.Map<ICollection<LetterResponse>>(letters);
    }
}
