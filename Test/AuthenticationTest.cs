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
     
    
     
}