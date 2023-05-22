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

}