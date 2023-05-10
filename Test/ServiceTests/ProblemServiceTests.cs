using System;
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
    // Test for ProblemService constructor
    [Fact]
    public void ProblemServiceWithNullProblemRepository_ShouldThrowNullReferenceExcptionWithMessage()
    {
        var problemValidator = new ProblemValidator();
        Action test = () => new ProblemService(null,problemValidator);
        test.Should().Throw<NullReferenceException>().WithMessage("ProblemRepository is null");
    }
//Test for null Validator
    [Fact]
    public void ProblemServiceWithNullValidator_ShouldThrowNullReferenceExceptionWithMessage()
    {
        var problemRepository = new Mock<IProblemRepository>();

        Action test = () => new ProblemService(problemRepository.Object, null);

        test.Should().Throw<NullReferenceException>().WithMessage("ProblemValidator is null");

    }
    // Test for GetAllProblems method
    [Fact]
    public void GetAllProblems_WithReturnOfNull_ShouldThrowNullReferenceExceptionWithMessage()
    {
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        problemRepository.Setup(x => x.GetAllProblems()).Returns((List<Problem>)null);

        Action test = () => problemService.GetAllProblems();

        test.Should().Throw<NullReferenceException>().WithMessage("Unable to fetch problems form database.");
    }

    
    
    // Test for AddProblem method
    [Theory]
    [InlineData(null, "AddProblemRequest is null.")]
    public void AddProblem_WithProblemAsNull_ShouldThrowNullReferenceExceptionWithMessage(AddProblemRequest problem, string errorMessage)
    {
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        Action result = () => problemService.AddProblem(problem);
        result.Should().Throw<NullReferenceException>().WithMessage(errorMessage);
        
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
            Location = "Location",
            Description = "Description",
            Image = "Image"
        };
        Action result = () => problemService.AddProblem(testProblem);
        result.Should().Throw<ValidationException>().WithMessage(errorMessage);
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
            Location = "Location",
            Description = "Description",
            Image = "Image"
        };
        Action result = () => problemService.AddProblem(testProblem);
        result.Should().Throw<ValidationException>().WithMessage(errorMessage);
    }
    // Test for AddProblem with wrong Location
    [Theory]
    [InlineData("", "Location cannot be empty.")]
    public void AddProblem_WithInvaldidLocation_ShouldThorwValidationExceptionWithMessage(string location, string errorMessage)
    {
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        var testProblem = new AddProblemRequest
        {
            ProblemName = "problemName",
            Status = "Status",
            Location = location,
            Description = "Description",
            Image = "Image"
        };
        Action result = () => problemService.AddProblem(testProblem);
        result.Should().Throw<ValidationException>().WithMessage(errorMessage);
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
            Location = "Location",
            Description = Description,
            Image = "image"
        };
        Action result = () => problemService.AddProblem(testProblem);
        result.Should().Throw<ValidationException>().WithMessage(errorMessage);
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
            Location = "Location",
            Description = "Description",
            Image = Image
        };
        Action result = () => problemService.AddProblem(testProblem);
        result.Should().Throw<ValidationException>().WithMessage(errorMessage);
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
            Location = "Location",
            Description = "Description",
            Image = "image"
        };

        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        problemRepository.Setup(x => x.EditProblem(editProblem));

        Action test = () => problemService.EditProblem(editProblem);
        test.Should().Throw<ValidationException>().WithMessage("Id must be greater than 0.");
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
            Location = "Location",
            Description = "Description",
            Image = "image"
        };
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        problemRepository.Setup(x => x.EditProblem(editProblem));
        Action test = () => problemService.EditProblem(editProblem);
        test.Should().Throw<ValidationException>().WithMessage(erroMessage);
    }
    // Test for EditProblem with wrong location
    [Theory]
    [InlineData("", "Name cannot be empty.")]
    [InlineData(null, "Name cannot be empty.")]
    public void EditProblem_WithEmptyLocation_ShouldReturnValidationExceptionWithMeaasge(string Location,
        string erroMessage)
    {
        var editProblem = new Problem
        {
            ProblemId = 1,
            ProblemName = "ProblemName",
            Status = "Staus",
            Location = Location,
            Description = "Description",
            Image = "Image"
        };
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        problemRepository.Setup(x => x.EditProblem(editProblem));
        Action test = () => problemService.EditProblem(editProblem);
        test.Should().Throw<ValidationException>().WithMessage(erroMessage);
    }
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
            ProblemName = "ProblemName",
            Status = status,
            Location = "Location",
            Description = "Description",
            Image = "Image"
        };
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        problemRepository.Setup(x => x.EditProblem(editProblem));
        Action test = () => problemService.EditProblem(editProblem);
        test.Should().Throw<ValidationException>().WithMessage(erroMessage);
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
            ProblemName = "ProblemName",
            Status = "Staus",
            Location = "Location",
            Description = description,
            Image = "Image"
        };
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        problemRepository.Setup(x => x.EditProblem(editProblem));
        Action test = () => problemService.EditProblem(editProblem);
        test.Should().Throw<ValidationException>().WithMessage(erroMessage);
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
            ProblemName = "ProblemName",
            Status = "Staus",
            Location = "Location",
            Description = "Description",
            Image = Image
        };
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        problemRepository.Setup(x => x.EditProblem(editProblem));
        Action test = () => problemService.EditProblem(editProblem);
        test.Should().Throw<ValidationException>().WithMessage(erroMessage);
    }

    // Test of EditProblem should not return Problem as null
    [Fact]
    public void EditProblem_ReturnProblemAsNull_ShouldReturnNullReferenceExeptionWithMessage()
    {
        var editProblem = new Problem
        {
            ProblemId = 2,
            ProblemName = "test problem",
            Status = "Staus",
            Location = "Location",
            Description = "Description",
            Image = "Image"
        };
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        problemRepository.Setup(x => x.EditProblem(editProblem)).Returns(() =>
        {
            return null;
        });
        Action test = () => problemService.EditProblem(editProblem);
        test.Should().Throw<NullReferenceException>().WithMessage("Problem does not exist in database.");
    }
