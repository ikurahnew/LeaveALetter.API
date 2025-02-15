using LeaveALetter.API.Data.Users.Entities;
using LeaveALetter.API.Data.Users.Repositories;

namespace LeaveALetter.API.Core.Users.Services;

/// <summary>
/// The contract for the user services.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Retrieves and validate user by the user ID and the password.
    /// </summary>
    /// <param name="userId">The email address of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <returns>The existing user.</returns>
    UserEntity GetAndValidateByCredentials(string userId, string password);

    /// <summary>
    /// Retrieves and validate user by the user ID.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="userId">The name of the user.</param>
    /// <returns>The existing reciever.</returns>
    UserEntity GetAndValidateReceiver(string userId, string name);
}

/// <summary>
/// The service that handles the <see cref="UserEntity"/> instances.
/// </summary>
/// <param name="userRepository">The repository responsible to access the user data.</param>
public class UserService(IUserRepository userRepository) : IUserService
{
    /// <inheritdoc />
    /// <exception cref="ArgumentException">thrown when the user does not exists in the database.</exception>
    public UserEntity GetAndValidateByCredentials(string userId, string password)
    {
        return userRepository.GetByUserIdndPassword(userId, password) ??
            throw new ArgumentException("The user does not exist with the provided user ID and password.");
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentException">thrown when the user does not exists in the database.</exception>
    public UserEntity GetAndValidateReceiver(string userId, string name)
    {
        return userRepository.GetByUserIdAndName(userId, name) ??
            throw new ArgumentException("The user does not exist with the provided user ID and name.");
    }
}
