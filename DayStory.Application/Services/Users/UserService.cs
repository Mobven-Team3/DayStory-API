using AutoMapper;
using DayStory.Application.Auth;
using DayStory.Application.DTOs;
using DayStory.Application.Interfaces;
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

    public UserService(IGenericRepository<User> repository, IMapper mapper, IUserRepository userRepository, IAuthHelper authHelper, IPasswordHasher<User> passwordHasher) : base(repository, mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _authHelper = authHelper;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> LoginUserAsync(UserLoginContract requestModel)
    {
        var user = await _userRepository.UserCheckAsync(requestModel.Email);
        var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, requestModel.Password);
        if (result == PasswordVerificationResult.Success)
        {
            return _authHelper.Token(user);
        }
        else
            throw new UserPasswordIncorrectException(user.Email);
    }

    public async Task RegisterUserAsync(UserContract requestModel)
    {
        var userEmailCheck = await _userRepository.UserEmailCheckAsync(requestModel.Email);
        var userUsernameCheck = await _userRepository.UserUsernameCheckAsync(requestModel.Username);
        if (!userEmailCheck && !userUsernameCheck)
        {
            var entity = _mapper.Map<User>(requestModel);
            entity.HashedPassword = _passwordHasher.HashPassword(entity, requestModel.Password);
            await _userRepository.AddAsync(entity);
        }
        else
        {
            throw new UserAlreadyExistsException(requestModel.Email);
        }
    }
}
