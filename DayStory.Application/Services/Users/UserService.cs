using AutoMapper;
using DayStory.Application.Auth;
using DayStory.Application.Interfaces;
using DayStory.Common.DTOs;
using DayStory.Domain.Entities;
using DayStory.Domain.Exceptions;
using DayStory.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace DayStory.Application.Services;

public class UserService : BaseService<User, UserContract>, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IAuthHelper _authHelper;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(IGenericRepository<User, UserContract> repository, IMapper mapper, IUserRepository userRepository, IAuthHelper authHelper, IPasswordHasher<User> passwordHasher) : base(repository, mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _authHelper = authHelper;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> LoginUserAsync(LoginUserContract requestModel)
    {
        var user = await _userRepository.UserCheckAsync(requestModel.Email);

        if (user == null)
            throw new UserNotFoundException(requestModel.Email);

        var result = VerifyPassword(user, requestModel.Password);

        if (result)
        {
            await _userRepository.UserLastLoginUpdateAsync(user);
            return _authHelper.Token(user);
        }
        else
            throw new UserPasswordIncorrectException(user.Email);
    }

    public async Task RegisterUserAsync(RegisterUserContract requestModel)
    {
        var userEmailCheck = await _userRepository.UserCheckAsync(requestModel.Email);
        var userUsernameCheck = await _userRepository.UsernameCheckAsync(requestModel.Username);

        if (userEmailCheck != null)
        {
            if (userEmailCheck.IsDeleted)
                await _userRepository.SoftDeletedUserAddAsync(userEmailCheck);
            else
                throw new UserAlreadyExistsException(requestModel.Email);
        }
        else if (userUsernameCheck)
        {
            throw new UserAlreadyExistsException(requestModel.Username);
        }
        else
        {
            var entity = _mapper.Map<User>(requestModel);
            entity.HashedPassword = HashPassword(entity, requestModel.Password);
            await _userRepository.AddAsync(entity);
        }
    }

    public async Task UpdatePasswordAsync(PasswordUpdateUserContract requestModel)
    {
        if (requestModel.Id == null)
            throw new ArgumentNullException(nameof(requestModel.Id), "User ID cannot be null");
        
        var user = await _userRepository.GetByIdAsync(requestModel.Id.Value);

        if (user == null)
            throw new UserNotFoundException(user.Id.ToString());

        if (!VerifyPassword(user, requestModel.CurrentPassword))
            throw new UserPasswordIncorrectException(user.Email);

        if (requestModel.Password == requestModel.CurrentPassword)
            throw new SamePasswordException(user.Email);

        string newPasswordHash = HashPassword(user, requestModel.Password);
        user.HashedPassword = newPasswordHash;
        await _userRepository.UpdateAsync(user);
    }

    public bool VerifyPassword(User user, string password)
    {
        var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, password);
        return result == PasswordVerificationResult.Success;
    }

    public string HashPassword(User user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }

    public async Task UpdateUserAsync(UpdateUserContract requestModel)
    {
        if (requestModel == null)
            throw new ArgumentNullException(nameof(requestModel), "Request model cannot be null.");

        var existingUser = await _userRepository.GetByIdAsync((int)requestModel.Id);

        if (existingUser == null)
            throw new UserNotFoundException(existingUser.Id.ToString());

        var entity = _mapper.Map<UserContract>(requestModel);
        await _userRepository.UpdateAsync(entity);
    }

    public async Task<GetUserContract> GetUserAsync(int id)
    {
        var result = await _userRepository.GetByIdAsync(id);
        return _mapper.Map<GetUserContract>(result);
    }
}
