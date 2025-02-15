using LeaveALetter.API.Core.Users.Responses;

namespace LeaveALetter.API.Core.Letters.Responses;

/// <summary>
///     Representing the <see cref="LetterEntity"/> instance.
/// </summary>
/// <param name="Title"><inheritdoc cref="ILetterResponse{TUserResponse}.Title" path="/summary"/></param>
/// <param name="Content"><inheritdoc cref="ILetterResponse{TUserResponse}.Content" path="/summary"/></param>
/// <param name="Author">The author of the letter.</param>
/// <param name="Reciever"><inheritdoc cref="ILetterResponse{TUserResponse}.Reciever" path="/summary"/></param>
public record LetterResponse(
    string Title,
    string Content,
    UserResponse Author,
    UserResponse? Reciever = null
) : ILetterResponse<UserResponse>;
