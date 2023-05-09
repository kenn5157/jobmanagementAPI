using Application.DTOs;
using Application.Interfaces;
using Application.Validators;
using Applicatoin.Helpers;
using Domain;
using FluentValidation;



namespace Application;

public class ProblemService : IProblemService
{
    private readonly IProblemRepository _problemRepository;
    private readonly IValidator<Problem> _problemValidator;

    public ProblemService(IProblemRepository problemRepository, ProblemValidator problemValidator)
    {
        _problemRepository = problemRepository ?? throw new NullReferenceException("ProblemRepository is null");
        _problemValidator = problemValidator ?? throw new NullReferenceException("ProblemValidator is null");
    }
    public List<Problem> GetAllProblems()
    {
        List<Problem> problemList = _problemRepository.GetAllProblems();
        if (problemList == null)
        {
            throw new NullReferenceException("Unable to fetch problems form database.");
        }

        return problemList;
    }

  
    public Problem GetById(int ProblemId)
    {
        return _problemRepository.GetById(ProblemId);
    }
    public Problem AddProblem(AddProblemRequest addProblemRequest)
    {
        if (addProblemRequest == null)
        {
            throw new NullReferenceException("AddProblemRequest is null.");
        }

        Problem problem = ObjectGeneator.ProblemRequestToProblem(addProblemRequest);
        
        var validation = _problemValidator.Validate(problem, options => options.IncludeRuleSets("Add"));

        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }

        Problem returnProblem = _problemRepository.AddProblem(problem);

        if (returnProblem == null)
        {
            throw new NullReferenceException("Item does not exist in database.");
        }

        var returnValidation = _problemValidator.Validate(returnProblem);
        if (!returnValidation.IsValid)
        {
            throw new ValidationException(returnValidation.ToString());
        }

        return returnProblem;
    }

    public Problem EditProblem(Problem problem)
    {
        if (problem == null)
        {
            throw new NullReferenceException("Problem is null.");
        }

        var validation = _problemValidator.Validate(problem);

        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }

        Problem? returnProblem = _problemRepository.EditProblem(problem);

        if (returnProblem == null)
        {
            throw new NullReferenceException("Problem does not exist in database.");
        }

        if (problem.ProblemName != returnProblem.ProblemName)
        {
            throw new ArgumentException("No change was made to the ProblemName.");
        }

        var validationReturn = _problemValidator.Validate(returnProblem);
        if (!validationReturn.IsValid)
        {
            throw new ValidationException(validationReturn.ToString());
        }

        return returnProblem;
    }

    public bool DeleteProblem(Problem problem)
    {
        if (problem == null)
        {
            throw new NullReferenceException("Problem is null.");
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




