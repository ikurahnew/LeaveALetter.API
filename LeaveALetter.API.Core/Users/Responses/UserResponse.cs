namespace LeaveALetter.API.Core.Users.Responses;

/// <summary>
/// Representing the response for the user.
/// </summary>
/// <param name="Name"><inheritdoc cref="IUserResponse.Name" path="/summary"/></param>
public record UserResponse(
    string Name,
    string UserId
) : IUserResponse;
