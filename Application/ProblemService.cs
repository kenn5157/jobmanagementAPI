using System.Net;
using Application.DTOs;
using Application.Interfaces;
using Application.Validators;
using Application.Helpers;
using Domain;
using FluentValidation;



namespace Application;

public class ProblemService : IProblemService
{
    private readonly IProblemRepository _problemRepository;
    private readonly IValidator<Problem> _problemValidator;

    public ProblemService(IProblemRepository problemRepository, ProblemValidator problemValidator)
    {
        _problemRepository = problemRepository;
        _problemValidator = problemValidator;
    }
    public ProblemResponse GetAllProblems()
    {
        var problemList = ProblemDB.ConvertToProblems(_problemRepository.GetAllProblems());

        if (problemList == null)
        {
            HttpStatusCode statusCode = HttpStatusCode.NotFound; 
            Console.WriteLine("Statuskode: " + (int)500 + " " + statusCode.ToString());
        
        }
        var response = new ProblemResponse { Problems = problemList };

        return response;
    }

  
    public Problem GetById(int ProblemId)
    {
        return ProblemDB.ConvertToProblem(_problemRepository.GetById(ProblemId));
    }
    
    public Problem AddProblem(AddProblemRequest addProblemRequest)
    {
        if (addProblemRequest == null)
        { HttpStatusCode statusCode = HttpStatusCode.NotFound; // Definerer statuskoden
            
            Console.WriteLine("Statuskode: " + (int)statusCode + " " + statusCode.ToString());
        }
        

        Problem problem = ObjectGeneator.ProblemRequestToProblem(addProblemRequest);
        
        var validation = _problemValidator.Validate(problem, options => options.IncludeRuleSets("Add"));

        if (!validation.IsValid)
        {
            HttpStatusCode statusCode = HttpStatusCode.NotFound;

            Console.WriteLine("Statuskode: " + (int)statusCode + " " + statusCode.ToString());
        }
        

        ProblemDB returnProblem = _problemRepository.AddProblem(Problem.ConvertToProblemDB(problem));

        if (returnProblem == null)
        {
            HttpStatusCode statusCode = HttpStatusCode.NotFound;
            Console.WriteLine("Statuskode: " + (int)statusCode + " " + statusCode.ToString());
        }
        

        var returnValidation = _problemValidator.Validate(ProblemDB.ConvertToProblem(returnProblem));
        if (!returnValidation.IsValid)
        {
            HttpStatusCode statusCode = HttpStatusCode.NotFound; 
            Console.WriteLine("Statuskode: " + (int)statusCode + " " + statusCode.ToString());
        }
        

        return ProblemDB.ConvertToProblem(returnProblem);
    }

    public Problem EditProblem(Problem problem)
    {
        if (problem == null)
        {
            HttpStatusCode statusCode = HttpStatusCode.NotFound;
            Console.WriteLine("Statuskode: " + (int)statusCode + " " + statusCode.ToString());
        }
        

        var validation = _problemValidator.Validate(problem);

        if (!validation.IsValid)
        {
            HttpStatusCode statusCode = HttpStatusCode.NotFound;
            Console.WriteLine("Statuskode: " + (int)statusCode + " " + statusCode.ToString());
        }
        

        ProblemDB? returnProblem = _problemRepository.EditProblem(Problem.ConvertToProblemDB(problem));

        if (returnProblem == null)
        {
            HttpStatusCode statusCode = HttpStatusCode.NotFound; 
            Console.WriteLine("Statuskode: " + (int)statusCode + " " + statusCode.ToString());
        
        }

        if (problem.ProblemName != returnProblem.ProblemName)
        {
            HttpStatusCode statusCode = HttpStatusCode.NotFound; 
            Console.WriteLine("Statuskode: " + (int)statusCode + " " + statusCode.ToString());
        
        }

        var validationReturn = _problemValidator.Validate(ProblemDB.ConvertToProblem(returnProblem));
        if (!validationReturn.IsValid)
        {
            HttpStatusCode statusCode = HttpStatusCode.NotFound;
            Console.WriteLine("Statuskode: " + (int)statusCode + " " + statusCode.ToString());
        }
        

        return ProblemDB.ConvertToProblem(returnProblem);
    }

    public bool DeleteProblem(Problem problem)
    {
        if (problem == null)
        {
            HttpStatusCode statusCode = HttpStatusCode.NotFound;
            Console.WriteLine("Statuskode: " + (int)statusCode + " " + statusCode.ToString());
        }

        if (problem.ProblemId <= 0)
        {
            return false;
        }

        int change = _problemRepository.DeleteProblem(problem.ProblemId);

        if (change == 0)
        {
            return false;
        }
        return true;
    }
}





