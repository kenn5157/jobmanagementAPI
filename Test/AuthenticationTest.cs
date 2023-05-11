using System;
using Application;
using Application.Interfaces;
using Application.DTOs.User;
using Applicatoin.Validators;
using Domain;
using FluentAssertions;
using FluentValidation;
using Moq;
using Xunit;

namespace Test;

public class AuthenticationTest
{
    private Mock<IUserRepository> userRepo;
    private Mock<IProblemRepository> probRepo;
    private RegisterValidator registerValidator;
    private UserValidator userValidator;
    private AuthenticationService authService;

    public AuthenticationTest() {
        this.userRepo = new Mock<IUserRepository>();
        this.probRepo = new Mock<IProblemRepository>();
        this.registerValidator = new RegisterValidator();
        this.userValidator = new UserValidator();
        this.authService = new AuthenticationService(userRepo.Object, registerValidator);

    }

    [Fact]
    public void AuthenticationServiceWithNullUserRepository_ShouldThrowNullReferenceExcption()
    {
        Action test = () => new AuthenticationService(null, registerValidator);

        test.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void AuthenticationServiceWithNullValidator_ShouldThrowNullReferenceException()
    {
        Action test = () => new AuthenticationService(userRepo.Object, null);

        test.Should().Throw<NullReferenceException>();
    }

    [Theory]
    [InlineData("", "Poggers", "")]
    public void Register_WithInvaldidEmail_ShouldThorwValidationExceptionWithMessage(string email, string password, string errorMessage)
    {
        var userRepository = new Mock<IUserRepository>();
        var userValidator = new UserValidator();
        var registerValidator = new RegisterValidator();
        var userService = new AuthenticationService(userRepository.Object, registerValidator);

        LoginAndRegisterDTO dto = new LoginAndRegisterDTO()
        {
            Email = email,
            Password = password

        };
        Action result = () => userService.Register(dto);
        result.Should().Throw<ValidationException>();
    }



}