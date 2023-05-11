using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.DTOs.User;
using Application.Helpers;
using Application.Interfaces;
using Domain;
using FluentValidation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Application;

public class AuthenticationService : IAuthenticationService
{
    
    
    private AppSettings _appSettings;
    private readonly IUserRepository _repository;
    private readonly IValidator<User> _userValidator;

    public AuthenticationService(IUserRepository repository, IOptions<AppSettings> appSettings,
        UserValidator userValidator)
    {
        _repository = repository ?? throw new NullReferenceException("UserRepository is null");
        _userValidator = userValidator ?? throw new NullReferenceException("UserValidator is null");
        _appSettings = appSettings.Value;
        

    }
    public AuthenticationService(IUserRepository repository, UserValidator userValidator)
    {
        if (repository == null)
        {
            throw new NullReferenceException();
        }
        
    }

    public AuthenticationService(IUserRepository userRepositoryObject)
    {
        throw new NullReferenceException();
    }

    public string Register(LoginAndRegisterDTO dto)
    {
        if(dto == null){
            throw new ValidationException("Email is null");
        }
        try
        {
            _repository.GetUserByEmail(dto.Email);
        }
        catch (KeyNotFoundException)
        {
            var salt = RandomNumberGenerator.GetBytes(32).ToString();
            var user = new User
            {
                Email = dto.Email,
                Salt = salt,
                Hash = BCrypt.Net.BCrypt.HashPassword(dto.Password + salt),
            };
            _repository.CreateNewUser(user);
            return GenerateToken(user);
        }

        throw new Exception("Email" + dto.Email + "is already taken");
    }

    public string GenerateToken(User user)
    {
        var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("email", user.Email) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }

    public string Login(LoginAndRegisterDTO dto)
    {
        var user = _repository.GetUserByEmail(dto.Email);
        if (BCrypt.Net.BCrypt.Verify(dto.Password + user.Salt, user.Hash))
        {
            return GenerateToken(user);
        }

        throw new Exception("Invalid login");
    }

   
}
    
