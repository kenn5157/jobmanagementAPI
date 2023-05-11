using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class DatabaseController : ControllerBase
{
    private readonly IDatabaseRepository _databaseRepository;
    
    public DatabaseController(IDatabaseRepository databaseRepository)
    {
        _databaseRepository = databaseRepository ?? throw new NullReferenceException("DatabaseRepository can not be null.");
    }
    
    [HttpGet]
    public string buildDB()
    {
        _databaseRepository.buildDB();
        return "Database has been built.";
    }
}