// Test for EditProblem 
    [Fact]
    public void editProblem_UnchangedDataInDB_ShouldThrowArgumentExcaption()
    {
        var editProblem = new Problem
        {
            ProblemId = 1,
            ProblemName = "Changed",
            Status = "Staus",
            Location = "Location",
            Description = "Description",
            Image = "Image"
        };
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        problemRepository.Setup(x => x.EditProblem(editProblem)).Returns(() =>
        {
            return new Problem { ProblemId = editProblem.ProblemId, ProblemName = "Unchanged" };
        });

        Action test = () => problemService.EditProblem(editProblem);
        test.Should().Throw<ArgumentException>();
    }
    
    // Test for EditProblem if a problem is null
    [Fact]
    public void EditProblem_ProblemAsNull_ShouldThrowNullReferenceException()
    {
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        Action test = () => problemService.EditProblem(null);
        test.Should().Throw<NullReferenceException>().WithMessage("Problem is null.");
    }
    
    // Test if EditProblem returns Null problems
    [Fact]
    public void EditProblem_ReturnNullProblems_ShouldThrowNullReferenceExeption()
    {
        var editProblem = new Problem
        {
            ProblemId = 1,
            ProblemName = "Test",
            Status = "Staus",
            Location = "Location",
            Description = "Description",
            Image = "Image"
        };
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
        var problemService = new ProblemService(problemRepository.Object, problemValidator);

        problemRepository.Setup(x => x.EditProblem(editProblem)).Returns(() =>
        {
            return null;
        });

        Action test = () => problemService.EditProblem(editProblem);
        test.Should().Throw<NullReferenceException>();
    }

    //Test if Problem does not exist in the database
    [Fact]
    public void EditProblem_NotExistInDB_ShouldReturnNullReferenceExeption()
    {
        var problem = new Problem
        {
            ProblemId = 1,
            ProblemName = "Unedited",
            Status = "kritisk",
            Location = "vejen",
            Description = "hul",
            Image = "image"
        };
        var EditProblem = new Problem
        {
            ProblemId = 2,
            ProblemName = "Edited",
            Status = "kritisk",
            Location = "vejen",
            Description = "hul",
            Image = "Image"
        };
        
        var problemRepository = new Mock<IProblemRepository>();
        var problemValidator = new ProblemValidator();
       
        if (problemRepository != null)
        {
            var problemService = new ProblemService(problemRepository.Object,problemValidator);

            problemRepository.Setup(x => x.EditProblem(EditProblem)).Returns(() =>
            {
                if (problem.ProblemId != EditProblem.ProblemId)
                {
                    return null;
                }

                problem.ProblemName = EditProblem.ProblemName;
                return problem;
            });
            Action test = () => problemService.EditProblem(EditProblem);
            test.Should().Throw<NullReferenceException>();
        }
    }
    
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
            ProblemName = "Test problem",
            Status = "Staus",
            Location = "Location",
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
            ProblemName = "test problem",
            Status = "Staus",
            Location = "Location",
            Description = "Description",
            Image = "Image"
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
            ProblemName = "test problem",
            Status = "Staus",
            Location = "Location",
            Description = "Description",
            Image = "image"
        };
        var result = problemService.DeleteProblem(testProblem);
        result.Should().BeFalse();
    }

 
}