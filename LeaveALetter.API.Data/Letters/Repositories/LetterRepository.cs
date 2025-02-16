using DependencyInjectionTool.Attributes;
using LeaveALetter.API.Data._Shared.Repositories;
using LeaveALetter.API.Data.Letters.Entities;
using LeaveALetter.API.Data.Users.Entities;

namespace LeaveALetter.API.Data.Letters.Repositories;

/// <summary>
/// Represents the contract for the letter repository.
/// </summary>
public interface ILetterRepository : IEntityRepository<LetterEntity>
{
    /// <summary>
    /// Retrieves the letter by the ID.
    /// </summary>
    /// <param name="Id">The ID of the letter.</param>
    /// <returns>The existing letter or null if not found in database.</returns>
    LetterEntity? GetById(long Id);

    /// <summary>
    /// Get the letter by the author.
    /// </summary>
    /// <param name="user">The <see cref="UserEntity"/> instance.</param>
    /// <param name="IncludeDeprecated">Whether to retrieve the deprecated letter or not.</param>
    /// <returns>All the letter from the author.</returns>
    ICollection<LetterEntity> GetByAuthor(UserEntity user, bool IncludeDeprecated = false);

    /// <summary>
    /// Get the letter by the reciever.
    /// </summary>
    /// <param name="user">The <see cref="UserEntity"/> instance of the sender..</param>
    /// <param name="reciever">The <see cref="UserEntity"/> instance of the reciever.</param>
    /// <param name="IncludeDeprecated">Whether to retrieve the deprecated letter or not.</param>
    /// <returns></returns>
    ICollection<LetterEntity> GetByReciever(UserEntity user, UserEntity reciever, bool IncludeDeprecated = false);
}

[RegisteredDependency]
public class LetterRepository : EntityRepository<LetterEntity>, ILetterRepository
{
    private readonly List<LetterEntity> _database = new List<LetterEntity>{
        new LetterEntity
        {
            Id = 1,
            Title = "Hello",
            Content = "Hello, World!",
            Author = new UserEntity
            {
                Id = 1,
                Name = "John Doe",
                UserId = "1A",
                Password = "password"
            },
            Reciever = new UserEntity
            {
                Id = 2,
                Name = "Ikurah New",
                UserId = "2B",
                Password = "password2"
            },
            CreatedDate = DateTime.Now,
            IsDeprecated = false
        },
        new LetterEntity
        {
            Id = 2,
            Title = "Hello2",
            Content = "Hello, World2!",
            Author = new UserEntity
            {
                Id = 1,
                Name = "John Doe",
                UserId = "1A",
                Password = "password"
            },
            Reciever = new UserEntity
            {
                Id = 3,
                Name = "Ikurah New",
                UserId = "3C",
                Password = "password3"
            },
            CreatedDate = DateTime.Now,
            IsDeprecated = false
        },
        new LetterEntity
        {
            Id = 3,
            Title = "Hello3",
            Content = "Hello, World3!",
            Author = new UserEntity
            {
                Id = 1,
                Name = "John Doe",
                UserId = "1A",
                Password = "password"
            },
            Reciever = new UserEntity
            {
                Id = 2,
                Name = "Ikurah New",
                UserId = "2B",
                Password = "password2"
            },
            CreatedDate = DateTime.Now,
            IsDeprecated = true
        }
    };

    /// <inheritdoc />
    public LetterEntity? GetById(long Id)
    {
        return base.GetById(Id, _database);
    }

    /// <inheritdoc />
    public ICollection<LetterEntity> GetByAuthor(UserEntity user, bool IncludeDeprecated = false)
    {
        return _database.Where(letter => letter.Author.Id == user.Id && (IncludeDeprecated || !letter.IsDeprecated)).ToList();
    }

    /// <inheritdoc />
    public ICollection<LetterEntity> GetByReciever(UserEntity user, UserEntity reciever, bool IncludeDeprecated = false)
    {
        return _database.Where(letter => letter.Author.Id == user.Id && letter.Reciever.Id == reciever.Id && (IncludeDeprecated || !letter.IsDeprecated)).ToList();
    }
}
