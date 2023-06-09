﻿using System;
using System.Collections.Generic;
using System.Net.Mime;
using Application;
using Application.DTOs;
using Application.Interfaces;
using Application.Validators;
using Domain;
using Xunit;
using FluentAssertions;
using FluentValidation;
using Moq;


namespace Test.ServiceTests;


public class ProblemServiceTests
{
    private Problem problem = new Problem
        {
            ProblemId = 1,
            ProblemName = "test",
            Status = "Staus",
            Longitude = 0.0,
            Latitude = 0.0,
            Description = "Description",
            Image = "image"
        };

    private AddProblemRequest addProblemRequest = new AddProblemRequest
        {
            ProblemName = "problemName",
            Status = "Status",
            Longitude = "0.0",
            Latitude = "0.0",
            Description = "Description",
            Image = "Image"
        };

    // Test for ProblemService constructor
    [Fact]
    public void ProblemServiceWithNullProblemRepository_ShouldThrowNullReferenceExcptionWithMessage()
    {
        var problemValidator = new ProblemValidator();
        Action test = () => new ProblemService(null,problemValidator);
        test.Should().Throw<NullReferenceException>();
    }
//Test for null Validator
    [Fact]
    public void ProblemServiceWithNullValidator_ShouldThrowNullReferenceExceptionWithMessage()
    {
        var problemRepository = new Mock<IProblemRepository>();

        Action test = () => new ProblemService(problemRepository.Object, null);

        test.Should().Throw<NullReferenceException>();

    }
    // Test for GetAllProblems method
    // [Fact]
    // public void GetAllProblems_WithReturnOfNull_ShouldThrowNullReferenceExceptionWithMessage()
    // {
    //     var problemRepository = new Mock<IProblemRepository>();
    //     var problemValidator = new ProblemValidator();
    //     var problemService = new ProblemService(problemRepository.Object, problemValidator);

    //     problemRepository.Setup(x => x.GetAllProblems()).Returns((List<ProblemDB>)null);

    //     Action test = () => problemService.GetAllProblems();

    //     test.Should().Throw<NullReferenceException>().WithMessage("Unable to fetch problems form database.");
    // }

    
    
    // Test for AddProblem method
    [Theory]
    [InlineData(null, "AddProblemRequest is null.")]
    public void AddProblem_WithProblemAsNull_ShouldThrowNullReferenceExceptionWithMessage(AddProblemRequest problem, string errorMessage)
    {
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        Action result = () => problemService.AddProblem(problem);
        result.Should().Throw<NullReferenceException>();
        
    }
    
    //Test for validation of name for a problem
    [Theory]
    [InlineData("", "Name cannot be empty.")]
    public void AddProblem_WithInvaldidName_ShouldThorwValidationExceptionWithMessage(string problemName, string errorMessage)
    {
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        var testProblem = new AddProblemRequest
        {
            ProblemName = problemName,
            Status = "Status",
            Longitude = "0.0",
            Latitude = "0.0",
            Description = "Description",
            Image = "Image"
        };
        Action result = () => problemService.AddProblem(testProblem);
        result.Should().Throw<ValidationException>();
    }
    
    // Test for AddProblem with wrong Status
    [Theory]
    [InlineData("", "Status cannot be empty.")]
    public void AddProblem_WithInvaldidStatus_ShouldThorwValidationExceptionWithMessage(string status, string errorMessage)
    {
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        var testProblem = new AddProblemRequest
        {
            ProblemName = "problemName",
            Status = status,
            Longitude = "0.0",
            Latitude = "0.0",
            Description = "Description",
            Image = "Image"
        };
        Action result = () => problemService.AddProblem(testProblem);
        result.Should().Throw<ValidationException>();
    }
    // Test for AddProblem with wrong Location
    [Theory]
    [InlineData("", "", "Location cannot be empty.")]
    public void AddProblem_WithInvaldidLocation_ShouldThorwValidationExceptionWithMessage(string Latitude, string Longitude, string errorMessage)
    {
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        var testProblem = new AddProblemRequest
        {
            ProblemName = "problemName",
            Status = "Status",
            Longitude = Longitude,
            Latitude = Latitude,
            Description = "Description",
            Image = "Image"
        };
        Action result = () => problemService.AddProblem(testProblem);
        result.Should().Throw<ValidationException>();
    }
    // Test for AddProblem with wrong Description
    [Theory]
    [InlineData("", "Description cannot be empty.")]
    public void AddProblem_WithInvaldidDescription_ShouldThorwValidationExceptionWithMessage(string Description, string errorMessage)
    {
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        var testProblem = new AddProblemRequest
        {
            ProblemName = "problemName",
            Status = "Status",
            Longitude = "0.0",
            Latitude = "0.0",
            Description = Description,
            Image = "Image"
        };
        Action result = () => problemService.AddProblem(testProblem);
        result.Should().Throw<ValidationException>();
    }
    
