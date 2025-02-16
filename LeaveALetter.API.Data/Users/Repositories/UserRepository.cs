using DependencyInjectionTool.Attributes;
using LeaveALetter.API.Data._Shared.Repositories;
using LeaveALetter.API.Data.Users.Entities;

namespace LeaveALetter.API.Data.Users.Repositories;

/// <summary>
/// Represents the contract for the user repository.
/// </summary>
public interface IUserRepository : IEntityRepository<UserEntity>
{
    /// <summary>
    /// Retrieves the user by the ID.
    /// </summary>
    /// <param name="Id">The name of the user.</param>
    /// <returns></returns>
    UserEntity? GetById(long Id);

    /// <summary>
    /// Retrieves the user by the user ID.
    /// </summary>
    /// <param name="name">The name of the user.</param>
    /// <param name="userId">The generated ID of the user.</param>
    /// <returns>The existing user or null if cannot found in database.</returns>
    UserEntity? GetByUserIdAndName(string userId, string name);

    /// <summary>
    /// Retrieves the user by the user ID and the password.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <returns>The existing user or null if cannot found in database.</returns>
    UserEntity? GetByUserIdndPassword(string userId, string password);
}

/// <summary>
/// Represents the accessing data for the user.
/// </summary>
[RegisteredDependency]
public class UserRepository : EntityRepository<UserEntity>, IUserRepository
{
    // TODO: Migrate data to access the data.
    private readonly List<UserEntity> _database = new List<UserEntity>
        {
            new UserEntity
            {
                Id = 1,
                Name = "John Doe",
                UserId = "1A",
                Password = "password"
            },
            new UserEntity
            {
                Id = 2,
                Name = "Ikurah New",
                UserId = "2B",
                Password = "password2"
            },
            new UserEntity
            {
                Id = 3,
                Name = "Ikurah New",
                UserId = "3C",
                Password = "password3"
            }
        };

    /// <inheritdoc />
    public UserEntity? GetById(long Id)
    {
        return base.GetById(Id, _database);
    }

    /// <inheritdoc />
    public UserEntity? GetByUserIdAndName(string userId, string name)
    {
        return _database.FirstOrDefault(user => user.UserId == userId && user.Name == name);
    }

    /// <inheritdoc />
    public UserEntity? GetByUserIdndPassword(string userId, string password)
    {
        return _database.FirstOrDefault(user => user.UserId == userId && user.Password == password);
    }
}
