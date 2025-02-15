using LeaveALetter.API.Data._Shared.Entities;
using LeaveALetter.API.Data.Users.Entities;

namespace LeaveALetter.API.Data.Letters.Entities;

/// <summary>
///     Representing the letter instance.
/// </summary>
public class LetterEntity : Entity
{
    /// <summary>
    /// The title of the letter.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// The content of the letter
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// The author of the letter.
    /// </summary>
    public UserEntity Author { get; set; }


    /// <summary>
    /// The optional reciever of the letter.
    /// </summary>
    public UserEntity? Reciever { get; set; }
}
