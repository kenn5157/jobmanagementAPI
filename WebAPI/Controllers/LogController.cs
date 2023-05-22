using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;



namespace WebAPI.Controllers;

public class LogController: ControllerBase
{
    private readonly ILogger<LogController> _logger;

    public LogController(ILogger<LogController> logger)
    {
        _logger = logger;
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