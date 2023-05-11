using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IProblemRepository
{
    public List<Problem> GetAllProblems();
    public Problem GetById(int ProblemId);
    public Problem AddProblem(Problem addProblemRequest);
    public Problem? EditProblem(Problem problem);
    public int DeleteProblem(int ProblemId);
 
}