    // Test for AddProblem with wrong Image
    [Theory]
    [InlineData("", "Image cannot be empty.")]
    public void AddProblem_WithInvaldidImage_ShouldThorwValidationExceptionWithMessage(string Image, string errorMessage)
    {
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        var testProblem = new AddProblemRequest
        {
            ProblemName = "problemName",
            Status = "Status",
            Longitude = "0.0",
            Latitude = "0.0",
            Description = "Description",
            Image = Image
        };
        Action result = () => problemService.AddProblem(testProblem);
        result.Should().Throw<ValidationException>();
    }
    
    // Test for EditProblem to wrong id
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    [InlineData(null)]
    public void EditProblem_WithInvalidId_ShouldThrowValidationExceptionWithMessage(int Id)
    {
        var editProblem = new Problem
        {
            ProblemId = Id,
            ProblemName = "test",
            Status = "Staus",
            Longitude = 0.0,
            Latitude = 0.0,
            Description = "Description",
            Image = "image"
        };

        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        problemRepository.Setup(x => x.EditProblem(editProblem));

        Action test = () => problemService.EditProblem(editProblem);
        test.Should().Throw<ValidationException>();
    }
    // Test for EditProblem to wrong name
    [Theory]
    [InlineData("", "Name cannot be empty.")]
    [InlineData(null, "Name cannot be empty.")]
    
    public void EditProblem_WithEmptyName_ShouldReturnValidationExceptionWithMeaasge(string problemName,
        string erroMessage)
    {
        var editProblem = new Problem
        {
            ProblemId = 1,
            ProblemName = problemName,
            Status = "Staus",
            Longitude = 0.0,
            Latitude = 0.0,
            Description = "Description",
            Image = "image"
        };
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        problemRepository.Setup(x => x.EditProblem(editProblem));
        Action test = () => problemService.EditProblem(editProblem);
        test.Should().Throw<ValidationException>();
    }
    // Test for EditProblem with wrong location
    // [Theory]
    // [InlineData(null, "Name cannot be empty.")]
    // public void EditProblem_WithEmptyLocation_ShouldReturnValidationExceptionWithMeaasge(Location location,
    //     string erroMessage)
    // {
    //     var editProblemDB = new ProblemDB
    //     {
    //         ProblemId = 1,
    //         ProblemName = "test",
    //         Status = "Staus",
    //         Latitude = location.Latitude,
    //         Longitude = location.Longitude,
    //         Description = "Description",
    //         Image = "image"
    //     };
    //     var editProblem = new Problem
    //     {
    //         ProblemId = 1,
    //         ProblemName = "test",
    //         Status = "Staus",
    //         Location = location,
    //         Description = "Description",
    //         Image = "image"
    //     };
    //     var problemRepository = new Mock<IProblemRepository>();
    //     var problemValidator = new ProblemValidator();
    //     var problemService = new ProblemService(problemRepository.Object, problemValidator);

    //     problemRepository.Setup(x => x.EditProblem(editProblemDB));
    //     Action test = () => problemService.EditProblem(editProblem);
        
    //     test.Should().Throw<ValidationException>().WithMessage(erroMessage);
    // }

