using LeaveALetter.API.Core.Users.Models;
using LeaveALetter.API.Core.Users.Responses;
using LeaveALetter.API.Data.Users.Entities;
using LeaveALetter.API.Data.Users.Repositories;
using MapsterMapper;
using MediatR;

namespace LeaveALetter.API.Core.Users.Commands;

/// <summary>
/// Create a new user.
/// </summary>
/// <param name="request">The request to create new user.</param>
public record CreateUser(RegisterUserRequest RegisterUserRequest) : IRequest<UserResponse>;

/// <summary>
/// Handler for creating a new user.
/// </summary>
/// <param name="mapper">Mapper responsible for mapping entity to response.</param>
/// <param name="userRepository">Repository responsible for retrieving or creating user entity.</param>
public class CreateUserHandler(IMapper mapper, IUserRepository userRepository) : IRequestHandler<CreateUser, UserResponse>
{
    public async Task<UserResponse> Handle(CreateUser command, CancellationToken cancellationToken)
    {
        ValidateRequest(command.RegisterUserRequest);

        var newUser = new UserEntity
        {
            Name = command.RegisterUserRequest.Name,
            Password = command.RegisterUserRequest.Password,
            UserId = command.RegisterUserRequest.UserId
        };

        var createdUser = userRepository.Create(newUser);

        return mapper.Map<UserResponse>(createdUser);
    }

    /// <summary>
    /// Validate the new user create request.
    /// </summary>
    /// <param name="request">The request to create new user.</param>
    /// <exception cref="ArgumentException">Throw when the name or password is empty</exception>
    /// <exception cref="ArgumentException">Throw when the name already exists in the database.</exception>
    public void ValidateRequest(RegisterUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("The name is required.");
        }
        if (string.IsNullOrWhiteSpace(request.Password))
        {
            throw new ArgumentException("The password is required.");
        }
        if (userRepository.GetByUserIdAndName(request.UserId, request.Name) is not null)
        {
            throw new ArgumentException("The user name already exists, please use another name.");
        }
    }
}
