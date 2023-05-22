using System.Net;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProblemController : ControllerBase
{
    private readonly IProblemService _problemService;
    private readonly ILogger<ProblemController> _logger;
    public ProblemController(IProblemService problemService, ILogger<ProblemController> logger){
        
        _problemService = problemService ?? throw new NullReferenceException("Faction Service can't be null");
        _logger = logger;
    }

    [HttpGet]
    public ActionResult GetAllProblems()
    {
        try
        {
      return Ok(_problemService.GetAllProblems());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something bad happened");
            return new StatusCodeResult(500);
        }
        
    }

    [HttpGet]
    [Route("{ProblemId}")]
    public ActionResult GetById(int ProblemId)
    {
        try
        {
            return Ok(_problemService.GetById(ProblemId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(500, "An error occurred");
        }
    }

    [HttpPost]
    public ActionResult AddProblem([FromBody] AddProblemRequest dto)
    {
        try
        {
            return Ok(_problemService.AddProblem(dto));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(500, "An error occurred");
        }
    }

    [HttpPut]
    public ActionResult EditProblem(Problem problem)
    {
        try
        {
            return Ok(_problemService.EditProblem(problem));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(500, "An error occurred");
        }
    }

    [HttpDelete]
    public ActionResult DeleteProblem(Problem problem)
    {
        try
        {
            var deleted = _problemService.DeleteProblem(problem);
            return Ok(deleted);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred");
            
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
      
    }
    
    
}