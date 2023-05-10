using System;
using Application;
using Application.Interfaces;
using Application.DTOs.User;
using Domain;
using FluentAssertions;
using FluentValidation;
using Moq;
using Xunit;

namespace Test;

public class AuthenticationTest
{
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
     [InlineData("check", "Poggers","")]
     public void Regiter_WithInvaldidEmail_ShouldThorwValidationExceptionWithMessage(string email,string password, string errorMessage)
     {
         var userRepository = new Mock<IUserRepository>();
         var userValidator = new UserValidator();
         var userService = new AuthenticationService(userRepository.Object, userValidator);
 
         LoginAndRegisterDTO dto = new LoginAndRegisterDTO()
         {
             Email = email,
             Password = password

         };
         Action result = () => userService.Register(dto);
         result.Should().Throw<ValidationException>().WithMessage(errorMessage);
   }
     
}