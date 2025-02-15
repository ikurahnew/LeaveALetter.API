namespace LeaveALetter.API.Core.Letters.Responses;

/// <summary>
///  Representing the contract of the <see cref="LetterResponse"/>.
/// </summary>
/// <typeparam name="TUserResponse">The generic type of the user.</typeparam>
public interface ILetterResponse<TUserResponse>
{
    /// <inheritdoc cref="LetterEntity.Title" path="/summary"/>
    public string Title { get; init; }

    /// <inheritdoc cref="LetterEntity.Content" path="/summary"/>
    public string Content { get; init; }

    /// <inheritdoc cref="LetterEntity.Reciever" path="/summary"/>
    public TUserResponse? Reciever { get; init; }
}
