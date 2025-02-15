using LeaveALetter.API.Core.Users.Responses;

namespace LeaveALetter.API.Core.Users.Models;

/// <summary>
/// Request for the receiver information a new user.
/// </summary>
/// <param name="Name"><inheritdoc cref="IUserResponse.Name" path="/summary"/></param>
/// <param name="UserId"><inheritdoc cref="IUserResponse.UserId" path="/summary"/></param>
public record ReceiverRequest(
    string Name,
    string UserId
) : IUserResponse;
