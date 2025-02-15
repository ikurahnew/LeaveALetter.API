using LeaveALetter.API.Core.Letters.Models;
using LeaveALetter.API.Core.Letters.Responses;
using LeaveALetter.API.Core.Users.Services;
using LeaveALetter.API.Data.Letters.Entities;
using LeaveALetter.API.Data.Letters.Repositories;
using MapsterMapper;
using MediatR;

namespace LeaveALetter.API.Core.Letters.Commands;

/// <summary>
/// The command for creating a new letter.
/// </summary>
/// <param name="senderUserId">The sender User ID to authenticate user.</param>
/// <param name="senderPassword">The sender password to authenticate user.</param>
/// <param name="letterRequest">The request to create a letter.</param>
public record CreateLetter(string senderUserId, string senderPassword, LetterRequest letterRequest) : IRequest<LetterResponse>;

/// <summary>
/// The handler for creating a new letter.
/// </summary>
/// <param name="mapper">The mapper to map between entity and response.</param>
/// <param name="userService">The service responsible to retrieves and validate user.</param>
/// <param name="letterRepository">The repository responsible to create letter.</param>
public class CreateLetterHandler(IMapper mapper, IUserService userService, ILetterRepository letterRepository) : IRequestHandler<CreateLetter, LetterResponse>
{
    /// <summary>
    /// Handles the creation of a new letter.
    /// </summary>
    /// <param name="command">The command to create a letter.</param>
    /// <returns>A created letter response.</returns>
    public async Task<LetterResponse> Handle(CreateLetter command, CancellationToken cancellationToken = default)
    {
        var existingUser = userService.GetAndValidateByCredentials(command.senderUserId, command.senderPassword);
        var reviever = command.letterRequest.Reciever is null ? null : userService.GetAndValidateReceiver(command.letterRequest.Reciever.UserId, command.letterRequest.Reciever.Name);

        var letter = new LetterEntity
        {
            Title = command.letterRequest.Title,
            Content = command.letterRequest.Content,
            Author = existingUser,
            Reciever = reviever,
            CreatedDate = DateTime.UtcNow
        };

        var createdLetter = letterRepository.Create(letter);

        return mapper.Map<LetterResponse>(createdLetter);
    }

}
