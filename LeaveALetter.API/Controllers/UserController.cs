using LeaveALetter.API.Core.Users.Commands;
using LeaveALetter.API.Core.Users.Models;
using LeaveALetter.API.Core.Users.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LeaveALetter.API.Controllers;

/// <summary>
/// The controller for the user actions.
/// </summary>
[ApiController]
[Route("api/user")] 
public class UserController(IMediator mediator) : Controller
{
    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="request">The request to create user.</param>
    /// <returns>A created user.</returns>
    [HttpPost, Route("")]
    public async Task<UserResponse> Register(RegisterUserRequest request)
    {
        var user = await mediator.Send(new CreateUser(request), default);
        
        return user;
    }
}
