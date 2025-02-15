using LeaveALetter.API.Data._Shared.Entities;
using LeaveALetter.API.Data.Letters.Entities;

namespace LeaveALetter.API.Data.Users.Entities;

/// <summary>
///    Representing the user instance.
/// </summary>
public class UserEntity : Entity
{
    /// <summary>
    /// The custom name of the user.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The generated user id to display.
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// The password of the user.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// The sent letters by the user.
    /// </summary>
    public ICollection<LetterEntity> SentLetters { get; set; } = [];

    /// <summary>
    /// The recieved letters by the user.
    /// </summary>
    public ICollection<LetterEntity> RecievedLetters { get; set; } = [];
}
