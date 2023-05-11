using Application.DTOs;
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
            Location = problem.Location,
            Status = problem.Status,
            Description = problem.Description,
            Image = problem.Image
        };
    }
    
}