    //Test for EditProblem with wrong Status
    [Theory]
    [InlineData("", "Name cannot be empty.")]
    [InlineData(null, "Name cannot be empty.")]
    public void EditProblem_WithEmptyStatus_ShouldReturnValidationExceptionWithMeaasge(string status,
        string erroMessage)
    {
        var editProblem = new Problem
        {
            ProblemId = 1,
            ProblemName = "test",
            Status = status,
            Longitude = 0.0,
            Latitude = 0.0,
            Description = "Description",
            Image = "image"
        };
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        problemRepository.Setup(x => x.EditProblem(editProblem));
        Action test = () => problemService.EditProblem(editProblem);
        test.Should().Throw<ValidationException>();
    }
    // Test for EditProblem with wrong Description
    [Theory]
    [InlineData("", "Name cannot be empty.")]
    [InlineData(null, "Name cannot be empty.")]
    public void EditProblem_WithEmptyDesciption_ShouldReturnValidationExceptionWithMeaasge(string description,
        string erroMessage)
    {
        var editProblem = new Problem
        {
            ProblemId = 1,
            ProblemName = "test",
            Status = "Staus",
            Longitude = 0.0,
            Latitude = 0.0,
            Description = description,
            Image = "image"
        };
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        problemRepository.Setup(x => x.EditProblem(editProblem));
        Action test = () => problemService.EditProblem(editProblem);
        test.Should().Throw<ValidationException>();
    }

    // Test for EditProblem with wrong Image
    [Theory]
    [InlineData("", "Image cannot be empty.")]
    [InlineData(null, "Image cannot be empty.")]
    public void EditProblem_WithEmptyImage_ShouldReturnValidationExceptionWithMeaasge(string Image,
        string erroMessage)
    {
        var editProblem = new Problem
        {
            ProblemId = 1,
            ProblemName = "test",
            Status = "Staus",
            Longitude = 0.0,
            Latitude = 0.0,
            Description = "Description",
            Image = Image
        };
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        problemRepository.Setup(x => x.EditProblem(editProblem));
        Action test = () => problemService.EditProblem(editProblem);
        test.Should().Throw<ValidationException>();
    }

    // Test of EditProblem should not return Problem as null
    // [Fact]
    // public void EditProblem_ReturnProblemAsNull_ShouldReturnNullReferenceExeptionWithMessage()
    // {
    //     var editProblem = new Problem
    //     {
    //         ProblemId = 1,
    //         ProblemName = "test",
    //         Status = "Staus",
    //         Longitude = 0.0,
    //         Latitude = 0.0,
    //         Description = "Description",
    //         Image = "image"
    //     };
    //     var problemRepository = new Mock<IProblemRepository>();
    //     var problemValidator = new ProblemValidator();
    //     var problemService = new ProblemService(problemRepository.Object, problemValidator);

    //     problemRepository.Setup(x => x.EditProblem(editProblem)).Returns(() =>
    //     {
    //         return null;
    //     });
    //     Action test = () => problemService.EditProblem(editProblem);
    //     test.Should().Throw<NullReferenceException>();
    // }
// Test for EditProblem 
    // [Fact]
    // public void editProblem_UnchangedDataInDB_ShouldThrowArgumentExcaption()
    // {
    //     var editProblem = new Problem
    //     {
    //         ProblemId = 1,
    //         ProblemName = "test",
    //         Status = "Staus",
    //         Location = new Location{
    //             Latitude = 55.8,
    //             Longitude = 8.42
    //         },
    //         Description = "Description",
    //         Image = "image"
    //     };;
    //     var problemRepository = new Mock<IProblemRepository>();
    //     var problemValidator = new ProblemValidator();
    //     var problemService = new ProblemService(problemRepository.Object, problemValidator);

    //     problemRepository.Setup(x => x.EditProblem(Problem.ConvertToProblemDB(editProblem))).Returns(() =>
    //     {
    //         return new Problem { ProblemId = editProblem.ProblemId, ProblemName = "Unchanged" };
    //     });

    //     Action test = () => problemService.EditProblem(editProblem);
    //     test.Should().Throw<ArgumentException>();
    // }
    
    // Test for EditProblem if a problem is null
    [Fact]
    public void EditProblem_ProblemAsNull_ShouldThrowNullReferenceException()
    {
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        Action test = () => problemService.EditProblem(null);
        test.Should().Throw<NullReferenceException>();
    }
    
