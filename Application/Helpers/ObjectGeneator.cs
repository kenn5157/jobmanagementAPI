using Application.DTOs;
using Application.DTOs.User;
using Domain;

namespace Application.Helpers;

public class ObjectGeneator
{
    public static Problem ProblemRequestToProblem(AddProblemRequest problem)
    {
     
        return new Problem
        {
            ProblemId = 0,
            ProblemName = problem.ProblemName,
            Longitude = problem.Longitude,
            Latitude = problem.Latitude,
            Status = problem.Status,
            Description = problem.Description,
            Image = problem.Image
        };
    }

    public static Register RegisterDtoToRegister(LoginAndRegisterDTO dto)
    {
        return new Register
        {
            Email = dto.Email,
            Password = dto.Password
        };
    }
    
}
