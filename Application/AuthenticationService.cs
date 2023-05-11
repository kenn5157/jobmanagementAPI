using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.DTOs.User;
using Application.Helpers;
using Application.Interfaces;
using Applicatoin.Validators;
using Domain;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace Application;

public class AuthenticationService : IAuthenticationService
{
    private Constants constants;
    private readonly IUserRepository _repository;
    //private readonly IValidator<User> _userValidator;
    private readonly IValidator<Register> _registerValidator;

    public AuthenticationService(
        IUserRepository repository,
        RegisterValidator registerValidator
        )
    {
        _repository = repository ?? throw new NullReferenceException("UserRepository is null");
        //_userValidator = userValidator ?? throw new NullReferenceException("UserValidator is null");
        _registerValidator = registerValidator ?? throw new NullReferenceException("RegisterValidator is null");
        this.constants = new Constants();

    }

    public string Register(LoginAndRegisterDTO dto)
    {
        if (dto == null)
        {
            throw new NullReferenceException("Register user object is null");
        }

        Register registerDto = ObjectGeneator.RegisterDtoToRegister(dto);

        var validation = _registerValidator.Validate(registerDto, options => options.IncludeRuleSets("Default"));

        if (!validation.IsValid)
        {
            throw new FluentValidation.ValidationException("Register user could not be validated");
        }

        try
        {
            _repository.GetUserByEmail(registerDto.Email);
        }
        catch (Exception e)
        {
            string generatedSalt = RandomNumberGenerator.GetBytes(32).ToString();
            var user = new User
            {
                email = dto.Email,
                salt = generatedSalt,
                hash = BCrypt.Net.BCrypt.HashPassword(dto.Password + generatedSalt)
            };
            try
            {
                _repository.CreateNewUser(user);
                return "hello world";
                //return GenerateToken(user);
            }
            catch (Exception g)
            {
                throw new Exception("Could not create new user in database. " + g.Message);
            }

        }
        throw new DuplicateNameException("User already exist in DB.");
    }

    public string GenerateToken(User user)
    {
        var key = Encoding.UTF8.GetBytes(constants.getSecret());
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("email", user.email) }),
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
        if (BCrypt.Net.BCrypt.Verify(dto.Password + user.salt, user.hash))
        {
            return GenerateToken(user);
        }

        throw new Exception("Invalid login");
    }


}