    // Test if EditProblem returns Null problems
    // [Fact]
    // public void EditProblem_ReturnNullProblems_ShouldThrowNullReferenceExeption()
    // {
    //     var editProblem = new Problem
    //     {
    //         ProblemId = 1,
    //         ProblemName = "test",
    //         Status = "Staus",
    //         Longitude = 0.0,
    //         Latitude = 0.0,
    //         Description = "Description",
    //         Image = "image"
    //     };
    //     var problemRepository = new Mock<IProblemRepository>();
    //     var problemValidator = new ProblemValidator();
    //     var problemService = new ProblemService(problemRepository.Object, problemValidator);

    //     problemRepository.Setup(x => x.EditProblem(editProblem)).Returns(() =>
    //     {
    //         return null;
    //     });

    //     Action test = () => problemService.EditProblem(editProblem);
    //     test.Should().Throw<NullReferenceException>();
    // }

    //Test if Problem does not exist in the database
    // [Fact]
    // public void EditProblem_NotExistInDB_ShouldReturnNullReferenceExeption()
    // {
    //     var problem = new Problem
    //     {
    //         ProblemId = 1,
    //         ProblemName = "test",
    //         Status = "Staus",
    //         Location = new Location{
    //             Latitude = 55.8,
    //             Longitude = 8.42
    //         },
    //         Description = "Description",
    //         Image = "image"
    //     };
    //     var editProblem = new Problem
    //     {
    //         ProblemId = 2,
    //         ProblemName = "test",
    //         Status = "Staus",
    //         Location = new Location{
    //             Latitude = 55.8,
    //             Longitude = 8.42
    //         },
    //         Description = "Description",
    //         Image = "image"
    //     };
        
    //     var problemRepository = new Mock<IProblemRepository>();
    //     var problemValidator = new ProblemValidator();
       
    //     if (problemRepository != null)
    //     {
    //         var problemService = new ProblemService(problemRepository.Object,problemValidator);

    //         problemRepository.Setup(x => x.EditProblem(Problem.ConvertToProblemDB(editProblem))).Returns(() =>
    //         {
    //             if (problem.ProblemId != editProblem.ProblemId)
    //             {
    //                 return null;
    //             }

    //             problem.ProblemName = editProblem.ProblemName;
    //             return problem;
    //         });
    //         Action test = () => problemService.EditProblem(editProblem);
    //         test.Should().Throw<NullReferenceException>();
    //     }
    // }
    
    // Test for DeleteProblem With Valid Id
    [Fact]
    public void DeleteProblem_WithValidId_ShouldReturnTrue()
    {
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        problemRepository.Setup(x => x.DeleteProblem(1)).Returns(() =>
        {
            return 1;
        });

        var testProblem = new Problem
        {
            ProblemId = 1,
            ProblemName = "test",
            Status = "Staus",
            Longitude = 0.0,
            Latitude = 0.0,
            Description = "Description",
            Image = "image"
        };
        var result = problemService.DeleteProblem(testProblem);
        result.Should().BeTrue();
    }
    
    //  Test if the DeleteProblem method has a Invalid Id
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void DeleteProblem_WithInvaldigId_ShouldReturnFalse(int id)
    {
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        var testProblem = new Problem
        {
            ProblemId = id,
            ProblemName = "test",
            Status = "Staus",
            Longitude = 0.0,
            Latitude = 0.0,
            Description = "Description",
            Image = "image"
        };
        var result = problemService.DeleteProblem(testProblem);
        result.Should().BeFalse();
    }
    
    // Test the DeleteProblem method with no change
    [Fact]
    public void DeleteItem_WithNoChangeReturned_ShouldReturnFalse()
    {
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        problemRepository.Setup(x => x.DeleteProblem(1)).Returns(() =>
        {
            return 0;
        });
        var testProblem = new Problem
        {
            ProblemId = 1,
            ProblemName = "test",
            Status = "Staus",
            Longitude = 0.0,
            Latitude = 0.0,
            Description = "Description",
            Image = "image"
        };
        var result = problemService.DeleteProblem(testProblem);
        result.Should().BeFalse();
    }

 
}
