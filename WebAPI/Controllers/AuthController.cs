using Application.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;

namespace WebAPI.Controllers;


[ApiController]
[Route("[Controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _auth;
    public AuthController(IAuthenticationService auth)
    {
        _auth = auth;
    }
    
    [HttpPost]
    [Route("Login")]
    public ActionResult Login(LoginAndRegisterDTO dto)
    {
        try
        {
            return Ok(_auth.Login(dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("Register")]
    public ActionResult Register(LoginAndRegisterDTO dto)
    {
        try
        {
            return Ok(_auth.Register(dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}