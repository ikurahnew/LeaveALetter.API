namespace LeaveALetter.API.Core.Users.Models;

/// <summary>
/// Request for registering a new user.
/// </summary>
/// <param name="Name">The name of the user.</param>
/// <param name="Password">The password to create the user.</param>
public record RegisterUserRequest(
    string Name,
    string Password,
    string UserId
);
