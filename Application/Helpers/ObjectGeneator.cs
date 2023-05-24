using System.Globalization;
using Application.DTOs;
using Application.DTOs.User;
using Domain;

namespace Application.Helpers;

public class ObjectGeneator
{
    public static Problem ProblemRequestToProblem(AddProblemRequest problem)
    {
        var culture = new CultureInfo("en-US"); // <-- This is so fucking stupid
        var _longitude = 0.0;
        var _latitude = 0.0;
        try
        {
            _longitude = Double.Parse(problem.Longitude, culture);
            _latitude = Double.Parse(problem.Latitude, culture);
        }
        catch (System.Exception)
        {
            
            throw new FluentValidation.ValidationException("Location is invalid");
        }

        return new Problem
        {
            ProblemId = 0,
            ProblemName = problem.ProblemName,
            Longitude = _longitude,
            Latitude = _latitude,
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
