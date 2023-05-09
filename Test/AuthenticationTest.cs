using System;
using Application;
using Application.Interfaces;
using Applicatoin.DTOs.User;
using Domain;
using FluentAssertions;
using FluentValidation;
using Moq;
using Xunit;

namespace Test;

public class AuthenticationTest
{
<<<<<<< HEAD
      [Fact]
     public void AuthenticationServiceWithNullUserRepository_ShouldThrowNullReferenceExcption()
     {
         var userValidator = new UserValidator();
         Action test = () => new AuthenticationService(null, userValidator);
         test.Should().Throw<NullReferenceException>();
     }
 
     [Fact]
     public void AuthenticationServiceWithNullValidator_ShouldThrowNullReferenceException()
     {
         var userRepository = new Mock<IUserRepository>();
         
         Action test = () => new AuthenticationService(userRepository.Object);
         test.Should().Throw<NullReferenceException>();
     }
     
    [Theory]
     [InlineData("", "P","")]
     public void Regiter_WithInvaldidEmail_ShouldThorwValidationExceptionWithMessage(string Email,string Password, string errorMessage)
     {
         var userRepository = new Mock<IUserRepository>();
         var userValidator = new UserValidator();
         var userService = new AuthenticationService(userRepository.Object, userValidator);
 
         var dto = new LoginAndRegisterDTO()
         {
             Email = "",
             Password = "123"

         };
         Action result = () => userService.Register(dto);
         result.Should().Throw<ValidationException>().WithMessage(errorMessage);
   }
     
}
=======
    [Fact]
    public void AuthenticationServiceWithNullUserRepository_ShouldThrowNullReferenceExcption()
    {
        var userValidator = new UserValidator();
        Action test = () => new AuthenticationService(null, userValidator);
        test.Should().Throw<NullReferenceException>();
    }
 
    [Fact]
    public void AuthenticationServiceWithNullValidator_ShouldThrowNullReferenceException()
    {
        var userRepository = new Mock<IUserRepository>();
         
        Action test = () => new AuthenticationService(userRepository.Object);
        test.Should().Throw<NullReferenceException>();
    }
     
    [Theory]
    [InlineData("", "P")]
    public void Regiter_WithInvaldidEmail_ShouldThorwValidationExceptionWithMessage(string Email,string Password)
    {
        var userRepository = new Mock<IUserRepository>();
        var userValidator = new UserValidator();
        var userService = new AuthenticationService(userRepository.Object, userValidator);
 
        var dto = new LoginAndRegisterDTO
        {
            Email = "",
            Password = "123"

        };
        Action result = () => userService.Register(dto);
        result.Should().Throw<ValidationException>();
    }

    [Theory]
    [InlineData("email", "")]
    public void Register_WithInValdidPassword_ShouldThrowValidationException(string Email, string Password)
    {
        var userRepository = new Mock<IUserRepository>();
        var userValidator = new UserValidator();
        var userService = new AuthenticationService(userRepository.Object, userValidator);

        var dto = new LoginAndRegisterDTO
        {
            Email = "email",
            Password = ""
        };
        

        Action result = () => userService.Register(dto);
        result.Should().Throw<ValidationException>();

    }
}
>>>>>>> d9102daae36c28eec54e535fe81b39025bc65f9e
