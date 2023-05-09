using Applicatoin.DTOs.User;

namespace Application.Interfaces;

public interface IAuthenticationService
{
    public string Register(LoginAndRegisterDTO dto);
    public string Login(LoginAndRegisterDTO dto);
    
}