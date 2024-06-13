using AutoMapper;
using DayStory.Application.Interfaces;
using DayStory.Common.DTOs;
using DayStory.Domain.Entities;
using DayStory.Domain.Exceptions;
using DayStory.Domain.Repositories;

namespace DayStory.Application.Services;

public class UserService : BaseService<User, UserContract>, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;

    public UserService(
        IGenericRepository<User, UserContract> repository, 
        IMapper mapper, 
        IUserRepository userRepository,
        IAuthService authService) : base(repository, mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _authService = authService;
    }

    public async Task<LoginUserResponseContract> LoginUserAsync(LoginUserContract requestModel)
    {
        requestModel.Email = NormalizeEmail(requestModel.Email);
        var user = await GetUserByEmailAsync(requestModel.Email);

        if (_authService.VerifyPassword(user, requestModel.Password))
        {
            await UpdateUserLastLoginAsync(user);
            var token = GenerateToken(user);
            return new LoginUserResponseContract { Token = token };
        }
        else
            throw new UserPasswordIncorrectException(user.Email);
    }

    public async Task RegisterUserAsync(RegisterUserContract requestModel)
    {
        requestModel.Email = NormalizeEmail(requestModel.Email);
        requestModel.Username = NormalizeUsername(requestModel.Username);

        await CheckAndHandleExistingUserAsync(requestModel.Email, requestModel.Username);

        var newUser = CreateUser(requestModel);
        await _userRepository.AddAsync(newUser);
    }

    public async Task UpdatePasswordAsync(PasswordUpdateUserContract requestModel)
    {
        var user = await GetUserByIdAsync(requestModel.Id);

        ValidateUserPassword(user, requestModel.CurrentPassword, requestModel.Password);

        string newPasswordHash = _authService.HashPassword(user, requestModel.Password);
        user.HashedPassword = newPasswordHash;
        await _userRepository.UpdateAsync(user);
    }

    public async Task UpdateUserAsync(UpdateUserContract requestModel)
    {
        requestModel.Email = NormalizeEmail(requestModel.Email);
        requestModel.Username = NormalizeUsername(requestModel.Username);

        if (await GetUserByIdAsync(requestModel.Id) != null)
        {
            var entity = _mapper.Map<UserContract>(requestModel);
            await _userRepository.UpdateAsync(entity);
        }
    }

    public async Task<GetUserContract> GetUserAsync(int id)
    {
        var user = await GetUserByIdAsync(id);
        return _mapper.Map<GetUserContract>(user);
    }

    #region Private Helper Methods
    private string NormalizeEmail(string email)
    {
        return email.ToLowerInvariant();
    }

    private string NormalizeUsername(string username)
    {
        return username.ToLowerInvariant();
    }

    private async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        return (user == null) ? throw new UserNotFoundException(email) : user;
    }

    private async Task<User> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user == null ? throw new UserNotFoundException(id.ToString()) : user;
    }

    private async Task CheckAndHandleExistingUserAsync(string email, string username)
    {
        var existingUserByEmail = await _userRepository.UserCheckAndSoftDeletedUserAddAsync(email);
        if (existingUserByEmail != null)
        {
            throw new UserAlreadyExistsException(email);
        }

        var existingUserByUsername = await _userRepository.UsernameCheckAsync(username);
        if (existingUserByUsername)
        {
            throw new UserAlreadyExistsException(username);
        }
    }

    private User CreateUser(RegisterUserContract requestModel)
    {
        var user = _mapper.Map<User>(requestModel);
        user.HashedPassword = _authService.HashPassword(user, requestModel.Password);
        return user;
    }

    private async Task UpdateUserLastLoginAsync(User user)
    {
        await _userRepository.UserLastLoginUpdateAsync(user);
    }

    private string GenerateToken(User user)
    {
        return _authService.Token(user);
    }

    private void ValidateUserPassword(User user, string currentPassword, string newPassword)
    {
        if (!_authService.VerifyPassword(user, currentPassword))
            throw new UserPasswordIncorrectException(user.Email);

        if (currentPassword == newPassword)
            throw new SamePasswordException(user.Email);
    }
    #endregion
}
