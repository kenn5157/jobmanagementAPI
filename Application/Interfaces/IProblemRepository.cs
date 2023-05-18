using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IProblemRepository
{
    public List<ProblemDB> GetAllProblems();
    public ProblemDB GetById(int ProblemId);
    public ProblemDB AddProblem(ProblemDB addProblemRequest);
    public ProblemDB? EditProblem(ProblemDB problem);
    public int DeleteProblem(int ProblemId);
 
}