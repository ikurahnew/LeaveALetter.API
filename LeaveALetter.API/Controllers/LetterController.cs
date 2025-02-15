using LeaveALetter.API.Core.Letters.Commands;
using LeaveALetter.API.Core.Letters.Models;
using LeaveALetter.API.Core.Letters.Queries;
using LeaveALetter.API.Core.Letters.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LeaveALetter.API.Controllers;

/// <summary>
/// The controller for the letter action.
/// </summary>
[ApiController]
[Route("api/letter")]
public class LetterController(IMediator mediator) : Controller
{
    /// <summary>
    /// Create a new letter.
    /// </summary>
    /// <param name="userId">The ID of the sender.</param>
    /// <param name="password">The password of the sender.</param>
    /// <param name="request">The request to create a letter.</param>
    /// <returns>A created letter.</returns>
    [HttpPost, Route("")]
    public async Task<LetterResponse> Send(string userId, string password, LetterRequest request)
    {
        var letter = await mediator.Send(new CreateLetter(userId, password, request), default);

        return letter;
    }

    /// <summary>
    /// Create a new letter.
    /// </summary>
    /// <param name="userId">The ID of the author.</param>
    /// <param name="password">The password of the author.</param>
    /// <returns>A list of letters by the author.</returns>
    [HttpGet, Route("author")]
    public async Task<ICollection<LetterResponse>> GetByAuthor(string userId, string password, bool includeDeprecated = false)
    {
        var letters = await mediator.Send(new GetLettersByAuthor(userId, password, includeDeprecated), default);

        return letters;
    }

    /// <summary>
    /// Create a new letter.
    /// </summary>
    /// <param name="userId">The ID of the reciever.</param>
    /// <param name="password">The password of the reciever.</param>
    /// <returns>A list of letters by the author.</returns>
    [HttpGet, Route("reciever")]
    public async Task<ICollection<LetterResponse>> GetByReciever(string userId, string password, string recieverId, string recieverName, bool includeDeprecated = false)
    {
        var letters = await mediator.Send(new GetLettersByReciever(userId, password, recieverId, recieverName, includeDeprecated), default);

        return letters;
    }
}
