using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("WebAPI/[controller]")]
public class ProblemController: ControllerBase
{
    private readonly IProblemService _problemService;

    public ProblemController(IProblemService problemService)
    {
        _problemService = problemService ?? throw new NullReferenceException("Faction Service can't be null");
        
    }

    [HttpGet]
    public List<Problem> GetAllProblems()
    {
        return _problemService.GetAllProblems();
}

    [HttpGet]
    [Route("{ProblemId}")]
    public Problem GetById( int ProblemId)
    {
        return _problemService.GetById(ProblemId);
    }
    
    [HttpPost]
    public Problem AddProblem([FromBody] AddProblemRequest dto)
    {
        return _problemService.AddProblem(dto);
    }

    [HttpPut]
    public Problem EditProblem( Problem problem)
    {
        return _problemService.EditProblem(problem);
    }

    [HttpDelete]
    public Boolean DeleteProblem(Problem problem)
    {
        return _problemService.DeleteProblem(problem);
    }
}