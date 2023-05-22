using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProblemController : ControllerBase
{
    private readonly IProblemService _problemService;
    private readonly ILogger<LogController> _logger;
    public ProblemController(IProblemService problemService){
        _problemService = problemService ?? throw new NullReferenceException("Faction Service can't be null");
    
    }

    [HttpGet]
    public IActionResult GetAllProblems()
    {
        return Ok(_problemService.GetAllProblems());
    }

    [HttpGet]
    [Route("{ProblemId}")]
    public Problem GetById(int ProblemId)
    {
        return _problemService.GetById(ProblemId);
    }

    [HttpPost]
    public Problem AddProblem([FromBody] AddProblemRequest dto)
    {
        return _problemService.AddProblem(dto);
    }

    [HttpPut]
    public Problem EditProblem(Problem problem)
    {
        return _problemService.EditProblem(problem);
    }

    [HttpDelete]
    public Boolean DeleteProblem(Problem problem)
    {
        return _problemService.DeleteProblem(problem);
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            var rng = new Random();
            if (rng.Next(0, 5) < 2)
            {
                throw new Exception("oops what happend");
            }
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,"Something bad happened");
            return new StatusCodeResult(500);
        }
      
    }
}