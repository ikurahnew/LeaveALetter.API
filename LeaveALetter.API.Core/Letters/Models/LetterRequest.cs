using LeaveALetter.API.Core.Letters.Responses;
using LeaveALetter.API.Core.Users.Models;

namespace LeaveALetter.API.Core.Letters.Models;

/// <summary>
///  Representing the request to create an letter.
/// </summary>
/// <param name="Title"><inheritdoc cref="ILetterResponse{TUserResponse}.Title" path="/summary"/></param>
/// <param name="Content"><inheritdoc cref="ILetterResponse{TUserResponse}.Content" path="/summary"/></param>
/// <param name="Reciever"><inheritdoc cref="ILetterResponse{TUserResponse}.Reciever" path="/summary"/></param>
public record LetterRequest(
    string Title,
    string Content,
    ReceiverRequest? Reciever = null
) : ILetterResponse<ReceiverRequest>;
