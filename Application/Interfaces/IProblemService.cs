using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IProblemService
{
    public ProblemResponse GetAllProblems();
    public Problem AddProblem(AddProblemRequest addProblemRequest);
    public Problem EditProblem(Problem problem);
    public bool DeleteProblem(Problem problem);
    public Problem GetById(int ProblemId);
}