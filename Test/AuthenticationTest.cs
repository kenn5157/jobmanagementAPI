using System;
using Application;
using Application.Interfaces;
using Domain;
using FluentAssertions;
using FluentValidation;
using Moq;
using Xunit;

namespace Test;

public class AuthenticationTest
{
    /*  [Fact]
     public void AuthenticationServiceWithNullUserRepository_ShouldThrowNullReferenceExcptionWithMessage()
     {
         var userValidator = new UserValidator();
         Action test = () => new AuthenticationService(null, userValidator);
         test.Should().Throw<NullReferenceException>().WithMessage("UserRepository is null");
     }
 
     [Fact]
     public void AuthenticationServiceWithNullValidator_ShouldThrowNullReferenceExceptionWithMessage()
     {
         var userRepository = new Mock<IUserRepository>();
         Action test = () => new AuthenticationService(userRepository.Object);
         test.Should().Throw<NullReferenceException>().WithMessage("UserValidator is null");
     }
    [Theory]
     [InlineData("", "Email cannot be empty.")]
     public void Regiter_WithInvaldidEmail_ShouldThorwValidationExceptionWithMessage(string Email, string errorMessage)
     {
         var userRepository = new Mock<IUserRepository>();
         var userValidator = new UserValidator();
         var userService = new AuthenticationService(userRepository.Object, userValidator);
 
         var testUser = new User
         {
             Email = ""
 
         };
         Action result = () => userService.Register(testUser);
         result.Should().Throw<ValidationException>().WithMessage(errorMessage);
   }*/
}
