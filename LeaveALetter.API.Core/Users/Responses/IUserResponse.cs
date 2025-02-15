using LeaveALetter.API.Data.Users.Entities;

namespace LeaveALetter.API.Core.Users.Responses;

/// <summary>
/// Representing the contract for the <see cref="UserEntity"/> instance.
/// </summary>
public interface IUserResponse
{
    /// <inheritdoc cref="UserEntity.Name"/>
    public string Name { get; init; }

    /// <inheritdoc cref="UserEntity.UserId"/>
    public string UserId { get; init; }
}
